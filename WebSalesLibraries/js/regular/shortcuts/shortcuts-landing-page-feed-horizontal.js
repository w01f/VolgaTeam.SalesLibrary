(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.HorizontalFeed = function (parameters)
	{
		var containerId = parameters.containerId;
		var querySettings = new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings);
		var viewSettings = new FeedViewSettings(parameters.viewSettings);
		var feedContainer = undefined;

		this.init = function ()
		{
			feedContainer = $('#horizontal-feed-' + containerId);

			initToggles();
			initCarouselControls();
			initSlider();

			if (viewSettings.feedType !== 'shortcut-slider')
			{
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
				url: window.BaseUrl + "landingPage/getHorizontalLinkFeedItems",
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

					feedContainer.find('.carousel-container').html(msg);

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

		var initCarouselControls = function ()
		{
			var leftButton = feedContainer.find('.portfolio_utube_carousel_control_left');
			var rightButton = feedContainer.find('.portfolio_utube_carousel_control_right');

			leftButton.prop('href', '#');
			leftButton.on('click', function ()
			{
				$(this).blur();
				feedContainer.find('.carousel').carousel('prev');
			});

			rightButton.prop('href', '#');
			rightButton.off('click').on('click', function ()
			{
				$(this).blur();
				feedContainer.find('.carousel').carousel('next');
			});
		};

		var initSlider = function ()
		{
			feedContainer.find('.carousel-slide-show').carousel();

			feedContainer.find('.carousel .carousel-inner').swipe({
				swipeLeft: function ()
				{
					$(this).parent().carousel('next');
				},
				swipeRight: function ()
				{
					$(this).parent().carousel('prev');
				},
				threshold: 0
			});

			var oneMoveItems = feedContainer.find('.carousel.one-link-move .item');
			oneMoveItems.each(function ()
			{
				var itemToClone = $(this);
				for (var i = 1; i < (oneMoveItems.length > viewSettings.linksPerSlide ? viewSettings.linksPerSlide : oneMoveItems.length); i++)
				{
					itemToClone = itemToClone.next();
					if (!itemToClone.length)
					{
						itemToClone = $(this).siblings(':first');
					}
					itemToClone.children(':first-child').clone()
						.addClass("cloneditem-" + (i))
						.appendTo($(this));
				}
			});

			feedContainer.find('.carousel-links .item .portfolio_utube_item').off('click').on('click', function (e)
			{
				e.stopPropagation();
				var linkId = $(this).find('.service-data .link-id').text();
				$.SalesPortal.LinkManager.requestViewDialog({
					linkId: linkId,
					isQuickSite: false
				});
			});

			feedContainer.find('.carousel').hover(function ()
			{
				$(this).carousel('pause')
			}, function ()
			{
				$(this).carousel('cycle')
			});

			feedContainer.on('mousewheel DOMMouseScroll', function (event)
			{
				event.stopPropagation();
				event.preventDefault();

				if (event.originalEvent.wheelDelta > 0 || event.originalEvent.detail < 0)
					feedContainer.find('.carousel').carousel('prev');
				else
					feedContainer.find('.carousel').carousel('next');
			});

			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(feedContainer);
		};
	};

	var FeedViewSettings = function (data)
	{
		this.feedType = undefined;
		this.linksPerSlide = undefined;
		this.linksScrollMode = undefined;
		this.slideShow = undefined;
		this.slideShowInterval = undefined;
		this.maxThumbnailHeight = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;
		this.controlsStyle = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);