(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.FileViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			if (viewerData.config.isEOBrowser == true && viewerData.config.forceEOOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				download();
			else
			{
				if (parameters.viewContainer == undefined)
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

			dialogContent = $('.link-viewer');

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

			dialogContent.find('.action-container .action').off('click.preview').on('click.preview', processSaveAction);

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

			dialogContent.find('.btn').off('mouseup.preview').on('mouseup.preview', function ()
			{
				$(this).blur();
			});

			setDialogTitle(viewerData.name);
		};

		this.afterClose = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.releaseLinkData();
			releaseDialogTitle();
		};

		var initDialogTitle = function ()
		{
			if (viewerData.totalViews > 0)
				$('.fancybox-title').addClass('link-viewer-title');
		};

		var releaseDialogTitle = function ()
		{
			$('.fancybox-title').removeClass('link-viewer-title');
		};

		var setDialogTitle = function (title)
		{
			if (viewerData.totalViews > 0)
				$('.fancybox-title .child').html('<div class="row"><div class="col col-xs-10 text-left">' + title + '</div><div class="col col-xs-2 text-right">views (' + viewerData.totalViews + ')</div></div>');
			else
				$('.fancybox-title .child').html(title);
		};

		var download = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
			});
		};

		var playAudio = function ()
		{
			$.fancybox.close();
			$.fancybox({
				title: viewerData.name,
				content: '<audio controls autoplay style="margin-top: 40px;"><source src="' + viewerData.url + '" type="audio/mpeg"/></audio>',
				width: 250,
				openEffect: 'none',
				closeEffect: 'none'
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
				case 'download':
					download();
					break;
				case 'download-pdf':
					$.SalesPortal.LinkManager.openFile(viewerData.documentInPdf.link);
					break;
				case 'play':
					playAudio();
					break;
				case 'open':
					$.fancybox.close();
					$.SalesPortal.LinkManager.openFile(viewerData.url);
					break;
				case 'view':
					viewExcel();
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
