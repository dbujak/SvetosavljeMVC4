function HomeCtrl($scope, $routeParams, $location, SharedService, HomeService){

	$scope.julianDate = SharedService.getJulianDate();
	$scope.dan = SharedService.getDay(SharedService.getJulianDate());
	$scope.mjesec = SharedService.getMonth(SharedService.getJulianDate());

	$scope.homeModel = {};

	HomeService.getSvetiDana().then(function(d){
		$scope.homeModel.svetiDana = d;
	});

	HomeService.getPostDana().then(function(d){
		$scope.homeModel.postDana = d.replace(/\"/g, '');
	});

	HomeService.getCitatDana().then(function(d){
		$scope.homeModel.citatDana = d;
	});

	HomeService.getDnevnoCitanjeDana().then(function(d){
		$scope.homeModel.dnevnoCitanjeDana = d;
	});

	HomeService.getMisija().then(function(d){
		$scope.homeModel.misija = SharedService.decodeWordPress(d);
	});

	HomeService.getVijesti().then(function(d){
		$scope.homeModel.vijesti = SharedService.decodeWordPress(d);
	});

	HomeService.getUrednistvo().then(function(d){
		$scope.homeModel.urednistvo = SharedService.decodeWordPress(d);
	});

	HomeService.getListaTopics().then(function(d){
		$scope.homeModel.listaTopics = d;
	});

	HomeService.getPitanjaPastiru().then(function(d){
		$scope.homeModel.pitanjaPastiru = d;
	});




}

