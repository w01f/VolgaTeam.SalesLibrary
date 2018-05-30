(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.FileViewer = function (parameters)
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
