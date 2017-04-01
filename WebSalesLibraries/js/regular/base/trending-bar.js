(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.TrendingBar = function (parameters)
	{
		var containerId = parameters.containerId;
		var trendingSettings = new TrendingSettings(parameters.settings);
		var trendingContainer = undefined;

		this.init = function ()
		{
			trendingContainer = $('#trending-bar-' + containerId);

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
				url: window.BaseUrl + "trending/getItems",
				data: {
					barId: containerId,
					settings: trendingSettings
				},
				beforeSend: function ()
				{
					if (showProgress)
						$.SalesPortal.Overlay.show();
				},
				success: function (msg)
				{
					$.SalesPortal.Overlay.hide();

					trendingContainer.find('.carousel-container').html(msg);

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
			trendingContainer.find('.date-range-toggle a').off('click.trending-bar').on('click.trending-bar', function ()
			{
				var dateRangeTitle = $(this).text();
				trendingContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				trendingSettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				reloadLinks(true);
			});

			trendingContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				trendingSettings.linkFormats = [];
				$.each(trendingContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					trendingSettings.linkFormats.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			})
		};

		var initCarouselControls = function ()
		{
			trendingContainer.find('.portfolio_utube_carousel_control_left').off('click').on('click', function ()
			{
				trendingContainer.find('.carousel').carousel('prev');
			});
			trendingContainer.find('.portfolio_utube_carousel_control_right').off('click').on('click', function ()
			{
				trendingContainer.find('.carousel').carousel('next');
			})
		};

		var initSlider = function ()
		{
			trendingContainer.find('.carousel-slide-show').carousel();

			trendingContainer.find('.carousel .carousel-inner').swipe({
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

			var oneMoveItems = trendingContainer.find('.carousel.one-link-move .item');
			oneMoveItems.each(function ()
			{
				var itemToClone = $(this);
				for (var i = 1; i < (oneMoveItems.length > trendingSettings.linksPerSlide ? trendingSettings.linksPerSlide : oneMoveItems.length); i++)
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

			trendingContainer.find('.carousel .item .portfolio_utube_item').off('click').on('click', function (e)
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


	var TrendingSettings = function (data)
	{
		this.linksPerSlide = undefined;
		this.maxLinks = undefined;
		this.linksScrollMode = undefined;
		this.linkFormats = undefined;
		this.libraries = undefined;
		this.dateRangeType = undefined;
		this.thumbnailMode = undefined;
		this.slideShow = undefined;
		this.slideShowInterval = undefined;
		this.controlSettings = undefined;
		this.controlActiveColor = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);