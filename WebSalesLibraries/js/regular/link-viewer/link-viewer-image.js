(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ImageViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.SimpleViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			if (viewerData.config.isEOBrowser == true && viewerData.config.forceOpen == true)
				$.SalesPortal.SalesLibraryExtensions.openLink(viewerData);
			else if (viewerData.config.forceDownload == true)
				downloadFile();
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

						dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

						dialogContent.find('.download-file').off('click.preview').on('click.preview', downloadFile);
						dialogContent.find('.add-quicksite').off('click.preview').on('click.preview', addToQuickSite);
						dialogContent.find('.add-favorites').off('click.preview').on('click.preview', addToFavorites);

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

						new $.SalesPortal.PreviewEmailer(viewerData);
					}
				});
		};

		var downloadFile = function ()
		{
			$.SalesPortal.LinkManager.downloadFile({
				name: viewerData.fileName,
				path: viewerData.filePath
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
