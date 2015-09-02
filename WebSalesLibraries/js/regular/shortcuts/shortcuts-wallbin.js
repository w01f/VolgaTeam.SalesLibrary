(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsWallbin = function ()
	{
		var libraryData = undefined;

		this.init = function (data)
		{
			libraryData = data;

			$.SalesPortal.Content.fillContent(
				libraryData.content,
				{
					title: libraryData.options.headerTitle,
					icon: libraryData.options.headerIcon
				},
				libraryData.actions,
				function ()
				{
					$.SalesPortal.Content.getContentObject().find('.page-container').show();
					initPage();
					initActionButtons();
					updateContentSize();
				}
			);
			initLibraryHeader();
			$(window).off('resize.library').on('resize.library', updateContentSize);
		};

		var initLibraryHeader = function ()
		{
			var libraryHeader = $.SalesPortal.Content.getContentObject().find('.wallbin-header');
			switch (libraryData.options.pageSelectorMode)
			{
				case 'tabs':
					var tabContainer = libraryHeader.find('.page-selector-container .tab-pages');
					tabContainer.scrollTabs({
						click_callback: function ()
						{
							tabContainer.find('.page-tab-header').removeClass('selected');
							$(this).addClass('selected');
							loadPage($.parseJSON($(this).find('.service-data .encoded-data').text()));
						}
					});

					var selectedTab = tabContainer.find('.selected');
					var left = tabContainer.scrollLeft();
					tabContainer.find('.scroll_tab_inner').animate({scrollLeft: left + selectedTab.position().left + 'px'}, 0);
					break;
				case 'combo':
					var comboSelector = libraryHeader.find('.selectpicker');
					comboSelector.selectpicker();
					comboSelector.off('change').on('change', function ()
					{
						loadPage($.parseJSON(atob(comboSelector.selectpicker('val'))));
						comboSelector.selectpicker('refresh');
					});
					break;
			}
		};

		var loadPage = function (pageData)
		{
			var contentObject = $.SalesPortal.Content.getContentObject();

			$.cookie("SelectedLibraryPageId-" + libraryData.options.libraryId, pageData.id, {
				expires: (60 * 60 * 24 * 7)
			});

			var libraryContent = contentObject.find('.wallbin-container');
			libraryContent.find('.page-container').hide();

			var selectedPage = libraryContent.find('#page-' + pageData.id);
			if (selectedPage.length == 0)
			{
				var viewPath = undefined;
				if (libraryData.options.pageViewType == 'columns')
					viewPath = "wallbin/getColumnsView";
				else if (libraryData.options.pageViewType == 'accordion')
					viewPath = "wallbin/getAccordionView";

				$.ajax({
					type: "POST",
					url: window.BaseUrl + viewPath,
					data: {
						libraryId: libraryData.options.libraryId,
						pageId: pageData.id
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (result)
					{
						libraryContent.append($(result));
						selectedPage = libraryContent.find('#page-' + pageData.id);
						selectedPage.show();
						initPage();
						updateContentSize();
						contentObject.find('.wallbin-logo-wrapper').html('<img class="wallbin-logo" src="' + pageData.logoContent + '">');
					},
					error: function ()
					{
					},
					async: true,
					dataType: 'html'
				});
			}
			else
			{
				selectedPage.show();
				initPage();
				updateContentSize();
				contentObject.find('.wallbin-logo-wrapper').html('<img class="wallbin-logo" src="' + pageData.logoContent + '">');
			}
		};

		var initPage = function ()
		{
			var pageContent = $.SalesPortal.Content.getContentObject().find('.wallbin-container');
			switch (libraryData.options.pageViewType)
			{
				case 'columns':
					$.SalesPortal.Wallbin.assignLinkEvents(pageContent);
					break;
				case 'accordion':
					$.SalesPortal.Wallbin.assignAccordionEvents(pageContent);
					break;
			}
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			shortcutActionsContainer.find('.page-select-tabs').off('click').on('click', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'tabs',
						pageViewType: libraryData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-select-combo').off('click').on('click', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: 'combo',
						pageViewType: libraryData.options.pageViewType
					}
				);
			});
			shortcutActionsContainer.find('.page-view-columns').off('click').on('click', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: libraryData.options.pageSelectorMode,
						pageViewType: 'columns'
					}
				);
			});
			shortcutActionsContainer.find('.page-view-accordion').off('click').on('click', function ()
			{
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + libraryData.options.serviceData + '</div>'),
					{
						pageSelectorMode: libraryData.options.pageSelectorMode,
						pageViewType: 'accordion'
					}
				);
			});

			shortcutActionsContainer.find('.page-zoom-in').off('click').on('click', function ()
			{
				$.SalesPortal.Wallbin.zoomIn();
			});

			shortcutActionsContainer.find('.page-zoom-out').off('click').on('click', function ()
			{
				$.SalesPortal.Wallbin.zoomOut();
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();
			var contentHeight = content.height();

			var wallbinHeader = content.find('.wallbin-header');
			var headerHeight = wallbinHeader.outerHeight();

			var wallbinContent = content.find('.wallbin-container');
			var wallbinHeight = contentHeight - headerHeight;
			wallbinContent.css({
				'height': wallbinHeight + 'px'
			});

			$.SalesPortal.Wallbin.updateContentSize();
		};
	};
})(jQuery);
