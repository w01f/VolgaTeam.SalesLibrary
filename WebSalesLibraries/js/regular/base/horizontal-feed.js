(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.HorizontalFeed = function (parameters)
	{
		var containerId = parameters.containerId;
		var querySettings = new LinkFeedQuerySettings(parameters.querySettings);
		var viewSettings = new HorizontalFeedViewSettings(parameters.viewSettings);
		var linkFeedContainer = undefined;

		this.init = function ()
		{
			linkFeedContainer = $('#horizontal-feed-' + containerId);

			initToggles();
			initCarouselControls();
			initSlider();

			setTimeout(function ()
			{
				reloadLinks(false);
			}, 900000);
		};

		var reloadLinks = function (showProgress)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "landingPage/getLinkFeedItems",
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

					linkFeedContainer.find('.carousel-container').html(msg);

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
			linkFeedContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function ()
			{
				var dateRangeTitle = $(this).text();
				linkFeedContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				reloadLinks(true);
			});

			linkFeedContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormats = [];
				$.each(linkFeedContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					querySettings.linkFormats.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			})
		};

		var initCarouselControls = function ()
		{
			linkFeedContainer.find('.portfolio_utube_carousel_control_left').off('click').on('click', function ()
			{
				linkFeedContainer.find('.carousel').carousel('prev');
			});
			linkFeedContainer.find('.portfolio_utube_carousel_control_right').off('click').on('click', function ()
			{
				linkFeedContainer.find('.carousel').carousel('next');
			})
		};

		var initSlider = function ()
		{
			linkFeedContainer.find('.carousel-slide-show').carousel();

			linkFeedContainer.find('.carousel .carousel-inner').swipe({
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

			var oneMoveItems = linkFeedContainer.find('.carousel.one-link-move .item');
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

			linkFeedContainer.find('.carousel .item .portfolio_utube_item').off('click').on('click', function (e)
			{
				e.stopPropagation();
				var linkId = $(this).find('.service-data .link-id').text();
				$.SalesPortal.LinkManager.requestViewDialog({
					linkId: linkId,
					isQuickSite: false
				});
			})
		};
	};

	var LinkFeedQuerySettings = function (data)
	{
		this.feedType = undefined;
		this.maxLinks = undefined;
		this.linkFormats = undefined;
		this.libraries = undefined;
		this.dateRangeType = undefined;
		this.thumbnailMode = undefined;
		this.conditions = undefined;
		this.linkConditions = undefined;
		this.sortSettings = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};

	var HorizontalFeedViewSettings = function (data)
	{
		this.feedType = undefined;
		this.linksPerSlide = undefined;
		this.linksScrollMode = undefined;
		this.slideShow = undefined;
		this.slideShowInterval = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;
		this.controlActiveColor = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);