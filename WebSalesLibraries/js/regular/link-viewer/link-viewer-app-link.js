(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.AppLinkViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.AppLinkViewerData(parameters.data);
		var dialogContent = undefined;

		this.show = function ()
		{
			$.SalesPortal.SalesLibraryExtensions.sendLinkData(viewerData);
		};
	};
})(jQuery);
