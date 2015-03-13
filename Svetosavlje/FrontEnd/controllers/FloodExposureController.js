function FloodExposureCtrl($scope, $rootScope, $routeParams, $location, FloodExposureService, SharedService){

	$rootScope.$on('showFlood', function(e, county, countyBounds){
		var fips = county.STATE_FIPS + county.CNTY_FIPS;

		FloodExposureService.async(fips).then(function(d){

			$scope.floodModel =  d;

			$scope.floodModel.mapImage = SharedService.config.Arcgis + 'Flood_BacksideMap/MapServer/export?' +
				'bbox=' + countyBounds + '&bboxSR=4326&layers=&layerdefs=6:STCOFIPS=' + fips + ';7:FIPS=' + fips + '&size=510,350&imageSR=&format=png&transparent=true&dpi=96&f=image';

			Highcharts.setOptions(SharedService.hightChartOptions());

			populationChart($scope.floodModel);
			populationOver65Chart($scope.floodModel);
			populationPovertyChart($scope.floodModel);
			
			infrastructureChart($scope.floodModel);

			environmentChart($scope.floodModel);

			$scope.$parent.showLoading = false;
		})

	});

	// $scope.fips = $routeParams.fips;





	function populationChart(floodExposureData){
		var  populationData;
		populationData = [];
		
		populationData.push(["Outside FEMA Floodplain", parseFloat(floodExposureData['Out_FP_Pop'])]);
		populationData.push(["Inside FEMA Floodplain", parseFloat(floodExposureData['In_FP_Pop'])]);

		renderPieChart(populationData, 'demo1');

	};

	function populationOver65Chart(floodExposureData){
		var populationOver65Data;
		populationOver65Data = [];
		
		populationOver65Data.push(["Outside FEMA Floodplain", parseFloat(floodExposureData['Out_FP_Over65'])]);
		populationOver65Data.push(["Inside FEMA Floodplain", parseFloat(floodExposureData['In_FP_Over65'])]);

		renderPieChart(populationOver65Data, 'demo2');

	};

	function populationPovertyChart(floodExposureData){
		var populationPovertyData;
		populationPovertyData = [];
		
		populationPovertyData.push(["Outside FEMA Floodplain", parseFloat(floodExposureData['Out_FP_Poverty'])]);
		populationPovertyData.push(["Inside FEMA Floodplain", parseFloat(floodExposureData['In_FP_Poverty'])]);

		renderPieChart(populationPovertyData, 'demo3');

	};


	function renderPieChart(chartData, chartElement, color1, color2){
		var chartOptions, pieChart;

		if (color1 == null )
		{
			color1 = '#003366';
		}
		if (color2 == null)
		{
			color2 = '#6699CC';
		}
		chartOptions = pieChartOptions();
		chartOptions.chart.renderTo = chartElement;
		chartOptions.series = [{type: 'pie', data: chartData}];
		chartOptions.colors = [color1,	color2];
		chartOptions.chart.events = { click: function(event) { SharedService.openModal(chartOptions); } }
		pieChart = new Highcharts.Chart(chartOptions);

	};


	function infrastructureChart(floodExposureData)
	{
		var infraData;
		infraData = [];

		infraData.push([parseFloat(floodExposureData['SCHOOL_OUT']),parseFloat(floodExposureData['POLICE_OUT']),parseFloat(floodExposureData['FIRE_OUT']),parseFloat(floodExposureData['EOC_OUT']),parseFloat(floodExposureData['MEDICAL_OUT']),parseFloat(floodExposureData['COMMUNICATION_OUT'])]);
		infraData.push([parseFloat(floodExposureData['SCHOOL_IN']),parseFloat(floodExposureData['POLICE_IN']),parseFloat(floodExposureData['FIRE_IN']),parseFloat(floodExposureData['EOC_IN']),parseFloat(floodExposureData['MEDICAL_IN']),parseFloat(floodExposureData['COMMUNICATION_IN'])]);

		var options = {
			chart: {
				renderTo: 'infra',
				defaultSeriesType: 'column'
			},
			plotOptions: {
				column: {
					enableMouseTracking: false,
					borderWidth: 0,
					shadow: false,
					stacking: 'normal'
				}
			},
			colors: [
			'#6699CC',
			'#003366'
			],
			xAxis: {
				categories: ['Schools','Police Stations','Fire Stations','Emergency Centers','Medical Facilites','Communication Towers'],
				labels: {
					staggerLines: 2,
					style: {
						fontSize: '10px',
						lineHeight: '10px',
						width: '90px'
					}
				},
				lineWidth: 0
			},
			yAxis: {
				alternateGridColor: '#EAF2F5',
				gridLineWidth: 0,
				title: {
					text: null,
					margin: 0
				},
				tickInterval: SharedService.findRange([infraData[0], infraData[1]])
			},
			series: [{
				name: 'Inside FEMA Floodplain',
				data: infraData[1]
			}, {
				name: 'Outside FEMA Floodplain',
				data: infraData[0]
			}],
			title: {
				text: null
			},
			legend: {
				enabled: false
			},
			exporting: {
				enabled: false
			}
		};	

		infraOptions = options;
		infraOptions.chart.events = { click: function(event) { SharedService.openModal(infraOptions); } }
		infraChart = new Highcharts.Chart(infraOptions);

	};

	function environmentChart(floodExposureData){

		hasAcres = parseFloat(floodExposureData['Acres_Converted']);
		if (hasAcres === -1) {
			$('#env1').html('<p class="nochange">No Data Available</p>').css('background-image', 'url(img/nochange.png)').css('cursor', 'default');
			$('#env2').html('<p class="nochange">No Data Available</p>').css('background-image', 'url(img/nochange.png)').css('cursor', 'default');
			$('.envChart .legendBlock').parent().css('visibility', 'hidden');
		} else if (hasAcres === 0) {
			$('#env1').html('<p class="nochange">No Change Detected</p>').css('background-image', 'url(img/nochange.png)').css('cursor', 'default');
			$('#env2').html('<p class="nochange">No Change Detected</p>').css('background-image', 'url(img/nochange.png)').css('cursor', 'default');
			$('.envChart .legendBlock').parent().css('visibility', 'hidden');
		} else {
			$('#env1').empty().css('background-image', 'none').css('cursor', 'pointer');
			$('#env2').empty().css('background-image', 'none').css('cursor', 'pointer');
			$('.envChart .legendBlock').parent().css('visibility', 'visible');
			
			var env1Data, env2Data;
			env1Data = [];
			env2Data = [];

			env1Data.push(["Outside FEMA Floodplain", parseFloat(floodExposureData['Out_FP_Acres_Converted'])]);
			env1Data.push(["Inside FEMA Floodplain", parseFloat(floodExposureData['In_FP_Acres_Converted'])]);

			env2Data.push(["Agricultural Area", parseFloat(floodExposureData['Acres_Ag2Dev'])]);
			env2Data.push(["Natural Area", parseFloat(floodExposureData['Acres_Nat2Dev'])]);

			renderPieChart(env1Data, 'env1');

			renderPieChart(env2Data, 'env2', '#9EC54E',	'#4C662E');

		}
	};




	function pieChartOptions() {
		var options = {
			chart: {
				plotBackgroundColor: null,
				plotBorderWidth: null,
				plotShadow: false
			},
			plotOptions: {
				pie: {
					enableMouseTracking: false,
					dataLabels: {
						color: '#fff',
						distance: -27,
						formatter: function() {
							//I couldn't figure out a way to use a different number for display, so I am just adding commas on the fly
							var fullNum = '' + this.y;
							fullNum = fullNum.replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
							return fullNum + '<br>' + Math.round(this.percentage) + '%';
						}
					},
					shadow: false,
					showInLegend: true,
					size: '100%'
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
			}
		};
		return options;
	};


}