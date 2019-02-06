(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLandingPage = function () {
		let pageData = undefined;

		let updateSizeDelegates = [];

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

					let initMarkupBlocks = function (markupObject) {
						$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(markupObject);

						$.each(markupObject.find('.shortcut-library-link'), function (key, value) {

							let libraryLinkBlock = $(value);
							if ($.SalesPortal.Content.isMobileDevice())
							{
								libraryLinkBlock.hammer().on('hold', function (event) {
									let linkId = $(this).find('.service-data .library-link-id').text();
									$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
									event.gesture.stopPropagation();
									event.gesture.preventDefault();
								});
							}
							else
							{
								libraryLinkBlock.off('contextmenu').on('contextmenu', function (event) {
									let linkId = $(this).find('.service-data .library-link-id').text();
									$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
									return false;
								});
							}

							if (libraryLinkBlock.hasClass('draggable'))
								libraryLinkBlock.off('dragstart').on('dragstart', function (e) {
									let urlHeader = $(this).data("url-header");
									let url = $(this).data('url');
									if (url !== '')
										e.originalEvent.dataTransfer.setData(urlHeader, url);
								});
						});

						$.each(markupObject.find('.horizontal-feed'), function (key, value) {
							let linkFeed = $(value);
							let feedId = linkFeed.prop('id').replace('horizontal-feed-', '');
							let querySettingsEncoded = linkFeed.find('>.service-data .encoded-object .query-settings').text();
							let querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							let viewSettingsEncoded = linkFeed.find('>.service-data .encoded-object .view-settings').text();
							let viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							new $.SalesPortal.LandingPage.HorizontalFeed({
								containerId: feedId,
								querySettings: querySettings,
								viewSettings: viewSettings
							}).init();
						});

						$.each(markupObject.find('.vertical-feed'), function (key, value) {
							let linkFeed = $(value);
							let feedId = linkFeed.prop('id').replace('vertical-feed-', '');
							let querySettingsEncoded = linkFeed.find('>.service-data .encoded-object .query-settings').text();
							let querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							let viewSettingsEncoded = linkFeed.find('>.service-data .encoded-object .view-settings').text();
							let viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							new $.SalesPortal.LandingPage.VerticalFeed({
								containerId: feedId,
								querySettings: querySettings,
								viewSettings: viewSettings
							}).init();
						});

						$.each(markupObject.find('.scroll-stripe'), function (key, value) {
							let stripeBlock = $(value);

							let arrowSize = 42;
							if(stripeBlock.hasClass('scrolltab-medium'))
								arrowSize = 62;
							else if(stripeBlock.hasClass('scrolltab-large'))
								arrowSize = 82;

							stripeBlock.scrollTabs({
								left_arrow_size: arrowSize,
								right_arrow_size: arrowSize
							});
							$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(stripeBlock);
						});

						$.each(markupObject.find('.landing-page-button-group'), function (key, value) {
							let buttonGroupBlock = $(value);
							$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(buttonGroupBlock);
						});

						$.each(markupObject.find('.masonry-container'), function (key, value) {
							let masonryBlock = $(value);
							let masonryId = masonryBlock.prop('id').replace('masonry-container-', '');
							let querySettingsEncoded = masonryBlock.find('>.service-data .encoded-object .query-settings').text();
							let querySettings = querySettingsEncoded !== undefined && querySettingsEncoded.length ? $.parseJSON(querySettingsEncoded) : undefined;
							let viewSettingsEncoded = masonryBlock.find('>.service-data .encoded-object .view-settings').text();
							let viewSettings = viewSettingsEncoded !== undefined && viewSettingsEncoded.length ? $.parseJSON(viewSettingsEncoded) : undefined;
							let masonryProcessor = new $.SalesPortal.LandingPage.Masonry({
								containerId: masonryId,
								parentShortcutId: pageData.options.linkId,
								querySettings: querySettings,
								viewSettings: viewSettings
							});
							masonryProcessor.init();
							updateSizeDelegates.push(function () {
								masonryProcessor.updateContentSize();
							});
						});

						$.each(markupObject.find('.toggle-panel'), function (key, value) {
							let togglePanelBlock = $(value);
							let togglePanelId = togglePanelBlock.prop('id').replace('toggle-panel-', '');

							new $.SalesPortal.LandingPage.TogglePanel({
								containerId: togglePanelId,
								parentShortcutId: pageData.options.linkId,
							}).init();
						});

						$.each(markupObject.find('.menu-stripe'), function (key, value) {
							let menuStripeBlock = $(value);
							let menuStripeId = menuStripeBlock.prop('id').replace('menu-stripe-', '');

							new $.SalesPortal.LandingPage.MenuStripe({
								containerId: menuStripeId
							}).init();
						});

						$.each(markupObject.find('.landing-page-video-group'), function (key, value) {
							let videoGroupBlock = $(value);
							let videoGroupId = videoGroupBlock.prop('id').replace('video-group-', '');

							new $.SalesPortal.LandingPage.VideoGroup({
								containerId: videoGroupId
							}).init();
						});

						$.each(markupObject.find('.drop-folder-container'), function (key, value) {
							let dropFolderBlock = $(value);
							let dropFolderContainerId = dropFolderBlock.prop('id').replace('drop-folder-container-', '');

							new $.SalesPortal.LandingPage.DropFolder({
								containerId: dropFolderContainerId
							}).init();
						});

						$.each(markupObject.find('.landing-page-calendar'), function (key, value) {
							let calendarBlock = $(value);
							let calendarContainerId = calendarBlock.prop('id').replace('calendar-', '');

							new $.SalesPortal.LandingPage.Calendar({
								containerId: calendarContainerId,
								parentShortcutId: pageData.options.linkId,
							}).init();
						});

						$.each(markupObject.find('.library-block'), function (key, value) {
							let libraryBlock = $(value);
							let libraryBlockId = libraryBlock.prop('id').replace('library-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryBlock({
								containerId: libraryBlockId
							}).init();
						});

						$.each(markupObject.find('.library-page-bundle-block'), function (key, value) {
							let libraryPageBundleBlock = $(value);
							let libraryPageBundleBlockId = libraryPageBundleBlock.prop('id').replace('library-page-bundle-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryPageBundleBlock({
								containerId: libraryPageBundleBlockId
							}).init();
						});

						$.each(markupObject.find('.library-page-block'), function (key, value) {
							let libraryPageBlock = $(value);
							let libraryPageBlockId = libraryPageBlock.prop('id').replace('library-page-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryPageBlock({
								containerId: libraryPageBlockId
							}).init();
						});

						$.each(markupObject.find('.library-window-block'), function (key, value) {
							let libraryWindowBlock = $(value);
							let libraryWindowBlockId = libraryWindowBlock.prop('id').replace('library-window-block-', '');

							new $.SalesPortal.LandingPage.Wallbin.LibraryWindowBlock({
								containerId: libraryWindowBlockId
							}).init();
						});

						$.each(markupObject.find('.search-results-block'), function (key, value) {
							let searchResultsBlock = $(value);
							let searchResultsBlockId = searchResultsBlock.prop('id').replace('search-results-block-', '');

							let searchResultsBlockManager = new $.SalesPortal.LandingPage.SearchResultsBlock({
								containerId: searchResultsBlockId
							});
							searchResultsBlockManager.init();
							updateSizeDelegates.push(function () {
								searchResultsBlockManager.updateContentSize();
							});
						});

						markupObject.find('[data-bs-hover-animate]')
							.mouseenter(function () {
								let elem = $(this);
								elem.addClass('animated ' + elem.attr('data-bs-hover-animate'))
							})
							.mouseleave(function () {
								let elem = $(this);
								elem.removeClass('animated ' + elem.attr('data-bs-hover-animate'))
							});

						markupObject.find('.landing-carousel.carousel-slide-show').carousel();

						markupObject.find('.tooltipster-target').tooltipster();
					};

					let pageContent = $.SalesPortal.Content.getContentObject();
					let landingPage = pageContent.find('.landing-page-markup');
					initMarkupBlocks(landingPage);

					let fixedPanels = $.SalesPortal.Content.getFixedPanels();
					initMarkupBlocks(fixedPanels);

					updateSizeDelegates.push(function () {
						let content = $.SalesPortal.Content.getContentObject();
						let shortcutsPage = content.find('.shortcuts-page-content');
						let height = content.outerHeight(true) - content.find('.shortcuts-search-bar-container').outerHeight(true) - 20;
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

		let initActionButtons = function () {
			let shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.hide-search, .show-search').addClass('hidden-xs');
		};

		let updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();

			$.each(updateSizeDelegates, function (i, val) {
				val();
			});
		};
	};
})(jQuery);
