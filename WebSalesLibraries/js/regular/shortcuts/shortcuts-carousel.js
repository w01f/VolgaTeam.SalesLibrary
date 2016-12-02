(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsCarousel = function ()
	{
		var carouselData = undefined;
		var carousel = undefined;
		var justLoaded = true;

		this.init = function (data)
		{
			carouselData = data;

			$.SalesPortal.Content.fillContent({
				content: carouselData.content,
				headerOptions: {
					title: carouselData.options.headerTitle,
					icon: carouselData.options.headerIcon
				},
				actions: carouselData.actions,
				navigationPanel: carouselData.navigationPanel,
				resizeCallback: updateContentSize
			});

			new $.SalesPortal.ShortcutsSearchBar({
				shortcutData: carouselData.options
			});

			FWDU3DCarUtils.checkIfHasTransforms();
			carousel = new FWDUltimate3DCarousel(carouselData.options.displayParameters);
			carouselData.options.displayParameters.predefinedDataList.forEach(function (category)
			{
				category.dataItems.forEach(function (dataItem)
				{
					if (dataItem.mediaType == 'func')
						dataItem.onClick = function ()
						{
							var shortcutData = $('<div>' + dataItem.dataContent + '</div>');
							var activityData = $.parseJSON(shortcutData.find('.activity-data').text());
							$.SalesPortal.ShortcutsManager.trackActivity(activityData);

							var hasCustomHandler = shortcutData.find('.has-custom-handler').length > 0;
							var samePage = dataItem.samePage;
							var url = dataItem.url;

							if (hasCustomHandler && samePage)
								$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(shortcutData, {pushHistory: true});
							else
								window.open(url.replace(/&amp;/g, '%26'), "_blank");
						};
				});
			});
			carousel.addListener(FWDUltimate3DCarousel.CATEGORY_CHANGE, function (ev)
			{
				updateContentSize();

				if (!justLoaded)
				{
					var shortcutData = $('<div>' + carouselData.options.serviceData + '</div>');
					$.SalesPortal.ShortcutsHistory.pushState(
						shortcutData,
						{
							pushHistory: true,
							pageViewType: 'carouselbundle',
							defaultCategoryIndex: (ev.id + 1)
						});
				}

				justLoaded = false;

				$.SalesPortal.LogHelper.write({
					type: 'Navigation',
					subType: 'Carousel Group Select',
					data: {
						file: carouselData.options.displayParameters.predefinedDataList[ev.id].name
					}
				});
			});

			initActionButtons();

			$(window).off('resize.carousel').on('resize.carousel', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.find('.grid').show();
			shortcutActionsContainer.find('.carousel').hide();

			shortcutActionsContainer.find('.grid').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(
					$('<div>' + carouselData.options.serviceData + '</div>'),
					{
						pageViewType: 'gridbundle'
					}
				);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();
			var menu = $('#main-menu');
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();

			var width = $(window).width() - navigationPanel.width() - 20;
			var height = $(window).height() - menu.height() - menu.offset().top;

			content.css({
				'max-width': width + 'px'
			});

			var carouselContainer = content.find('.shortcuts-links-carousel-view');
			carouselContainer.css({
				'max-width': width + 'px'
			});

			navigationPanel.find('ul').css({
				'height': height + 'px'
			});
		};
	};
})(jQuery);
