svetosavlje.factory('PrologService', function($http, SharedService) {

  var PrologService = {};

  PrologService.asyncSvetiDana = function (date) {
     var promise = $http.get('http://localhost/Svetosavlje/Services/PrologServiceSvetiDana.js')
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

  return PrologService;
})