(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsGrid = function ()
	{
		var gridData = undefined;

		this.init = function (data)
		{
			gridData = data;

			new $.SalesPortal.ShortcutsSearchBar(gridData);

			var pageContent = $.SalesPortal.Content.getContentObject();
			var grid = pageContent.find('.shortcuts-links-grid');
			grid.cubeportfolio(gridData.displayParameters);

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
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + gridData.serviceData + '</div>'),
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
			var height = content.height() - content.find('.shortcuts-search-bar.open').height() - 20;
			shortcutsPage.css({
				'height': height + 'px'
			});
		};
	};
})(jQuery);
