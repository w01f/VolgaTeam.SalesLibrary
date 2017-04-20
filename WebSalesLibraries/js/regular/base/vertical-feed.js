(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.VerticalFeed = function (parameters)
	{
		var containerId = parameters.containerId;
		var viewSettings = new FeedViewSettings(parameters.settings);
		var feedContainer = undefined;

		this.init = function ()
		{
			feedContainer = $('#vertical-feed-' + containerId);
			var feedItemsList = feedContainer.find('.feed-items-list');
			feedItemsList.bootstrapNews({
				newsPerPage: viewSettings.itemsCount,
				autoplay: viewSettings.autoPlay,
				pauseOnHover: true,
				direction: viewSettings.direction,
				newsTickerInterval: viewSettings.tickerInterval,
				onReset: function ()
				{
					$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(feedItemsList);
					feedItemsList.find('.library-link-block').off('click').on('click', function (e)
					{
						e.stopPropagation();
						var linkId = $(this).find('.service-data .link-id').text();
						$.SalesPortal.LinkManager.requestViewDialog({
							linkId: linkId,
							isQuickSite: false
						});
					});
				}
			});
			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(feedItemsList);
			feedItemsList.find('.library-link-block').off('click').on('click', function (e)
			{
				e.stopPropagation();
				var linkId = $(this).find('.service-data .link-id').text();
				$.SalesPortal.LinkManager.requestViewDialog({
					linkId: linkId,
					isQuickSite: false
				});
			});
		};
	};


	var FeedViewSettings = function (data)
	{
		this.title = undefined;
		this.itemsCount = undefined;
		this.autoPlay = undefined;
		this.direction = undefined;
		this.tickerInterval = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);