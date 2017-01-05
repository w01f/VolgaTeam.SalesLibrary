(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.InternalLinkViewer = function (parameters)
	{
		var viewerData = new $.SalesPortal.InternalLinkViewerData(parameters.data);
		this.show = function ()
		{
			switch (viewerData.previewInfo.internalLinkType)
			{
				case 1:
					new $.SalesPortal.ShortcutsWallbin().init({
						content: parameters.content,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerTitle: viewerData.previewInfo.showHeaderText ? viewerData.name : '',
							headerIcon: viewerData.previewInfo.headerIcon,
							libraryId: viewerData.previewInfo.libraryId,
							pageSelectorMode: viewerData.previewInfo.pageSelectorType,
							pageViewType: viewerData.previewInfo.pageViewType
						}
					});
					$.SalesPortal.ShortcutsHistory.pushState(
						undefined,
						{
							pushHistory: true
						});
					break;
				case 2:
					new $.SalesPortal.ShortcutsLibraryPage().init({
						content: parameters.content,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerTitle: viewerData.previewInfo.showHeaderText ? viewerData.name : '',
							headerIcon: viewerData.previewInfo.headerIcon,
							pageViewType: viewerData.previewInfo.pageViewType
						}
					});
					$.SalesPortal.ShortcutsHistory.pushState(
						undefined,
						{
							pushHistory: true
						});
					break;
				case 3:
					new $.SalesPortal.ShortcutsLibraryWindow().init({
						content: parameters.content,
						navigationPanel: viewerData.previewInfo.navigationPanel,
						options: {
							headerTitle: viewerData.previewInfo.showHeaderText ? viewerData.name : '',
							headerIcon: viewerData.previewInfo.headerIcon,
							windowViewType: viewerData.previewInfo.windowViewType
						}
					});
					$.SalesPortal.ShortcutsHistory.pushState(
						undefined,
						{
							pushHistory: true
						});
					break;
				case 4:
					if (viewerData.previewInfo.libraryLinkId != null)
						$.SalesPortal.LinkManager.requestViewDialog({
							linkId: viewerData.previewInfo.libraryLinkId,
							isQuickSite: false
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
									clickHandler: function ()
									{
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
					if (hasCustomHandler == true)
						$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, {pushHistory: true});
					break;
			}
		};
	};
})(jQuery);
