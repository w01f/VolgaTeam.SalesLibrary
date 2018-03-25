(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};

	var openDefaultShortcut = function () {
		var defaultShortcutData = $('.default-shortcut-data');
		if (defaultShortcutData.length > 0)
			$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(defaultShortcutData, {
				pushHistory: true,
				singlePage: true
			});
	};
	$(document).ready(function () {
		$.SalesPortal.MainMenu.init();
		$.SalesPortal.Content.init();
		$.SalesPortal.HistoryManager.init();
		$.SalesPortal.ScreenManager.init();
		openDefaultShortcut();
	});
})(jQuery);
