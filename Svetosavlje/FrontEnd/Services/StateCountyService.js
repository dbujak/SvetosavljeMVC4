cscSnapshots.factory('StateCountyService', function($http, SharedService) {

  var StateCountyService = {
    async : function () {
      var promise = $http.jsonp(SharedService.config.Snapshots + 'GeographyLookUp?f=jsonp&callback=JSON_CALLBACK')
      .then(function (data) {
        return data.data.SnapshotGeoData.County;
      });
      return promise;
    }
  };

  return StateCountyService;
})