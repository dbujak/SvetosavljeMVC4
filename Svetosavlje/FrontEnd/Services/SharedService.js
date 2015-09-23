svetosavlje.factory('SharedService', function($location) {

  var SharedService = {
    config: {
      webServicePath: 'http://...'
    }
  };

  var host = $location.$$host;
  var webServicePath = "";

  switch (host.toLowerCase()){
    case "localhost":   // Developers
      webServicePath = "http://localhost:1389/api/";
      //break;
    default:  // everything else
      webServicePath = 'http://www.svetosavlje.org/net4/api/';
  }

  SharedService.config.webServicePath = webServicePath;

  SharedService.getJulianDate = function(gregorianDate){  // if no date will return today's date
    
    if (gregorianDate == undefined){ // assign today's date
      gregorianDate = new Date();
    }
    
    var julianDate = new Date(gregorianDate.getTime() - (13 * 24 * 60 * 60 * 1000)); // 13 days (* 24 hrs * 60 mins * 60 sec * 1000 milisec)

    return julianDate;  
  };

  SharedService.getDay = function(date){
    // the date is really a gregorian date, which has day of the week different than julian (so greg Friday is julian Thursday) - that's why these offsets
    switch (date.getDay()){
      case 1:
        return 'недеља';
        break;
      case 2:
        return 'понедељак';
        break;
      case 3:
        return 'уторак';
        break;
      case 4:
        return 'сриједа';
        break;
      case 5:
        return 'четвртак';
        break;
      case 6:
        return 'петак';
        break;
      case 0:
        return 'субота';
        break;
    }
  };

  SharedService.getMonth = function(date){
    return date.toString("MMMM")
    // switch (date.getMonth()){
    //   case 0:
    //     return 'јануар';
    //     break;
    //   case 1:
    //     return 'фебруар';
    //     break;
    //   case 2:
    //     return 'март';
    //     break;
    //   case 3:
    //     return 'април';
    //     break;
    //   case 4:
    //     return 'мај';
    //     break;
    //   case 5:
    //     return 'јуни';
    //     break;
    //   case 6:
    //     return 'јули';
    //     break;
    //   case 7:
    //     return 'август';
    //     break;
    //   case 8:
    //     return 'септембар';
    //     break;
    //   case 9:
    //     return 'октобар';
    //     break;
    //   case 10:
    //     return 'новембар';
    //     break;
    //   case 11:
    //     return 'децембар';
    //     break;
    // }
  };  

  SharedService.decodeHTML = function(value){
    return $('<div/>').html(value).text();
  };

  SharedService.decodeWordPress = function(jsonObject){
    for (var key in jsonObject){
      if (jsonObject.hasOwnProperty(key)){
        jsonObject[key].Title = SharedService.decodeHTML(jsonObject[key].Title);
        jsonObject[key].Content = SharedService.decodeHTML(jsonObject[key].Content);
      }
    }

    return jsonObject;
  };

  return SharedService;

})