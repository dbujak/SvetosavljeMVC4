svetosavlje.factory('HomeService', function($http, SharedService) {

  var HomeService = {};

  HomeService.async = function (date) {
     var promise = $http.get('http://localhost/Svetosavlje/Services/HomeServiceTestData.js')
      .then(function(data) {
       return data.data;
      });
      return promise;
      // var promise = $http.jsonp(SharedService.config.webServicePath + 'xxx' + StateCountyFIPS + '&callback=JSON_CALLBACK&f=jsonp')
      // .then(function (data) {
      //   return data.data.CountyDataForYear.CountyData;
      // });
      // return promise;
    };

  return HomeService;
})