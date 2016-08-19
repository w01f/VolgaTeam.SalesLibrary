(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsCarousel = function ()
	{
		var carouselData = undefined;
		var justLoaded = true;

		this.init = function (data)
		{
			carouselData = data;

			new $.SalesPortal.ShortcutsSearchBar(carouselData);

			FWDU3DCarUtils.checkIfHasTransforms();
			var carousel = new FWDUltimate3DCarousel(carouselData.displayParameters);
			carouselData.displayParameters.predefinedDataList.forEach(function (category)
			{
				category.dataItems.forEach(function (dataItem)
				{
					if (dataItem.mediaType == 'func')
						dataItem.onClick = function ()
						{
							var shortcutData = $('<div>' + dataItem.dataContent + '</div>');
							var activityData = $.parseJSON(shortcutData.find('.activity-data').text());
							$.SalesPortal.ShortcutsManager.trackActivity(activityData);

							var hasPageContent = shortcutData.find('.has-page-content').length > 0;
							var samePage = dataItem.samePage;
							var url = dataItem.url;

							if (hasPageContent && samePage)
								$.SalesPortal.ShortcutsManager.openShortcut(shortcutData, {pushHistory: true});
							else
								window.open(url.replace(/&amp;/g, '%26'), "_blank");
						};
				});
			});
			carousel.addListener(FWDUltimate3DCarousel.CATEGORY_CHANGE, function (ev)
			{
				updateContentSize();

				if(!justLoaded)
				{
					var shortcutData = $('<div>' + carouselData.serviceData + '</div>');
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
						file: carouselData.displayParameters.predefinedDataList[ev.id].name
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
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + carouselData.serviceData + '</div>'),
					{
						pageViewType: 'gridbundle'
					}
				);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();
		};
	};
})(jQuery);
