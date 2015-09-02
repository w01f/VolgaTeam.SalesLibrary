(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsLibraryPage = function (data)
	{
		var shortcutData = data;
		var contentContainer = $('#shortcut-link-page-' + shortcutData.options.linkId).find('.content-data');

		this.init = function ()
		{
			contentContainer.find('div[data-role=collapsible]').collapsible();

			var pageId = "#shortcut-link-page-" + shortcutData.options.linkId;
			$.SalesPortal.Wallbin.initPageContent(contentContainer, pageId);
			$.mobile.changePage(pageId, {
				transition: "slidefade"
			});
		};
	};
})(jQuery);