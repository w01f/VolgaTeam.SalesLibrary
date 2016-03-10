(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.VideoViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.VideoViewerData(parameters.data);
		var dialogContent = undefined;
		var player = undefined;

		this.show = function ()
		{
			if (viewerData.forcePreview == true)
			{
				showVideoModal();
				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Preview Modal',
					data: {
						Name: viewerData.name,
						File: viewerData.fileName,
						'Original Format': viewerData.format
					}
				});
			}
			else
				$.fancybox({
					content: parameters.content,
					title: viewerData.name,
					autoSize: true,
					openEffect: 'none',
					closeEffect: 'none',
					afterShow: function ()
					{
						dialogContent = $('.fancybox-wrap');

						var formLogger = new $.SalesPortal.FormLogger();
						formLogger.init({
							logObject: {
								name: viewerData.name,
								fileName: viewerData.fileName,
								format: viewerData.format
							},
							formContent: dialogContent
						});

						var fullScreenMode = dialogContent.find('.link-viewer').hasClass('eo') ? 'eo' : 'regular';

						dialogContent.find('.tab-above-header').first().addClass('active');
						dialogContent.find('#link-viewer-body-tabs li').first().addClass('active');
						dialogContent.find('.tab-content .tab-pane').first().addClass('active');

						dialogContent.find('#link-viewer-body-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
						{
							dialogContent.find('.tab-above-header').removeClass('active');
							var tabTag = e.target.attributes['href'].value.replace("#link-viewer-tab-", "");
							dialogContent.find('#tab-above-header-' + tabTag).addClass('active');
						});

						dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

						dialogContent.find('.download-file').off('click.preview').on('click.preview', downloadFile);
						dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
						dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);
						dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

						dialogContent.find('.open-video-modal').off('click.preview').on('click.preview', showVideoModal);
						dialogContent.find('.open-video-fullscreen-regular').off('click.preview').on('click.preview', showVideoFullScreenForEO);
						dialogContent.find('.open-video-fullscreen-mobile').off('click.preview').on('click.preview', showVideoFullScreen);

						VideoJS.players = {};
						player = _V_("video-player", {
								controls: true,
								autoplay: false,
								preload: 'auto',
								poster: viewerData.thumbImageSrc
							},
							function ()
							{
							});
						player.src([
							{
								src: viewerData.mp4Src.href,
								href: viewerData.mp4Src.href,
								title: viewerData.fileName,
								type: viewerData.mp4Src.type
							}
						]);
						if (fullScreenMode == 'eo')
						{
							$('.vjs-fullscreen-control').css({ 'display': 'none' });
							$('.vjs-volume-control').css({ 'margin-right': '20px' });
						}
						player.addEvent('play', function ()
						{
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Play',
								data: {
									Name: viewerData.name,
									File: viewerData.fileName,
									'Original Format': viewerData.format
								}
							});
						});

						new $.SalesPortal.RateManager().init(
							{
								id: viewerData.linkId,
								name: viewerData.name,
								file: viewerData.fileName,
								format: viewerData.format
							},
							dialogContent.find('#user-link-rate-container'),
							viewerData.rateData);

						new $.SalesPortal.PreviewEmailer(viewerData);
					}
				});
		};

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.mp4Src.title,
				path: viewerData.mp4Src.path
			});
		};

		var addToQuickSite = function ()
		{
			$.fancybox.close();
			$.SalesPortal.QBuilder.LinkCart.addLinks([viewerData.linkId]);
		};

		var addToFavorites = function ()
		{
			$.SalesPortal.LinkManager.addToFavorites(
				viewerData.linkId,
				viewerData.name,
				viewerData.fileName,
				viewerData.format);
		};

		var showVideoModal = function ()
		{
			$.fancybox.close();
			var viewerBar = new $.SalesPortal.ViewerBar();
			$.SalesPortal.LinkManager.playVideo([
				{
					src: viewerData.mp4Src.href,
					href: viewerData.mp4Src.href,
					title: viewerData.fileName,
					type: viewerData.mp4Src.type,
					swf: viewerData.playerSrc
				}
			], viewerBar);
			viewerBar.show({
				returnCallback: function ()
				{
					$.fancybox.close();
					viewerData.forcePreview = false;
					parameters.data = viewerData;
					new $.SalesPortal.VideoViewer(parameters).show();
				}
			});
		};

		var showVideoFullScreen = function ()
		{
			player.requestFullScreen();
		};

		var showVideoFullScreenForEO = function ()
		{
			VideoJS.players = {};
			$.fancybox({
				content: $('<video id="video-player" class="video-js vjs-default-skin" height = "' + ($(window).height() - 100) + '" width="' + ($(window).width() - 100) + '"></video>'),
				openEffect: 'none',
				closeEffect: 'none',
				autoSize: true,
				helpers: {
					title: false
				}
			});
			_V_.options.flash.swf = viewerData.playerSrc;
			var myPlayer = _V_("video-player", {
					controls: true,
					autoplay: false,
					preload: 'auto',
					poster: viewerData.thumbImageSrc
				},
				function ()
				{
				});
			myPlayer.src([
				{
					src: viewerData.mp4Src.href,
					href: viewerData.mp4Src.href,
					title: viewerData.fileName,
					type: viewerData.mp4Src.type
				}
			]);
			$('.vjs-fullscreen-control').css({ 'display': 'none' });
			$('.vjs-volume-control').css({ 'margin-right': '20px' });
			$('.fancybox-inner').css({ 'overflow': 'hidden' });
			myPlayer.play();
		};

		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'download':
					$.SalesPortal.LinkManager.downloadFile({
						name: viewerData.mp4Src.title,
						path: viewerData.mp4Src.path
					});
					break;
				case 'quicksite':
					addToQuickSite();
					break;
				case 'favorites':
					addToFavorites();
					break;
			}
		};
	};
})(jQuery);
