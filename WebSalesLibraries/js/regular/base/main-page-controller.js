(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};

	var openDefaultShortcut = function () {
		var defaultShortcutData = $('.default-shortcut-data');
		if (defaultShortcutData.length > 0)
		{
			var customParametersEncoded = defaultShortcutData.find('.custom-parameters').text();
			var customParameters = {
				pushHistory: true
			};
			if (customParametersEncoded !== undefined && customParametersEncoded != '')
			{
				customParameters = $.parseJSON(customParametersEncoded);
				customParameters.pushHistory = true;
			}
			$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(defaultShortcutData, customParameters);
		}
	};
	$(document).ready(function () {
		$.SalesPortal.MainMenu.init();
		$.SalesPortal.Content.init();
		$.SalesPortal.HistoryManager.init();
		$.SalesPortal.ScreenManager.init();
		openDefaultShortcut();
	});
})(jQuery);
