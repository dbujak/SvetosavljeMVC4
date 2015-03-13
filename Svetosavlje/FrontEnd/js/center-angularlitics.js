  (function(i,s,o,g,r,a,m){i['GoogleAnalyticsObject']=r;i[r]=i[r]||function(){(i[r].q=i[r].q||[]).push(arguments)},i[r].l=1*new Date();a=s.createElement(o), m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','//www.google-analytics.com/analytics.js','ga');
  
  	//swap analytics ID based on hostname
	var tier = "";
	var host = location.hostname;
	
	if (host.indexOf("webdev") > -1) {
		tier = "UA-41501375-2";
	} else if (host.indexOf("webqa") > -1) {
		tier = "UA-41501375-7";
	} else if (host.indexOf("www") > -1) {
		tier = "UA-41501375-1";
	} else if (host.indexOf("maps") > -1) {
		tier = "UA-41501375-1";
	} else if (host.indexOf("localhost") > -1) {
		tier = "UA-41501375-2"; //won't report
	} else {
		tier = "UA-41501375-1";
	}

  ga('create', tier, 'noaa.gov');
  ga('set', 'anonymizeIp', true);