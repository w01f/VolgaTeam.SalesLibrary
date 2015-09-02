(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var openShortcut = function ()
	{
		var defaultShortcutData = $('.default-shortcut-data');
		if (defaultShortcutData.length > 0)
			$.SalesPortal.ShortcutsManager.openShortcut(defaultShortcutData);
	};

	$(document).ready(function ()
	{
		$.SalesPortal.Content.init();
		openShortcut();
	});
})(jQuery);
