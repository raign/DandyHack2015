// THE FOLLOWING IS FROM :  http://padilicious.com/code/touchevents/swipesensejs.html
	// TOUCH-EVENTS SINGLE-FINGER SWIPE-SENSING JAVASCRIPT
	// Courtesy of PADILICIOUS.COM and MACOSXAUTOMATION.COM
	
	// this script can be used with one or more page elements to perform actions based on them being swiped with a single finger

	var triggerElementID = null; // this variable is used to identity the triggering element
	var fingerCount = 0;
	var startX = 0;
	var startY = 0;
	var curX = 0;
	var curY = 0;
	var deltaX = 0;
	var deltaY = 0;
	var horzDiff = 0;
	var vertDiff = 0;
	var minLength = 72; // the shortest distance the user may swipe
	var swipeLength = 0;
	var swipeAngle = null;
	var swipeDirection = null;
	var path = "img/";
	var MIN = 1;
	var MAX = 100;
	// The 4 Touch Event Handlers
	
	// NOTE: the touchStart handler should also receive the ID of the triggering element
	// make sure its ID is passed in the event call placed in the element declaration, like:
	// <div id="picture-frame" ontouchstart="touchStart(event,'picture-frame');"  ontouchend="touchEnd(event);" ontouchmove="touchMove(event);" ontouchcancel="touchCancel(event);">


	function touchStart(event,passedName) {
		// disable the standard ability to select the touched object
		event.preventDefault();
		// get the total number of fingers touching the screen
		fingerCount = event.touches.length;
		// since we're looking for a swipe (single finger) and not a gesture (multiple fingers),
		// check that only one finger was used
		if ( fingerCount == 1 ) {
			// get the coordinates of the touch
			startX = event.touches[0].pageX;
			startY = event.touches[0].pageY;
			// store the triggering element ID
			triggerElementID = passedName;
		} else {
			// more than one finger touched so cancel
			touchCancel(event);
		}
	}

	function touchMove(event) {
		event.preventDefault();
		if ( event.touches.length == 1 ) {
			curX = event.touches[0].pageX;
			curY = event.touches[0].pageY;
		} else {
			touchCancel(event);
		}
	}
	
	function touchEnd(event) {
		event.preventDefault();
		// check to see if more than one finger was used and that there is an ending coordinate
		if ( fingerCount == 1 && curX != 0 ) {
			// use the Distance Formula to determine the length of the swipe
			swipeLength = Math.round(Math.sqrt(Math.pow(curX - startX,2) + Math.pow(curY - startY,2)));
			// if the user swiped more than the minimum length, perform the appropriate action
			if ( swipeLength >= minLength ) {
				caluculateAngle();
				determineSwipeDirection();
				processingRoutine();
				touchCancel(event); // reset the variables
			} else {
				touchCancel(event);
			}	
		} else {
			touchCancel(event);
		}
	}

	function touchCancel(event) {
		// reset the variables back to default values
		fingerCount = 0;
		startX = 0;
		startY = 0;
		curX = 0;
		curY = 0;
		deltaX = 0;
		deltaY = 0;
		horzDiff = 0;
		vertDiff = 0;
		swipeLength = 0;
		swipeAngle = null;
		swipeDirection = null;
		triggerElementID = null;
	}
	
	function caluculateAngle() {
		var X = startX-curX;
		var Y = curY-startY;
		var Z = Math.round(Math.sqrt(Math.pow(X,2)+Math.pow(Y,2))); //the distance - rounded - in pixels
		var r = Math.atan2(Y,X); //angle in radians (Cartesian system)
		swipeAngle = Math.round(r*180/Math.PI); //angle in degrees
		if ( swipeAngle < 0 ) { swipeAngle =  360 - Math.abs(swipeAngle); }
	}
	
	function determineSwipeDirection() {
		if ( (swipeAngle <= 45) && (swipeAngle >= 0) ) {
			swipeDirection = 'left';
		} else if ( (swipeAngle <= 360) && (swipeAngle >= 315) ) {
			swipeDirection = 'left';
		} else if ( (swipeAngle >= 135) && (swipeAngle <= 225) ) {
			swipeDirection = 'right';
		} else if ( (swipeAngle > 45) && (swipeAngle < 135) ) {
			swipeDirection = 'down';
		} else {
			swipeDirection = 'up';
		}
	}
	
	function processingRoutine() {
		var swipedElement = document.getElementById(triggerElementID);
		if ( swipeDirection == 'left' ) {
			// REPLACE WITH YOUR ROUTINES
			
			var id = getRandomInt(MIN, MAX);
			var img = document.getElementById("image_shown");
			img.src = path.concat(id);
			img.dataset.num = id;
			swipedElement.style.backgroundColor = 'red';
			//swipedElement.style.backgroundColor = 'white';
			
		} else if ( swipeDirection == 'right' ) {
			
			//get the current image
			var img = document.getElementById("image_shown");
			//update in database if +1
			doAction({img_num: img.dataset.num}).done(function(data) {
				if(data.success) {
					//alert("we did it reddit!");
					//alert(JSON.stringify(data));
				}
				}).fail(function(j, t, e) {
					alert("ajax failed");
					alert(e);
				});
			//randomly generate next image.
			var id = getRandomInt(MIN, MAX);
			//change current picture num & path
			img.src = path.concat(id);
			img.dataset.num = id;
			
			//change background color
			swipedElement.style.backgroundColor = 'green';
			//swipedElement.style.backgroundColor = 'white';
		} 
	}
		
	function getRandomInt() {
    	return Math.floor(Math.random() * (MAX - MIN + 1)) + MIN + "";
	}
	// JavaScript Document//handles the click event for link 1, sends the query
	function getOutput() {
	  getRequest(
		  'response.php', // URL for the PHP file
		   drawOutput,  // handle successful request
		   drawError    // handle error
	  );
	  	src = getRandomInt(MIN, MAX);
		document.images["image_shown"].src = "";
		
		document.getElementById("image_shown").src = "SourceOfImage";
	  return false;
	}  
	// handles drawing an error message
	function drawError() {
		var container = document.getElementById('output');
		container.innerHTML = 'Bummer: there was an error!';
	}
	// handles the response, adds the html
	function drawOutput(responseText) {
		var container = document.getElementById('output');
		container.innerHTML = responseText;
	}
	// helper function for cross-browser request object
	function getRequest(url, success, error) {
		var req = false;
		try{
			// most browsers
			req = new XMLHttpRequest();
		} catch (e){
			// IE
			try{
				req = new ActiveXObject("Msxml2.XMLHTTP");
			} catch(e) {
				// try an older version
				try{
					req = new ActiveXObject("Microsoft.XMLHTTP");
				} catch(e) {
					return false;
				}
			}
		}
		if (!req) return false;
		if (typeof success != 'function') success = function () {};
		if (typeof error!= 'function') error = function () {};
		req.onreadystatechange = function(){
			if(req.readyState == 4) {
				return req.status === 200 ? 
					success(req.responseText) : error(req.status);
			}
		}
		req.open("GET", url, true);
		req.send(null);
		return req;
	}
	
	
	//JINZE AJAX
function doAction(params) {
	//alert("doing action")
  
    var request = {
        type: 'POST',
        url: 'upvote.php',
        dataType: 'json',
    };
	request.data = params;
	//alert(JSON.stringify(request));
    // return the Deferred so that code can .done() and .fail() it
    return $.ajax(request);
}


//BUTTON ACTION LISTENERS
function leftButton(){
	var swipedElement = document.getElementById("swipeBox");
	var id = getRandomInt(MIN, MAX);
	var img = document.getElementById("image_shown");
	img.src = path.concat(id);
	img.dataset.num = id;
	swipedElement.style.backgroundColor = 'red';
}

function rightButton(){
	
	var swipedElement = document.getElementById("swipeBox");
	//get the current image
	var img = document.getElementById("image_shown");
	//update in database if +1
	doAction({img_num: img.dataset.num}).done(function(data) {
		if(data.success) {
			//alert("we did it reddit!");
			//alert(JSON.stringify(data));
		}
		}).fail(function(j, t, e) {
			alert("ajax failed");
			alert(e);
		});
	//randomly generate next image.
	var id = getRandomInt(MIN, MAX);
	//change current picture num & path
	img.src = path.concat(id);
	img.dataset.num = id;
	swipedElement.style.backgroundColor = 'green';
}