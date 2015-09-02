(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var openDefaultShortcut = function ()
	{
		var defaultShortcutData = $('.default-shortcut-data');
		if (defaultShortcutData.length > 0)
			$.SalesPortal.ShortcutsManager.openShortcut(defaultShortcutData);
	};

	$(document).ready(function ()
	{
		$.SalesPortal.MainMenu.init();
		$.SalesPortal.Content.init();
		openDefaultShortcut();
	});
})(jQuery);
