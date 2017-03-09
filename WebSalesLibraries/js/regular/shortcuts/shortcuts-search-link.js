(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSearchLink = function (data)
	{
		var that = this;

		var parentSearchData = data;

		if (parentSearchData.content != undefined)
		{
			$.SalesPortal.Content.fillContent({
				content: parentSearchData.content,
				headerOptions: {
					title: parentSearchData.options.headerTitle,
					icon: parentSearchData.options.headerIcon,
					titleHideCondition: parentSearchData.options.headerTitleHideCondition,
					iconHideCondition: parentSearchData.options.headerIconHideCondition
				},
				actions: parentSearchData.actions,
				navigationPanel: parentSearchData.navigationPanel,
				resizeCallback: function ()
				{
					updateContentSize();
				}
			});
		}

		var optionsContainer = data.optionsContainer != undefined ? data.optionsContainer : $.SalesPortal.Content.getContentObject();
		var searchShortcutOptions = new $.SalesPortal.SearchOptions($.parseJSON(optionsContainer.find('.search-conditions .encoded-object').text()));
		var searchViewOptions = new $.SalesPortal.SearchViewOptions($.parseJSON(optionsContainer.find('.search-view-options .encoded-object').text()));
		var content = $.SalesPortal.Content.getContentObject();

		var baseDatasetKey = undefined;

		var dataTable = new $.SalesPortal.SearchDataTable(
			{
				tableContainerSelector: '.shortcuts-search-link-data-table-container',
				saveState: false,
				backHandler: parentSearchData.backHandler,
				logHandler: function ()
				{
					$.SalesPortal.ShortcutsManager.trackActivity(
						{details: {File: searchShortcutOptions.title}},
						'Search Activity',
						'Search Activity');
				}
			}
		);

		this.runSearch = function (resultCallback)
		{
			$.SalesPortal.SearchHelper.runSearch(
				{
					datasetKey: undefined,
					conditions: $.toJSON(searchShortcutOptions.conditions)
				},
				function ()
				{
					$.SalesPortal.Overlay.show();
				},
				function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				function (data)
				{
					if (data.dataset.length > 0)
					{
						$.SalesPortal.Content.getContentObject().html('<table id="shortcuts-search-container" style="table-layout:fixed;"><tr>' +
							'<td id="right-navbar" style="display: none;"></td>' +
							'<td>' +
							'<div class="data-table-content-container shortcuts-search-link-data-table-container"></div>' +
							'</td>' +
							'</tr></table>');

						baseDatasetKey = data.datasetKey;
						if (searchShortcutOptions.subSearchDefaultView == 'all')
							SimpleSearch(content);
						else if (searchShortcutOptions.subSearchDefaultView == 'search')
							SubSearchCustom(content);
						else if (searchShortcutOptions.subSearchDefaultView == 'links')
							SubSearchByTemplates(content);

						initActionButtons();
						$(window).off('resize.search-link').on('resize.search-link', updateContentSize);
					}
					if (resultCallback != undefined)
						resultCallback(data);
				}
			);
		};

		var SimpleSearch = function (content)
		{
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function ()
			{
				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: baseDatasetKey
					},
					function ()
					{
						sideBar.hide();
						$.SalesPortal.Overlay.show();
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataOptions: searchViewOptions,
							sortColumnTag: searchShortcutOptions.conditions.sortColumn,
							sortDirection: searchShortcutOptions.conditions.sortDirection
						});
						updateContentSize();
					}
				);
			};
			getSearchResults();
		};

		var SubSearchCustom = function (content)
		{
			var sideBar = content.find('#right-navbar');

			var customSearchData = new $.SalesPortal.SearchConditions(function ()
			{
			});

			var getSearchResults = function ()
			{
				var customSearchConditions = customSearchData.getConditionsFormatted();

				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: undefined,
						conditions: $.toJSON({
							text: customSearchConditions.text,
							textExactMatch: customSearchConditions.textExactMatch,
							fileTypes: customSearchConditions.fileTypes,
							libraries: searchShortcutOptions.conditions.libraries,
							superFilters: customSearchConditions.superFilters,
							categories: customSearchConditions.categories,
							onlyWithCategories: searchShortcutOptions.conditions.onlyWithCategories,
							onlyByName: customSearchConditions.onlyByName,
							baseDatasetKey: baseDatasetKey,
							datasetKey: null
						})
					},
					function ()
					{
						$.SalesPortal.Overlay.show();
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataOptions: searchViewOptions,
							sortColumnTag: searchShortcutOptions.conditions.sortColumn,
							sortDirection: searchShortcutOptions.conditions.sortDirection
						});
						updateContentSize();
					}
				);
			};

			var initPanel = function ()
			{
				var formLogger = new $.SalesPortal.FormLogger();
				formLogger.init({
					logObject: {name: searchShortcutOptions.title},
					formContent: sideBar
				});

				sideBar.find('.tags-filter-panel-switcher').off('click.search-options').on('click.search-options', function ()
				{
					editTagsCondition();
				});

				sideBar.find('.search-bar-text')
					.off('input').on('input', function ()
				{
					var text = $(this).val();
					if (text != '')
						customSearchData.set('text', text);
					else
						customSearchData.set('text', null);
				})
					.off('keypress').on('keypress', function (e)
				{
					if (e.which == 13)
						getSearchResults();
				});

				sideBar.find('.file-filter-panel .file-selector input').off('change').on('change', function ()
				{
					applySelectedFileTypes();
					applySearchSettings();
				});

				sideBar.find('.search-bar-run').on('click', function ()
				{
					getSearchResults();
				});

				applySelectedFileTypes();
				applySearchSettings();
			};

			var applySelectedFileTypes = function ()
			{
				customSearchData.setFileTypesSettings({
					showPowerPoint: sideBar.find('#search-file-type-powerpoint').prop('checked'),
					showVideo: sideBar.find('#search-file-type-video').prop('checked'),
					showPdf: sideBar.find('#search-file-type-other').prop('checked'),
					showWord: sideBar.find('#search-file-type-other').prop('checked'),
					showExcel: sideBar.find('#search-file-type-other').prop('checked'),
					showImages: sideBar.find('#search-file-type-other').prop('checked'),
					showUrls: sideBar.find('#search-filter-edit-file-url').prop('checked')
				});
			};

			var applySearchSettings = function ()
			{
				customSearchData.set('onlyFileNames', sideBar.find('#search-file-names-only').prop('checked'));
				customSearchData.set('exactMatch', sideBar.find('#search-exact-match').prop('checked'));
			};

			var editTagsCondition = function ()
			{
				var categoryConditions = sideBar.find('.search-conditions');
				var categorySelector = categoryConditions.find('.tag-condition-selector-wrapper');
				categorySelector.find('.tag-condition-selector').addClass('logger-form');
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

						var formLogger = new $.SalesPortal.FormLogger();
						formLogger.init({
							logObject: {name: searchShortcutOptions.title},
							formContent: innerContent
						});

						var categoriesContent = innerContent.find(".category-list");
						var categories = customSearchData.getCategorySettings();
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
						var superFilters = customSearchData.getSuperFiltersSettings();
						$.each(superFilters, function (index, value)
						{
							superFiltersContent.find('.btn:contains("' + value + '")').button('toggle');
						});
						superFiltersContent.find('.btn').off('click.search-options').on('click.search-options', function ()
						{
							$(this).button('toggle').blur();
						});


						innerContent.find('.tags-clear-all').off('click.search-options').on('click.search-options', function ()
						{
							superFiltersContent.find('.btn').removeClass('active').blur();
							categoriesContent.find(":checked").prop('checked', false);
						});

						innerContent.find('.cancel-button').on('click.search-app', function ()
						{
							$.fancybox.close();
						});

						innerContent.find('.accept-button').on('click.search-app', function ()
						{
							var selectedSuperFilters = [];
							var allSuperFilterButtons = superFiltersContent.find('.btn');
							var selectedSuperFilterButtons = superFiltersContent.find('.btn.active');
							if (allSuperFilterButtons.length != selectedSuperFilterButtons.length)
								$.each(selectedSuperFilterButtons, function ()
								{
									selectedSuperFilters.push($(this).text());
								});
							customSearchData.setSuperFiltersSettings(selectedSuperFilters);

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
							customSearchData.setCategorySettings(selectedCategories);

							var superFiltersStr = customSearchData.getSuperFiltersSettings().join(', ');
							var categoriesStr = customSearchData.getCategoryDescription().join(', ');
							var categoryStr = '';
							if (categoriesStr != '' || superFiltersStr != '')
								categoryStr = [superFiltersStr, categoriesStr].join(', ');
							else if (categoriesStr != '')
								categoryStr = categoriesStr;
							else if (superFiltersStr != '')
								categoryStr = superFiltersStr;
							var selectedCategoryLabel = sideBar.find('.tag-condition-selected small');
							if (categoryStr != "")
								selectedCategoryLabel.html(sideBar.find('.tags-filter-panel-switcher').html() + ': ' + categoryStr);
							else
								selectedCategoryLabel.html('');

							$.fancybox.close();
						});
					}
				});
			};

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchCustomPanel",
				data: {
					bundleId: searchShortcutOptions.isSearchBar ? parentSearchData.options.linkId : undefined,
					linkId: searchShortcutOptions.isSearchBar ? undefined : parentSearchData.options.linkId
				},
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
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					updateContentSize();
					getSearchResults();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var SubSearchByTemplates = function (content)
		{
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function ()
			{
				var selectedTemplateConditions = new $.SalesPortal.SearchOptions($.parseJSON(sideBar.find('a.opened .search-conditions .encoded-object').text()));

				var textCondition = selectedTemplateConditions.conditions.text;
				var templateFileTypes = selectedTemplateConditions.conditions.fileTypes;
				var templateStartDate = selectedTemplateConditions.conditions.startDate;
				var templateEndDate = selectedTemplateConditions.conditions.endDate;

				var templateLibraries = selectedTemplateConditions.conditions.libraries;
				var templateSuperFilters = selectedTemplateConditions.conditions.superFilters;
				var templateCategories = selectedTemplateConditions.conditions.categories;
				var templateOnlyWithCategories = selectedTemplateConditions.conditions.onlyWithCategories;
				var templateOnlyByName = selectedTemplateConditions.conditions.onlyByName;

				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: undefined,
						conditions: $.toJSON({
							text: textCondition,
							textExactMatch: false,
							fileTypes: templateFileTypes,
							startDate: templateStartDate,
							endDate: templateEndDate,
							libraries: templateLibraries,
							superFilters: templateSuperFilters,
							categories: templateCategories,
							onlyWithCategories: templateOnlyWithCategories,
							onlyByName: templateOnlyByName,
							baseDatasetKey: baseDatasetKey,
							datasetKey: null
						})
					},
					function ()
					{
						$.SalesPortal.Overlay.show();
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataOptions: searchViewOptions,
							sortColumnTag: selectedTemplateConditions.conditions.sortColumn != undefined ?
								selectedTemplateConditions.conditions.sortColumn :
								searchShortcutOptions.conditions.sortColumn,
							sortDirection: selectedTemplateConditions.conditions.sortDirection != undefined ?
								selectedTemplateConditions.conditions.sortDirection :
								searchShortcutOptions.conditions.sortDirection
						});
						updateContentSize();
					}
				);
			};

			var initPanel = function ()
			{
				var logoSelector = sideBar.find('.logo-list ul');
				sideBar.find('.logo-list ul li.enabled img').tooltip({
					animation: false,
					trigger: 'hover',
					placement: 'bottom',
					delay: {show: 500, hide: 100}
				});
				var selectorItems = logoSelector.find('li.enabled a');
				var firstItem = selectorItems.first();
				if (firstItem != null)
					firstItem.addClass('opened');
				selectorItems.on('click.search-app', function ()
				{
					selectorItems.removeClass('opened');
					$(this).addClass('opened');
					getSearchResults();
				});
			};

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchTemplatesPanel",
				data: {
					bundleId: searchShortcutOptions.isSearchBar ? parentSearchData.options.linkId : undefined,
					linkId: searchShortcutOptions.isSearchBar ? undefined : parentSearchData.options.linkId
				},
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
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					updateContentSize();
					getSearchResults();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			if (searchShortcutOptions.enableSubSearch)
			{
				shortcutActionsContainer.find('.sub-search-action').show();
				if (searchShortcutOptions.subSearchDefaultView == 'all')
					shortcutActionsContainer.find('.sub-search-all').hide();
				else if (searchShortcutOptions.subSearchDefaultView == 'search')
					shortcutActionsContainer.find('.sub-search-criteria').hide();
				else if (searchShortcutOptions.subSearchDefaultView == 'links')
					shortcutActionsContainer.find('.sub-search-links').hide();

				shortcutActionsContainer.find('.sub-search-all').off('click.action').on('click.action', function ()
				{
					SimpleSearch(content);
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});

				shortcutActionsContainer.find('.sub-search-criteria').off('click.action').on('click.action', function ()
				{
					SubSearchCustom(content);
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});

				shortcutActionsContainer.find('.sub-search-links').off('click.action').on('click.action', function ()
				{
					SubSearchByTemplates(content);
					shortcutActionsContainer.find('.sub-search-action').show();
					$(this).hide();
				});
			}
			else
			{
				shortcutActionsContainer.find('.sub-search-action').hide();
			}
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.ShortcutsManager.updateContentSize();

			var content = $.SalesPortal.Content.getContentObject();
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();
			var sideBar = content.find('#right-navbar');

			var height = content.outerHeight(true);
			var width = $(window).width() - navigationPanel.outerWidth(true) - 5;

			$('#content').css({
				'overflow': 'hidden'
			});

			content.css({
				'max-width': width + 'px',
				'width': width + 'px',
				'overflow': 'hidden'
			});

			var shortcutsSearchContainer = $('#shortcuts-search-container');
			shortcutsSearchContainer.css({
				'width': width + 'px'
			});

			sideBar.find('.logo-list').css({
				'height': height + 'px'
			});

			dataTable.updateSize();
		};

		return that;
	};

	$.SalesPortal.SearchOptions = function (data)
	{
		this.title = undefined;
		this.isSearchBar = undefined;
		this.openInSamePage = undefined;

		this.enableSubSearch = undefined;
		this.subSearchDefaultView = undefined;

		this.conditions = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);