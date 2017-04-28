(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.Masonry = function (parameters)
	{
		var masonryId = parameters.containerId;
		var querySettings = parameters.querySettings !== undefined ? new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings) : null;
		var viewSettings = new MasonryViewSettings(parameters.viewSettings);
		var masonryContainer = undefined;

		this.init = function ()
		{
			masonryContainer = $('#masonry-container-' + masonryId);

			initMasonry();

			if (viewSettings.feedType !== 'masonry')
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
				url: window.BaseUrl + "landingPage/getMasonryLinkFeedItems",
				data: {
					feedId: masonryId,
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

					masonryContainer.find('#masonry-grid-' + masonryId).html(msg);

					initMasonry();
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
			masonryContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function ()
			{
				var dateRangeTitle = $(this).text();
				masonryContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				reloadLinks(true);
			});

			masonryContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormats = [];
				$.each(masonryContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					querySettings.linkFormats.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			})
		};

		var initMasonry = function ()
		{
			var grid = masonryContainer.find('#masonry-grid-' + masonryId);
			try
			{
				grid.cubeportfolio({
					filters: '#masonry-filter-' + masonryId,
					layoutMode: 'grid',
					defaultFilter: viewSettings.defaultFilter ? ('.' + viewSettings.defaultFilter.tags.join(', .')) : '*',
					animationType: 'quicksand',
					gapHorizontal: parseInt(viewSettings.itemsPadding.left) + parseInt(viewSettings.itemsPadding.right),
					gapVertical: parseInt(viewSettings.itemsPadding.top) + parseInt(viewSettings.itemsPadding.bottom),
					gridAdjustment: 'responsive',
					caption: viewSettings.enableCaptionZoom === true ? 'zoom' : '',
					displayType: 'fadeIn',
					displayTypeSpeed: 100
				});
				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(grid);
				grid.find('.library-link-item').off('click').on('click', function (e)
				{
					e.stopPropagation();
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});
			}
			catch (err)
			{
				grid.cubeportfolio('destroy', function ()
				{
					initMasonry();
				});
			}
		};
	};

	var MasonryViewSettings = function (data)
	{
		this.itemsPadding = undefined;
		this.enableCaptionZoom = undefined;
		this.captionZoomScale = undefined;

		this.imageWidth = undefined;
		this.imageHeight = undefined;
		this.controlsStyle = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;

		this.filters = undefined;
		this.defaultFilter = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);