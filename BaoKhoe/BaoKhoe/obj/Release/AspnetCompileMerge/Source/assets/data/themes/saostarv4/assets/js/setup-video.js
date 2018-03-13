/*setup videojs*/
window.players = [];
$(document).ready(function(){
	$('#content_detail video.vjs-saostar, .slide-video video, #md-video video.vjs-saostar').each(function(index, el) {
		init_single_video(index, el)
	});

	$('.remodal video.vjs-saostar').each(function(index, el){
		var id = 'vjsauto_' + Date.now();
		el.setAttribute('id', id);
		players.push(new SaostarPlayer(id, false).init());
	});
});

function init_single_video(index, el, play){
	var id = 'vjs_' + Date.now();
	el.setAttribute('id', id);
	players.push(new SaostarPlayer(id).init(play));
}


$(document).ready(function(){
	$('.homeplay-videos').click(function(){        
		var id = $(this).attr("id");
		var type = $(this).find('.vsource').val();
        var idyoutube = $(this).find('.vidyoutube').val();
		var src  = $(this).find('.vlink').val();
		var poster  = $(this).find('.vposter').val();
		var player  = id;
		var autoplay= 'autoplay';
		play_setupvideo_ssv4(type, poster, src, idyoutube, player, autoplay);
	});
	
});

function play_setupvideo_ssv4(type, poster, src, idyoutube, player, autoplay){
	player = $('.'+player);
	autoplay = autoplay ? "autoplay" : "";
	switch(type){
		case 'youtube':
			var html = "<iframe  width='500' height='281' src='https://www.youtube.com/embed/"+idyoutube+"?rel=0&autoplay=1' frameborder='0' allowfullscreen></iframe>";
			player.html(html);
			break;
		
		case 'saostar':
			var html = "<video class='video-js vjs-saostar' controls "+autoplay+" poster='"+poster+"'><source src='"+src+"' type='video/mp4' /></video>";
			player.html(html);
			var id = 'vjs_' + Date.now();
			player.find('video').attr('id', id);
			var vjs_player = new Player(id);
			vjs_player.init();

			break;

		case 'embed':
			var html = "<iframe src='"+src+"' frameborder='0' allowfullscreen></iframe>";
			player.html(html);
			break;
	};
}



var Player = function(id) {
	this.id = id;
	this.init = function(play) {
		var player = videojs(this.id);
		this.imaAvailable = typeof(google) != 'undefined' && typeof(google.ima) != 'undefined';
		console.log(id, this.imaAvailable);
		var options = {
		  id: id,
		  debug: true,
		  showControlsForJSAds: false,
		  adLabel: "Quảng cáo",
		  locale: 'vi',
		  adTagUrl: 'https://pubads.g.doubleclick.net/gampad/ads?sz=640x480&iu=/22552065/video_preroll&impl=s&gdfp_req=1&env=vp&output=vast&unviewed_position_start=1&correlator='+Date.now()
		};

		if(this.imaAvailable){
			player.ima(options);
		}

		// Remove controls from the player on iPad to stop native controls from stealing
		// our click
		var contentPlayer =  document.getElementById(id + '_html5_api');

		// Initialize the ad container when the video player is clicked, but only the
		// first time it's clicked.
		var startEvent = 'click';
		if(play == true){
			if(this.imaAvailable){
				player.ima.initializeAdDisplayContainer();
		    	player.ima.requestAds();
		    	player.play();
			} else {
		    	player.play();
			}
		} else {
			player.one(startEvent, function() {
		    	if(this.imaAvailable){
		    		player.ima.initializeAdDisplayContainer();
		        	player.ima.requestAds();
		        	player.play();
		    	} else {
		    		player.play();
		    	}
			    
			});
		}

		return this;

		
	};
	this.play = function(){
		var player = videojs(this.id);
		player.play();
	};

	this.pause = function(){
		var player = videojs(this.id);
		player.pause();
		if(this.imaAvailable){
			player.ima.pauseAd();
		}
		
	}
}

var SaostarPlayer = function(id, autoplay) {
  this.id = id;
  this.init = function() {
    var player = videojs(this.id);

    var options = {
      id: id,
      adTagUrl: 'https://pubads.g.doubleclick.net/gampad/ads?sz=640x480&iu=/22552065/video_preroll&impl=s&gdfp_req=1&env=vp&output=vast&unviewed_position_start=1&correlator='+Date.now()
    };

    player.ima(options);

    // Remove controls from the player on iPad to stop native controls from stealing
    // our click
    var contentPlayer =  document.getElementById(id + '_html5_api');
    if ((navigator.userAgent.match(/iPad/i) ||
          navigator.userAgent.match(/Android/i)) &&
        contentPlayer.hasAttribute('controls')) {
      contentPlayer.removeAttribute('controls');
    }

    // Initialize the ad container when the video player is clicked, but only the
    // first time it's clicked.
    var startEvent = 'click';
    if (navigator.userAgent.match(/iPhone/i) ||
        navigator.userAgent.match(/iPad/i) ||
        navigator.userAgent.match(/Android/i)) {
      startEvent = 'touchend';
    }

    player.one(startEvent, function() {
        player.ima.initializeAdDisplayContainer();
        player.ima.requestAds();
        player.play();
    });
  }
  return this;
}
