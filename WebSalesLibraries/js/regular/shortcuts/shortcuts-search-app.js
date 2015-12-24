(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchApp = function ()
	{
		var appData = undefined;
		var viewOptions = undefined;
		var dataTable = new $.SalesPortal.SearchDataTable(
			{
				saveState: true,
				logHandler: function ()
				{
					$.SalesPortal.ShortcutsManager.trackActivity(
						$.parseJSON($('<div>' + appData.options.serviceData + '</div>').find('.activity-data').text()),
						'Search Activity',
						'Search Activity');
				}
			}
		);

		var dateFormat = 'MM/DD/YYYY';

		var currentSearchConditions = new $.SalesPortal.SearchConditions(function ()
		{
			updateFilterString();
			updateContentSize();
		});
		var existedSearchResults = undefined;

		this.init = function (data)
		{
			appData = data;

			viewOptions = new $.SalesPortal.SearchViewOptions(appData.options.viewOptions);

			$.SalesPortal.Content.fillContent(appData.content,
				{
					title: appData.options.headerTitle,
					icon: appData.options.headerIcon
				},
				appData.actions);

			initActionButtons();
			applySearchConditions();
			updateContentSize();

			$(window).off('resize.search-app').on('resize.search-app', updateContentSize);
		};

		var initActionButtons = function ()
		{
			var shortcutActionsContainer = $('#shortcut-action-container');

			shortcutActionsContainer.find('.search-app-run').off('click.action').on('click.action', function ()
			{
				runSearch();
			});

			shortcutActionsContainer.find('.search-app-clear').off('click.action').on('click.action', function ()
			{
				dataTable.clear();
				$.SalesPortal.ShortcutsManager.openShortcut(
					$('<div>' + appData.options.serviceData + '</div>'));
			});

			shortcutActionsContainer.find('.search-app-keyword').off('click.action').on('click.action', function ()
			{
				editSearchConditions('keyword',
					function (content)
					{
						content.find('#search-filter-edit-condition-content-value').val(currentSearchConditions.get('text'));
						content.find("#search-filter-edit-condition-clear-content-value").off('click.search-app').on('click.search-app', function ()
						{
							content.find('#search-filter-edit-condition-content-value').val('');
						});
					},
					function (content)
					{
						var text = content.find('#search-filter-edit-condition-content-value').val();
						if (text && text != '')
							currentSearchConditions.set('text', text);
						else
							currentSearchConditions.set('text', null);
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-date-range').off('click.action').on('click.action', function ()
			{
				editSearchConditions('dateRange',
					function (content)
					{
						var dateStart = currentSearchConditions.get('dateStart');
						var dateEnd = currentSearchConditions.get('dateEnd');
						var startDateSelector = content.find('#search-filter-edit-date-range-custom-start-container');
						var endDateSelector = content.find('#search-filter-edit-date-range-custom-end-container');

						if (dateStart && dateEnd)
						{
							if (moment(dateEnd, dateFormat).subtract('days', 1).format(dateFormat) == dateStart)
								content.find('#search-filter-edit-data-range-last-day').prop('checked', true);
							else if (moment(dateEnd, dateFormat).subtract('days', 14).format(dateFormat) == dateStart)
								content.find('#search-filter-edit-data-range-15-days').prop('checked', true);
							else if (moment(dateEnd, dateFormat).subtract('days', 30).format(dateFormat) == dateStart)
								content.find('#search-filter-edit-data-range-30-days').prop('checked', true);
							else
							{
								content.find('#search-filter-edit-data-range-custom').prop('checked', true);
								startDateSelector.find('input').val(dateStart);
								endDateSelector.find('input').val(dateEnd);
								content.find('#search-filter-edit-date-range-custom-start-container button').removeClass('disabled');
								content.find('#search-filter-edit-date-range-custom-start-container input').prop('disabled', false);
								content.find('#search-filter-edit-date-range-custom-end-container button').removeClass('disabled');
								content.find('#search-filter-edit-date-range-custom-end-container input').prop('disabled', false);
							}
						}
						else
							content.find('#search-filter-edit-data-range-last-day').prop('checked', true);

						content.find('.search-filter-edit-data-range-toggle').on('change', function ()
						{
							if ($(this).prop('checked') == true)
								content.find('.search-filter-edit-data-range-toggle').not($(this)).prop('checked', false);
							if (content.find('#search-filter-edit-data-range-custom').prop('checked') == true)
							{
								content.find('#search-filter-edit-date-range-custom-start-container button').removeClass('disabled');
								content.find('#search-filter-edit-date-range-custom-start-container input').prop('disabled', false);
								content.find('#search-filter-edit-date-range-custom-end-container button').removeClass('disabled');
								content.find('#search-filter-edit-date-range-custom-end-container input').prop('disabled', false);
							}
							else
							{
								content.find('#search-filter-edit-date-range-custom-start-container button').addClass('disabled');
								content.find('#search-filter-edit-date-range-custom-start-container input').prop('disabled', true);
								content.find('#search-filter-edit-date-range-custom-start-container input').val('');
								content.find('#search-filter-edit-date-range-custom-end-container button').addClass('disabled');
								content.find('#search-filter-edit-date-range-custom-end-container input').prop('disabled', true);
								content.find('#search-filter-edit-date-range-custom-end-container input').val('');
							}
						});

						startDateSelector.daterangepicker(
							{
								format: dateFormat,
								singleDatePicker: true
							}
						);

						endDateSelector.daterangepicker(
							{
								format: dateFormat,
								singleDatePicker: true
							}
						);
					},
					function (content)
					{
						var dateStart = null;
						var dateEnd = null;
						if (content.find('#search-filter-edit-data-range-last-day').prop('checked') == true)
						{
							dateEnd = moment().format(dateFormat);
							dateStart = moment().subtract('day', 1).startOf('day').format(dateFormat);
						}
						else if (content.find('#search-filter-edit-data-range-15-days').prop('checked') == true)
						{
							dateEnd = moment().format(dateFormat);
							dateStart = moment().subtract('day', 14).startOf('day').format(dateFormat);
						}
						else if (content.find('#search-filter-edit-data-range-30-days').prop('checked') == true)
						{
							dateEnd = moment().format(dateFormat);
							dateStart = moment().subtract('day', 30).startOf('day').format(dateFormat);
						}
						else if (content.find('#search-filter-edit-data-range-custom').prop('checked') == true)
						{
							dateEnd = content.find('#search-filter-edit-date-range-custom-end-container input').val();
							dateStart = content.find('#search-filter-edit-date-range-custom-start-container input').val();
						}
						currentSearchConditions.set('dateStart', dateStart);
						currentSearchConditions.set('dateEnd', dateEnd);
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-filters').off('click.action').on('click.action', function ()
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
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-file-types').off('click.action').on('click.action', function ()
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
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-tags').off('click.action').on('click.action', function ()
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

						content.find('#search-filter-edit-clear-all').off('click.search-app').on('click.search-app', function ()
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
									});
								}
							});
						currentSearchConditions.setCategorySettings(categories);
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-super-tags').off('click.action').on('click.action', function ()
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
					},
					$(this).find('.icon i').prop('class')
				);
			});

			shortcutActionsContainer.find('.search-app-libraries').off('click.action').on('click.action', function ()
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

						content.find('#search-filter-edit-select-all').off('click.search-app').on('click.search-app', function ()
						{
							librariesPane.find('input[type="checkbox"]').prop('checked', true);
						});

						content.find('#search-filter-edit-clear-all').off('click.search-app').on('click.search-app', function ()
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
					},
					$(this).find('.icon i').prop('class')
				);
			});
		};

		var runSearch = function ()
		{
			$.SalesPortal.SearchHelper.runSearch(
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

		var editSearchConditions = function (conditionTag, initCallback, acceptCallback, iconClass)
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

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: appData.options.headerTitle},
						formContent: content
					});

					content.find('.header i').addClass(iconClass);

					initCallback(content);

					content.find('.accept-button').off('click.search-app').on('click.search-app', function ()
					{
						acceptCallback(content);
						$.fancybox.close();
					});
					content.find('.search-button').off('click.search-app').on('click.search-app', function ()
					{
						acceptCallback(content);
						$.fancybox.close();
						runSearch();
					});
					$(document).on('keydown.search', function (e)
					{
						if (e.keyCode == 13)
						{
							e.preventDefault();
							e.stopPropagation();
							acceptCallback(content);
							$.fancybox.close();
							runSearch();
						}
					});
					content.find('.cancel-button').off('click.search-app').on('click.search-app', function ()
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
						},
						afterClose: function ()
						{
							$(document).off('keydown.search');
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
			$('#search-app-condition-date-container').find('input').val((dateStart != null && dateEnd != null) ? (dateStart + ' - ' + dateEnd) : '');
			currentSearchConditions.raiseOnChange();

			initDataTable();
		};

		var updateFilterString = function ()
		{
			var filterDescriptionContainer = $.SalesPortal.Content.getContentObject().find('.applied-filters-block');
			{
				var keywordFilterDescription = filterDescriptionContainer.find('.keyword');
				var keywordValue = currentSearchConditions.get('text');
				keywordFilterDescription.find('.value').html(keywordValue);
				if (keywordValue)
					keywordFilterDescription.show();
				else
					keywordFilterDescription.hide();
			}

			{
				var dateRangeFilterDescription = filterDescriptionContainer.find('.date-range');
				var dateStart = currentSearchConditions.get('dateStart');
				var dateEnd = currentSearchConditions.get('dateEnd');
				if (dateStart && dateEnd)
				{
					dateRangeFilterDescription.find('.value').html(dateStart + ' - ' + dateEnd);
					dateRangeFilterDescription.show();
				}
				else
					dateRangeFilterDescription.hide();
			}

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
			dataTable.init(existedSearchResults != undefined ? existedSearchResults.dataset : undefined, viewOptions);
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Content.updateSize();
			dataTable.updateSize();
		};
	};
})(jQuery);