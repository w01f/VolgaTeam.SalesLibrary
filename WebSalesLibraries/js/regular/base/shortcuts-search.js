(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchManager = function (content, objectId)
	{
		var that = this;

		var searchShortcutOptions = new $.SalesPortal.SearchOptions($.parseJSON(content.find('.search-conditions .encoded-object').text()));

		var dataTable = new $.SalesPortal.SearchDataTable();

		var subSearchBar = undefined;

		content.html('');
		content.append($('<table id="shortcuts-search-container" style="table-layout:fixed;"><tr>' +
			'<td id="right-navbar" style="display: none; width: 15%; min-width: 240px;"></td>' +
			'<td>' +
			'<div class="search-results-above sub-search-bar" style="display: none;"></div>' +
			'<div class="search-results-container shortcuts-search-results-container"></div>' +
			'</td>' +
			'</tr></table>'));

		if (searchShortcutOptions.enableSubSearch)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchBar",
				data: {
					pageId: searchShortcutOptions.isPage ? objectId : undefined,
					linkId: searchShortcutOptions.isPage ? undefined : objectId
				},
				success: function (msg)
				{
					subSearchBar = content.find('.sub-search-bar');
					subSearchBar.html(msg);
					initSubSearchButtons();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		}
		else
			subSearchBar = content.find('.sub-search-bar');

		$.SalesPortal.SearchHelper.runSearchJson(
			{
				datasetKey: undefined,
				conditions: $.toJSON(searchShortcutOptions.conditions)
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
				searchShortcutOptions.conditions.datasetKey = data.datasetKey;
				if (searchShortcutOptions.subSearchDefaultView == 'all')
					SimpleSearch(content);
				else if (searchShortcutOptions.subSearchDefaultView == 'search')
					SubSearchCustom(content);
				else if (searchShortcutOptions.subSearchDefaultView == 'links')
					SubSearchByTemplates(content);
			}
		);


		var SimpleSearch = function (content)
		{
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function ()
			{
				$.SalesPortal.SearchHelper.runSearchJson(
					{
						datasetKey: searchShortcutOptions.conditions.datasetKey
					},
					function ()
					{
						sideBar.hide();
						subSearchBar.hide();
						$.SalesPortal.Overlay.show(false);
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init(searchResults);
						subSearchBar.show();
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

				$.SalesPortal.SearchHelper.runSearchJson(
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
							baseDatasetKey: searchShortcutOptions.conditions.datasetKey,
							datasetKey: null
						})
					},
					function ()
					{
						subSearchBar.hide();
						$.SalesPortal.Overlay.show(false);
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init(searchResults);
						subSearchBar.show();
						updateContentSize();
					}
				);
			};

			var initPanel = function ()
			{
				sideBar.find('.tags-filter-panel-switcher').off('click').on('click', function ()
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
				var categorySelector = categoryConditions.find('.tag-condition-selector');
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
							var selectedCategoryLabel = sideBar.find('.tag-condition-selected');
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
					pageId: searchShortcutOptions.isPage ? objectId : undefined,
					linkId: searchShortcutOptions.isPage ? undefined : objectId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
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

				$.SalesPortal.SearchHelper.runSearchJson(

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
							baseDatasetKey: searchShortcutOptions.conditions.datasetKey,
							datasetKey: null
						})
					},
					function ()
					{
						subSearchBar.hide();
						$.SalesPortal.Overlay.show(false);
					},
					function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					function (data)
					{
						var searchResults = new $.SalesPortal.SearchResult(data.datasetKey, data.dataset);
						dataTable.init(searchResults);
						subSearchBar.show();
						updateContentSize();
					}
				);
			};

			var initPanel = function ()
			{
				var logoSelector = sideBar.find('.logo-list ul');
				sideBar.find('.logo-list ul li.enabled img').tooltip({animation: false, trigger: 'hover', placement: 'bottom', delay: { show: 500, hide: 100 }});
				var selectorItems = logoSelector.find('li.enabled a');
				var firstItem = selectorItems.first();
				if (firstItem != null)
					firstItem.addClass('opened');
				selectorItems.on('click', function ()
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
					pageId: searchShortcutOptions.isPage ? objectId : undefined,
					linkId: searchShortcutOptions.isPage ? undefined : objectId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
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

		var initSubSearchButtons = function ()
		{
			var subSearchBar = content.find('.sub-search-bar');
			subSearchBar.find('img').tooltip({
				animation: false,
				trigger: 'hover',
				placement: 'bottom',
				delay: {
					show: 500,
					hide: 100 },
				container: 'body'});
			subSearchBar.find('img').off('click.toggle').on('click.toggle',function (e)
			{
				if (!$(this).hasClass('active'))
				{
					subSearchBar.find('img').removeClass('active');
					$(this).addClass('active')
				}
				else
					e.stopPropagation();
			}).off('click.process').on('click.process', function ()
			{
				if ($(this).hasClass('no-filter'))
					SimpleSearch(content);
				else if ($(this).hasClass('custom-filter'))
					SubSearchCustom(content);
				else if ($(this).hasClass('predefined-filter'))
					SubSearchByTemplates(content);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Shortcuts.updateContentSize();

			var content = $('#content');
			var height = content.height();
			var width = content.width();

			$('#shortcuts-search-container').css({
				'width': width + 'px'
			});

			var sideBar = $('#right-navbar');
			sideBar.find('.logo-list').css({
				'height': (height - 5) + 'px'
			});

			dataTable.updateSize();
		};
		$(window).off('resize').on('resize', updateContentSize);

		return that;
	};

	$.SalesPortal.SearchOptions = function (data)
	{
		this.title = undefined;
		this.isPage = undefined;
		this.openInSamePage = undefined;

		this.enableSubSearch = undefined;
		this.subSearchDefaultView = undefined;

		this.conditions = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);