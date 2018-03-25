(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLibraryPage = function () {
		var libraryPageData = undefined;
		var wallbinManager = undefined;

		this.init = function (data) {
			libraryPageData = data;

			$.SalesPortal.Content.fillContent({
				content: libraryPageData.content,
				headerOptions: libraryPageData.options.headerOptions,
				actions: libraryPageData.actions,
				navigationPanel: libraryPageData.navigationPanel,
				loadCallback: function () {
					var pageContent = $.SalesPortal.Content.getContentObject();

					wallbinManager = new $.SalesPortal.WallbinManager({
						contentObject: pageContent,
						shortcutId: libraryPageData.options.linkId,
						pageViewType: libraryPageData.options.pageViewType,
						processResponsiveColumns: libraryPageData.options.processResponsiveColumns,
						fitWallbinToWholeScreen: true
					});
					wallbinManager.initContent();

					pageContent.find('.page-container').addClass('selected').show();

					new $.SalesPortal.ShortcutsSearchBar({
						shortcutData: libraryPageData,
						sizeChangedCallback: updateContentSize
					});

					updateContentSize();

					if (data.autoLoadLinkCallback !== undefined)
						data.autoLoadLinkCallback();
				},
				resizeCallback: updateContentSize
			});
			initActionButtons();
			$(window).off('resize.library-page').on('resize.library-page', updateContentSize);
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.page-view-columns').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryPageData.options.serviceData + '</div>'),
					{
						pageViewType: 'columns'
					}
				);
			});
			shortcutActionsContainer.find('.page-view-accordion').off('click.action').on('click.action', function () {
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryPageData.options.serviceData + '</div>'),
					{
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
