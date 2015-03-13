function IndexCtrl($scope, $rootScope, $routeParams, $location, StateCountyService, BoundariesService, SharedService){

	$rootScope.$on('startOver', function(e){
		$scope.search.state = null;
		$scope.search.county = null;
		setTimeout(function(){mapObject.showmapsModel(0, true);}, 100);	// it messes up map if I don't use setTimeout
	});

	$rootScope.$on('map', function(e){
		setTimeout(function(){map.invalidateSize();}, 100);	// if I don't use timeout map will not be fixed with map.invalidateSize() - it gets messed up if print was called
	});

	$scope.$parent.showLoading = true;

	// search holds selected state and county
	var search = {};
	$scope.search = search;

	$scope.mapsModel = {
		// used for showing state/county names
		stateCountyInfoText: '',
		stateCountyInfoShow: 0,
		// used to show small maps of Alaska, Hawaii and USA continental
		showUSAContinental: 0,
		showAlaska: 0,
		showHawaii: 0
	};


	// if coming back to the page, set up state and county
	$scope.statefips = $routeParams.statefips;
	$scope.countyfips = $routeParams.countyfips;

	// object used to sync all the async calls
	var syncObject = {
		count: 0,
		check: function(){
			this.count--;
			if (this.count <= 0) this.synced();
		},
		onHtmlLoadedCheck: function(){
			if (this.count <= 0) this.synced();
		},
		synced: function(){
			if ($scope.mapsModel.htmlLoaded){
				if ($scope.statefips != undefined && $scope.countyfips != undefined){
					selectStateAndCounty($scope.statefips, $scope.countyfips)
					mapObject.renderCountiesLayer(true);
					mapObject.showmapsModel($scope.statefips, false);
				}
				else{
					mapObject.renderStatesLayer();
					mapObject.showmapsModel('0', false)
				}
				$scope.$parent.showLoading = false;
			}
		}
	}

	syncObject.count++;
	StateCountyService.async().then(function(d){
		$scope.statesAndCounties =  d;
		$scope.States = getStates($scope.statesAndCounties);
		syncObject.check();
	});

	var map;

	$scope.onHtmlLoaded = function() {
		$scope.mapsModel.htmlLoaded = true;
		map = L.map('leafletMap').setView([38, -96], 4);

		L.tileLayer(SharedService.config.Arcgis + "Snapshot_Base/MapServer/tile/{z}/{y}/{x}", {minZoom:0, maxZoom:10}).addTo(map);

		L.tileLayer(SharedService.config.Arcgis + "Snapshot_Cities/MapServer/tile/{z}/{y}/{x}", {opacity: 1, zIndex: 100}).addTo(map);
		syncObject.onHtmlLoadedCheck();

		// position div with state/county name over the mouse
		$("#leafletMap").mousemove(function(e) {		
			$('#stateCountyInfo').css({'top': e.pageY+5, 'left': e.pageX+10});
		});
	};


	$scope.zoomToState = function()	{
		$scope.search.county=null;
		mapObject.renderStatesLayer(true);
	};

	$scope.convertToBool = function(val){
		if (val == "1"){
			return true;
		}
		else{
			return false;
		}
	};


	$scope.showFlood = function(county){
		$location.url('process?action=flood&state=' + county.STATE_FIPS + '&county=' + county.CNTY_FIPS + '&bounds=' + mapObject.countyBoundsString);
		map.setView(mapObject.countyBounds.getCenter());		
	};

	$scope.showOcean = function(county){
		$location.url('process?action=ocean&state=' + county.STATE_FIPS + '&county=' + county.CNTY_FIPS + '&bounds=' + mapObject.countyBoundsString);
		map.setView(mapObject.countyBounds.getCenter());
	};

	$scope.showWetlands = function(county){
		$location.url('process?action=wetlands&state=' + county.STATE_FIPS + '&county=' + county.CNTY_FIPS + '&bounds=' + mapObject.countyBoundsString);
		map.setView(mapObject.countyBounds.getCenter());
	};

	$scope.zoomToCounty = function(){
		$scope.search.state = getState($scope.search.county.STATE_FIPS);
		mapObject.renderCountiesLayer(true);
	}

	$scope.showmapsModel = function(stateFips, boolCenterMap){
		mapObject.showmapsModel(stateFips, boolCenterMap);
	}

	function getStateAndCounty(stateFips, countyFips, statesAndCounties){
		for (var i = 0; i < statesAndCounties.length; i++){
			if (statesAndCounties[i].STATE_FIPS == stateFips && statesAndCounties[i].CNTY_FIPS == countyFips){
				return statesAndCounties[i];
			}
		}
	}	

	function getState(stateFips){
		for (var i = 0; i < $scope.States.length; i++){
			if ($scope.States[i].STATE_FIPS == stateFips){
				return $scope.States[i];
			}
		}
	}

	function selectStateAndCounty(stateFips, countyFips){
		$scope.search.county = getStateAndCounty(stateFips, countyFips, $scope.statesAndCounties);
		$scope.search.state = getState(stateFips);
	}

	function getStates(statesAndCounties){
		var lastState = "";
		var states = new Array();
		for (var i = 0; i < statesAndCounties.length; i++){
			if (lastState != statesAndCounties[i].STATE_NAME){
				states.push(statesAndCounties[i]);
				lastState = statesAndCounties[i].STATE_NAME;
			}
		}
		return states;
	}



	var mapObject = {
		stateBorders: null, // stores state shapes coming from web service
		countyBorders: null, // stores county shapes coming from web service
		geojsonStates: null,	// stores geo jason for states
		geojsonCounties: null, // stores geo jason for counties
		_boolShowingStateBorders: true,	// if true showing states, if false showing counties - don't change it directly
		countyBoundsString: null,	// will send these bounds to get image of the county for a snapshot
		countyBounds: null,	// need these to center map on the county, once a snapshot was selected, so when a user comes back the county will be centered
		stateOnEachFeature: 	function(feature, layer) {	// defines what functions are called for State layer events
			layer.on({
				mouseover: mapObject.stateHighlightFeature,
				mouseout: mapObject.stateResetHighlight,
				click: mapObject.zoomToState
			});
			map.on({
				zoomend: mapObject.zoomEnded
			});
		},
		stateHighlightFeature: function (e){	// highlight state
			var layer = e.target;

			layer.setStyle({fillColor: '#969EA7'});

			if (!L.Browser.ie && !L.Browser.opera) {
				layer.bringToFront();
			}
			

			$scope.mapsModel.stateCountyInfoText = e.target.feature.properties.STATE_FULL_NAME;
			$scope.mapsModel.stateCountyInfoShow = 1;
			$scope.$apply();
		},
		stateResetHighlight: function(e){	// reset state color
			var layer = e.target.feature.properties;
			mapObject.geojsonStates.resetStyle(e.target);	

			$scope.mapsModel.stateCountyInfoText = "";
			$scope.mapsModel.stateCountyInfoShow = 0;
			$scope.$apply();
		},
		zoomToState: function(e){	// zoom to state bounds

			var bounds = mapObject.getStateBounds(e.target.getBounds(), e.target.feature.properties.STATE_FULL_NAME);
			mapObject.zoomToStateLayer(bounds);

			// select state
			$scope.search.state = mapObject.getState(e.target.feature.properties.STATE_FULL_NAME);
			$scope.$apply();
		},
		zoomEnded: function(e){	// after zoom ended for state layer
			if (map.getZoom() <= 5 && mapObject._boolShowingStateBorders == false){	// show states
				mapObject.renderStatesLayer();
			}
			if (map.getZoom() >= 6 && mapObject._boolShowingStateBorders == true){	// show counties
				mapObject.renderCountiesLayer();
			}
		},		
		_clearGeoJsonLayers: function(){
			if (this.geojsonCounties != null){
				map.removeLayer(this.geojsonCounties);
			}
			if (this.geojsonStates != null){
				map.removeLayer(this.geojsonStates);
			}
		},
		renderStatesLayer: function(boolZoom){	// renders states on the map
			// remove layers if present
			this._clearGeoJsonLayers();
			
			
			this.geojsonStates = L.geoJson(this.stateBorders, {
					style: {
						fillColor: '#C6D6E7', 
						weight: 1, 
						opacity: 1, 
						color: 'black', 
						fillOpacity: 0.7
					}, 
					onEachFeature: this.stateOnEachFeature
				}).addTo(map);

			this._boolShowingStateBorders = true;

			if (boolZoom){
				this.geojsonStates.eachLayer(function(layer){
					if (layer.feature.properties.STATE_FULL_NAME == $scope.search.state.STATE_NAME){
						var bounds = mapObject.getStateBounds(layer.getBounds(), layer.feature.properties.STATE_FULL_NAME);
						
						mapObject.zoomToStateLayer(bounds);

						mapObject.showmapsModel($scope.search.state.STATE_FIPS, false);
						return;
					}
				});
			}
		},
		zoomToStateLayer: function(bounds){	//
			if (map.getBoundsZoom(bounds) <=5){
				map.setView(bounds.getCenter(), 6);							
			}
			else{
				map.fitBounds(bounds);
			}

			mapObject.renderCountiesLayer();
		},
		zoomToCountyLayer: function(bounds){	//
			var boundsZoom = map.getBoundsZoom(bounds);
			console.log(boundsZoom);
			if (boundsZoom <= 7){
				map.setView(bounds.getCenter(), 6);							
			}
			else if (boundsZoom <= 9){
				map.setView(bounds.getCenter(), 7);							
			}
			else if (boundsZoom <= 11){
				map.setView(bounds.getCenter(), 8);							
			}
			else{
				map.setView(bounds.getCenter(), 9);							
			}
		},		countyHighlightFeature: function (e){	// highlight county
			var layer = e.target;

			// check if selected county, don't change color then
			if (!mapObject.isSelectedCounty(e.target.feature.properties)){
				layer.setStyle({fillColor: '#969EA7', fillOpacity: 0.7});
			}
			
			if (!L.Browser.ie && !L.Browser.opera) {
				layer.bringToFront();
			}
			
			$scope.mapsModel.stateCountyInfoText =  layer.feature.properties.NAME + ', ' + layer.feature.properties.STATE_NAME;
			$scope.mapsModel.stateCountyInfoShow = 1;
			$scope.$apply();
		},	
		countyResetHighlight: function(e){	// reset county color
			if (!mapObject.isSelectedCounty(e.target.feature.properties)){
				mapObject.geojsonCounties.resetStyle(e.target);		
			}
			$scope.mapsModel.stateCountyInfoText = '';
			$scope.mapsModel.stateCountyInfoShow = 0;
			$scope.$apply();
		},	
		clickedOnCounty: function(e){	// select clicked county
			var props = e.target.feature.properties;
			selectStateAndCounty(props.STATE_FIPS, props.CNTY_FIPS);
			$scope.$apply();
			mapObject.renderCountiesLayer();
			mapObject.countyBounds = e.target.getBounds();
		},			
		countyOnEachFeature: 	function(feature, layer) {	// defines what functions are called for County layer events
			layer.on({
				mouseover: mapObject.countyHighlightFeature,
				mouseout: mapObject.countyResetHighlight,
				click: mapObject.clickedOnCounty
			});
			map.on({
				zoomend: mapObject.zoomEnded
			});
		},		
		renderCountiesLayer: function(boolZoom){	// renders counties on the map
			// remove layers if present
			this._clearGeoJsonLayers();

			this.geojsonCounties = L.geoJson(this.countyBorders, {
					style: {
						fillColor: '#C6D6E7', 
						weight: 0.5, 
						opacity: 1, 
						color: 'black', 
						fillOpacity: 0.0,
						zIndex: 1
					}, 
					onEachFeature: this.countyOnEachFeature
				}).addTo(map);

			this.geojsonCounties.eachLayer(function(layer){
				if (mapObject.isSelectedCounty(layer.feature.properties)){
					layer.setStyle({fillColor: 'red', fillOpacity:0.7});
					var bounds = layer.getBounds();
					var margin = 0.4;
					mapObject.countyBoundsString = (bounds._southWest.lng + margin) + ',' + (bounds._southWest.lat - margin) + ',' + (bounds._northEast.lng - margin) + ',' + (bounds._northEast.lat + margin);
					mapObject.countyBounds = bounds;
					if (boolZoom){
						mapObject.zoomToCountyLayer(bounds)
					}
					return;
				}
			});

			this._boolShowingStateBorders = false;	
		},
		getState: function(fullName){	// return state object based on state name
			for (var i = 0; i < $scope.States.length; i++){
				if ($scope.States[i].STATE_NAME == fullName){
					return $scope.States[i];
				}
			}
		},
		getStateBounds: function(bounds, stateName){	// some state bounds aren't correct, this function corrects them
			if (stateName == "Alaska"){
				bounds._northEast.lat = 61;
				bounds._northEast.lng = -156;
				bounds._southWest.lat = 58.6;
				bounds._southWest.lng = -157;				
			}
			return bounds;
		},
		isSelectedCounty: function(countyInfo){ 	// returns true if selected county or false if not
			if ($scope.search.county != null && countyInfo.STATE_FIPS == $scope.search.county.STATE_FIPS && countyInfo.CNTY_FIPS == $scope.search.county.CNTY_FIPS){
				return true;
			}
			else {
				return false;
			}
		},
		showmapsModel: function(stateFips, boolCenterMap){
			var latLng, zoom; 

			switch(stateFips){
				case '02': 	//  Alaska (show USA and Hawaii)
					$scope.mapsModel.showUSAContinental = 1;
					$scope.mapsModel.showAlaska = 0;
					$scope.mapsModel.showHawaii = 1;
					
					latLng = [60, -160];
					zoom = 6;
					break;
				case '15': // 15 - Hawaii (show USA and Alaska)
					$scope.mapsModel.showUSAContinental = 1;
					$scope.mapsModel.showAlaska = 1;
					$scope.mapsModel.showHawaii = 0;
					
					latLng = [20, -158];
					zoom = 6;
					break;
				default: // 0 - US continental (show Alaska and Hawaii)
					$scope.mapsModel.showUSAContinental = 0;
					$scope.mapsModel.showAlaska = 1;
					$scope.mapsModel.showHawaii = 1;

					latLng = [38, -96];
					zoom = 4;
					break;
			}

			if (boolCenterMap) {
				map.setView(latLng, zoom);
			}

			map.invalidateSize();
		}
	};

	syncObject.count++;
	BoundariesService.States.async().then(function(d){

		// Add full State name to json object
		var statesNameAbrv = [{"Name":"Alabama", "Name_Short":"AL"},{"Name":"Alaska", "Name_Short":"AK"},{"Name":"Arizona", "Name_Short":"AZ"},{"Name":"Arkansas", "Name_Short":"AR"},{"Name":"California", "Name_Short":"CA"},{"Name":"Colorado", "Name_Short":"CO"},{"Name":"Connecticut", "Name_Short":"CT"},{"Name":"Delaware", "Name_Short":"DE"},{"Name":"Florida", "Name_Short":"FL"},{"Name":"Georgia", "Name_Short":"GA"},{"Name":"Hawaii", "Name_Short":"HI"},{"Name":"Idaho", "Name_Short":"ID"},{"Name":"Illinois", "Name_Short":"IL"},{"Name":"Indiana", "Name_Short":"IN"},{"Name":"Iowa", "Name_Short":"IA"},{"Name":"Kansas", "Name_Short":"KS"},{"Name":"Kentucky", "Name_Short":"KY"},{"Name":"Louisiana", "Name_Short":"LA"},{"Name":"Maine", "Name_Short":"ME"},{"Name":"Maryland", "Name_Short":"MD"},{"Name":"Massachusetts", "Name_Short":"MA"},{"Name":"Michigan", "Name_Short":"MI"},{"Name":"Minnesota", "Name_Short":"MN"},{"Name":"Mississippi", "Name_Short":"MS"},{"Name":"Missouri", "Name_Short":"MO"},{"Name":"Montana", "Name_Short":"MT"},{"Name":"Nebraska", "Name_Short":"NE"},{"Name":"Nevada", "Name_Short":"NV"},{"Name":"New Hampshire", "Name_Short":"NH"},{"Name":"New Jersey", "Name_Short":"NJ"},{"Name":"New Mexico", "Name_Short":"NM"},{"Name":"New York", "Name_Short":"NY"},{"Name":"North Carolina", "Name_Short":"NC"},{"Name":"North Dakota", "Name_Short":"ND"},{"Name":"Ohio", "Name_Short":"OH"},{"Name":"Oklahoma", "Name_Short":"OK"},{"Name":"Oregon", "Name_Short":"OR"},{"Name":"Pennsylvania", "Name_Short":"PA"},{"Name":"Rhode Island", "Name_Short":"RI"},{"Name":"South Carolina", "Name_Short":"SC"},{"Name":"South Dakota", "Name_Short":"SD"},{"Name":"Tennessee", "Name_Short":"TN"},{"Name":"Texas", "Name_Short":"TX"},{"Name":"Utah", "Name_Short":"UT"},{"Name":"Vermont", "Name_Short":"VT"},{"Name":"Virginia", "Name_Short":"VA"},{"Name":"Washington", "Name_Short":"WA"},{"Name":"West Virginia", "Name_Short":"WV"},{"Name":"Wisconsin", "Name_Short":"WI"},{"Name":"Wyoming", "Name_Short":"WY"},{"Name":"American Samoa", "Name_Short":"AS"},{"Name":"District of Columbia", "Name_Short":"DC"},{"Name":"Federated States of Micronesia", "Name_Short":"FM"},{"Name":"Guam", "Name_Short":"GU"},{"Name":"Marshall Islands", "Name_Short":"MH"},{"Name":"Northern Mariana Islands", "Name_Short":"MP"},{"Name":"Palau", "Name_Short":"PW"},{"Name":"Puerto Rico", "Name_Short":"PR"},{"Name":"Virgin Islands", "Name_Short":"VI"},{"Name":"Armed Forces Africa", "Name_Short":"AE"},{"Name":"Armed Forces Americas", "Name_Short":"AA"},{"Name":"Armed Forces Canada", "Name_Short":"AE"},{"Name":"Armed Forces Europe", "Name_Short":"AE"},{"Name":"Armed Forces Middle East", "Name_Short":"AE"},{"Name":"Armed Forces Pacific", "Name_Short":"AP"}];
		for (var i = 0; i < d.features.length; i++){
			for (var j = 0; j < statesNameAbrv.length; j++){
				if (d.features[i].properties.STATE == statesNameAbrv[j].Name_Short){
					d.features[i].properties.STATE_FULL_NAME = statesNameAbrv[j].Name;
					break;
				}
			}
		}

		mapObject.stateBorders = d;
		syncObject.check();
	})

	syncObject.count++;
	BoundariesService.Counties.async().then(function(d){
		mapObject.countyBorders = d;
		syncObject.check();
	})




}

