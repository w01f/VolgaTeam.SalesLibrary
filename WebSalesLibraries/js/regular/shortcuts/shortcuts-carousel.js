(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsCarousel = function ()
	{
		var carouselData = undefined;

		this.init = function (data)
		{
			carouselData = data;

			new $.SalesPortal.ShortcutsSearchBar(carouselData.linkId);

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
							$.SalesPortal.ShortcutsManager.trackActivity(shortcutData);

							var hasPageContent = shortcutData.find('.has-page-content').length > 0;
							var samePage = shortcutData.find('.same-page').length > 0;
							var url = shortcutData.find('.url').text();

							if (hasPageContent && samePage)
								$.SalesPortal.ShortcutsManager.openShortcut(shortcutData);
							else
								window.open(url.replace(/&amp;/g, '%26'), "_blank");
						};
				});
			});
			carousel.addListener(FWDUltimate3DCarousel.CATEGORY_CHANGE, function (ev)
			{
				updateContentSize();
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "statistic/writeActivity",
					data: {
						type: 'Shortcuts',
						subType: 'Carousel Group Select',
						data: $.toJSON({
							File: carouselData.displayParameters.predefinedDataList[ev.id].name
						})
					},
					async: true,
					dataType: 'html'
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

			shortcutActionsContainer.find('.grid').off('click').on('click', function ()
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
