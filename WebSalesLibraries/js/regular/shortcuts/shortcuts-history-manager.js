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
			var pushHistory = data.find('.push-history').length > 0;
			if (pushHistory && customParameters && customParameters.pushHistory)
			{
				var currentSate = window.history.state;
				if (currentSate && currentSate.isShortcut)
				{
					var newParameters = currentSate.customParameters;
					newParameters.scrollPosition = $.SalesPortal.Content.getContentObject().scrollTop();
					var newSate = {
						isShortcut: true,
						shortcutData: currentSate.shortcutData,
						customParameters: newParameters
					};
					var prevTitle = currentSate.shortcutData != undefined ? $.parseJSON($(currentSate.shortcutData).find('.activity-data').text()) : undefined;
					if (prevTitle != undefined)
						window.history.replaceState(newSate, prevTitle);
				}

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