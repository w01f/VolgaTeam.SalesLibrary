(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var SearchManager = function ()
	{
		var that = this;

		var dataTable = new $.SalesPortal.SearchDataTable(true);

		var currentSearchConditions = new $.SalesPortal.SearchConditions(function ()
		{
			updateFilterString();
		});

		var existedSearchResults = undefined;

		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getSearchView",
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					$('#content').html(msg);
					applySearchConditions();
					updateContentSize();
				},
				async: true,
				dataType: 'html'
			});
			$(window).off('resize').on('resize', updateContentSize);
		};

		this.initRibbonControls = function ()
		{
			var textCondition = $('#search-ribbon-condition-content-value');
			textCondition
				.off('input').on('input', function ()
				{
					var text = $(this).val();
					if (text != '')
						currentSearchConditions.set('text', text);
					else
						currentSearchConditions.set('text', null);
				})
				.off('keypress').on('keypress', function (e)
				{
					if (e.which == 13)
						runSearch();
				});
			$("#search-ribbon-clear-content-value").off('click').on('click', function ()
			{
				textCondition.val('');
			});

			var dateFormat = 'MM/DD/YYYY';
			var dateCondition = $('#search-ribbon-condition-date-container');
			dateCondition.daterangepicker(
				{
					format: dateFormat,
					ranges: {
						'Last day': [moment().subtract('day', 1).startOf('day'), moment()],
						'Last 15 Days': [moment().subtract('days', 14), moment()],
						'Last 30 Days': [moment().subtract('days', 29), moment()]
					}
				},
				function (start, end)
				{
					dateCondition.find('input').val(start.format(dateFormat) + ' - ' + end.format(dateFormat));
					currentSearchConditions.set('dateStart', start.format(dateFormat));
					currentSearchConditions.set('dateEnd', end.format(dateFormat));
				}
			);

			$("#search-ribbon-condition-date-clear").off('click').on('click', function ()
			{
				dateCondition.find('input').val('');
			});

			$("#search-ribbon-filter").off('click').on('click', function ()
			{
				editSearchConditions('settings',
					function (content)
					{
						content.find('#search-filter-edit-file-names-only').prop('checked', currentSearchConditions.get('onlyFileNames'));
						content.find('#search-filter-edit-exact-match').prop('checked', currentSearchConditions.get('exactMatch'));
					},
					function (content)
					{
						currentSearchConditions.set('onlyFileNames', content.find('#search-filter-edit-file-names-only').prop('checked'));
						currentSearchConditions.set('exactMatch', content.find('#search-filter-edit-exact-match').prop('checked'));
					}
				);
			});

			$("#search-ribbon-file-types").off('click').on('click', function ()
			{
				editSearchConditions('fileTypes',
					function (content)
					{
						var fileSettings = currentSearchConditions.getFileTypesSettings();
						content.find('#search-filter-edit-file-power-point').prop('checked', fileSettings.showPowerPoint);
						content.find('#search-filter-edit-file-video').prop('checked', fileSettings.showVideo);
						content.find('#search-filter-edit-file-pdf').prop('checked', fileSettings.showPdf);
						content.find('#search-filter-edit-file-word').prop('checked', fileSettings.showWord);
						content.find('#search-filter-edit-file-excel').prop('checked', fileSettings.showExcel);
						content.find('#search-filter-edit-file-image').prop('checked', fileSettings.showImages);
						content.find('#search-filter-edit-file-url').prop('checked', fileSettings.showUrls);
					},
					function (content)
					{
						currentSearchConditions.setFileTypesSettings({
							showPowerPoint: content.find('#search-filter-edit-file-power-point').prop('checked'),
							showVideo: content.find('#search-filter-edit-file-video').prop('checked'),
							showPdf: content.find('#search-filter-edit-file-pdf').prop('checked'),
							showWord: content.find('#search-filter-edit-file-word').prop('checked'),
							showExcel: content.find('#search-filter-edit-file-excel').prop('checked'),
							showImages: content.find('#search-filter-edit-file-image').prop('checked'),
							showUrls: content.find('#search-filter-edit-file-url').prop('checked')
						});
					}
				);
			});

			$("#search-ribbon-tags").off('click').on('click', function ()
			{
				editSearchConditions('categories',
					function (content)
					{
						var categories = currentSearchConditions.getCategorySettings();
						var categoriesPane = content.find('#search-filter-edit-categories');
						var groupsCount = categoriesPane.find('.group-selector-container').length;

						if (categories.length > 0)
						{
							$.each(categoriesPane.find('>.checkbox'), function ()
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

						categoriesPane.accordion({
							heightStyle: "content",
							active: groupsCount > 1 ? false : 0,
							collapsible: groupsCount > 1,
							icons: {
								header: "ui-icon-circle-arrow-e",
								activeHeader: "ui-icon-circle-arrow-s"
							}
						});

						categoriesPane.find('.group-selector').off('change').on('change', function ()
						{
							$(this).closest('.group-checkbox').find('.tag-selector').prop('checked', $(this).is(':checked'));
						});

						content.find('#search-filter-edit-clear-all').off('click').on('click', function ()
						{
							categoriesPane.find(":checked").prop('checked', false);
						});
					},
					function (content)
					{
						var categories = [];
						var categoriesPane = content.find('#search-filter-edit-categories');
						var allCheckBoxes = categoriesPane.find('.tag-selector');
						var selectedCheckBoxes = categoriesPane.find('.tag-selector-container :checked');
						if (allCheckBoxes.length != selectedCheckBoxes.length)
							$.each(categoriesPane.find('>.checkbox'), function ()
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
									categories.push({
										name: groupName,
										items: tags
									})
								}
							});
						currentSearchConditions.setCategorySettings(categories);
					}
				);
			});

			$("#search-ribbon-super-filters").off('click').on('click', function ()
			{
				editSearchConditions('superFilters',
					function (content)
					{
						var superFilters = currentSearchConditions.getSuperFiltersSettings();
						$.each(superFilters, function (index, item)
						{
							$.each(content.find('.checkbox'), function ()
							{
								if ($(this).find('.name').text() == item)
									$(this).find('input[type="checkbox"]').prop('checked', true);
							});
						});
					},
					function (content)
					{
						var items = [];
						var allCheckBoxes = content.find('.checkbox');
						var selectedCheckBoxes = content.find('.checkbox :checked');
						if (allCheckBoxes.length != selectedCheckBoxes.length)
							$.each(allCheckBoxes, function ()
							{
								if ($(this).find('input[type="checkbox"]').prop('checked') == true)
									items.push($(this).find('.name').text());
							});
						currentSearchConditions.setSuperFiltersSettings(items);
					}
				);
			});

			$("#search-ribbon-libraries").off('click').on('click', function ()
			{
				editSearchConditions('libraries',
					function (content)
					{
						var libraries = currentSearchConditions.getLibrarySettings();
						var librariesPane = content.find('#search-filter-edit-libraries');
						var groupsCount = librariesPane.find('h3').length;

						$.each(librariesPane.find('input[type="checkbox"]'), function ()
						{
							var checkBox = $(this);
							var libraryId = checkBox.val();
							if (libraries.length > 0)
								$.each(libraries, function ()
								{
									if (this.id == libraryId)
										checkBox.prop('checked', true);
								});
							else
								checkBox.prop('checked', true);
						});

						librariesPane.accordion({
							heightStyle: "content",
							active: groupsCount > 1 ? false : 0,
							collapsible: groupsCount > 1,
							icons: {
								header: "ui-icon-circle-arrow-e",
								activeHeader: "ui-icon-circle-arrow-s"
							}
						});

						content.find('#search-filter-edit-select-all').off('click').on('click', function ()
						{
							librariesPane.find('input[type="checkbox"]').prop('checked', true);
						});

						content.find('#search-filter-edit-clear-all').off('click').on('click', function ()
						{
							librariesPane.find(":checked").prop('checked', false);
						});
					},
					function (content)
					{
						var items = [];
						var allCheckBoxes = content.find('#search-filter-edit-libraries .checkbox');
						var selectedCheckBoxes = content.find('#search-filter-edit-libraries :checked');
						if (allCheckBoxes.length != selectedCheckBoxes.length)
							$.each(allCheckBoxes, function ()
							{
								if ($(this).find('input[type="checkbox"]').prop('checked') == true)
									items.push({
										id: $(this).find('input[type="checkbox"]').val(),
										name: $(this).find('.name').text()
									});
							});
						currentSearchConditions.setLibrarySettings(items);
					}
				);
			});

			$("#search-ribbon-run").off('click').on('click', function ()
			{
				runSearch();
			});

			$("#search-ribbon-clear").off('click').on('click', function ()
			{
				clearSearchConditions();
				that.init();
			});
		};

		var runSearch = function ()
		{
			$.SalesPortal.SearchHelper.runSearchJson(
				{
					datasetKey: undefined,
					conditions: $.toJSON(currentSearchConditions.getConditionsFormatted())
				},
				function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				function (data)
				{
					existedSearchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
					initDataTable();
					updateContentSize();
				}
			);
		};

		var editSearchConditions = function (conditionTag, initCallback, acceptCallback)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/editConditions",
				data: {
					conditionTag: conditionTag
				},
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

					initCallback(content);

					content.find('.accept-button').off('click').on('click', function ()
					{
						acceptCallback(content);
						$.fancybox.close();
					});
					content.find('.cancel-button').off('click').on('click', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: content,
						width: 500,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						}
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var applySearchConditions = function ()
		{
			$("#search-ribbon-condition-content-value").val(currentSearchConditions.get('text'));
			var dateStart = currentSearchConditions.get('dateStart');
			var dateEnd = currentSearchConditions.get('dateEnd');
			$('#search-ribbon-condition-date-container').find('input').val((dateStart != null && dateEnd != null) ? (dateStart + ' - ' + dateEnd) : '');
			currentSearchConditions.raiseOnChange();

			initDataTable();
		};

		var clearSearchConditions = function ()
		{
			dataTable.clear();
			existedSearchResults = undefined;
			currentSearchConditions.clear();
			applySearchConditions();
		};

		var updateFilterString = function ()
		{
			var filterDescriptionContainer = $('#content').find('.search-filters');

			{
				var settingsFilterDescription = filterDescriptionContainer.find('.settings');
				var filters = [];
				if (currentSearchConditions.get('onlyFileNames'))
					filters.push('file names only');
				else
					filters.push('full database');
				if (currentSearchConditions.get('exactMatch'))
					filters.push('exact search');
				else
					filters.push('partial search');
				settingsFilterDescription.find('.value').html(filters.join(', '));
			}

			{
				var fileTypesFilterDescription = filterDescriptionContainer.find('.file-types');
				var fileTypesFilterWarning = filterDescriptionContainer.find('.file-types-warning');
				var fileTypes = currentSearchConditions.getFileTypesDescription();
				if (fileTypes.length > 0)
				{
					fileTypesFilterDescription.find('.value').html(fileTypes.join(', '));
					fileTypesFilterDescription.show();
					fileTypesFilterWarning.hide();
				}
				else
				{
					fileTypesFilterDescription.hide();
					fileTypesFilterWarning.show();
				}
			}

			{
				var categoriesFilterDescription = filterDescriptionContainer.find('.categories');
				var categories = currentSearchConditions.getCategoryDescription();
				if (categories.length > 0)
				{
					categoriesFilterDescription.find('.value').html(categories.join(', '));
					categoriesFilterDescription.show();
				}
				else
					categoriesFilterDescription.hide();
			}

			{
				var superTagsFilterDescription = filterDescriptionContainer.find('.super-filters');
				var superFilters = currentSearchConditions.getSuperFiltersSettings();
				if (superFilters.length > 0)
				{
					superTagsFilterDescription.find('.value').html(superFilters.join(', '));
					superTagsFilterDescription.show();
				}
				else
					superTagsFilterDescription.hide();
			}

			{
				var librariesFilterDescription = filterDescriptionContainer.find('.libraries');
				var libraries = currentSearchConditions.getLibrariesDescription();
				if (libraries.length > 0)
				{
					librariesFilterDescription.find('.value').html(libraries.join(', '));
					librariesFilterDescription.show();
				}
				else
				{
					librariesFilterDescription.hide();
				}
			}
		};

		var initDataTable = function ()
		{
			dataTable.init(existedSearchResults);
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			dataTable.updateSize();
		};
	};

	$.SalesPortal.Search = new SearchManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Search.initRibbonControls();
	});
})(jQuery);

