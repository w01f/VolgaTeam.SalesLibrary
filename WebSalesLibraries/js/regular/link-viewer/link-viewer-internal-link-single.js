(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};

	var openLink = function ()
	{
		var linkId = $('.link-viewer-data .link-id').text();

	};
	$(document).ready(function ()
	{
		$.SalesPortal.MainMenu.init();
		$.SalesPortal.Content.init();
		$.SalesPortal.HistoryManager.init();

		var singleLink = $('.single-link');
		singleLink.on('click', function (e)
		{
			e.preventDefault();
			var linkId = $(this).find('.link-viewer-data .link-id').text();
			$.SalesPortal.LinkManager.requestViewDialog({
				linkId: linkId,
				isQuickSite: false
			});
		});
		singleLink.click();
	});
})(jQuery);