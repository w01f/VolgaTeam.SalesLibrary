(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLandingPage = function ()
	{
		var pageData = undefined;

		this.init = function (data)
		{
			pageData = data;

			$.SalesPortal.Content.fillContent({
				content: pageData.content,
				headerOptions: {
					title: pageData.options.headerTitle,
					icon: pageData.options.headerIcon,
					titleHideCondition: pageData.options.headerTitleHideCondition,
					iconHideCondition: pageData.options.headerIconHideCondition
				},
				actions: pageData.actions,
				navigationPanel: pageData.navigationPanel,
				resizeCallback: updateContentSize
			});

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: pageData.options
			});

			var pageContent = $.SalesPortal.Content.getContentObject();
			var landingPage = pageContent.find('.landing-page');

			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(landingPage);

			$.each(landingPage.find('.link-feed'), function (key, value)
			{
				var linkFeed = $(value);
				var feedId = linkFeed.prop('id').replace('link-feed-', '');
				var feedSettings = $.parseJSON(linkFeed.find('.service-data .encoded-object').text());
				new $.SalesPortal.LinkFeed({
					containerId: feedId,
					settings: feedSettings
				}).init();
			});

			$.each(landingPage.find('.news-block'), function (key, value)
			{
				var newsBlock = $(value);
				var newsId = newsBlock.prop('id').replace('news-block-', '');
				var newsBlockSettings = $.parseJSON(newsBlock.find('>.service-data .encoded-object').text());
				new $.SalesPortal.NewsBlock({
					containerId: newsId,
					settings: newsBlockSettings
				}).init();
			});

			$.each(landingPage.find('.scroll-stripe'), function (key, value)
			{
				var stripeBlock = $(value);
				stripeBlock.scrollTabs();
				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(stripeBlock);
			});

			$.each(landingPage.find('.masonry-container'), function (key, value)
			{
				var masonryBlock = $(value);
				var masonryId = masonryBlock.prop('id').replace('masonry-container-', '');
				var masonrySettings = masonryBlock.find('>.service-data');
				var horizontalGap = parseInt(masonrySettings.find('.horizontal-gap').text());
				var verticalGap = parseInt(masonrySettings.find('.vertical-gap').text());
				var grid = $('#masonry-grid-' + masonryId);
				grid.cubeportfolio({
					filters: '#masonry-filter-' + masonryId,
					layoutMode: 'grid',
					defaultFilter: '*',
					animationType: 'quicksand',
					gapHorizontal: horizontalGap,
					gapVertical: verticalGap,
					gridAdjustment: 'responsive',
					caption: 'zoom',
					displayType: 'fadeIn',
					displayTypeSpeed: 100
				});
				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(grid);
			});

			landingPage.find('[data-bs-hover-animate]')
				.mouseenter(function ()
				{
					var elem = $(this);
					elem.addClass('animated ' + elem.attr('data-bs-hover-animate'))
				})
				.mouseleave(function ()
				{
					var elem = $(this);
					elem.removeClass('animated ' + elem.attr('data-bs-hover-animate'))
				});

			landingPage.find('.landing-carousel.carousel-slide-show').carousel();

			initActionButtons();

			$(window).off('resize.landing-page').on('resize.landing-page', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.hide-search, .show-search').addClass('hidden-xs');
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();

			var shortcutsPage = content.find('.shortcuts-page-content');
			var height = content.outerHeight(true) - content.find('.shortcuts-search-bar-container').outerHeight(true) - 20;
			shortcutsPage.css({
				'height': height + 'px'
			});
		};
	};
})(jQuery);
