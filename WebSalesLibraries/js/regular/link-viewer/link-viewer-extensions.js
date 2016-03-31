(function ($)
{
	$.SalesPortal = $.SalesPortal || { };
	var LinkViewerExtensions = function ()
	{
		this.activate = function ()
		{
			try
			{
				SalesLibraryExtensions_activate();
			}
			catch(err) {}
		};

		this.sendLinkData = function (viewerData)
		{
			try
			{
				var format = viewerData.format;
				var fileName = viewerData.fileName;
				var originalUrl = (window.BaseUrl + viewerData.url).replace(/\/\/+/g, '/');
				var parts = [];
				if (viewerData.pages !== undefined)
					$.each(viewerData.pages, function (itemIndex, item)
					{
						parts.push((window.BaseUrl + item.href).replace(/\/\/+/g, '/'));
					});
				else if (viewerData.mp4Src !== undefined)
					parts.push((window.BaseUrl + viewerData.mp4Src.href).replace(/\/\/+/g, '/'));

				var slideWidth = 0;
				if (viewerData.slideWidth !== undefined)
					slideWidth = viewerData.slideWidth;
				var slideHeight = 0;
				if (viewerData.slideHeight !== undefined)
					slideHeight = viewerData.slideHeight;

				SalesLibraryExtensions_sendLinkData(format, fileName, originalUrl, parts, slideWidth, slideHeight);
			}
			catch(err) {}
		};

		this.releaseLinkData = function ()
		{
			try
			{
				SalesLibraryExtensions_releaseLinkData();
			}
			catch(err) {}
		};

		this.switchDocumentPage = function (pageIndex)
		{
			try
			{
				SalesLibraryExtensions_switchPage(pageIndex);
			}
			catch(err) {}
		};
	};
	$.SalesPortal.SalesLibraryExtensions = new LinkViewerExtensions();
})(jQuery);
