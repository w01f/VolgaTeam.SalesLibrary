(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.InternalLinkViewer = function (parameters) {
		var viewerData = new $.SalesPortal.InternalLinkViewerData(parameters.data);
		this.show = function () {
			var headerOptions = undefined;
			switch (viewerData.previewInfo.internalLinkType)
			{
				case 1:
					if (!parameters.data.doNotPushHistory)
						$.SalesPortal.HistoryManager.pushPreviewLink(
							viewerData,
							{
								linkId: viewerData.linkId,
								isQuickSite: false
							});
					headerOptions  = viewerData.previewInfo.headerSettings;
					headerOptions.title = viewerData.previewInfo.showHeaderText ? viewerData.name : '';
					new $.SalesPortal.ShortcutsWallbin().init({
						content: parameters.content,
						actions: viewerData.previewInfo.actionsContent,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerOptions: headerOptions,
							libraryId: viewerData.previewInfo.libraryId,
							pageSelectorMode: viewerData.previewInfo.pageSelectorMode,
							pageViewType: viewerData.previewInfo.pageViewType,
							processResponsiveColumns: viewerData.previewInfo.processResponsiveColumns
						}
					});
					break;
				case 2:
					if (!parameters.data.doNotPushHistory)
						$.SalesPortal.HistoryManager.pushPreviewLink(
							viewerData,
							{
								linkId: viewerData.linkId,
								isQuickSite: false
							});
					headerOptions  = viewerData.previewInfo.headerSettings;
					headerOptions.title = viewerData.previewInfo.showHeaderText ? viewerData.name : '';
					new $.SalesPortal.ShortcutsLibraryPage().init({
						content: parameters.content,
						actions: viewerData.previewInfo.actionsContent,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerOptions: headerOptions,
							pageViewType: viewerData.previewInfo.pageViewType,
							processResponsiveColumns: viewerData.previewInfo.processResponsiveColumns
						}
					});
					break;
				case 3:
					if (!parameters.data.doNotPushHistory)
						$.SalesPortal.HistoryManager.pushPreviewLink(
							viewerData,
							{
								linkId: viewerData.linkId,
								isQuickSite: false
							});
					headerOptions  = viewerData.previewInfo.headerSettings;
					headerOptions.title = viewerData.previewInfo.showHeaderText ? viewerData.name : '';
					new $.SalesPortal.ShortcutsLibraryWindow().init({
						content: parameters.content,
						actions: viewerData.previewInfo.actionsContent,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerOptions: headerOptions,
							windowViewType: viewerData.previewInfo.windowViewType,
							processResponsiveColumns: viewerData.previewInfo.processResponsiveColumns
						}
					});
					break;
				case 4:
					if (viewerData.previewInfo.libraryLinkId !== null)
						$.SalesPortal.LinkManager.requestViewDialog({
							linkId: viewerData.previewInfo.libraryLinkId,
							isQuickSite: false,
							viewContainer: parameters.viewContainer,
							parentPreviewParameters: parameters.parentPreviewParameters,
							afterViewerOpenedCallback: parameters.afterViewerOpenedCallback
						});
					else
					{
						var modalDialog = new $.SalesPortal.ModalDialog({
							title: '<span class="text-danger">Sorry...</span>',
							description: 'Link not found.',
							buttons: [
								{
									tag: 'close',
									title: 'Close',
									width: 80,
									clickHandler: function () {
										modalDialog.close();
									}
								}
							],
							closeOnOutsideClick: true
						});
						modalDialog.show();
					}
					break;
				case 5:
					var data = $(parameters.content);
					var activityData = $.parseJSON(data.find('.activity-data').text());
					$.SalesPortal.ShortcutsManager.trackActivity(activityData);

					var hasCustomHandler = data.find('.has-custom-handler').length > 0;
					if (hasCustomHandler === true)
						$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, {pushHistory: true});
					break;
			}
		};
	};
})(jQuery);
