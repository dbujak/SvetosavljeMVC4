function NavigationCtrl($scope, $rootScope, $routeParams, $location, $analytics, FloodExposureService, SharedService){
	
	$scope.$on('$locationChangeSuccess', function(event) {
		var params = $location.search();

		if (params != "")
		{
			var county = {};
			county.STATE_FIPS = params.state;
			county.CNTY_FIPS = params.county;
			var fips = county.STATE_FIPS + county.CNTY_FIPS;
			switch (params.action){
				case "flood":
					hideMap();
					$scope.$parent.showFloods = true;
					logGoogleAnalytics(params.action, fips);
					$rootScope.$emit('showFlood', county, params.bounds);
					break;
				case "ocean":
					hideMap();
					$scope.$parent.showOceanJobs = true;
					logGoogleAnalytics(params.action, fips);
					$rootScope.$emit('showOceanJobs', county, params.bounds);
					break;
				case "wetlands":
					hideMap();
					$scope.$parent.showWetlandBenefits = true;
					logGoogleAnalytics(params.action, fips);
					$rootScope.$emit('showWetlandBenefits', county, params.bounds);
					break;
				case "startover":
					hideAll();
					$scope.$parent.showMap = true;
					$rootScope.$emit('startOver');
					break;
				case "map":
					hideAll();
					$scope.$parent.showMap = true;
					$rootScope.$emit('map');
					break;
				default:
					hideAll();
					$scope.$parent.showMap = true;
					logGoogleAnalytics();
					break;
			}
		}
	});

	function logGoogleAnalytics(snapshot, fips){
		var snapshotsName = '/snapshotsAngular/';
		if (snapshot != undefined && fips != undefined && snapshot != "" && fips != ""){
			$analytics.pageTrack(snapshotsName + snapshot + '#' + fips);
		}
		else{
			$analytics.pageTrack(snapshotsName);
		}
		
	};

	function hideMap(){
		$scope.$parent.showMap = false;
		$scope.$parent.showFloods = false;
		$scope.$parent.showOceanJobs = false;
		$scope.$parent.showWetlandBenefits = false;
		$scope.$parent.showNavigation = true;
		$scope.$parent.showLoading = true;
	};

	function hideAll(){
		$scope.$parent.showMap = false;
		$scope.$parent.showFloods = false;
		$scope.$parent.showOceanJobs = false;
		$scope.$parent.showWetlandBenefits = false;
		$scope.$parent.showNavigation = false;
	};


	$scope.showMap = function()	{
		$location.url('process?action=map');
	};

	$scope.startOver = function(){
		$location.url('process?action=startover');
	};


	$scope.print = function(){
		window.print();
	};

}