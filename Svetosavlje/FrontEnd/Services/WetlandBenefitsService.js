svetosavlje.factory('WetlandBenefitsService', function($http, SharedService) {

  var WetlandBenefitsService = {
    async : function (StateCountyFIPS) {
      var promise = $http.jsonp(SharedService.config.Snapshots + 'WetlandsBenefits?GeoId=' + StateCountyFIPS + '&callback=JSON_CALLBACK&f=jsonp')
      .then(function (data) {
        return data.data.CountyDataForYear.CountyData;
      });
      return promise;
    }
  };

  return WetlandBenefitsService;
})