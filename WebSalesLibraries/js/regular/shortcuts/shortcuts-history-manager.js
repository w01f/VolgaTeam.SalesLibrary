(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsHistoryManager = function ()
	{
		this.init = function ()
		{
			window.onpopstate = function (event)
			{
				if (event.state && event.state.isShortcut)
				{
					$.SalesPortal.ShortcutsManager.openShortcut($(event.state.shortcutData), event.state.customParameters);
					event.preventDefault();
				}
			};
		};

		this.pushState = function (data, customParameters)
		{
			if (customParameters && customParameters.pushHistory)
			{
				customParameters.pushHistory = false;

				var activityData = $.parseJSON(data.find('.activity-data').text());
				window.history.pushState(
					{
						isShortcut: true,
						shortcutData: ('<div class="service-data">' + data.html() + '</div>'),
						customParameters: customParameters
					},
					activityData.title
				)
			}
		};
	};
	$.SalesPortal.ShortcutsHistory = new ShortcutsHistoryManager();
})(jQuery);