(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSearchBar = function (parameters)
	{
		var searchBar = $('.shortcuts-search-bar');

		var init = function ()
		{
			if (searchBar.length > 0)
			{
				searchBar.find('.search-bar-text').keypress(function (e)
				{
					updateSearchButtonState();
					if (e.which === 13)
						search();
				});

				searchBar.find('.search-bar-run').each(function ()
				{
					var img = $(this).find('img');
					if (img.length > 0)
					{
						var imgURL = img.attr('src');
						$.get(imgURL, function (data)
						{
							var svg = $(data).find('svg');
							svg = svg.removeAttr('xmlns:a');
							img.replaceWith(svg);
						}, 'xml');
					}
				});
				searchBar.find('.search-bar-run').on('click.search-bar', search);

				searchBar.find('.file-filter-panel .file-selector input').off('change').on('change', function ()
				{
					updateSearchButtonState();
				});

				searchBar.find('.search-bar-options').off('click.search-bar').on('click.search-bar', function ()
				{
					editSettings();
				});

				searchBar.find('.tags-filter-panel-switcher').off('click.search-bar').on('click.search-bar', function ()
				{
					editTagsCondition();
				});

				updateSearchButtonState();
				updateSelectedCategories();

				initActionButtons();

				var formLogger = new $.SalesPortal.FormLogger();
				formLogger.init({
					logObject: {name: 'Search Bar'},
					formContent: searchBar
				});
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
					$.SalesPortal.Overlay.show();
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

					modalContent.find('.search-button').off('click.search-bar').on('click.search-bar', function ()
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
							searchBarOptions.conditions = searchBarConditions.getConditionsFormatted();
							var options = $('<div>' +
								'<div class="search-conditions" style="display: none;"><div class="encoded-object">' + $.toJSON(searchBarOptions) + '</div></div>' +
								'<div class="search-view-options" style="display: none;"><div class="encoded-object">' + $.toJSON(searchViewOptions) + '</div></div>' +
								'</div>');
							$.SalesPortal.ShortcutsSearchLink({
								optionsContainer: options,
								options: {
									linkId: parentShortcutData.linkId
								},
								backHandler: function ()
								{
									location.reload();
								}
							}).runSearch(function (data)
							{
								if (data.dataset.length === 0)
								{
									var modalDialog = new $.SalesPortal.ModalDialog({
										title: '<span><img src="/images/shortcuts/search-bar/search-bar-no-results-warning.png"></span>' +
										'<span style="margin-left: 20px">Search: “<b>' + searchBarConditions.get('text') + '</b>”</span>',
										description: 'Sorry, but there are no files on the site with this specific word or phrase.<br><br>' +
										'You might want to try a simpler, more general keyword search.<br><br>' +
										'<i>For Example</i>:<br>' +
										'Instead of searching for “<i>Doppler Mobile Weather App</i>”<br><br>' +
										'You might try searching for: “<i>Weather App</i>”',
										width: 500,
										buttons: [
											{
												tag: 'ok',
												title: 'Continue',
												clickHandler: function ()
												{
													modalDialog.close();
												}
											}
										]
									});
									modalDialog.show();
								}
							});
						}
						else
						{
							window.open("shortcuts/GetQuickSearchResult?linkId=" + parentShortcutData.linkId +
								"&text=" + searchBarConditions.get('text') +
								"&textExactMatch=" + searchBarConditions.get('exactMatch') +
								"&onlyFiles=" + searchBarConditions.get('onlyFileNames') +
								"&onlyNewFiles=" + searchBarConditions.get('onlyNewFiles') +
								"&fileTypesInclude=" + $.toJSON(searchBarConditions.getFileTypesSettings().selectedTypeTags()) +
								"&superFilters=" + $.toJSON(searchBarConditions.getSuperFiltersSettings()) +
								"&categories=" + $.toJSON(searchBarConditions.getCategorySettings()));
						}
					});
					modalContent.find('.cancel-button').off('click.search-bar').on('click.search-bar', function ()
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
			var hasKeyword = $('.shortcuts-search-bar .search-bar-text').val() !== "";
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
			var categoriesStr = searchBarConditions.getCategoryDescription().join(', ');
			searchBar.find('.tag-condition-selected small').html(categoriesStr !== "" ? (searchBar.find('.tags-filter-panel-switcher').html() + ': ' + categoriesStr) : '');
			updateSize();
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
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var content = $(msg);

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: 'Search Bar'},
						formContent: content
					});

					var fileSettings = searchBarConditions.getFileTypesSettings();
					content.find('#search-bar-edit-file-power-point').prop('checked', fileSettings.showPowerPoint);
					content.find('#search-bar-edit-file-video').prop('checked', fileSettings.showVideo);
					content.find('#search-bar-edit-file-others').prop('checked', fileSettings.showPdf || fileSettings.showWord || fileSettings.showExcel || fileSettings.showImages || fileSettings.showUrls);

					content.find('#search-bar-edit-file-names').prop('checked', searchBarConditions.get('onlyFileNames'));
					content.find('#search-bar-edit-exact-match').prop('checked', searchBarConditions.get('exactMatch'));
					content.find('#search-bar-edit-only-new-files').prop('checked', searchBarConditions.get('onlyNewFiles'));

					content.find('.accept-button').off('click.search-bar').on('click.search-bar', function ()
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
					content.find('.cancel-button').off('click.search-bar').on('click.search-bar', function ()
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
			var categorySelector = searchBar.find('.tag-condition-selector-wrapper');
			categorySelector.find('.tag-condition-selector').addClass('logger-form');

			$.fancybox({
				content: categorySelector.html(),
				title: $('.tags-filter-panel-switcher').html(),
				autoSize: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					var innerContent = $('.fancybox-skin');

					innerContent.css({
						'padding': 0
					});

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: 'Search Bar'},
						formContent: innerContent
					});

					var categoryFilters = searchBarConditions.getCategoryFilters();
					var categoriesFiltersContent = innerContent.find(".category-filter-list");
					var categoriesContent = innerContent.find(".category-list");
					var tagsContent = innerContent.find(".tag-list");
					var selectedCategories = searchBarConditions.getCategorySettings();

					var updateCategoryItemsAccordingFilter = function ()
					{
						var selectedFilter = categoriesFiltersContent.find('li.selected');
						var filterText = selectedFilter.find('a .text').text();

						var allCategoryItems = categoriesContent.find('.category');
						var underlinedCategoryItems = categoriesContent.find('.category[data-category-filter="' + filterText + '"]');
						allCategoryItems.hide();
						underlinedCategoryItems.show();

						allCategoryItems.removeClass('selected');
						underlinedCategoryItems.first().addClass('selected');

						updateCategoryTagsAccordingCategory();
					};

					var updateCategoryTagsAccordingCategory = function ()
					{
						var selectedCategory = categoriesContent.find('li.selected');
						var categoryText = selectedCategory.find('a span').text();

						var allCategoryTags = tagsContent.find('.tag-group');
						var underlinedCategoryTags = tagsContent.find('.tag-group[data-category="' + categoryText + '"]');
						allCategoryTags.hide();
						underlinedCategoryTags.show();
					};

					var updateCategoriesAccordingSelection = function ()
					{
						selectedCategories = [];
						var tagGroups = tagsContent.find('.tag-group');
						$.each(tagGroups, function ()
						{
							var tagGroup = $(this);
							var tagSelectors = tagGroup.find('.tag-selector');
							var checkedSelectors = tagSelectors.find('.tag:checked');
							if (checkedSelectors.length > 0)
							{
								var tags = [];
								var categoryName = tagGroup.data('category');
								$.each(tagSelectors, function ()
								{
									var tagSelector = $(this);
									var tagCheckBox = tagSelector.find('.tag');
									if (tagCheckBox.prop('checked') === true)
										tags.push(tagSelector.find('.name').text());
								});
								selectedCategories.push({
									name: categoryName,
									items: tags
								})
							}
						});
					};

					var updateCategoriesLabel = function ()
					{
						var tagsTextArray = [];
						$.each(selectedCategories, function (categoryIndex, category)
						{
							tagsTextArray.push(category.items.join(', '));
						});
						innerContent.find(".selected-category-label span").text(tagsTextArray.length > 0 ? tagsTextArray.join(', ') : 'No categories selected');
					};

					if (categoryFilters.length > 0)
					{
						$.each(categoryFilters, function (index, value)
						{
							categoriesFiltersContent.find('li:contains("' + value + '")').addClass('selected');
						});
					}
					else
						categoriesFiltersContent.find('li').first().addClass('selected');


					categoriesFiltersContent.find('li').off('click').on('click', function ()
					{
						var listItem = $(this);
						if (!listItem.hasClass('selected'))
						{
							categoriesFiltersContent.find('li').removeClass('selected');
							listItem.addClass('selected');
							updateCategoryItemsAccordingFilter();
						}
					});

					categoriesContent.find('li').off('click').on('click', function ()
					{
						var listItem = $(this);
						if (!listItem.hasClass('selected'))
						{
							categoriesContent.find('li').removeClass('selected');
							listItem.addClass('selected');
							updateCategoryTagsAccordingCategory();
						}
					});

					tagsContent.find('.select-all-selector input').off('change').on('change', function ()
					{
						$(this).closest('.tag-group').find('.tag-selector .tag').prop('checked', $(this).is(':checked'));
						updateCategoriesAccordingSelection();
						updateCategoriesLabel();
					});

					tagsContent.find('.tag').off('change').on('change', function ()
					{
						updateCategoriesAccordingSelection();
						updateCategoriesLabel();
					});

					innerContent.find('.tags-clear-all').off('click.search-bar').on('click.search-bar', function ()
					{
						tagsContent.find(":checked").prop('checked', false);
						updateCategoriesAccordingSelection();
						updateCategoriesLabel();
					});

					updateCategoryItemsAccordingFilter();

					if (selectedCategories.length > 0)
					{
						$.each(tagsContent.find('.tag-group'), function ()
						{
							var tagGroup = $(this);
							var categoryName = tagGroup.data('category');
							var tagSelectors = tagGroup.find('.tag-selector');
							var selectAllSelector = tagGroup.find('.select-all-selector');
							$.each(selectedCategories, function (groupIndex, group)
							{
								if (group.name === categoryName)
								{
									if (group.items.length === tagSelectors.length)
									{
										tagSelectors.find('.tag').prop('checked', true);
										selectAllSelector.find('input').prop('checked', true);
									}
									else
									{
										$.each(tagSelectors, function ()
										{
											var tagSelector = $(this);
											var tagCheckBox = tagSelector.find('.tag');
											var tagName = tagSelector.find('.name').text();
											$.each(group.items, function (itemIndex, item)
											{
												if (item === tagName)
													tagCheckBox.prop('checked', true);
											});
										});
										selectAllSelector.find('input').prop('checked', false);
									}
								}
							});
						});
					}

					updateCategoriesLabel();

					innerContent.find('.cancel-button').on('click.search-bar', function ()
					{
						$.fancybox.close();
					});

					innerContent.find('.accept-button').on('click.search-bar', function ()
					{
						var selectedCategoryFilters = [];
						var selectedFilter = categoriesFiltersContent.find('li.selected');
						var filterText = selectedFilter.find('a .text').text();
						selectedCategoryFilters.push(filterText);
						searchBarConditions.setCategoryFilters(selectedCategoryFilters);

						updateCategoriesAccordingSelection();
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

			shortcutActionsContainer.find('.show-search').off('click.action').on('click.action', function ()
			{
				changeVisibility(true);
				updateSearchToggleButtonState();
			});

			shortcutActionsContainer.find('.hide-search').off('click.action').on('click.action', function ()
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
			updateSize();
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

		var updateSize = function ()
		{
			parameters.sizeChangedCallback();
		};

		if (searchBar.length > 0)
		{
			if (parameters === undefined)
				parameters = {
					shortcutData: undefined,
					sizeChangedCallback: undefined
				};
			parameters.shortcutData = parameters.shortcutData !== undefined ? parameters.shortcutData : null;
			parameters.sizeChangedCallback = parameters.sizeChangedCallback !== undefined ? parameters.sizeChangedCallback : function ()
			{
			};

			var parentShortcutData = parameters.shortcutData;
			var searchBarOptions = new $.SalesPortal.SearchOptions($.parseJSON(searchBar.find('.search-conditions .encoded-object').text()));
			var searchViewOptions = new $.SalesPortal.SearchResultsDataViewOptions($.parseJSON(searchBar.find('.search-view-options .encoded-object').text()));
			var searchBarConditions = new $.SalesPortal.SearchConditions(function ()
			{
			});

			setDefaultSettings();
		}
		init();
	};
})(jQuery);