(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var openShortcut = function ()
	{
		$.SalesPortal.ShortcutsHistory.init();

		var defaultShortcutData = $('.default-shortcut-data');
		if (defaultShortcutData.length > 0)
		{
			$.SalesPortal.ShortcutsManager.openShortcut(defaultShortcutData, {pushHistory: true});
		}
	};

	$(document).ready(function ()
	{
		$.SalesPortal.Content.init();
		openShortcut();
	});
})(jQuery);
