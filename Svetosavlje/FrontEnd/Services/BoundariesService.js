cscSnapshots.factory('BoundariesService', function($http, SharedService) {

	var BoundariesService = {};

	BoundariesService.States = {
		async : function() {
//			var promise = $http.get('http://localhost/Snapshots/Services/StateBoundariesData.js') // to improve performance we could use this file
			// .then(function(data) {
			// 	return data.data;
			// });
			var promise = $http.jsonp(SharedService.config.Arcgis + 'Snapshot_Query/MapServer/2/query?f=json&spatialRel=esriSpatialRelIntersects&where=1=1&time=&outSR=4326&returnGeometry=true&callback=JSON_CALLBACK') // production
			.then(function(data) {
				return esriConverter().toGeoJson(data.data);
			});
			return promise;
		}
	};

	BoundariesService.Counties = {
		async : function() {
			var promise = $http.jsonp(SharedService.config.Arcgis + 'Snapshot_Query/MapServer/1/query?f=json&spatialRel=esriSpatialRelIntersects&outFields=STATE_FIPS,CNTY_FIPS,FLOOD,OCEAN_JOBS,WETLAND,STATE_NAME,NAME&returnGeometry=true&where=1=1&outSR=4326&callback=JSON_CALLBACK') 
			.then(function(data) {
				return esriConverter().toGeoJson(data.data);
			});
			return promise;
		}
	};
	return BoundariesService;

	
	
})




