(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsGrid = function ()
	{
		var gridData = undefined;

		this.init = function (data)
		{
			gridData = data;

			$.SalesPortal.Content.fillContent({
				content: gridData.content,
				headerOptions: {
					title: gridData.options.headerTitle,
					icon: gridData.options.headerIcon,
					titleHideCondition: gridData.options.headerTitleHideCondition,
					iconHideCondition: gridData.options.headerIconHideCondition
				},
				actions: gridData.actions,
				navigationPanel: gridData.navigationPanel,
				resizeCallback: updateContentSize
			});

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: gridData.options
			});

			var pageContent = $.SalesPortal.Content.getContentObject();
			var grid = pageContent.find('.shortcuts-links-grid');
			grid.cubeportfolio(gridData.options.displayParameters);

			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(grid);

			initActionButtons();

			$(window).off('resize.grid').on('resize.grid', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.carousel').show();
			shortcutActionsContainer.find('.grid').hide();

			shortcutActionsContainer.find('.carousel').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + gridData.options.serviceData + '</div>'),
					{
						pageViewType: 'carouselbundle'
					}
				);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();

			var shortcutsPage = content.find('.shortcuts-page-content');
			var height = content.outerHeight(true) - content.find('.shortcuts-search-bar-container').outerHeight(true) - 20;
			shortcutsPage.css({
				'height': height + 'px'
			});
		};
	};
})(jQuery);
