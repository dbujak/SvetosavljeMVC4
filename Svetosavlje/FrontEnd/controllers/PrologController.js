function PrologCtrl($scope, $routeParams, $location, SharedService, HomeService){

	$scope.julianDate = SharedService.getJulianDate();
	$scope.dan = SharedService.getDay(SharedService.getJulianDate());
	$scope.mjesec = SharedService.getMonth(SharedService.getJulianDate());

	$scope.homeModel = {};

	HomeService.getSvetiDana().then(function(d){
		$scope.homeModel.svetiDana = d;
	});





}

