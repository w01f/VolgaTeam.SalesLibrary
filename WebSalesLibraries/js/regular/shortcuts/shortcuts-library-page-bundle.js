(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLibraryPageBundle = function () {
		var bundleData = undefined;
		var wallbinManager = undefined;

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

			var contentObject = $.SalesPortal.Content.getContentObject();

			wallbinManager = new $.SalesPortal.WallbinManager({
				contentObject: contentObject,
				shortcutId: bundleData.options.linkId,
				wallbinId: bundleData.options.bundleId,
				wallbinName: bundleData.options.headerTitle,
				pageViewType:bundleData.options.pageViewType,
				pageSelectorMode:bundleData.options.pageSelectorMode,
				fitWallbinToWholeScreen: true
			});
			wallbinManager.initPageSelector();
			wallbinManager.initContent();
			initActionButtons();

			contentObject.find('.page-container').addClass('selected').show();

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: bundleData,
				sizeChangedCallback: updateContentSize
			});

			updateContentSize();
			$(window).off('resize.library-page-bundle').on('resize.library-page-bundle', updateContentSize);
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
				wallbinManager.zoomIn();
			});

			shortcutActionsContainer.find('.page-zoom-out').off('click.action').on('click.action', function () {
				wallbinManager.zoomOut();
			});
		};

		var updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();
			wallbinManager.updateContentSize();
		};
	};
})(jQuery);
