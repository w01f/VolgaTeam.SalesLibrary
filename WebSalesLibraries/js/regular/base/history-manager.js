(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var HistoryManager = function ()
	{
		const HistoryShortcutItem = 1;
		const HistoryPreviewItem = 2;

		this.init = function ()
		{
			window.onpopstate = function (event)
			{
				if (event.state && event.state.itemType)
				{
					switch (event.state.itemType)
					{
						case HistoryShortcutItem:
							$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData($(event.state.shortcutData), event.state.customParameters);
							event.preventDefault();
							break;
						case HistoryPreviewItem:
							$.SalesPortal.LinkManager.requestViewDialog(event.state.dialogData);
							event.preventDefault();
							break;
					}
				}
			};
		};

		this.pushPreviewLink = function (linkName, dialogData)
		{
			dialogData.doNotPushHistory = true;
			window.history.pushState(
				{
					itemType: HistoryPreviewItem,
					dialogData: dialogData
				},
				linkName
			)
		};

		this.pushShortcut = function (data, customParameters)
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
						itemType: HistoryShortcutItem,
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
						itemType: HistoryShortcutItem,
						shortcutData: data != undefined ? ('<div class="service-data">' + data.html() + '</div>') : undefined,
						customParameters: customParameters
					},
					title
				)
			}
		};
	};
	$.SalesPortal.HistoryManager = new HistoryManager();
})(jQuery);