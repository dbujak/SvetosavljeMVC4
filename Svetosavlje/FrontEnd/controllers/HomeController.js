function HomeCtrl($scope, $routeParams, $location, SharedService, PrologService){

	$scope.homeModel = {
		julianDate: SharedService.getJulianDate(),
		dan: SharedService.getDay(SharedService.getJulianDate()),
		mjesec: SharedService.getMonth(SharedService.getJulianDate())
	};

	PrologService.asyncSvetiDana($scope.homeModel.julianDate).then(function(d){
		$scope.homeModel.svetiDana = d;
	});



	console.log($scope.homeModel);
}

