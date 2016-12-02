(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsLibraryWindow = function ()
	{
		var libraryWindowData = undefined;

		this.init = function (data)
		{
			libraryWindowData = data;
			var pageContent = $.SalesPortal.Content.getContentObject();
			$.SalesPortal.Content.fillContent({
				content: libraryWindowData.content,
				headerOptions: {
					title: libraryWindowData.options.headerTitle,
					icon: libraryWindowData.options.headerIcon
				},
				actions: libraryWindowData.actions,
				navigationPanel: libraryWindowData.navigationPanel,
				resizeCallback: updateContentSize
			});
			switch (libraryWindowData.options.windowViewType)
			{
				case 'columns':
					$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
					break;
				case 'accordion':
					$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
					break;
			}
			initActionButtons();

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: libraryWindowData,
				sizeChangedCallback: updateContentSize
			});

			updateContentSize();
			$(window).off('resize.library-page').on('resize.library-page', updateContentSize);
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
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
		};
	};
})(jQuery);
