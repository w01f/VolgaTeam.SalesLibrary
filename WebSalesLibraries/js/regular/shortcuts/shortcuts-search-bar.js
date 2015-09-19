(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchBar = function (bundleData)
	{
		var searchBar = $('.shortcuts-search-bar');

		var init = function ()
		{
			if (searchBar.length > 0)
			{
				searchBar.find('.search-bar-text').keypress(function (e)
				{
					updateSearchButtonState();
					if (e.which == 13)
						search();
				});
				searchBar.find('.search-bar-run').on('click', search);

				searchBar.find('.file-filter-panel .file-selector input').off('change').on('change', function ()
				{
					updateSearchButtonState();
				});

				searchBar.find('.search-bar-options').off('click').on('click', function ()
				{
					editSettings();
				});

				searchBar.find('.tags-filter-panel-switcher').off('click').on('click', function ()
				{
					editTagsCondition();
				});

				updateSearchButtonState();
				updateSelectedCategories();

				initActionButtons();
			}
		};

		var search = function ()
		{
			if (searchBar.find('.btn.search-bar-run').hasClass('disabled')) return;
			searchBarConditions.set('text', searchBar.find('.search-bar-text').val());

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/confirmSearchBarSearch",
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var modalContent = $(msg);

					modalContent.find('.keyword').html(searchBarConditions.get('text'));

					var fileSettings = searchBarConditions.getFileTypesSettings();
					modalContent.find('#search-bar-edit-file-power-point').prop('checked', fileSettings.showPowerPoint);
					modalContent.find('#search-bar-edit-file-video').prop('checked', fileSettings.showVideo);
					modalContent.find('#search-bar-edit-file-others').prop('checked', fileSettings.showPdf || fileSettings.showWord || fileSettings.showExcel || fileSettings.showImages || fileSettings.showUrls);

					modalContent.find('#search-bar-edit-file-names').prop('checked', searchBarConditions.get('onlyFileNames'));
					modalContent.find('#search-bar-edit-exact-match').prop('checked', searchBarConditions.get('exactMatch'));
					modalContent.find('#search-bar-edit-only-new-files').prop('checked', searchBarConditions.get('onlyNewFiles'));

					modalContent.find('.search-button').off('click').on('click', function ()
					{
						searchBarConditions.setFileTypesSettings({
							showPowerPoint: modalContent.find('#search-bar-edit-file-power-point').prop('checked'),
							showVideo: modalContent.find('#search-bar-edit-file-video').prop('checked'),
							showPdf: modalContent.find('#search-bar-edit-file-others').prop('checked'),
							showWord: modalContent.find('#search-bar-edit-file-others').prop('checked'),
							showExcel: modalContent.find('#search-bar-edit-file-others').prop('checked'),
							showImages: modalContent.find('#search-bar-edit-file-others').prop('checked'),
							showUrls: modalContent.find('#search-bar-edit-file-others').prop('checked')
						});
						searchBarConditions.set('onlyFileNames', modalContent.find('#search-bar-edit-file-names').prop('checked'));
						searchBarConditions.set('exactMatch', modalContent.find('#search-bar-edit-exact-match').prop('checked'));
						searchBarConditions.set('onlyNewFiles', modalContent.find('#search-bar-edit-only-new-files').prop('checked'));

						$.fancybox.close();

						if (searchBarOptions.openInSamePage)
						{
							var content = $.SalesPortal.Content.getContentObject();
							searchBarOptions.conditions = searchBarConditions.getConditionsFormatted();
							$.SalesPortal.Content.fillContent(
								'<div class="search-conditions" style="display: none;"><div class="encoded-object">' + $.toJSON(searchBarOptions) + '</div></div>' +
									'<div class="search-view-options" style="display: none;"><div class="encoded-object">' + $.toJSON(searchViewOptions) + '</div></div>',
								undefined,
								searchActions
							);
							$.SalesPortal.ShortcutsSearchLink(
								content,
								shortcutBundleData.linkId,
								function ()
								{
									$.SalesPortal.ShortcutsManager.openShortcut($('<div>' + shortcutBundleData.serviceData + '</div>'));
								}
							);
						}
						else
						{
							window.open("shortcuts/GetQuickSearchResult?bundleId=" + shortcutBundleData.linkId +
								"&text=" + searchBarConditions.get('text') +
								"&textExactMatch=" + searchBarConditions.get('exactMatch') +
								"&onlyFiles=" + searchBarConditions.get('onlyFileNames') +
								"&onlyNewFiles=" + searchBarConditions.get('onlyNewFiles') +
								"&fileTypes=" + $.toJSON(searchBarConditions.getFileTypesSettings().selectedTypeTags()) +
								"&superFilters=" + $.toJSON(searchBarConditions.getSuperFiltersSettings()) +
								"&categories=" + $.toJSON(searchBarConditions.getCategorySettings()));
						}
					});
					modalContent.find('.cancel-button').off('click').on('click', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: modalContent,
						title: 'Search Options',
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateSearchButtonState = function ()
		{
			var hasKeyword = $('.shortcuts-search-bar .search-bar-text').val() != "";
			var hasSuperFilters = searchBarConditions.getSuperFiltersSettings().length > 0;
			var hasCategories = searchBarConditions.getCategorySettings().length > 0;
			var fileSettings = searchBarConditions.getFileTypesSettings();
			var searchButton = searchBar.find('.btn.search-bar-run');
			searchButton.removeClass('disabled');
			if (!(hasKeyword ||
				hasSuperFilters ||
				hasCategories) || !(fileSettings.showPowerPoint ||
				fileSettings.showVideo ||
				fileSettings.showPdf ||
				fileSettings.showWord ||
				fileSettings.showExcel ||
				fileSettings.showImages ||
				fileSettings.showUrls
				)
				)
				searchButton.addClass('disabled');
		};

		var updateSelectedCategories = function ()
		{
			var superFiltersStr = searchBarConditions.getSuperFiltersSettings().join(', ');
			var categoriesStr = searchBarConditions.getCategoryDescription().join(', ');
			var categoryStr = '';
			if (categoriesStr != '' || superFiltersStr != '')
				categoryStr = [superFiltersStr, categoriesStr].join(', ');
			else if (categoriesStr != '')
				categoryStr = categoriesStr;
			else if (superFiltersStr != '')
				categoryStr = superFiltersStr;
			var selectedCategoryLabel = searchBar.find('.tag-condition-selected');
			if (categoryStr != "")
				selectedCategoryLabel.html(searchBar.find('.tags-filter-panel-switcher').html() + ': ' + categoryStr);
			else
				selectedCategoryLabel.html('');
		};

		var setDefaultSettings = function ()
		{
			searchBarConditions.loadFromConditionsFormatted(searchBarOptions.conditions);

			searchBarConditions.setFileTypesSettings({
				showPowerPoint: true,
				showVideo: true,
				showPdf: false,
				showWord: false,
				showExcel: false,
				showImages: false,
				showUrls: false
			});
			searchBarConditions.set('onlyFileNames', true);
			searchBarConditions.set('exactMatch', true);
		};

		var editSettings = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/editSearchBarSettings",
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var content = $(msg);

					var fileSettings = searchBarConditions.getFileTypesSettings();
					content.find('#search-bar-edit-file-power-point').prop('checked', fileSettings.showPowerPoint);
					content.find('#search-bar-edit-file-video').prop('checked', fileSettings.showVideo);
					content.find('#search-bar-edit-file-others').prop('checked', fileSettings.showPdf || fileSettings.showWord || fileSettings.showExcel || fileSettings.showImages || fileSettings.showUrls);

					content.find('#search-bar-edit-file-names').prop('checked', searchBarConditions.get('onlyFileNames'));
					content.find('#search-bar-edit-exact-match').prop('checked', searchBarConditions.get('exactMatch'));
					content.find('#search-bar-edit-only-new-files').prop('checked', searchBarConditions.get('onlyNewFiles'));

					content.find('.accept-button').off('click').on('click', function ()
					{
						searchBarConditions.setFileTypesSettings({
							showPowerPoint: content.find('#search-bar-edit-file-power-point').prop('checked'),
							showVideo: content.find('#search-bar-edit-file-video').prop('checked'),
							showPdf: content.find('#search-bar-edit-file-others').prop('checked'),
							showWord: content.find('#search-bar-edit-file-others').prop('checked'),
							showExcel: content.find('#search-bar-edit-file-others').prop('checked'),
							showImages: content.find('#search-bar-edit-file-others').prop('checked'),
							showUrls: content.find('#search-bar-edit-file-others').prop('checked')
						});
						searchBarConditions.set('onlyFileNames', content.find('#search-bar-edit-file-names').prop('checked'));
						searchBarConditions.set('exactMatch', content.find('#search-bar-edit-exact-match').prop('checked'));
						searchBarConditions.set('onlyNewFiles', content.find('#search-bar-edit-only-new-files').prop('checked'));

						$.fancybox.close();
					});
					content.find('.cancel-button').off('click').on('click', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: content,
						title: 'Search Options',
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var editTagsCondition = function ()
		{
			var categorySelector = searchBar.find('.tag-condition-selector');
			$.fancybox({
				content: categorySelector.html(),
				title: $('.tags-filter-panel-switcher').html(),
				width: 550,
				autoSize: false,
				autoHeight: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					var innerContent = $('.fancybox-inner');

					var categoriesContent = innerContent.find(".tag-list");
					var categories = searchBarConditions.getCategorySettings();
					var groupsCount = categoriesContent.find('.group-selector-container').length;
					if (categories.length > 0)
					{
						$.each(categoriesContent.find('>.checkbox'), function ()
						{
							var groupCheckBoxContainer = $(this);
							var groupName = groupCheckBoxContainer.find('.group-selector-container .name').text();
							var groupCheckBoxItems = groupCheckBoxContainer.find('.checkbox');
							$.each(categories, function (groupIndex, group)
							{
								if (group.name == groupName)
								{
									if (group.items.length == groupCheckBoxItems.length)
										groupCheckBoxItems.find('.tag-selector').prop('checked', true);
									else
										$.each(groupCheckBoxItems, function ()
										{
											var tagCheckBoxContainer = $(this);
											var tagCheckBox = tagCheckBoxContainer.find('.tag-selector');
											var tagName = tagCheckBoxContainer.find('.name').text();
											$.each(group.items, function (itemIndex, item)
											{
												if (item == tagName)
													tagCheckBox.prop('checked', true);
											});
										});
								}
							});
						});
					}
					categoriesContent.find('.group-selector').off('change').on('change', function ()
					{
						$(this).closest('.group-checkbox').find('.tag-selector').prop('checked', $(this).is(':checked'));
					});
					categoriesContent.accordion({
						heightStyle: "content",
						active: groupsCount > 1 ? false : 0,
						collapsible: groupsCount > 1,
						icons: {
							header: "ui-icon-circle-arrow-e",
							activeHeader: "ui-icon-circle-arrow-s"
						}
					});

					var superFiltersContent = innerContent.find(".super-filter-list");
					var superFilters = searchBarConditions.getSuperFiltersSettings();
					$.each(superFilters, function (index, value)
					{
						superFiltersContent.find('.btn:contains("' + value + '")').button('toggle');
					});
					superFiltersContent.find('.btn').off('click').on('click', function ()
					{
						$(this).button('toggle').blur();
					});


					innerContent.find('.tags-clear-all').off('click').on('click', function ()
					{
						superFiltersContent.find('.btn').removeClass('active').blur();
						categoriesContent.find(":checked").prop('checked', false);
					});

					innerContent.find('.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});

					innerContent.find('.accept-button').on('click', function ()
					{
						var selectedSuperFilters = [];
						var allSuperFilterButtons = superFiltersContent.find('.btn');
						var selectedSuperFilterButtons = superFiltersContent.find('.btn.active');
						if (allSuperFilterButtons.length != selectedSuperFilterButtons.length)
							$.each(selectedSuperFilterButtons, function ()
							{
								selectedSuperFilters.push($(this).text());
							});
						searchBarConditions.setSuperFiltersSettings(selectedSuperFilters);

						var selectedCategories = [];
						var allCategoryTagCheckBoxes = categoriesContent.find('.tag-selector');
						var selectedCategoryTagCheckBoxes = categoriesContent.find('.tag-selector-container :checked');
						if (allCategoryTagCheckBoxes.length != selectedCategoryTagCheckBoxes.length)
							$.each(categoriesContent.find('>.checkbox'), function ()
							{
								var groupCheckBoxContainer = $(this);
								var groupCheckBoxItems = groupCheckBoxContainer.find('.checkbox');
								if (groupCheckBoxItems.find('.tag-selector-container :checked').length > 0)
								{
									var tags = [];
									var groupName = groupCheckBoxContainer.find('.group-selector-container .name').text();
									$.each(groupCheckBoxItems, function ()
									{
										var tagCheckBoxContainer = $(this);
										var tagCheckBox = tagCheckBoxContainer.find('.tag-selector');
										if (tagCheckBox.prop('checked') == true)
											tags.push(tagCheckBoxContainer.find('.name').text());
									});
									selectedCategories.push({
										name: groupName,
										items: tags
									})
								}
							});
						searchBarConditions.setCategorySettings(selectedCategories);

						updateSearchButtonState();
						updateSelectedCategories();

						$.fancybox.close();
					});
				}
			});
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			updateSearchToggleButtonState();

			shortcutActionsContainer.find('.show-search').off('click').on('click', function ()
			{
				changeVisibility(true);
				updateSearchToggleButtonState();
			});

			shortcutActionsContainer.find('.hide-search').off('click').on('click', function ()
			{
				changeVisibility(false);
				updateSearchToggleButtonState();
			});
		};

		var changeVisibility = function (show)
		{
			if (show)
				searchBar.addClass('open').show();
			else
				searchBar.removeClass('open').hide();
		};

		var updateSearchToggleButtonState = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');
			if (searchBar.hasClass('open'))
			{
				shortcutActionsContainer.find('.show-search').hide();
				shortcutActionsContainer.find('.hide-search').show();
			}
			else
			{
				shortcutActionsContainer.find('.show-search').show();
				shortcutActionsContainer.find('.hide-search').hide();
			}
		};

		if (searchBar.length > 0)
		{
			var shortcutBundleData = bundleData;
			var searchBarOptions = new $.SalesPortal.SearchOptions($.parseJSON(searchBar.find('.search-conditions .encoded-object').text()));
			var searchViewOptions = new $.SalesPortal.SearchViewOptions($.parseJSON(searchBar.find('.search-view-options .encoded-object').text()));
			var searchActions = searchBar.find('.search-bar-actions').html();
			var searchBarConditions = new $.SalesPortal.SearchConditions(function ()
			{

			});
			setDefaultSettings();
		}
		init();
	};
})(jQuery);