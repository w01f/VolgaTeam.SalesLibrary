(function ($) {
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
				resizeCallback: updateContentSize,
				loadCallback: function () {
					var pageContent = $.SalesPortal.Content.getContentObject();
					wallbinManager = new $.SalesPortal.WallbinManager({
						contentObject: pageContent,
						shortcutId: libraryWindowData.options.linkId,
						pageViewType: libraryWindowData.options.windowViewType,
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

					if (data.autoLoadLinkiCallback !== undefined)
						data.autoLoadLinkiCallback();
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
