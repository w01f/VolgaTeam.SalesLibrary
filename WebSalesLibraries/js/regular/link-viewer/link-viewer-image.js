(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ImageViewer = function (parameters)
	{
		var that = this;
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			if (viewerData.config.isEOBrowser == true && viewerData.config.forceEOOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				downloadFile();
			else if (viewerData.config.forceWebOpen == true)
				open();
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

			dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

			dialogContent.find('.download-file').off('click.preview').on('click.preview', downloadFile);
			dialogContent.find('.open-gallery-modal').off('click.preview').on('click.preview', showGalleryModal);
			dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
			dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);

			dialogContent.find('.open-quick-link').off('click.preview').on('click.preview', openQuickLink);

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

			setDialogTitle(viewerData.name)
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

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
			});
		};

		var open = function ()
		{
			$.SalesPortal.LinkManager.openFile(viewerData.url, "_self");
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

		var showGalleryModal = function ()
		{
			$.fancybox.close();
			$.fancybox({
				title: viewerData.name,
				content: '<img src="' + viewerData.url + '">',
				openEffect: 'none',
				closeEffect: 'none'
			});
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
					downloadFile();
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
