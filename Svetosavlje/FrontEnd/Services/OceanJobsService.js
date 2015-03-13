cscSnapshots.factory('OceanJobsService', function($http, SharedService) {

  var OceanJobsService = {
    async : function (StateCountyFIPS) {
      var promise = $http.jsonp(SharedService.config.Snapshots + 'OceanJobs?GeoId=' + StateCountyFIPS + '&callback=JSON_CALLBACK&f=jsonp')
      .then(function (data) {
        return data.data.CountyDataForYear;
      });
      return promise;
    }
  };

  return OceanJobsService;
})