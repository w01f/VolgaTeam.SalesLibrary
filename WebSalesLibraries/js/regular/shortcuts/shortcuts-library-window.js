(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLibraryWindow = function () {
		var libraryWindowData = undefined;
		var wallbinManager = undefined;

		this.init = function (data) {
			libraryWindowData = data;
			$.SalesPortal.Content.fillContent({
				content: libraryWindowData.content,
				headerOptions: libraryWindowData.options.headerOptions,
				actions: libraryWindowData.actions,
				navigationPanel: libraryWindowData.navigationPanel,
				fixedPanels: libraryWindowData.fixedPanels,
				resizeCallback: updateContentSize,
				loadCallback: function () {
					var pageContent = $.SalesPortal.Content.getContentObject();
					wallbinManager = new $.SalesPortal.WallbinManager({
						contentObject: pageContent,
						shortcutId: libraryWindowData.options.linkId,
						pageViewType: libraryWindowData.options.windowViewType,
						processResponsiveColumns: libraryWindowData.options.processResponsiveColumns,
						fitWallbinToWholeScreen: true
					});
					wallbinManager.initContent();

					initActionButtons();

					new $.SalesPortal.ShortcutsSearchBar({
						shortcutData: libraryWindowData,
						sizeChangedCallback: updateContentSize
					});

					updateContentSize();
					$(window).off('resize.library-page').on('resize.library-page', updateContentSize);

					if (data.autoLoadModalContentCallback !== undefined)
						data.autoLoadModalContentCallback();
				}
			});
		};

		var initActionButtons = function () {
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.page-zoom-in').off('click.action').on('click.action', function () {
				wallbinManager.zoomIn();
			});

			shortcutActionsContainer.find('.page-zoom-out').off('click.action').on('click.action', function () {
				wallbinManager.zoomOut();
			});
		};

		var updateContentSize = function () {
			$.SalesPortal.ShortcutsManager.updateContentSize();
		};
	};
})(jQuery);
