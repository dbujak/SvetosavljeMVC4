function WetlandBenefitsCtrl($scope, $rootScope, $routeParams, WetlandBenefitsService, SharedService){

	$rootScope.$on('showWetlandBenefits', function(e, county, countyBounds){
		var fips = county.STATE_FIPS + county.CNTY_FIPS;
		$scope.county = county;
		$scope.countyBounds = countyBounds;

		Highcharts.setOptions(SharedService.hightChartOptions());

		WetlandBenefitsService.async(fips).then(function(d){

			$scope.wetlandModel =  d;

			$scope.wetlandModel.mapImage = SharedService.config.Arcgis + 'Wetlands_Snapshot_Data/MapServer/export?' +
				'bbox=' + countyBounds + '&bboxSR=4326&layerdefs=1:NOT FIPS=' + fips + ';2:NOT FIPSSTCO =' + fips + '&size=330,330&format=png&transparent=true&dpi=96&f=image';
			$scope.wetlandModel.mapImageLarge = 'http://www.arcgis.com/home/webmap/viewer.html?webmap=f19a213e1a2248ec9f6d3863908bdce9&extent=' + countyBounds;
			$scope.wetlandModel.LCA_Link = 'http://www.coast.noaa.gov/ccapatlas/?s=' + county.STATE_FIPS + '&r=county&t=4&fr=1996&to=2010&c=' + fips;

			buildChart(d.LandUse_FloodPlain_Percent);

			if(d.Fish_Econ.GDP_County === 'unavailable*') {
				$scope.wetlandModel.HideLegend = true;
			}
			else{
				$scope.wetlandModel.HideLegend = true;
			}

			$scope.$parent.showLoading = false;
		});

	});


	function buildChart(wetlandData){
		var data;
		data = [];

		if(wetlandData.WetlandsPercentTotal !== '0') 
			data.push({ name: "Wetlands", y: parseFloat(wetlandData.WetlandsPercentTotal), color:'#6FA359', sliced: true, selected: true });
		if(wetlandData.DevelopedPercentTotal !== '0')
			data.push({ name: "Developed", y: parseFloat(wetlandData.DevelopedPercentTotal), color:'#9732CC', sliced: false, selected: false });
		if(wetlandData.AgriculturePercentTotal !== '0')
			data.push({ name: "Agriculture", y: parseFloat(wetlandData.AgriculturePercentTotal), color:'#EFAE50', sliced: false, selected: false });
		if(wetlandData.OtherPercentTotal !== '0')
			data.push({ name: "Other (grasslands, forests,<br>scrub vegetation, and<br>barren land)", y: parseFloat(wetlandData.OtherPercentTotal), color:'#dddddd', sliced: false, selected: false });

		var options = {
			chart: {
				plotBackgroundColor: null,
				backgroundColor:'rgba(0,0,0,0)',
				plotBorderWidth: null,
				plotShadow: false,
				renderTo: 'safer2',
				events: { click: function(event) { SharedService.openModal(options); } }
			},
			plotOptions: {
				pie: {
					enableMouseTracking: false,
					dataLabels: {
						distance: 13,
						formatter: function() {
							return this.y + '%';
						}
					}, 
					size: '70%',
            		shadow: false,
					showInLegend: true,
					slicedOffset: 5
				}
			},
			title: {
				text: null
			},
			legend: {
				enabled: false
			},
			exporting: {
				enabled: false
			},
			series: [{type: 'pie', data: data}]			
		};







		var chart = new Highcharts.Chart(options);
	};

	$scope.showOcean = function(){
		var win = window.open('#/process?action=ocean&state=' + $scope.county.STATE_FIPS + '&county=' + $scope.county.CNTY_FIPS + '&bounds=' + $scope.countyBounds, '_blank');
  		win.focus();
	};

	$scope.showFlood = function(){
		var win = window.open('#/process?action=flood&state=' + $scope.county.STATE_FIPS + '&county=' + $scope.county.CNTY_FIPS + '&bounds=' + $scope.countyBounds, '_blank');
  		win.focus();
	};	
}