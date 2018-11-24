(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLandingPage = function () {
		var pageData = undefined;

		var updateSizeDelegates = [];

		this.init = function (data) {
			pageData = data;

			$.SalesPortal.Content.fillContent({
				content: pageData.content,
				headerOptions: pageData.options.headerOptions,
				actions: pageData.actions,
				navigationPanel: pageData.navigationPanel,
				fixedPanels: pageData.fixedPanels,
				resizeCallback: updateContentSize,
				loadCallback: function () {
					new $.SalesPortal.ShortcutsSearchBar({
						shortcutData: pageData.options
					});

					var initMarkupBlocks = function (markupObject) {
						$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(markupObject);

						$.each(markupObject.find('.shortcut-library-link'), function (key, value) {

							var libraryLinkBlock = $(value);
							if ($.SalesPortal.Content.isMobileDevice())
							{
								libraryLinkBlock.hammer().on('hold', function (event) {
									var linkId = $(this).find('.service-data .library-link-id').text();
									$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
									event.gesture.stopPropagation();
									event.gesture.preventDefault();
								});
							}
							else
							{
								libraryLinkBlock.off('contextmenu').on('contextmenu', function (event) {
									var linkId = $(this).find('.service-data .library-link-id').text();
									$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
									return false;
								});
							}

							if (libraryLinkBlock.hasClass('draggable'))
								libraryLinkBlock.off('dragstart').on('dragstart', function (e) {
									var urlHeader = $(this).data("url-header");
									var url = $(this).data('url');
									if (url !== '')
										e.originalEvent.dataTransfer.setData(urlHeader, url);
								});
						});

						$.each(markupObject.find('.horizontal-feed'), function (key, value) {
							var linkFeed = $(value);
							var feedId = linkFeed.prop('id').replace('horizontal-feed-', '');
							var querySettingsEncoded = linkFeed.find('>.service-data .encoded-object .query-settings').text();
							var querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							var viewSettingsEncoded = linkFeed.find('>.service-data .encoded-object .view-settings').text();
							var viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							new $.SalesPortal.LandingPage.HorizontalFeed({
								containerId: feedId,
								querySettings: querySettings,
								viewSettings: viewSettings
							}).init();
						});

						$.each(markupObject.find('.vertical-feed'), function (key, value) {
							var linkFeed = $(value);
							var feedId = linkFeed.prop('id').replace('vertical-feed-', '');
							var querySettingsEncoded = linkFeed.find('>.service-data .encoded-object .query-settings').text();
							var querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							var viewSettingsEncoded = linkFeed.find('>.service-data .encoded-object .view-settings').text();
							var viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							new $.SalesPortal.LandingPage.VerticalFeed({
								containerId: feedId,
								querySettings: querySettings,
								viewSettings: viewSettings
							}).init();
						});

						$.each(markupObject.find('.scroll-stripe'), function (key, value) {
							var stripeBlock = $(value);
							stripeBlock.scrollTabs();
							$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(stripeBlock);
						});

						$.each(markupObject.find('.landing-page-button-group'), function (key, value) {
							var buttonGroupBlock = $(value);
							$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(buttonGroupBlock);
						});

						$.each(markupObject.find('.masonry-container'), function (key, value) {
							var masonryBlock = $(value);
							var masonryId = masonryBlock.prop('id').replace('masonry-container-', '');
							var querySettingsEncoded = masonryBlock.find('>.service-data .encoded-object .query-settings').text();
							var querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							var viewSettingsEncoded = masonryBlock.find('>.service-data .encoded-object .view-settings').text();
							var viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							new $.SalesPortal.LandingPage.Masonry({
								containerId: masonryId,
								parentShortcutId: pageData.options.linkId,
								querySettings: querySettings,
								viewSettings: viewSettings
							}).init();
						});

						$.each(markupObject.find('.toggle-panel'), function (key, value) {
							var togglePanelBlock = $(value);
							var togglePanelId = togglePanelBlock.prop('id').replace('toggle-panel-', '');

							new $.SalesPortal.LandingPage.TogglePanel({
								containerId: togglePanelId
							}).init();
						});

						$.each(markupObject.find('.menu-stripe'), function (key, value) {
							var menuStripeBlock = $(value);
							var menuStripeId = menuStripeBlock.prop('id').replace('menu-stripe-', '');

							new $.SalesPortal.LandingPage.MenuStripe({
								containerId: menuStripeId
							}).init();
						});

						$.each(markupObject.find('.library-block'), function (key, value) {
							var libraryBlock = $(value);
							var libraryBlockId = libraryBlock.prop('id').replace('library-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryBlock({
								containerId: libraryBlockId
							}).init();
						});

						$.each(markupObject.find('.library-page-bundle-block'), function (key, value) {
							var libraryPageBundleBlock = $(value);
							var libraryPageBundleBlockId = libraryPageBundleBlock.prop('id').replace('library-page-bundle-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryPageBundleBlock({
								containerId: libraryPageBundleBlockId
							}).init();
						});

						$.each(markupObject.find('.library-page-block'), function (key, value) {
							var libraryPageBlock = $(value);
							var libraryPageBlockId = libraryPageBlock.prop('id').replace('library-page-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryPageBlock({
								containerId: libraryPageBlockId
							}).init();
						});

						$.each(markupObject.find('.library-window-block'), function (key, value) {
							var libraryWindowBlock = $(value);
							var libraryWindowBlockId = libraryWindowBlock.prop('id').replace('library-window-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryWindowBlock({
								containerId: libraryWindowBlockId
							}).init();
						});

						$.each(markupObject.find('.search-results-block'), function (key, value) {
							var searchResultsBlock = $(value);
							var searchResultsBlockId = searchResultsBlock.prop('id').replace('search-results-block-', '');

							var searchResultsBlockManager = new $.SalesPortal.LandingPage.SearchResultsBlock({
								containerId: searchResultsBlockId
							});
							searchResultsBlockManager.init();
							updateSizeDelegates.push(function () {
								searchResultsBlockManager.updateContentSize();
							});
						});

						markupObject.find('[data-bs-hover-animate]')
							.mouseenter(function () {
								var elem = $(this);
								elem.addClass('animated ' + elem.attr('data-bs-hover-animate'))
							})
							.mouseleave(function () {
								var elem = $(this);
								elem.removeClass('animated ' + elem.attr('data-bs-hover-animate'))
							});

						markupObject.find('.landing-carousel.carousel-slide-show').carousel();
					};

					var pageContent = $.SalesPortal.Content.getContentObject();
					var landingPage = pageContent.find('.landing-page-markup');
					initMarkupBlocks(landingPage);

					var fixedPanels = $.SalesPortal.Content.getFixedPanels();
					initMarkupBlocks(fixedPanels);

					updateSizeDelegates.push(function () {
						var content = $.SalesPortal.Content.getContentObject();
						var shortcutsPage = content.find('.shortcuts-page-content');
						var height = content.outerHeight(true) - content.find('.shortcuts-search-bar-container').outerHeight(true) - 20;
						shortcutsPage.css({
							'height': height + 'px'
						});
					});

					initActionButtons();

					$(window).off('resize.landing-page').on('resize.landing-page', updateContentSize);
					updateContentSize();

					if (data.autoLoadLinkCallback !== undefined)
						data.autoLoadLinkCallback();
				}
			});

		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.hide-search, .show-search').addClass('hidden-xs');
		};

		var updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();

			$.each(updateSizeDelegates, function (i, val) {
				val();
			});
		};
	};
})(jQuery);
