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

			$.each(landingPage.find('.trending-bar'), function (key, value)
			{
				var trendingBar = $(value);
				var barId = trendingBar.prop('id').replace('trending-bar-', '');
				var trendingSettings = $.parseJSON(trendingBar.find('.service-data .encoded-object').text());
				new $.SalesPortal.TrendingBar({
					containerId: barId,
					settings: trendingSettings
				}).init();
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
