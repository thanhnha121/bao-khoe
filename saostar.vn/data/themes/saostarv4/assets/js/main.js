/*Playlist*/
var rypp = (function($, undefined) {
    'use strict';

    function Rypp(el, api_key, options) {
        if (typeof api_key === 'undefined') {
            console.log("Youtube API V3 requires a valid API KEY.\nFollow the instructions at: https://developers.google.com/youtube/v3/getting-started");
            return false;
        }
        // DOM Elements container
        this.DOM = {};
        // Default settings container
        this.options = {};
        // Data / urls
        this.data = {
            // Playlist url
            ytapi: {
                playlist_info: 'https://www.googleapis.com/youtube/v3/playlists?part=snippet&id={{RESOURCES_ID}}&key={{YOUR_API_KEY}}',
                playlist: 'https://www.googleapis.com/youtube/v3/playlistItems?part=snippet&maxResults=50&playlistId={{RESOURCES_ID}}&key={{YOUR_API_KEY}}',
                pl_ID: '',
                videolist: 'https://www.googleapis.com/youtube/v3/videos?part=snippet,status&maxResults=50&id={{RESOURCES_ID}}&key={{YOUR_API_KEY}}',
            },
            temp_vl: [], // Temporary videolist
            firsttime: true,
            ismobile: (typeof window.orientation !== 'undefined'),
            ispopulated: false
        };
        // Initialize
        this.init(el, api_key, options);
    }
    // Prototype for the instance
    Rypp.prototype = {
        init: function(el, api_key, options) {
            // Api key
            this.api_key = api_key;
            // Default options
            this.options = {
                update_title_desc: false,
                autoplay: true,
                autonext: true,
                loop: true,
                mute: true,
                debug: false
            };
            // Merge initial options
            if (typeof options !== 'undefined') {
                $.extend(this.options, options);
            }
            // DOM elements
            this.DOM = {};
            this.DOM.$el = $(el);
            this.DOM.$playlc = this.DOM.$el.find('.rypp-playlist');
            this.DOM.$items = this.DOM.$el.find('.rypp-items');
            this.DOM.$videoc = this.DOM.$el.find('.rypp-video');
            this.DOM.$title = this.DOM.$el.find('.rypp-title');
            this.DOM.$desc = this.DOM.$el.find('.rypp-desc');
            this.DOM.$tvideo = this.DOM.$el.find('.total-video');
            this.DOM.$cvideo = this.DOM.$el.find('.cur-video');
            // YT Player object
            this.DOM.$el[0].ytplayer = null;
            this.userClicked = false;
            // Unique player ID
            this.data.player_uid = (Math.random().toString(16).substr(2, 8));
            this.DOM.$el.attr('data-rypp', this.data.player_uid).find('.rypp-video-player').attr('id', 'rypp-vp-' + this.data.player_uid).attr('name', 'rypp-vp-' + this.data.player_uid);
            if (this.options.debug) console.log('Unique ID: rypp-vp-' + this.data.player_uid);
            // Link JS only once
            if (typeof window.YT === 'undefined') {
                var tag = document.createElement('script'),
                    hID = document.getElementsByTagName('head')[0];
                // Add youtube API in HEAD
                // tag.src = "https://www.youtube.com/iframe_api";
                tag.src = 'https://www.youtube.com/iframe_api?version=3';
                hID.appendChild(tag);
            } else {
                this.addAPIPlayer();
            }
        },
        onYTIframeAPIReadyCallback: function() {
            this.addAPIPlayer();
        },
        updateTitleDesc: function() {
            var that = this,
                resources_id = this.DOM.$el.attr('data-playlist'),
                url = this.data.ytapi.playlist_info.replace('{{RESOURCES_ID}}', resources_id).replace('{{YOUR_API_KEY}}', this.api_key);
            $.ajaxSetup({
                cache: false
            });
            $.ajax(url, {
                context: this,
                dataType: 'json',
                crossDomain: true,
                error: function() {
                    // Not successful
                },
                success: function(data) {
                    // console.log(data);
                    this.DOM.$title.html(data.items[0].snippet.title);
                    this.DOM.$desc.html(data.items[0].snippet.description);
                }
            });
        },
        populatePlaylist: function() {
            if (this.options.update_title_desc) {
                if (this.options.debug) console.log(this.data.player_uid + ': Updating playlist title / desc');
                this.updateTitleDesc();
            }
            // Empty playlist
            if (this.options.debug) console.log(this.data.player_uid + ': Populating playlist');
            this.DOM.$items.html('').append($('<ol>'));
            // Now we read the video list from playlist data or from IDs...
            if (this.DOM.$el.attr('data-playlist')) {
                this.data.pl_ID = this.DOM.$el.attr('data-playlist');
                this.getVideosFrom('playlist', this.data.pl_ID);
            } else if (this.DOM.$el.attr('data-ids')) {
                var vl = this.DOM.$el.attr('data-ids');
                // Clean spaces
                vl = ($.map(vl.split(','), $.trim)).join(',');
                this.getVideosFrom('videolist', vl);
            }
        },
        addAPIPlayer: function() {
            var that = this;
            window.YTConfig = {
                'host': 'https://www.youtube.com'
            };
            this.DOM.$el[0].ytplayer = new YT.Player('rypp-vp-' + that.data.player_uid, {
                // height: '390',
                // width: '640',
                playerVars: {
                    // controls: 0,
                    // showinfo: 0 ,
                    // autoplay: 0,
                    // html5: 1,
                    enablejsapi: 1,
                    rel: 0,
                    modestbranding: 1,
                    wmode: 'transparent'
                },
                events: {
                    'onReady': function() {
                        if (that.options.debug) console.log(that.data.player_uid + ': ytplayer ready');
                        that.onPlayerReady();
                    },
                    'onStateChange': function(e) {
                        that.onPlayerStateChange(e);
                    },
                    'onError': function(e) {
                        console.log(e);
                    }
                }
            });
        },
        // Ready to play
        onPlayerReady: function() {
            if (this.options.debug) console.log(this.data.player_uid + ': ytplayer ready callback');
            this.populatePlaylist();
            // this.startPlayList();
        },
        // When video finish
        onPlayerStateChange: function(e) {
            var that = this;
            if (typeof e !== 'undefined') {
                // On video loaded?
                if (e.data === -1 && this.data.firsttime) {
                    if (!this.options.autoplay && !this.data.ismobile) { // Is desktop
                        this.DOM.$el[0].ytplayer.stopVideo();
                        this.data.firsttime = false;
                    }
                    if (this.options.mute) {
                        var clickSrc = this.DOM.$items.find('li.selected').data('click-src');
                        var isMuted = this.DOM.$el[0].ytplayer.isMuted();
                        if (clickSrc === 'user') {
                            this.userClicked = true;
                            this.DOM.$el[0].ytplayer.unMute();
                        } else {
                            if (this.userClicked == true) {
                                this.DOM.$el[0].ytplayer.unMute();
                            } else {
                                this.DOM.$el[0].ytplayer.mute();
                            }
                        }
                    }
                }
                // If mobile and stored in buffer we STOP the video in mobile devices
                if (e.data === 3 && this.data.ismobile && this.data.firsttime) {
                    setTimeout(function() {
                        that.DOM.$el[0].ytplayer.stopVideo();
                        that.data.firsttime = false;
                    }, 500);
                }
                // Play next only if not mobile
                var next = null;
                if (e.data === 0 && !this.data.ismobile && this.options.autonext) {
                    next = this.DOM.$items.find('li.selected').next();
                    if (next.length === 0 && this.options.loop) {
                        next = this.DOM.$items.find('li').first();
                    }
                    next.click();
                }
            }
        },
        // Get video from data-ids or playlist
        // It's impossible to know if a video in a playlist its available or currently deleted. So we do 2 request, first we get all the video IDs an then we ask for info about them.
        getVideosFrom: function(kind, resources_id, page_token) {
            var that = this,
                url = this.data.ytapi[kind].replace('{{RESOURCES_ID}}', resources_id).replace('{{YOUR_API_KEY}}', this.api_key);
            if (typeof page_token !== 'undefined') {
                url += '&pageToken=' + page_token;
            }
            $.ajaxSetup({
                cache: false
            });
            $.ajax(url, {
                context: this,
                dataType: 'json',
                crossDomain: true,
                error: function() {
                    // Not successful
                },
                success: function(data) {
                    // We queried for a playlist
                    if (data.kind === 'youtube#playlistItemListResponse') {
                        var video_set = [];
                        // We get the video IDs and query gain, its the only way to be sure that all the videos are available, and not were deleted :(
                        $.map(data.items, function(val, idx) {
                            if (typeof val.snippet.resourceId.videoId !== 'undefined') {
                                // Add video to temporary list
                                video_set.push(val.snippet.resourceId.videoId);
                                // return val.snippet.resourceId.videoId;
                            }
                        });
                        that.data.temp_vl.push(video_set);
                        // If there are several pages we ask for next
                        if (typeof data.nextPageToken !== 'undefined' && data.nextPageToken !== '') {
                            that.getVideosFrom('playlist', that.data.pl_ID, data.nextPageToken);
                        } else {
                            // No more pages... we process the videos
                            for (var j = 0, len_pl = that.data.temp_vl.length; j < len_pl; j++) {
                                video_set = that.data.temp_vl.shift();
                                that.getVideosFrom('videolist', video_set.join(','));
                            }
                        }
                    } else if (data.kind === 'youtube#videoListResponse') {
                        // Videos froma  Videolist
                        that.DOM.$tvideo.html(data.items.length);
                        for (var i = 0, len = data.items.length; i < len; i++) {
                            var item = data.items[i];
                            // Videos without thumbnail, deleted or rejected are not included in the player!
                            if ($.inArray(item.status.uploadStatus, ['rejected', 'deleted', 'failed']) === -1 && typeof item.snippet.thumbnails !== 'undefined') {
                                var vid = item.id,
                                    tit = item.snippet.title,
                                    aut = item.snippet.channelTitle,
                                    thu = item.snippet.thumbnails.default.url;
                                that.addVideo2Playlist(vid, tit, aut, thu);
                            }
                            if ($.isEmptyObject(that.data.temp_vl)) {
                                this.startPlayList();
                            }
                        }
                    }
                }
            });
        },
        // All videos are supossed to be loaded
        // lets start the playlist
        startPlayList: function() {
            var D = this.DOM;
            // Select first if none
            if (D.$items.find('li.selected').length === 0) {
                D.$items.find('li').first().click();
            }
        },
        // Add video block to playlist
        addVideo2Playlist: function(vid, tit, aut, thu) {
            var D = this.DOM;
            var that = this;
            var $el = $('<li data-video-id="' + vid + '"><p class="title"><span>' + tit + '</span><small class="author">' + aut + '</small></p><img src="' + thu + '" class="thumb"></li>');
            $el.appendTo(D.$items.find('ol'));
            $el.on('click', function(e) {
                e.preventDefault();
                D.$items.find('li').removeClass('selected');
                $(this).addClass('selected');
                D.$cvideo.html($(this).index() + 1);
                vid = $(this).data('video-id');
                // Call YT API function
                // If we are in mobile we must stop
                that.DOM.$el[0].ytplayer.loadVideoById(vid);
                if (e.originalEvent !== undefined) {
                    $(this).data('click-src', 'user');
                } else {
                    $(this).data('click-src', 'auto');
                }
                if (that.data.ismobile) {
                    that.data.firsttime = true;
                }
            });
        },
    }; // prototypes
    return Rypp;
}(jQuery));
// YOUTUBE API CALLBACK
function onYouTubeIframeAPIReady() {
    // console.log( 'Youtube API script loaded. Start players.' );
    $('[data-rypp]').each(function(idx, el) {
        $(el)[0].rypp_data_obj.onYTIframeAPIReadyCallback();
    });
}
// JQuery hook
$.fn.rypp = function(api_key, options) {
    return this.each(function() {
        // Store object in DOM element
        this.rypp_data_obj = new rypp(this, api_key, options);
    });
};
/*end playlist*/
$(document).ready(function() {
    // hide #back-top first
    $("#back-top").hide();
    // fade in #back-top
    $(function() {
        $(window).scroll(function() {
            if ($(this).scrollTop() > 100) {
                $('#back-top').fadeIn();
            } else {
                $('#back-top').fadeOut();
            }
        });
        // scroll body to 0px on click
        $('#back-top a').click(function() {
            $('body,html').animate({
                scrollTop: 0
            }, 800);
            return false;
        });
    });
    $('.time-ago').each(function() {
        var $this = $(this);
        var posttime = new Date($this.attr('datetime'));
        var now = new Date();
        var diff = now - posttime;
        if (diff < 86400000 && diff > 0) {
            if (diff < 3600000) {
                $this.html((Math.floor(diff / 1000 / 60)) + ' phút trước');
            } else {
                $this.html((Math.floor(diff / 1000 / 60 / 60)) + ' giờ trước');
            }
        }
    });
    if ($().headroom) {
        $("#menu").headroom({
            tolerance: {
                down: 0,
                up: 10
            },
            offset: 100,
            classes: {
                initial: "sstick-menu",
                pinned: "sstick-menu-pinned",
                unpinned: "sstick-menu-unpinned",
                top: "sstick-menu-top",
                notTop: "sstick-menu-not-top",
            }
        });
    }
    if ($().stick_in_parent) {
        $(".ss-sidebar").stick_in_parent({
            parent: ".grid1000",
            spacer: ".ss-spacer",
        });
        $(".sidebar").stick_in_parent({
            parent: "main",
            offset_top: 10,
            bottoming: true
        });
        $(window).on('load', function() {
            $(document.body).trigger("sticky_kit:recalc");
        });
    }
});
if ($().lazyLoadXT) {
    $.extend($.lazyLoadXT, {
        edgeY: 800,
        edgeX: 800,
        selector: 'img[data-src], iframe[data-src]'
    });
    $(window).lazyLoadXT();
    $(window).on("lazyload", function(e) {
        var $el = $(e.target);
        if ($el.hasClass('auto-size-iframe')) {
            $el.css('background', 'transparent').iFrameResize([{}]);
        }
    });
}

function share_link_img(links, imgs, names, desc) {
    FB.ui({
        method: 'feed',
        link: links,
        picture: imgs,
        name: names,
        description: desc,
    }, function(response) {});
}

function getLikeShareCount(a, d, c, b) {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "http://api.facebook.com/restserver.php?method=links.getStats&format=JSON&urls=" + a,
        success: function(e) {
            //console.log(e);
            if ($(d) != null) {
                $(d).html(e[0].share_count)
            }
            if ($(c) != null) {
                $(c).html(e[0].like_count)
            }
            if ($(b) != null) {
                $(b).html(e[0].total_count)
            }
        }
    })
}
// Move video top left
$(document).ready(function() {
    // $('.video-stick').appear();
    players = new Array();
    $(document.body).on('appear', '.video-stick', function(e, $affected) {
        $(this).find('.dis-video-stick').removeClass('psfix psfix2').removeAttr('style');
    });
    $(document.body).on('disappear', '.video-stick', function(e, $affected) {
        var w_offset = $('.video-stick')[0].getBoundingClientRect().top;
        if (w_offset < 0) {
            $(this).find('.dis-video-stick').addClass('psfix').animate({
                left: '10px'
            }, 500);
        } else {
            $(this).find('.dis-video-stick').addClass('psfix2').animate({
                top: '10px',
                left: '10px'
            }, 500);
        }
    });

});

$(window).load(function(){
  $(".youtube").each(function() {
      var $this = $(this);
      var link = "https://www.youtube.com/embed/" + this.id;
      var iframe = document.createElement('iframe');
      iframe.frameBorder=0;
      iframe.setAttribute("src", link);
      var $parent = $this.parent();
      $parent.append($(iframe));
      $this.remove();
  });
});

function onPlayerStateChange(event) {
    if (event.data == YT.PlayerState.PLAYING) {
        //console.log(event);
        var temp = event.target.a.src;
        var idtemp = event.target.a.id;
        var tempPlayers = $("iframe.yt_players");
        for (var i = 0; i < players.length; i++) {
            if (players[i].a.src != temp) players[i].stopVideo();
        }
        var id_move = idtemp.substring(3);
        $('.youtube').find('.dis-video-stick').removeClass('psfix psfix2').removeAttr('style');
        $('.youtube').removeClass("video-stick");
        $('#' + id_move).addClass("video-stick");
    }
}
// END Move video top left
$('.video-more-popup li .thumb_video').click(function() {
    var id = $(this).closest('.remodal-is-opened').find('.info-video-group').attr('data-post-id');
    $('.info-video-group[data-post-id="' + id + '"] .video-more-popup li .active').removeClass('active');
    $(this).addClass("active");
});
$(".list_video_bottom_home li.video-popup").hover(function() {
    $(this).toggleClass("active-video");
});
$(document).on('click', '.thumb_video', function() {
    var link = $(this).attr('data-video');
    var tpl = $('#modal-video').html();
    var id = $(this).closest('.remodal-is-opened').find('.video-view').attr('data-post-id');
    tpl = tpl.replace(/{video}/g, link);
    $('.video-view[data-post-id="' + id + '"]').html('');
    $('.video-view[data-post-id="' + id + '"]').html(tpl);
    $('.video-view[data-post-id="' + id + '"] video').each(function(index, el) {
        init_single_video(index, el, true);
    });
});
// $(document).on('opened', '.remodal', function () {
//     var id = $('.remodal-is-opened .video-view').attr('data-post-id');
//     var number = $('.video-view[data-post-id="'+id+'"]').attr('data-video-count');
//     for (var i = 0; i < players.length; i++) {
//         if (i == number) { 
//             players[i].play();
//         }
//    }
// });
$(document).on('closed', '.remodal', function() {
    for (var i = 0; i < players.length; i++) {
        players[i].pause();
    }
    $(".video-view iframe").attr("src", null);
});
$(document).ready(function () {
    $('#open_sub').click(function () {
        $('body').toggleClass("active");
    });
});