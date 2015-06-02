(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ImageViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.SimpleViewerData($.parseJSON(parameters.data));
		var dialogContent = undefined;

		this.show = function ()
		{
			$.fancybox({
				content: parameters.content,
				title: viewerData.name,
				autoSize: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					dialogContent = $('.fancybox-wrap');

					dialogContent.find('#link-viewer-body-tabs a[data-toggle="tab"]').on('shown.bs.tab', function (e)
					{
						dialogContent.find('.tab-above-header').removeClass('active');
						var tabTag = e.target.attributes['href'].value.replace("#link-viewer-tab-", "");
						dialogContent.find('#tab-above-header-' + tabTag).addClass('active');
					});

					dialogContent.find('.file-size').html('(' + viewerData.fileSize + ')');

					dialogContent.find('.download-file').off('click').on('click', downloadFile);
					dialogContent.find('.add-quicksite').off('click').on('click', addToQuickSite);
					dialogContent.find('.add-favorites').off('click').on('click', addToFavorites);

					dialogContent.find('.action-container .action').off('click').on('click', processSaveAction);

					new $.SalesPortal.RateManager().init(
						viewerData.linkId,
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
