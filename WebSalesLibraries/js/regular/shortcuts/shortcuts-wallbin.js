(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsWallbin = function () {
		var libraryData = undefined;
		var wallbinManager = undefined;

		this.init = function (data) {
			libraryData = data;

			$.SalesPortal.Content.fillContent({
				content: libraryData.content,
				headerOptions: libraryData.options.headerOptions,
				actions: libraryData.actions,
				navigationPanel: libraryData.navigationPanel,
				fixedPanels: libraryData.fixedPanels,
				resizeCallback: updateContentSize,
				loadCallback: function () {
					var contentObject = $.SalesPortal.Content.getContentObject();
					wallbinManager = new $.SalesPortal.WallbinManager({
						contentObject: contentObject,
						shortcutId: libraryData.options.linkId,
						wallbinId: libraryData.options.libraryId,
						wallbinName: libraryData.options.libraryName,
						pageViewType: libraryData.options.pageViewType,
						pageSelectorMode: libraryData.options.pageSelectorMode,
						processResponsiveColumns: libraryData.options.processResponsiveColumns,
						fitWallbinToWholeScreen: true
					});
					wallbinManager.initPageSelector();
					wallbinManager.initContent();
					initActionButtons();
					contentObject.find('.page-container').addClass('selected').show();

					new $.SalesPortal.ShortcutsSearchBar({
						shortcutData: libraryData,
						sizeChangedCallback: updateContentSize
					});

					updateContentSize();
					$(window).off('resize.library').on('resize.library', updateContentSize);

					if (data.autoLoadModalContentCallback !== undefined)
						data.autoLoadModalContentCallback();
				}
			});
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');

			shortcutActionsContainer.find('.page-select-tabs').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'tabs',
						pageViewType: libraryData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-select-combo').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'combo',
						pageViewType: libraryData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-view-columns').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: libraryData.options.pageSelectorMode,
						pageViewType: 'columns'
					}
				);
			});
			shortcutActionsContainer.find('.page-view-accordion').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: libraryData.options.pageSelectorMode,
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
			var libraryUpdateStamp = $('#library-update-stamp');
			libraryUpdateStamp.css({
				'left': ($('body').outerWidth() - libraryUpdateStamp.outerWidth()) + 'px'
			});
		};
	};
})(jQuery);
