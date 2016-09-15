(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.VideoViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.VideoViewerData(parameters.data);
		var dialogContent = undefined;
		var player = undefined;
		var allowFullScreenMode = undefined;

		this.show = function ()
		{
			if (viewerData.forcePreview == true)
			{
				showVideoModal();
				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Preview Modal',
					linkId: viewerData.linkId,
					data: {
						name: viewerData.name,
						file: viewerData.fileName,
						originalFormat: viewerData.format
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
						$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);

						dialogContent = $('.fancybox-wrap');

						if (viewerData.config.enableLogging)
						{
							var formLogger = new $.SalesPortal.FormLogger();
							formLogger.init({
								logObject: {
									name: viewerData.name,
									fileName: viewerData.fileName,
									format: viewerData.format,
									linkId: viewerData.linkId
								},
								formContent: dialogContent
							});
						}

						allowFullScreenMode = dialogContent.find('.link-viewer').hasClass('eo') ? 'eo' : 'regular';

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

						dialogContent.find('.open-quick-link').off('click.preview').on('click.preview', openQuickLink);
						dialogContent.find('.open-video-modal').off('click.preview').on('click.preview', showVideoModal);
						dialogContent.find('.open-video-fullscreen-regular').off('click.preview').on('click.preview', showVideoFullScreenRegular);
						dialogContent.find('.open-video-fullscreen-mobile').off('click.preview').on('click.preview', showVideoFullScreenMobile);

						player = dialogContent.find('#video-player');
						player.on('play', function ()
						{
							$.SalesPortal.LogHelper.write({
								type: 'Link',
								subType: 'Play',
								linkId: viewerData.linkId,
								data: {
									name: viewerData.name,
									file: viewerData.fileName,
									originalFormat: viewerData.format
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

						new $.SalesPortal.PreviewEmailer(viewerData, false);
						new $.SalesPortal.PreviewEmailer(viewerData, true);
					},
					afterClose: function ()
					{
						$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
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
						type: viewerData.mp4Src.type
					}],
				viewerBar,
				function ()
				{
					$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);
				},
				function ()
				{
					$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
				}
			);
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

		var showVideoFullScreenMobile = function ()
		{
			var videoElement = player[0];
			if (videoElement.requestFullscreen)
				videoElement.requestFullscreen();
			else if (videoElement.mozRequestFullScreen)
				videoElement.mozRequestFullScreen();
			else if (videoElement.webkitRequestFullscreen)
				videoElement.webkitRequestFullscreen();
		};

		var showVideoFullScreenRegular = function ()
		{
			$.fancybox.close();
			var videoClass = allowFullScreenMode ? 'eo' : '';
			$.fancybox({
				content: $('<video controls autoplay preload="auto"' +
					' id="video-player" class="' + videoClass + '"' +
					' height = "' + ($(window).height() - 100) + '"' +
					' width="' + ($(window).width() - 100) + '"' +
					' poster="' + viewerData.thumbImageSrc + '">' +
					'<source src="' + viewerData.mp4Src.href + '" type="video/mp4">' +
					'</video>'),
				openEffect: 'none',
				closeEffect: 'none',
				autoSize: true,
				helpers: {
					title: false
				},
				afterShow: function ()
				{
					$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);
				},
				afterClose: function ()
				{
					$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
				}
			});
			$('.fancybox-inner').css({'overflow': 'hidden'});
		};

		var openQuickLink = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.quickLinkUrl);
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
