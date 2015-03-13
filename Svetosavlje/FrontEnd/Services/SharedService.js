cscSnapshots.factory('SharedService', function($location) {
	
  var SharedService = {};

  var host = $location.$$host;
  var argis, snapshots, isProduction;

  isProduction = false;
  switch (host.toLowerCase()){
    case "coast.noaa.gov":  // Prod
    case "www.coast.noaa.gov":  // Prod
      argis = "http://maps.coast.noaa.gov/arcgis/rest/services/Snapshot/";
      snapshots = "http://coast.noaa.gov/dataservices/Snapshot/v2/";
      isProduction = true;
      break;
    case "webqa.coast.noaa.gov":  // QA
      // argis = "http://coast.noaa.gov/ArcGIS_QA/rest/services/Snapshot/";
      argis = "http://maps.coast.noaa.gov/arcgis/rest/services/Snapshot/";
      snapshots = "http://webqa.coast.noaa.gov/dataservices/Snapshot/v2/";
      break;
    default:  // Dev
      // argis = "http://coast.noaa.gov/ArcGIS_QA/rest/services/Snapshot/";
      argis = "http://maps.coast.noaa.gov/arcgis/rest/services/Snapshot/";
      snapshots = "http://webdev/dataservices/Snapshot/v2/";
  }

  SharedService.config = {
    "Arcgis": argis,
    "Snapshots": snapshots
  };
  SharedService.isProduction =isProduction;
  
  SharedService.hightChartOptions = function(){
    return {
      chart: {
        borderColor: '#ffffff',
        borderRadius: 0
      },
      credits: {
        enabled: false 
      },
      plotOptions: {
        pie: {
          point: {
            events: {
              legendItemClick: function(event) {
                event.preventDefault();
              }
            }
          },
          states: {
            hover: {
              enabled: false
            }
          }
        }
      },
      title: {
        text: null
      },
      tooltip: {
        formatter: function() {
          var name = this.point.name;
          var value= Math.round(this.percentage) + '%';
          if(this.point.name === undefined) {
            name = this.series.name;
            value = Math.round(this.y*100)/100;
          }

          return '<strong>' + name + '</strong><br>' + value;
        }
      },
      legend: {
        layout: 'vertical',
        align: 'right',
        verticalAlign: 'middle'
      },
      exporting: {
        buttons: {
          printButton: {
            enabled: false
          },
          exportButton: {
            enabled: false
          }
        }
      }
    };
  };

  SharedService.findRange = function(array){
    var minArr = [];
    var maxArr = [];
    var minNum, maxNum, stepNum, numDig;
    var bigNum = '100000000';
    for(i=0; i<array.length; i++) {
      minNum = Math.min.apply( Math, array[i] );
      maxNum = Math.max.apply( Math, array[i] );
      minArr.push(minNum);
      maxArr.push(maxNum);
    }
    minNum = Math.min.apply( Math, minArr );
    maxNum = Math.max.apply( Math, maxArr );
    
    stepNum = (Math.abs(minNum) + Math.abs(maxNum))/6;
    if(stepNum <1)
      stepNum = 1;
    numDig = Math.round(stepNum) + '';
    numDig = parseInt(bigNum.substr(0,numDig.length));
    
    var finalNum = Math.round(stepNum/numDig)*numDig;
    return finalNum;  
  };

  SharedService.findChange = function(type, theArray, years) {
    var fullArray = [];
    var firstNum = 0;
    var k = 0;
    while ((firstNum === 0) || (firstNum === "0") || (firstNum === null)) {
      firstNum = theArray[k];
      k++;
    }
    k--;
    for (var i = 0; i < years.length; i++) {
      var num;
      if(k > i || theArray[i] === null) {
        num = null;
      } else {
        if(type == 'percent') {
          num = ((theArray[i]/theArray[k])-1)*100;
        } else {
          num = theArray[i]-theArray[0];
        }
      }
      if (isNaN(num)) {
        num = null;
      }
      fullArray.push(num);
    }
    return fullArray;
  };


  SharedService.openModal = function(chartOptions)  {
    var chartRenderTo = chartOptions.chart.renderTo;
    var chartEvents = chartOptions.chart.events;
    var chartTitle = $('#' + chartRenderTo).parent().find('p:first').text();
    var chartLegend = '';
    $('#' + chartRenderTo).parents('.section').find('p.mapLegend').each(function() {
      chartLegend += $(this).html() + '<br>';
    });
    chartLegend = chartLegend.replace(/[\(\)NA0-9\%\.]/g, '');
    chartOptions.chart.events = { click: null };
    chartOptions.chart.renderTo = 'modal';
    chartOptions.exporting.enabled = true;
    chartOptions.legend.enabled = true;
    chartOptions.title.text = chartTitle;
    if (chartOptions.plotOptions.column)
      chartOptions.plotOptions.column.enableMouseTracking = true;
    if (chartOptions.plotOptions.line)
      chartOptions.plotOptions.line.enableMouseTracking = true;
    $.modal('<a class="modalSaveImg" onClick="modalChart.exportChart();" title="Save Chart as PNG"></a><div id="modal"></div>', {
      onClose: function() {
        chartOptions.chart.renderTo = chartRenderTo;
        chartOptions.exporting.enabled = false;
        chartOptions.chart.events = chartEvents;
        chartOptions.legend.enabled = false;
        chartOptions.title.text = null;
        if (chartOptions.plotOptions.column)
          chartOptions.plotOptions.column.enableMouseTracking = false;
        if (chartOptions.plotOptions.line)
          chartOptions.plotOptions.line.enableMouseTracking = false;
        $.modal.close();
      }
    });
    modalChart = new Highcharts.Chart(chartOptions);
  };

  SharedService.getQueryStringParameter = function(name){
    name = name.replace(/[\[]/, "\\[").replace(/[\]]/, "\\]");
    var regex = new RegExp("[\\?&]" + name + "=([^&#]*)"),
        results = regex.exec(location.search);
    return results == null ? "" : decodeURIComponent(results[1].replace(/\+/g, " "));
  };


  

  return SharedService;

})

