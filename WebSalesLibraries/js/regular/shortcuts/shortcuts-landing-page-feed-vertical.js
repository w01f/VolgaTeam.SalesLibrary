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
				initControls();
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

		var initControls = function ()
		{
			feedContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function ()
			{
				var dateRangeTitle = $(this).text();
				feedContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				updateDetailsHoverTip();

				reloadLinks(true);
			});

			feedContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormatsInclude = [];
				$.each(feedContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					querySettings.linkFormatsInclude.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			});

			feedContainer.find('.feed-details-button').each(function ()
			{
				var img = $(this).find('.svg');
				if (img.length > 0)
				{
					var imgClass = img.attr('class');
					var imgURL = img.attr('src');

					$.get(imgURL, function (data)
					{
						var svg = $(data).find('svg');
						if (typeof imgClass !== 'undefined')
							svg = svg.attr('class', imgClass + ' replaced-svg');
						svg = svg.removeAttr('xmlns:a');
						img.replaceWith(svg);
					}, 'xml');
				}
			});

			updateDetailsHoverTip();
			feedContainer.find('.date-range-toggle-group, .link-format-toggle').last().css("margin-right", "6px");
			feedContainer.find('.feed-details-button').off('click').on('click', function ()
			{
				$(this).blur();

				var data = $(this).find('.service-data');
				var samePage = data.find('.same-page').length > 0;
				if (samePage === true)
				{
					var linkId = '';
					switch (querySettings.dateRangeType)
					{
						case "today":
							linkId = data.find('.today-link-id').text();
							break;
						case "week":
							linkId = data.find('.week-link-id').text();
							break;
						case "month":
							linkId = data.find('.month-link-id').text();
							break;
						case "all-time":
							linkId = data.find('.all-time-link-id').text();
							break;
						default:
							linkId = data.find('.default-link-id').text();
							break;
					}
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "shortcuts/getShortcutDataById",
						data: {
							linkId: linkId
						},
						beforeSend: function () {
							$.SalesPortal.Overlay.show();
						},
						complete: function () {
							$.SalesPortal.Overlay.hide();
						},
						success: function (msg) {
							var shortcutData = $('<div>' + msg + '</div>');
							$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(shortcutData, {pushHistory: true});
						},
						error: function () {
							$.SalesPortal.Overlay.hide();
						},
						async: true,
						dataType: 'html'
					});
				}
				else
				{
					var url = '#';
					switch (querySettings.dateRangeType)
					{
						case "today":
							url = data.find('.today-url').text();
							break;
						case "week":
							url = data.find('.week-url').text();
							break;
						case "month":
							url = data.find('.month-url').text();
							break;
						case "all-time":
							url = data.find('.all-time-url').text();
							break;
						default:
							url = data.find('.default-url').text();
							break;
					}
					window.open(url, "_blank");
				}
			});
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

					if ($.SalesPortal.Content.isMobileDevice())
					{
						feedItemsList.find('.library-link-block').hammer().on('hold', function (event) {
							var linkId = $(this).find('.service-data .link-id').text();
							$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
							event.gesture.stopPropagation();
							event.gesture.preventDefault();
						});
					}
					else
					{
						feedItemsList.find('.library-link-block').off('contextmenu').on('contextmenu', function (event) {
							var linkId = $(this).find('.service-data .link-id').text();
							$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
							return false;
						});
					}
				}
			});

			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(feedItemsList);

			feedItemsList.find('.library-link-block.previewable').off('click').on('click', function (e)
			{
				e.stopPropagation();
				var linkId = $(this).find('.service-data .link-id').text();
				$.SalesPortal.LinkManager.requestViewDialog({
					linkId: linkId,
					isQuickSite: false
				});
			});

			feedItemsList.find('.library-link-block.direct-url').off('click').on('click', function (e)
			{
				var linkId = $(this).find('.service-data .link-id').text();
				var url = $(this).data('url');
				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Open',
					linkId: linkId,
					data: {
						file: url
					}
				});
			});

			if ($.SalesPortal.Content.isMobileDevice())
			{
				feedItemsList.find('.library-link-block').hammer().on('hold', function (event) {
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				feedItemsList.find('.library-link-block').off('contextmenu').on('contextmenu', function (event) {
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
					return false;
				});
			}

			feedItemsList.find('.draggable').off('dragstart').on('dragstart', function (e)
			{
				var urlHeader = $(this).data("url-header");
				var url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});
		};

		var updateDetailsHoverTip = function ()
		{
			var hoverTipButton = feedContainer.find('.feed-details-button');
			var hoverTipTemplateObject = hoverTipButton.find('.service-data .hover-tip-template');
			if (hoverTipTemplateObject.length > 0)
			{
				var hoverTipTemplate = hoverTipTemplateObject.text();
				if (feedContainer.find('.date-range-toggle').length > 0)
					hoverTipTemplate += (' by ' + querySettings.dateRangeType);
				hoverTipButton.prop('title', hoverTipTemplate);
			}
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