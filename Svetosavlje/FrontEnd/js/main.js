var svetosavlje = angular.module('svetosavlje', ['ngRoute']);

svetosavlje.config(function () {
    // turn off automatic tracking
    // $analyticsProvider.virtualPageviews(false);
});

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



