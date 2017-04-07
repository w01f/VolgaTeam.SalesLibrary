(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.NewsBlock = function (parameters)
	{
		var containerId = parameters.containerId;
		var newsBlockSettings = new NewsBlockSettings(parameters.settings);
		var newsBlockContainer = undefined;

		this.init = function ()
		{
			newsBlockContainer = $('#news-block-' + containerId);
			var newsboxList = newsBlockContainer.find('.news-block-list');
			newsboxList.bootstrapNews({
				newsPerPage: newsBlockSettings.itemsCount,
				autoplay: newsBlockSettings.autoPlay,
				pauseOnHover: true,
				direction: newsBlockSettings.direction,
				newsTickerInterval: newsBlockSettings.tickerInterval,
				onReset: function ()
				{
					$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(newsboxList);
				}
			});
			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(newsboxList);
		};
	};


	var NewsBlockSettings = function (data)
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