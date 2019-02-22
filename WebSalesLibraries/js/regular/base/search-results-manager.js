(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.SearchResultsManager = function (inputSettings) {
		var that = this;
		var searchResultsSettings = new SearchResultsSettings(inputSettings);

		var baseDatasetKey = undefined;
		var dataTable = undefined;

		this.loadResults = function (data) {
			if (data.dataset.length > 0)
			{
				if (searchResultsSettings.hideFixedPanel)
					$.SalesPortal.Content.hideFixedPanels();

				dataTable = new $.SalesPortal.SearchDataTable(
					{
						tableIdentifier: 'search-results-data-table-content' + searchResultsSettings.searchShortcutId,
						tableContainerSelector: '.search-results-data-table-container',
						saveState: false,
						defaultLength: searchResultsSettings.dataViewOptions.defaultPageLength,
						sizeSettings: searchResultsSettings.tableSizeSettings,
						backHandler: searchResultsSettings.backHandler,
						logHandler: function () {
							$.SalesPortal.ShortcutsManager.trackActivity(
								{details: {File: searchResultsSettings.searchShortcutTitle}},
								'Search Activity',
								'Search Activity');
						}
					}
				);

				searchResultsSettings.containerObject.html('<table class="search-results-container" style="table-layout:fixed;"><tr>' +
					'<td class="search-side-bar" style="display: none;"></td>' +
					'<td>' +
					'<div class="data-table-content-container search-results-data-table-container"></div>' +
					'</td>' +
					'</tr></table>');

				baseDatasetKey = data.datasetKey;
				if (searchResultsSettings.subSearchDefaultView === 'all')
					that.showSimpleSearchView();
				else if (searchResultsSettings.subSearchDefaultView === 'search')
					that.showSubSearchView();
				else if (searchResultsSettings.subSearchDefaultView === 'links')
					that.showSearchByTemplateView();
			}
		};

		this.showSimpleSearchView = function () {
			SimpleSearch();
		};

		this.showSubSearchView = function () {
			SubSearchCustom();
		};

		this.showSearchByTemplateView = function () {
			SubSearchByTemplates();
		};

		this.updateContentSize = function () {
			var shortcutsSearchContainer = searchResultsSettings.containerObject.find('.search-results-container');
			var sideBar = searchResultsSettings.containerObject.find('.search-side-bar');
			if (searchResultsSettings.tableSizeSettings === undefined || searchResultsSettings.tableSizeSettings.useFixedSize)
			{
				var width = searchResultsSettings.containerObject.width();
				shortcutsSearchContainer.css({
					'width': width + 'px'
				});

				var height = searchResultsSettings.containerObject.outerHeight(true);
				sideBar.find('.logo-list').css({
					'height': height + 'px'
				});
			}
			else
			{
				shortcutsSearchContainer.css({
					'width': '100%'
				});

				sideBar.find('.logo-list').css({
					'height': '100%'
				});
			}
			if (dataTable !== undefined)
				dataTable.updateSize();
		};

		var SimpleSearch = function () {
			var sideBar = searchResultsSettings.containerObject.find('.search-side-bar');

			var getSearchResults = function () {
				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: baseDatasetKey
					},
					function () {
						sideBar.hide();
						$.SalesPortal.Overlay.show();
					},
					function () {
						$.SalesPortal.Overlay.hide();
					},
					function (data) {
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataViewOptions: searchResultsSettings.dataViewOptions,
							sortColumnTag: searchResultsSettings.baseSearchConditions.sortSettings.columnTag,
							sortDirection: searchResultsSettings.baseSearchConditions.sortSettings.order
						});
						that.updateContentSize();
					}
				);
			};
			getSearchResults();
		};

		var SubSearchCustom = function () {
			var sideBar = searchResultsSettings.containerObject.find('.search-side-bar');

			var customSearchData = new $.SalesPortal.SearchConditions(function () {
			});

			var getSearchResults = function () {
				var customSearchConditions = customSearchData.getConditionsFormatted();

				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: undefined,
						conditions: $.toJSON({
							text: customSearchConditions.text,
							textExactMatch: customSearchConditions.textExactMatch,
							fileTypesInclude: customSearchConditions.fileTypesInclude,
							fileTypesExclude: [],
							libraries: searchResultsSettings.baseSearchConditions.libraries,
							superFilters: customSearchConditions.superFilters,
							categories: customSearchConditions.categories,
							onlyWithCategories: searchResultsSettings.baseSearchConditions.onlyWithCategories,
							onlyByName: customSearchConditions.onlyByName,
							baseDatasetKey: baseDatasetKey,
							datasetKey: null
						})
					},
					function () {
						$.SalesPortal.Overlay.show();
					},
					function () {
						$.SalesPortal.Overlay.hide();
					},
					function (data) {
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataViewOptions: searchResultsSettings.dataViewOptions,
							sortColumnTag: searchResultsSettings.baseSearchConditions.sortSettings.columnTag,
							sortDirection: searchResultsSettings.baseSearchConditions.sortSettings.order
						});
						that.updateContentSize();
					}
				);
			};

			var initPanel = function () {
				var formLogger = new $.SalesPortal.FormLogger();
				formLogger.init({
					logObject: {name: searchResultsSettings.searchShortcutTitle},
					formContent: sideBar
				});

				sideBar.find('.tags-filter-panel-switcher').off('click.search-options').on('click.search-options', function () {
					editTagsCondition();
				});

				sideBar.find('.search-bar-text')
					.off('input').on('input', function () {
					var text = $(this).val();
					if (text !== '')
						customSearchData.set('text', text);
					else
						customSearchData.set('text', null);
				})
					.off('keypress').on('keypress', function (e) {
					if (e.which === 13)
						getSearchResults();
				});

				sideBar.find('.file-filter-panel .file-selector input').off('change').on('change', function () {
					applySelectedFileTypes();
					applySearchSettings();
				});

				sideBar.find('.search-bar-run').on('click', function () {
					getSearchResults();
				});

				applySelectedFileTypes();
				applySearchSettings();
			};

			var applySelectedFileTypes = function () {
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

			var applySearchSettings = function () {
				customSearchData.set('onlyFileNames', sideBar.find('#search-file-names-only').prop('checked'));
				customSearchData.set('exactMatch', sideBar.find('#search-exact-match').prop('checked'));
			};

			var editTagsCondition = function () {
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
					afterShow: function () {
						var innerContent = $('.fancybox-inner');

						var formLogger = new $.SalesPortal.FormLogger();
						formLogger.init({
							logObject: {name: searchResultsSettings.searchShortcutTitle},
							formContent: innerContent
						});

						var categoriesContent = innerContent.find(".category-list");
						var categories = customSearchData.getCategorySettings();
						var groupsCount = categoriesContent.find('.group-selector-container').length;
						if (categories.length > 0)
						{
							$.each(categoriesContent.find('>.checkbox'), function () {
								var groupCheckBoxContainer = $(this);
								var groupName = groupCheckBoxContainer.find('.group-selector-container .name').text();
								var groupCheckBoxItems = groupCheckBoxContainer.find('.checkbox');
								$.each(categories, function (groupIndex, group) {
									if (group.name === groupName)
									{
										if (group.items.length === groupCheckBoxItems.length)
											groupCheckBoxItems.find('.tag-selector').prop('checked', true);
										else
											$.each(groupCheckBoxItems, function () {
												var tagCheckBoxContainer = $(this);
												var tagCheckBox = tagCheckBoxContainer.find('.tag-selector');
												var tagName = tagCheckBoxContainer.find('.name').text();
												$.each(group.items, function (itemIndex, item) {
													if (item === tagName)
														tagCheckBox.prop('checked', true);
												});
											});
									}
								});
							});
						}
						categoriesContent.find('.group-selector').off('change').on('change', function () {
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
						$.each(superFilters, function (index, value) {
							superFiltersContent.find('.btn:contains("' + value + '")').button('toggle');
						});
						superFiltersContent.find('.btn').off('click.search-options').on('click.search-options', function () {
							$(this).button('toggle').blur();
						});


						innerContent.find('.tags-clear-all').off('click.search-options').on('click.search-options', function () {
							superFiltersContent.find('.btn').removeClass('active').blur();
							categoriesContent.find(":checked").prop('checked', false);
						});

						innerContent.find('.cancel-button').on('click.search-app', function () {
							$.fancybox.close();
						});

						innerContent.find('.accept-button').on('click.search-app', function () {
							var selectedSuperFilters = [];
							var allSuperFilterButtons = superFiltersContent.find('.btn');
							var selectedSuperFilterButtons = superFiltersContent.find('.btn.active');
							if (allSuperFilterButtons.length !== selectedSuperFilterButtons.length)
								$.each(selectedSuperFilterButtons, function () {
									selectedSuperFilters.push($(this).text());
								});
							customSearchData.setSuperFiltersSettings(selectedSuperFilters);

							var selectedCategories = [];
							var allCategoryTagCheckBoxes = categoriesContent.find('.tag-selector');
							var selectedCategoryTagCheckBoxes = categoriesContent.find('.tag-selector-container :checked');
							if (allCategoryTagCheckBoxes.length !== selectedCategoryTagCheckBoxes.length)
								$.each(categoriesContent.find('>.checkbox'), function () {
									var groupCheckBoxContainer = $(this);
									var groupCheckBoxItems = groupCheckBoxContainer.find('.checkbox');
									if (groupCheckBoxItems.find('.tag-selector-container :checked').length > 0)
									{
										var tags = [];
										var groupName = groupCheckBoxContainer.find('.group-selector-container .name').text();
										$.each(groupCheckBoxItems, function () {
											var tagCheckBoxContainer = $(this);
											var tagCheckBox = tagCheckBoxContainer.find('.tag-selector');
											if (tagCheckBox.prop('checked') === true)
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
							if (categoriesStr !== '' || superFiltersStr !== '')
								categoryStr = [superFiltersStr, categoriesStr].join(', ');
							else if (categoriesStr !== '')
								categoryStr = categoriesStr;
							else if (superFiltersStr !== '')
								categoryStr = superFiltersStr;
							var selectedCategoryLabel = sideBar.find('.tag-condition-selected small');
							if (categoryStr !== "")
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
					bundleId: searchResultsSettings.isSearchBar ? searchResultsSettings.searchShortcutId : undefined,
					linkId: searchResultsSettings.isSearchBar ? undefined : searchResultsSettings.searchShortcutId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					that.updateContentSize();
					getSearchResults();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		var SubSearchByTemplates = function () {
			var sideBar = searchResultsSettings.containerObject.find('.search-side-bar');

			var getSearchResults = function () {
				var selectedTemplateConditions = new $.SalesPortal.SearchOptions($.parseJSON(sideBar.find('a.opened .search-conditions .encoded-object').text()));

				var textCondition = selectedTemplateConditions.conditions.text;
				var templateFileTypes = selectedTemplateConditions.conditions.fileTypesInclude;
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
							fileTypesInclude: templateFileTypes,
							fileTypesExclude: [],
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
					function () {
						$.SalesPortal.Overlay.show();
					},
					function () {
						$.SalesPortal.Overlay.hide();
					},
					function (data) {
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init({
							dataset: searchResults.dataset,
							dataViewOptions: searchResultsSettings.dataViewOptions,
							sortColumnTag: selectedTemplateConditions.conditions.sortSettings.isConfigured ?
								selectedTemplateConditions.conditions.sortSettings.columnTag :
								searchResultsSettings.baseSearchConditions.sortSettings.columnTag,
							sortDirection: selectedTemplateConditions.conditions.sortSettings.isConfigured ?
								selectedTemplateConditions.conditions.sortSettings.order :
								searchResultsSettings.baseSearchConditions.sortSettings.order
						});
						that.updateContentSize();
					}
				);
			};

			var initPanel = function () {
				var logoSelector = sideBar.find('.logo-list ul');
				sideBar.find('.logo-list ul li.enabled img').tooltip({
					animation: false,
					trigger: 'hover',
					placement: 'bottom',
					delay: {show: 500, hide: 100}
				});
				var selectorItems = logoSelector.find('li.enabled a');
				var firstItem = selectorItems.first();
				if (firstItem !== null)
					firstItem.addClass('opened');
				selectorItems.on('click.search-app', function () {
					selectorItems.removeClass('opened');
					$(this).addClass('opened');
					getSearchResults();
				});
			};

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchTemplatesPanel",
				data: {
					bundleId: searchResultsSettings.isSearchBar ? searchResultsSettings.searchShortcutId : undefined,
					linkId: searchResultsSettings.isSearchBar ? undefined : searchResultsSettings.searchShortcutId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					that.updateContentSize();
					getSearchResults();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};
	};

	var SearchResultsSettings = function (data) {
		this.containerObject = undefined;
		this.baseSearchConditions = undefined;
		this.dataViewOptions = undefined;

		this.subSearchDefaultView = undefined;
		this.searchShortcutId = undefined;
		this.searchShortcutTitle = undefined;
		this.isSearchBar = undefined;
		this.hideFixedPanel = undefined;
		this.backHandler = undefined;
		this.tableSizeSettings = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);