(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var openLink = function ()
	{
		var linkId = $('.link-viewer-data .link-id').text();
		$.SalesPortal.LinkManager.requestViewDialog({
			linkId: linkId,
			isQuickSite: false
		});
	};
	$(document).ready(function ()
	{
		$.SalesPortal.MainMenu.init();
		$.SalesPortal.Content.init();
		$.SalesPortal.ShortcutsHistory.init();
		openLink();
	});
})(jQuery);