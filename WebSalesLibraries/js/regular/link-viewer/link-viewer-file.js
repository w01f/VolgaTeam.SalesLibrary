(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.FileViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			if (viewerData.config.isEOBrowser == true && viewerData.config.forceOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				download();
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
					}
				});
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
