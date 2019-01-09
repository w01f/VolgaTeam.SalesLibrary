(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.VideoGroup = function (parameters) {
		var videoGroupId = parameters.containerId;
		var videoGroup = undefined;
		var videoGroupData = undefined;

		this.init = function () {
			videoGroup = $('#video-group-' + videoGroupId);
			videoGroupData = $.parseJSON(videoGroup.find('.video-group-data').text());
			initVideoGroup();
			loadVideoGroupState(true);
		};

		var initVideoGroup = function () {
			videoGroup.find('.video-item').off('click').on('click', function () {
				var videoItem = $(this);
				if (videoItem.hasClass('suspended'))
				{
					var modalDialog = new $.SalesPortal.ModalDialog({
						title: 'Video',
						description: "You can't play this video yet.",
						buttons: [
							{
								tag: 'ok',
								title: 'Close',
								width: 160,
								clickHandler: function () {
									modalDialog.close();
								}
							}
						]
					});
					modalDialog.show();
				}
				else
				{
					var videoItemIndex = parseInt(videoItem.data('index'));
					var videoSource = videoItem.data('source');
					var videoItemStateEncoded = videoItem.find('.video-item-data').text();
					var videoItemState = videoItemStateEncoded != '' ?
						$.parseJSON(videoItem.find('.video-item-data').text()) :
						null;

					var playVideo = function (startPosition) {
						$.fancybox({
							title: 'Video',
							content: $('<video controls autoplay preload="auto"' +
								' id="video-player"' +
								' height = "480" width="680">' +
								'<source src="' + videoSource + '" type="video/mp4">' +
								'</video>'),
							openEffect: 'none',
							closeEffect: 'none',
							afterShow: function () {
								$('.fancybox-wrap').addClass('content-boxed');
								var videoPlayer = document.getElementById('video-player');
								videoPlayer.currentTime = startPosition;
							},
							beforeClose: function () {
								var videoPlayer = document.getElementById('video-player');
								videoPlayer.pause();
								updateVideoItemState(
									videoItemIndex,
									(videoItemState != null && videoItemState.fullyViewed == true) || videoPlayer.ended,
									videoPlayer.ended ? 0 : videoPlayer.currentTime);
								$('#video-player').remove();
							}
						});
					};

					var lastViewPosition = videoItemState ? parseFloat(videoItemState.lastViewPosition) : 0;
					if (lastViewPosition > 0)
					{
						var playbackRequestDialog = new $.SalesPortal.ModalDialog({
							title: 'Video',
							description: "Do you want to resume where you left off?",
							width: 500,
							buttons: [
								{
									tag: 'ok',
									title: 'Resume',
									width: 200,
									clickHandler: function () {
										playbackRequestDialog.close();
										playVideo(lastViewPosition);
									}
								},
								{
									tag: 'cancel',
									title: 'Start from beginning ',
									width: 200,
									clickHandler: function () {
										playbackRequestDialog.close();
										playVideo(0);
									}
								}
							]
						});
						playbackRequestDialog.show();
					}
					else
						playVideo(0);


				}
			});
		};

		var loadVideoGroupState = function (showOverlay) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "videoGroup/getState",
				data: {
					groupId: videoGroupData.groupId,
					shortcutId: videoGroupData.shortcutId
				},
				beforeSend: function () {
					if(showOverlay)
						$.SalesPortal.Overlay.show();
				},
				success: function (videoGroupState) {
					$.SalesPortal.Overlay.hide();

					var suspendNextItem = false;
					var videoItems = videoGroup.find('.video-item');
					$.each(videoItems, function () {
						var videoItem = $(this);
						var img = videoItem.find('img');

						if (suspendNextItem)
						{
							if (!videoItem.hasClass('suspended'))
								videoItem.addClass('suspended');
						}
						else
							videoItem.removeClass('suspended');


						var videoItemIndex = videoItem.data('index');
						if (videoItemIndex in videoGroupState.itemStates)
						{
							var videoItemState = videoGroupState.itemStates[videoItemIndex];

							if (videoItemState.fullyViewed == true)
								img.prop('src', videoItem.data('placeholder-complete'));
							else if (parseFloat(videoItemState.lastViewPosition) > 0)
								img.prop('src', videoItem.data('placeholder-in-progress'));
							else if (suspendNextItem)
								img.prop('src', videoItem.data('placeholder-not-ready'));
							else
								img.prop('src', videoItem.data('placeholder-ready'));

							videoItem.find('.service-data').text($.toJSON(videoItemState));
							suspendNextItem = videoItemState.fullyViewed != true;
						}
						else
						{
							if (suspendNextItem)
								img.prop('src', videoItem.data('placeholder-not-ready'));
							else
								img.prop('src', videoItem.data('placeholder-ready'));
							suspendNextItem = true;
						}
					});
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'json'
			});
		};

		var updateVideoItemState = function (itemIndex, isFullyViewed, currentPosition) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "videoGroup/updateItemState",
				data: {
					groupId: videoGroupData.groupId,
					shortcutId: videoGroupData.shortcutId,
					itemState: {
						index: itemIndex,
						fullyViewed: isFullyViewed,
						lastViewPosition: currentPosition
					}
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				success: function () {
					loadVideoGroupState(false);
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'json'
			});
		};
	};
})(jQuery);