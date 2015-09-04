(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsLibraryWindow = function (data)
	{
		var shortcutData = data;
		var contentContainer = $('#shortcut-link-page-' + shortcutData.options.linkId).find('.content-data');

		this.init = function ()
		{
			contentContainer.find('div[data-role=collapsible]').collapsible();
			var pageId = "#shortcut-link-page-" + shortcutData.options.linkId;
			$.SalesPortal.Wallbin.initFolderLinks(contentContainer, pageId);
			$.mobile.pageContainer.pagecontainer("change", pageId, {
				transition: "slidefade"
			});
		};
	};
})(jQuery);