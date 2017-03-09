(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var LinkManager = function ()
	{
		this.requestViewDialog = function (linkId, parentPageData, isQuickSite)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getViewDialog",
				data: {
					linkId: linkId,
					isQuickSite: isQuickSite
				},
				beforeSend: function ()
				{
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				complete: function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				success: function (parameters)
				{
					switch (parameters.format)
					{
						case 'document':
							new $.SalesPortal.DocumentViewer(parameters, parentPageData).show();
							break;
						case 'link bundle':
							new $.SalesPortal.LinkBundleViewer(parameters, parentPageData).show();
							break;
						default :
							new $.SalesPortal.FileViewer(parameters, parentPageData).show();
							break;
					}
				},
				async: true,
				dataType: 'json'
			});
		};

		this.openFile = function (url, target)
		{
			window.open(url.replace(/&amp;/g, '%26'), target);
		};
	};
	$.SalesPortal.LinkManager = new LinkManager();
})(jQuery);

