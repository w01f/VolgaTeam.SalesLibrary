(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ShortcutsHistoryManager = function ()
	{
		this.init = function ()
		{
			window.onpopstate = function (event)
			{
				if (event.state && event.state.isShortcut)
				{
					$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData($(event.state.shortcutData), event.state.customParameters);
					event.preventDefault();
				}
			};
		};

		this.pushState = function (data, customParameters)
		{
			if (customParameters && customParameters.pushHistory)
			{
				customParameters.pushHistory = false;

				var title = data != undefined ? $.parseJSON(data.find('.activity-data').text()) : undefined;
				window.history.pushState(
					{
						isShortcut: true,
						shortcutData: data != undefined ? ('<div class="service-data">' + data.html() + '</div>') : undefined,
						customParameters: customParameters
					},
					title
				)
			}
		};
	};
	$.SalesPortal.ShortcutsHistory = new ShortcutsHistoryManager();
})(jQuery);