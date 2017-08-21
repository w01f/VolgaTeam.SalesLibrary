(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLibraryPageBundle = function () {
		var bundleData = undefined;

		this.init = function (data) {
			bundleData = data;

			$.SalesPortal.Content.fillContent({
				content: bundleData.content,
				headerOptions: {
					title: bundleData.options.headerTitle,
					icon: bundleData.options.headerIcon,
					titleHideCondition: bundleData.options.headerTitleHideCondition,
					iconHideCondition: bundleData.options.headerIconHideCondition
				},
				actions: bundleData.actions,
				navigationPanel: bundleData.navigationPanel,
				resizeCallback: updateContentSize
			});
			initBundleHeader();
			$.SalesPortal.Content.getContentObject().find('.page-container').addClass('selected').show();
			initPage();
			initActionButtons();

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: bundleData,
				sizeChangedCallback: updateContentSize
			});

			updateContentSize();
			$(window).off('resize.library-page-bundle').on('resize.library-page-bundle', updateContentSize);
		};

		var initBundleHeader = function () {
			var bundleHeader = $.SalesPortal.Content.getContentObject().find('.wallbin-header');
			switch (bundleData.options.pageSelectorMode)
			{
				case 'tabs':
					var tabContainer = bundleHeader.find('.page-selector-container .tab-pages');
					tabContainer.scrollTabs({
						click_callback: function () {
							tabContainer.find('.page-tab-header').removeClass('selected');
							$(this).addClass('selected');
							loadPage($.parseJSON($(this).find('.service-data .encoded-data').text()));
						}
					});
					var selectedTab = tabContainer.find('.selected');
					var left = tabContainer.scrollLeft();
					tabContainer.find('.scroll_tab_inner').animate({scrollLeft: left + selectedTab.position().left + 'px'}, 0);
					break;
				case 'combo':
					var comboSelector = bundleHeader.find('.selectpicker');
					comboSelector.selectpicker();
					comboSelector.off('change').on('change', function () {
						loadPage($.parseJSON(atob(comboSelector.selectpicker('val'))));
						comboSelector.selectpicker('refresh');
					});
					break;
			}
		};

		var loadPage = function (pageData) {
			var contentObject = $.SalesPortal.Content.getContentObject();

			$.cookie("SelectedLibraryPageId-" + bundleData.options.bundleId, pageData.pageId, {
				expires: (60 * 60 * 24 * 7)
			});

			var libraryContent = contentObject.find('.wallbin-container');
			libraryContent.find('.page-container').removeClass('selected').hide();

			var selectedPage = libraryContent.find('#page-' + pageData.pageId);
			if (selectedPage.length === 0)
			{
				var viewPath = undefined;
				if (bundleData.options.pageViewType === 'columns')
					viewPath = "wallbin/getColumnsView";
				else if (bundleData.options.pageViewType === 'accordion')
					viewPath = "wallbin/getAccordionView";

				$.ajax({
					type: "POST",
					url: window.BaseUrl + viewPath,
					data: {
						shortcutId: pageData.shortcutId,
						libraryId: pageData.libraryId,
						pageId: pageData.pageId
					},
					beforeSend: function () {
						$.SalesPortal.Overlay.show();
					},
					complete: function () {
						$.SalesPortal.Overlay.hide();
					},
					success: function (result) {
						libraryContent.append($(result));
						selectedPage = libraryContent.find('#page-' + pageData.pageId);
						selectedPage.addClass('selected').show();
						initPage();
						updateContentSize();
					},
					error: function () {
					},
					async: true,
					dataType: 'html'
				});
			}
			else
			{
				selectedPage.addClass('selected').show();
				initPage();
				updateContentSize();
			}
			$.SalesPortal.LogHelper.write({
				type: 'Wallbin',
				subType: 'Page Open',
				data: {
					Name: bundleData.options.headerTitle,
					pageName: pageData.name
				}
			});
		};

		var initPage = function () {
			var pageContent = $.SalesPortal.Content.getContentObject().find('.wallbin-container');
			switch (bundleData.options.pageViewType)
			{
				case 'columns':
					$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
					break;
				case 'accordion':
					$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
					break;
			}
		};

		var fixColumnBorders = function () {
			var pageContent = $.SalesPortal.Content.getContentObject().find('.wallbin-container .page-container.selected');
			var column1Height = pageContent.find('.column0 .page-column-inner').outerHeight(true);
			var column2Height = pageContent.find('.column1 .page-column-inner').outerHeight(true);
			var column3Height = pageContent.find('.column2 .page-column-inner').outerHeight(true);

			if (column1Height > column2Height)
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}

			if (column2Height > column3Height)
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');

			shortcutActionsContainer.find('.page-select-tabs').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + bundleData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'tabs',
						pageViewType: bundleData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-select-combo').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + bundleData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'combo',
						pageViewType: bundleData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-view-columns').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + bundleData.options.serviceData + '</div>'),
					{
						pageSelectorMode: bundleData.options.pageSelectorMode,
						pageViewType: 'columns'
					}
				);
			});
			shortcutActionsContainer.find('.page-view-accordion').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + bundleData.options.serviceData + '</div>'),
					{
						pageSelectorMode: bundleData.options.pageSelectorMode,
						pageViewType: 'accordion'
					}
				);
			});

			shortcutActionsContainer.find('.page-zoom-in').off('click.action').on('click.action', function () {
				$.SalesPortal.Wallbin.zoomIn();
			});

			shortcutActionsContainer.find('.page-zoom-out').off('click.action').on('click.action', function () {
				$.SalesPortal.Wallbin.zoomOut();
			});
		};

		var updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.Wallbin.updateContentSize();
			fixColumnBorders();
		};
	};
})(jQuery);
