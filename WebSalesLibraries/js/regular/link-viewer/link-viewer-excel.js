(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ExcelViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
		var dialogContent = undefined;
		var embeddedViewer = false;

		this.show = function ()
		{
			embeddedViewer = parameters.viewContainer !== undefined;

			if (viewerData.config.isEOBrowser == true && viewerData.config.forceEOOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				download();
			else if (viewerData.config.forceOpenGallery == true)
				viewExcel();
			else if (viewerData.config.forceOpenOneDrive == true)
				openOneDrive();
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

			dialogContent.find('.tab-above-header').first().addClass('active');
			dialogContent.find('#link-viewer-body-tabs li').first().addClass('active');
			dialogContent.find('.tab-content .tab-pane').first().addClass('active');

			dialogContent.find('#link-viewer-body-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
			{
				dialogContent.find('.tab-above-header').removeClass('active');
				var tabTag = e.target.attributes['href'].value.replace("#link-viewer-tab-", "");
				dialogContent.find('#tab-above-header-' + tabTag).addClass('active');
			});

			dialogContent.find('.one-drive-link-copy').off('click.preview').on('click.preview', function (e) {
				$.SalesPortal.LinkManager.copyTextToClipboard($(this).prop('href'));
				e.preventDefault();
			});

			dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

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

			new $.SalesPortal.PreviewEmailer(viewerData);

			dialogContent.find('.btn').off('mouseup.preview').on('mouseup.preview', function ()
			{
				$(this).blur();
			});

			setDialogTitle(viewerData.name);
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

		var openOneDrive = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.oneDriveUrl, "_blank");
		};

		var download = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
			});
		};

		var viewExcel = function ()
		{
			var content = '<style>' +
				'.link-viewer {width: 100%; height: 100%;}' +
				'.iframe-scroll-area {width: 100%; height: 100%; overflow: auto;}' +
				'.link-viewer .iframe-container {width: 100%; height: 100%; -ms-zoom: 1;-webkit-transform: scale(1);-webkit-transform-origin: 0 0;}' +
				'.link-viewer .iframe-container::after {background: #f9f9f9 none repeat scroll 0 0;bottom: 0;content: "";height: 30px;left: 0;position: absolute;width: 100%;}' +
				'#excel-view-frame {width: 100%; height: 100%;}' +
				'</style>' +
				'<table class="link-viewer">' +
				'<tr style="height: 100%"><td><div class="iframe-scroll-area"><div class="iframe-container"><iframe id="excel-view-frame" src="https://view.officeapps.live.com/op/embed.aspx?src=' + viewerData.url + '"></iframe></div></div></td></tr>' +
				'<tr style="height: 64px;"><td>' +
				'<div class="row">' +
				'<div class="col-xs-10"></div>' +
				'<div class="col-xs-1 text-center"><div class="image-button excel-view-zoom-in"><img src="' + window.BaseUrl + 'images/preview/gallery/button-zoom-in.png"></div></div>' +
				'<div class="col-xs-1 text-center"><div class="image-button excel-view-zoom-out"><img src="' + window.BaseUrl + 'images/preview/gallery/button-zoom-out.png"></div></div>' +
				'</div>' +
				'</td></tr>' +
				'</table>';
			$.fancybox.close();
			$.fancybox({
				content: content,
				width: $(window).width() - 100,
				height: $(window).height() - 100,
				autoSize: false,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					var excelViewContent = $('.link-viewer');

					var currentZoomFactor = 1;
					var updateZoom = function ()
					{
						excelViewContent.find('.iframe-container').css({
							'-ms-zoom': currentZoomFactor,
							'-webkit-transform': 'scale(' + currentZoomFactor + ')'
						});
					};

					excelViewContent.find(".excel-view-zoom-in").off('click').on('click', function ()
					{
						currentZoomFactor = currentZoomFactor + 0.1;
						updateZoom();
					});
					excelViewContent.find(".excel-view-zoom-out").off('click').on('click', function ()
					{
						if (currentZoomFactor > 0.1)
							currentZoomFactor = currentZoomFactor - 0.1;
						updateZoom();
					});

					$(window).off('resize.excel-view').on('resize.excel-view', function ()
					{
						$('.fancybox-wrap').css({
							'width': $(window).width() - 70 + 'px'
						});
						$('.fancybox-inner').css({
							'width': $(window).width() - 100 + 'px',
							'height': $(window).height() - 100 + 'px'
						});
						if ($.fancybox.isOpened)
						{
							if ($.fancybox.current)
							{
								$.fancybox.current.width = $(window).width() - 100;
								$.fancybox.current.height = $(window).height() - 100;
							}
						}
						$.fancybox.reposition();
					});
				},
				afterClose: function ()
				{
					$(window).off('resize.excel-view');
				}
			})
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

		var processSaveAction = function ()
		{
			var tag = $(this).find('.service-data .tag').text();
			switch (tag)
			{
				case 'view':
					viewExcel();
					break;
				case 'download':
					download();
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
