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
					icon: libraryPageData.options.headerIcon,
					titleHideCondition: libraryPageData.options.headerTitleHideCondition,
					iconHideCondition: libraryPageData.options.headerIconHideCondition
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

					new $.SalesPortal.ShortcutsSearchBar({
						shortcutData: libraryPageData,
						sizeChangedCallback: updateContentSize
					});

					updateContentSize();
				},
				resizeCallback: updateContentSize
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

		var fixColumnBorders = function () {
			var pageContent = $.SalesPortal.Content.getContentObject().find('.wallbin-container .page-container');
			var column1Height = pageContent.find('.column0 .page-column-inner').outerHeight(true);
			var column2Height = pageContent.find('.column1 .page-column-inner').outerHeight(true);
			var column3Height = pageContent.find('.column2 .page-column-inner').outerHeight(true);

			if (column1Height > column2Height)
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}

			if (column2Height > column3Height)
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
			$.SalesPortal.Wallbin.updateContentSize();
			fixColumnBorders();
		};
	};
})(jQuery);
