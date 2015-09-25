var svetosavlje = angular.module('svetosavlje', ['ngRoute', 'ngSanitize']);

svetosavlje.config(['$routeProvider',
  function($routeProvider) {
    $routeProvider.
      when('/home', {
        templateUrl: 'views/HomeView.html',
        controller: 'HomeCtrl'
      }).
      when('/prolog', {
        templateUrl: 'views/PrologView.html',
        controller: 'PrologCtrl'
      }).
      when('/phones/:phoneId', {
        templateUrl: 'partials/phone-detail.html',
        controller: 'PhoneDetailCtrl'
      }).
      otherwise({
        redirectTo: '/home'
      });
  }]);

svetosavlje.filter('unique', function() {
    return function(input, key) {
        if(typeof input != 'undefined')
        {
            var unique = {};
            var uniqueList = [];
            for(var i = 0; i < input.length; i++){
                if(typeof unique[input[i][key]] == "undefined"){
                    unique[input[i][key]] = "";
                    uniqueList.push(input[i]);
                }
            }
            return uniqueList;
        }
    };
});



