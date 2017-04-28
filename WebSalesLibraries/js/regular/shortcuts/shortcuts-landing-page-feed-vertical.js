(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.VerticalFeed = function (parameters)
	{
		var containerId = parameters.containerId;
		var querySettings = parameters.querySettings !== undefined ? new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings) : null;
		var viewSettings = new FeedViewSettings(parameters.viewSettings);
		var feedContainer = undefined;

		this.init = function ()
		{
			feedContainer = $('#vertical-feed-' + containerId);

			initSlider();

			if (viewSettings.feedType !== 'news')
			{
				initToggles();
				setTimeout(function ()
				{
					reloadLinks(false);
				}, 900000);
			}
		};

		var reloadLinks = function (showProgress)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "landingPage/getVerticalLinkFeedItems",
				data: {
					feedId: containerId,
					feedType: querySettings.feedType,
					querySettings: querySettings,
					viewSettings: viewSettings
				},
				beforeSend: function ()
				{
					if (showProgress)
						$.SalesPortal.Overlay.show();
				},
				success: function (msg)
				{
					$.SalesPortal.Overlay.hide();

					feedContainer.find('.feed-items-list-container').html(msg);

					initSlider();
				},
				error: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var initToggles = function ()
		{
			feedContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function ()
			{
				var dateRangeTitle = $(this).text();
				feedContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				reloadLinks(true);
			});

			feedContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormats = [];
				$.each(feedContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					querySettings.linkFormats.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			})
		};


		var initSlider = function ()
		{
			feedContainer.find('.panel-footer').html('');
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
		this.feedType = undefined;
		this.title = undefined;
		this.icon = undefined;
		this.itemsCount = undefined;
		this.autoPlay = undefined;
		this.direction = undefined;
		this.tickerInterval = undefined;
		this.hideHeader = undefined;
		this.hideFooter = undefined;
		this.style = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;
		this.controlsStyle = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);