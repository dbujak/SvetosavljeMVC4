function OceanJobsCtrl($scope, $rootScope, $routeParams, OceanJobsService, SharedService){



	$rootScope.$on('showOceanJobs', function(e, county, countyBounds){
		var fips = county.STATE_FIPS + county.CNTY_FIPS;

		OceanJobsService.async(fips).then(function(d){

			$scope.oceanJobsModel =  d;

			$scope.oceanJobsModel.mapImage = SharedService.config.Arcgis + 'OceanJobs_BacksideMap/MapServer/export?bbox=' + countyBounds + '&bboxSR=4326&size=490,310&format=png&transparent=true&dpi=96&f=image';

			Highcharts.setOptions(SharedService.hightChartOptions());

			sectorCountyChart($scope.oceanJobsModel);
			sectorStateChart($scope.oceanJobsModel);
			sectorNationChart($scope.oceanJobsModel);

			var years;
			var trendsData = [];
			years = getYears($scope.oceanJobsModel.MinAnalysisYear, $scope.oceanJobsModel.AnalysisYear)
			trendsData = cleanUpTrandsData($scope.oceanJobsModel.JobsDecadeEmployment);

			jobsTrendsPercentChart(trendsData, years);
			jobsTrendsAbsoluteChart(trendsData, years);

			wagesChart($scope.oceanJobsModel);

			$scope.$parent.showLoading = false;
		});

	});


	function sectorCountyChart(oceanJobsData){
		var data;
		data = [];

		data.push(['Living Resources', parseFloat(oceanJobsData.JobsPercentages.Living_Resources.PercentEmployment)]);
		data.push(['Marine Construction', parseFloat(oceanJobsData.JobsPercentages.Marine_Construction.PercentEmployment)]);
		data.push(['Marine Transportation', parseFloat(oceanJobsData.JobsPercentages.Marine_Transportation.PercentEmployment)]);
		data.push(['Offshore Mineral Extraction', parseFloat(oceanJobsData.JobsPercentages.Offshore_Mineral_Extraction.PercentEmployment)]);
		data.push(['Ship and Boat Building', parseFloat(oceanJobsData.JobsPercentages.Ship_and_Boat_Building.PercentEmployment)]);
		data.push(['Tourism and Recreation', parseFloat(oceanJobsData.JobsPercentages.Tourism_and_Recreation.PercentEmployment)]);
		if (parseFloat(oceanJobsData.JobsPercentages.Suppressed.PercentEmployment) > 0){
			data.push(['Suppressed', parseFloat(oceanJobsData.JobsPercentages.Suppressed.PercentEmployment)]);			
		}

		renderPieChart(data,'sector1');
	};

	function sectorStateChart(oceanJobsData){
		var data;
		data = [];

		data.push(['Living Resources', parseFloat(oceanJobsData.JobsPercentagesForState.Living_Resources.PercentEmployment)]);
		data.push(['Marine Construction', parseFloat(oceanJobsData.JobsPercentagesForState.Marine_Construction.PercentEmployment)]);
		data.push(['Marine Transportation', parseFloat(oceanJobsData.JobsPercentagesForState.Marine_Transportation.PercentEmployment)]);
		data.push(['Offshore Mineral Extraction', parseFloat(oceanJobsData.JobsPercentagesForState.Offshore_Mineral_Extraction.PercentEmployment)]);
		data.push(['Ship and Boat Building', parseFloat(oceanJobsData.JobsPercentagesForState.Ship_and_Boat_Building.PercentEmployment)]);
		data.push(['Tourism and Recreation', parseFloat(oceanJobsData.JobsPercentagesForState.Tourism_and_Recreation.PercentEmployment)]);
		if (parseFloat(oceanJobsData.JobsPercentagesForState.Suppressed.PercentEmployment)){
			data.push(['Suppressed', parseFloat(oceanJobsData.JobsPercentagesForState.Suppressed.PercentEmployment)]);			
		}

		renderPieChart(data,'sector2');
	};

	function sectorNationChart(oceanJobsData){
		var data;
		data = [];

		data.push(['Living Resources', parseFloat(oceanJobsData.JobsPercentagesForNation.Living_Resources.PercentEmployment)]);
		data.push(['Marine Construction', parseFloat(oceanJobsData.JobsPercentagesForNation.Marine_Construction.PercentEmployment)]);
		data.push(['Marine Transportation', parseFloat(oceanJobsData.JobsPercentagesForNation.Marine_Transportation.PercentEmployment)]);
		data.push(['Offshore Mineral Extraction', parseFloat(oceanJobsData.JobsPercentagesForNation.Offshore_Mineral_Extraction.PercentEmployment)]);
		data.push(['Ship and Boat Building', parseFloat(oceanJobsData.JobsPercentagesForNation.Ship_and_Boat_Building.PercentEmployment)]);
		data.push(['Tourism and Recreation', parseFloat(oceanJobsData.JobsPercentagesForNation.Tourism_and_Recreation.PercentEmployment)]);

		renderPieChart(data,'sector3');
	};

	function jobsTrendsPercentChart(employmentData, years){
		var data;
		data = [];

		if (!isSurpressed(employmentData)){
			data.push({name: 'Living Resources', data: SharedService.findChange('percent', employmentData.Living_Resources, years)});
			data.push({name: 'Marine Construction', data: SharedService.findChange('percent', employmentData.Marine_Construction, years)});
			data.push({name: 'Marine Transportation', data: SharedService.findChange('percent', employmentData.Marine_Transportation, years)});
			data.push({name: 'Offshore Mineral Extraction', data: SharedService.findChange('percent', employmentData.Offshore_Mineral_Extraction, years)});
			data.push({name: 'Ship and Boat Building', data: SharedService.findChange('percent', employmentData.Ship_and_Boat_Building, years)});
			data.push({name: 'Tourism and Recreation', data: SharedService.findChange('percent', employmentData.Tourism_and_Recreation, years)});
		}
		else{
			data.push({name: "Suppressed", data: [0,0,0,0,0,0,0] })
		}

		renderTrendsChart(data,'trends1', years);
	};

	function jobsTrendsAbsoluteChart(employmentData, years){
		var data;
		data = [];
		if (!isSurpressed(employmentData)){
			data.push({name: 'Living Resources', data: SharedService.findChange('absolute', employmentData.Living_Resources, years)});
			data.push({name: 'Marine Construction', data: SharedService.findChange('absolute', employmentData.Marine_Construction, years)});
			data.push({name: 'Marine Transportation', data: SharedService.findChange('absolute', employmentData.Marine_Transportation, years)});
			data.push({name: 'Offshore Mineral Extraction', data: SharedService.findChange('absolute', employmentData.Offshore_Mineral_Extraction, years)});
			data.push({name: 'Ship and Boat Building', data: SharedService.findChange('absolute', employmentData.Ship_and_Boat_Building, years)});
			data.push({name: 'Tourism and Recreation', data: SharedService.findChange('absolute', employmentData.Tourism_and_Recreation, years)});
		}
		else{
			data.push({name: "Suppressed", data: [0,0,0,0,0,0,0] })
		}

		renderTrendsChart(data,'trends2', years);
	};

	function isSurpressed(employmentData){

		if (!isSurpressedArray(employmentData.Living_Resources)) return false;
		if (!isSurpressedArray(employmentData.Marine_Construction)) return false;
		if (!isSurpressedArray(employmentData.Marine_Transportation)) return false;
		if (!isSurpressedArray(employmentData.Offshore_Mineral_Extraction)) return false;
		if (!isSurpressedArray(employmentData.Ship_and_Boat_Building)) return false;
		if (!isSurpressedArray(employmentData.Tourism_and_Recreation)) return false;

		return true;
	}

	function getEmptyArray(years){
		var data = [];
		for (var i = 0; i < years.length; i++){
			data.push(0);
		}

		return data;
	}

	function isSurpressedArray(data){

		for (var i = 0; i < data.length; i++){
			if (data[i] != null && data[i] != '0'){
				return false;
			}
		}

		return true;
	};

	function wagesChart(oceanJobsData){
		var data, countyData, nationData;
		data = [];
		countyData = [];
		nationData = [];

		countyData.push(parseFloat(oceanJobsData.AnnualWages.Living_Resources.Wages) || 0);
		countyData.push(parseFloat(oceanJobsData.AnnualWages.Marine_Construction.Wages) || 0);
		countyData.push(parseFloat(oceanJobsData.AnnualWages.Marine_Transportation.Wages) || 0); 
		countyData.push(parseFloat(oceanJobsData.AnnualWages.Offshore_Mineral_Extraction.Wages) || 0);
		countyData.push(parseFloat(oceanJobsData.AnnualWages.Ship_and_Boat_Building.Wages) || 0);
		countyData.push(parseFloat(oceanJobsData.AnnualWages.Tourism_and_Recreation.Wages) || 0);

		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Living_Resources.Wages));
		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Marine_Construction.Wages));
		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Marine_Transportation.Wages)); 
		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Offshore_Mineral_Extraction.Wages));
		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Ship_and_Boat_Building.Wages));
		nationData.push(parseFloat(oceanJobsData.AnnualWagesNation.Tourism_and_Recreation.Wages));

		data.push(countyData);
		data.push(nationData);
		data.push(['Living Resources', 'Marine Construction', 'Marine Transportation', 'Offshore Mineral Extraction', 'Ship and Boat Building', 'Tourism and Recreation']);

		var options = {
			chart: {
				renderTo: 'wages1',
				defaultSeriesType: 'column',
				marginBottom: 90,
				spacingBottom: 15
			 },
			 plotOptions: {
				column: {
					enableMouseTracking: false,
					borderWidth: 0,
					groupPadding: 0.1,
					pointPadding: 0,
					shadow: false
				}
			 },
			 colors: [
				'#003366',
				'#6699CC' 
			 ],
			 xAxis: {
				categories: data[2],
				labels: {
					staggerLines: 3,
					/*formatter:function() {
						oddFlag = !oddFlag;
						returnString = '';
						if(oddFlag) {
							returnString = this.value;
						} else {
							returnString = this.value;
						}
						//return (oddFlag ? '<br>' : '') + this.value;
						return returnString;
					},*/
					style: {
						fontSize: '10px',
						width: '10px'
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
				}
			 },
			 series: [{
				name: 'County',
				data: data[0]
			 }, {
				name: 'Nation',
				data: data[1]
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

		options.chart.events = { click: function(event) { SharedService.openModal(options); } }
		var wagesChart = new Highcharts.Chart(options);
	};

	function renderTrendsChart(chartData, chartElement, years){
		var trendsOptions = jobsTrendsChartSetup(chartData);
		trendsOptions.chart.renderTo = chartElement;
		trendsOptions.xAxis.categories = years;
		trendsOptions.series = chartData;
		trendsOptions.chart.events = { click: function(event) { SharedService.openModal(trendsOptions); } }
		var trends1Chart = new Highcharts.Chart(trendsOptions);
	};

	function renderPieChart(chartData, chartElement){
		var options, pieChart;

		options = sectorChartSetup();
		options.chart.renderTo = chartElement;
		options.series = [{type: 'pie', data: chartData}];
		options.chart.events = { click: function(event) { SharedService.openModal(options); } }
		pieChart = new Highcharts.Chart(options);
	};

	function cleanUpTransArray(data){
		var retArray = [];

		for (var i = 0; i < data.length; i++){
			if (data[i] === '-9999'){
				retArray[i] = null
			}
			else{
				retArray[i] = data[i];	
			}
		}

		return retArray;
	};
	
	function cleanUpTrandsData(data){
		var retData = {};
		retData.Living_Resources = cleanUpTransArray(data.Living_Resources);
		retData.Marine_Construction = cleanUpTransArray(data.Marine_Construction);
		retData.Marine_Transportation = cleanUpTransArray(data.Marine_Transportation);
		retData.Offshore_Mineral_Extraction = cleanUpTransArray(data.Offshore_Mineral_Extraction);
		retData.Ship_and_Boat_Building = cleanUpTransArray(data.Ship_and_Boat_Building);
		retData.Tourism_and_Recreation = cleanUpTransArray(data.Tourism_and_Recreation);

		return retData;
	};

	function sectorChartSetup() {
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
						distance: 13,
						formatter: function() {
							if( this.y > 0 ) {
								return this.y + '%';
							} else {
								return null;
							}
						}
					}, 
					size: '70%',
					borderWidth: 0,
					shadow: false,
					showInLegend: true
				}
			},
			colors: ['#67B8E0', '#FF9E5F', '#B94B3E', '#FDE260', '#CDEA64', '#6969B4', '#cccccc'],
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

	function jobsTrendsChartSetup (jobsData) {
		var tick, color;
		if (jobsData[0].name == "Suppressed"){
			tick = 0.05;
			color=['#cccccc']
		}
		else{
			tick = SharedService.findRange([jobsData[0].data, jobsData[1].data, jobsData[2].data, jobsData[3].data, jobsData[4].data, jobsData[5].data]);
			color=['#67B8E0', '#FF9E5F', '#B94B3E', '#FDE260', '#CDEA64', '#6969B4', '#cccccc'];
		}
		var options = {
			chart: {
				defaultSeriesType: 'line',
				borderColor: '#ffffff'
			},
			plotOptions: {
				line: {
					enableMouseTracking: false,
					marker: {
						symbol: 'circle'
					},
					shadow: false
				}
			},
			colors: color,
			xAxis: {
				labels: {
					style: {
						fontSize: '10px'
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
				tickInterval: tick
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

	function getYears(firstYear, lastYear){
		var years;
		years=[];
		for (i=firstYear; i<=lastYear; i++) {
			years.push(i);
		}
		return years;
	}
}