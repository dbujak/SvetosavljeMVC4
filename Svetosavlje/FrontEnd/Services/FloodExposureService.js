cscSnapshots.factory('FloodExposureService', function($http, SharedService) {
	

  var FloodExposureService = {
    async : function (StateCountyFIPS) {
 
      var promise = $http.jsonp(SharedService.config.Snapshots + 'FloodData?GeoId=' + StateCountyFIPS + '&f=jsonp&callback=JSON_CALLBACK')
        .then(function (data) {
          return data.data.SnapshotFloodData.FloodData;
      });
      return promise;
    }
  };

  return FloodExposureService;
})

