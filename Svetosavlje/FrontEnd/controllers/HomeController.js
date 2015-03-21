function HomeCtrl($scope, $routeParams, $location, SharedService, HomeService){

	$scope.julianDate = SharedService.getJulianDate();
	$scope.dan = SharedService.getDay(SharedService.getJulianDate());
	$scope.mjesec = SharedService.getMonth(SharedService.getJulianDate());

	HomeService.async($scope.julianDate).then(function(d){
		$scope.homeModel = d;
	});

}

