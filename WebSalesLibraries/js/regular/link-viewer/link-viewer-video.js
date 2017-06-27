(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.VideoViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.VideoViewerData(parameters.data);
		var dialogContent = undefined;
		var player = undefined;
		var allowFullScreenMode = undefined;
		var embeddedViewer = false;

		this.show = function ()
		{
			embeddedViewer = parameters.viewContainer !== undefined;

			if (viewerData.forcePreview === true)
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
			{
				if (parameters.viewContainer === undefined)
					$.fancybox({
						content: parameters.content,
						title: viewerData.name,
						autoSize: true,
						openEffect: 'none',
						closeEffect: 'none',
						afterShow: that.afterShow,
						afterClose: that.afterClose
					});
				else
				{
					parameters.viewContainer.html(parameters.content);
					parameters.viewContainer = undefined;
					that.afterShow();
					return that;
				}
			}
		};

		this.afterShow = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);

			initDialogTitle();

			dialogContent = embeddedViewer ? $('.link-viewer-container .link-viewer') : $('.link-viewer');

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

			dialogContent.find('.download-mp4-file').off('click.preview').on('click.preview', downloadMp4File);
			dialogContent.find('.download-original-file').off('click.preview').on('click.preview', downloadOriginalFile);
			dialogContent.find('.download-link-bundle').off('click.preview').on('click.preview', downloadLinkBundle);
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
				dialogContent.find('.user-link-rate-container'),
				viewerData.rateData,
				function (newRateData)
				{
					viewerData.rateData = newRateData;
				}
			);

			new $.SalesPortal.PreviewEmailer(viewerData, false);
			new $.SalesPortal.PreviewEmailer(viewerData, true);

			dialogContent.find('.btn').off('mouseup.preview').on('mouseup.preview', function ()
			{
				$(this).blur();
			});

			setDialogTitle(viewerData.name)
		};

		this.afterClose = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
		};

		var initDialogTitle = function ()
		{
			var fancyboxTitle = $('.fancybox-title');
			if (viewerData.totalViews > 0 && !fancyboxTitle.hasClass('link-viewer-title'))
				fancyboxTitle.addClass('link-viewer-title');
		};

		var setDialogTitle = function (title)
		{
			if (!embeddedViewer)
			{
				if (viewerData.totalViews > 0)
					$('.fancybox-title .child').html('<div class="row"><div class="col col-xs-10 text-left">' + title + '</div><div class="col col-xs-2 text-right">views (' + viewerData.totalViews + ')</div></div>');
				else
					$('.fancybox-title .child').html(title);
			}
			else
			{
				$('.fancybox-title .child .text-left').html(title);
			}
		};

		var downloadMp4File = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.mp4Src.title,
				path: viewerData.mp4Src.path
			});
		};

		var downloadOriginalFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
			});
		};

		var downloadLinkBundle = function ()
		{
			$.SalesPortal.ZipDownloadFilesHelper.processLinkBundle(viewerData.linkBundleId);
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
					var parentPreviewParameters = parameters.parentPreviewParameters;
					$.SalesPortal.LinkManager.openViewerDialog(parentPreviewParameters);
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
