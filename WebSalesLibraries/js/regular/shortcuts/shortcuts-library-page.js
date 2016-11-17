(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsLibraryPage = function ()
	{
		var libraryPageData = undefined;

		this.init = function (data)
		{
			libraryPageData = data;
			var pageContent = $.SalesPortal.Content.getContentObject();
			$.SalesPortal.Content.fillContent({
				content: libraryPageData.content,
				headerOptions: {
					title: libraryPageData.options.headerTitle,
					icon: libraryPageData.options.headerIcon
				},
				actions: libraryPageData.actions,
				navigationPanel: libraryPageData.navigationPanel,
				loadCallback: function ()
				{
					switch (libraryPageData.options.pageViewType)
					{
						case 'columns':
							$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
							break;
						case 'accordion':
							$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
							break;
					}
					updateContentSize();
				}
			});
			initActionButtons();
			$(window).off('resize.library-page').on('resize.library-page', updateContentSize);
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.page-view-columns').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryPageData.options.serviceData + '</div>'),
					{
						pageViewType: 'columns'
					}
				);
			});
			shortcutActionsContainer.find('.page-view-accordion').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + libraryPageData.options.serviceData + '</div>'),
					{
						pageViewType: 'accordion'
					}
				);
			});
			shortcutActionsContainer.find('.page-zoom-in').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.Wallbin.zoomIn();
			});

			shortcutActionsContainer.find('.page-zoom-out').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.Wallbin.zoomOut();
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.Wallbin.updateContentSize();
		};
	};
})(jQuery);
