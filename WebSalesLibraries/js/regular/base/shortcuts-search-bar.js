(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchBar = function (pageId)
	{
		var that = this;
		var searchBar = $('.shortcuts-search-bar');
		if (searchBar.length > 0)
		{
			var shortcutPageId = pageId;
			var searchBarOptions = new $.SalesPortal.SearchOptions($.parseJSON(searchBar.find('.search-conditions .encoded-object').text()));
			var searchBarConditions = new $.SalesPortal.SearchConditions(function ()
			{

			});
			searchBarConditions.loadFromConditionsFormatted(searchBarOptions.conditions);
		}

		this.changeVisibility = function (show)
		{
			if (show)
				searchBar.addClass('open').show();
			else
				searchBar.removeClass('open').hide();
		};

		var init = function ()
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

			searchBar.find('.tags-filter-panel-switcher').off('click').on('click', function ()
			{
				editTagsCondition();
			});

			updateSearchButtonState();
			updateSelectedCategories();
		};

		var search = function ()
		{
			if (searchBar.find('.btn.search-bar-run').hasClass('disabled')) return;
			searchBarConditions.set('text', searchBar.find('.search-bar-text').val());
			searchBarConditions.set('exactMatch', searchBar.find('#search-exact-match').is(':checked'));
			searchBarConditions.set('onlyByName', searchBar.find('#search-file-names-only').is(':checked'));
			searchBarConditions.setFileTypesSettings({
				showPowerPoint: searchBar.find('#search-file-type-powerpoint').prop('checked'),
				showVideo: searchBar.find('#search-file-type-video').prop('checked'),
				showPdf: searchBar.find('#search-file-type-other').prop('checked'),
				showWord: searchBar.find('#search-file-type-other').prop('checked'),
				showExcel: searchBar.find('#search-file-type-other').prop('checked'),
				showImages: searchBar.find('#search-file-type-other').prop('checked'),
				showUrls: searchBar.find('#search-file-type-other').prop('checked')
			});
			if (searchBarOptions.openInSamePage)
			{
				that.changeVisibility(false);
				var content = $('#content').find('.shortcuts-page-content');
				searchBarOptions.conditions = searchBarConditions.getConditionsFormatted();
				content.html('<div class="search-conditions" style="display: none;"><div class="encoded-object">' + $.toJSON(searchBarOptions) + '</div></div>');
				$.SalesPortal.ShortcutsSearchManager(content, shortcutPageId);
			}
			else
			{
				window.open("shortcuts/GetQuickSearchResult?pageId=" + shortcutPageId +
					"&text=" + searchBarConditions.get('text') +
					"&textExactMatch=" + searchBarConditions.get('exactMatch') +
					"&onlyFiles=" + searchBarConditions.get('onlyFileNames') +
					"&fileTypes=" + $.toJSON(searchBarConditions.getFileTypesSettings().selectedTypeTags()) +
					"&superFilters=" + $.toJSON(searchBarConditions.getSuperFiltersSettings()) +
					"&categories=" + $.toJSON(searchBarConditions.getCategorySettings()));
			}
		};

		var updateSearchButtonState = function ()
		{
			var hasKeyword = $('.shortcuts-search-bar .search-bar-text').val() != "";
			var hasSuperFilters = searchBarConditions.getSuperFiltersSettings().length > 0;
			var hasCategories = searchBarConditions.getCategorySettings().length > 0;
			var searchButton = searchBar.find('.btn.search-bar-run');
			searchButton.removeClass('disabled');
			if (!(hasKeyword || hasSuperFilters || hasCategories) || !($('#search-file-type-powerpoint').is(':checked') || $('#search-file-type-video').is(':checked') || $('#search-file-type-other').is(':checked')))
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

		var editTagsCondition = function ()
		{
			var categorySelector = searchBar.find('.search-conditions .tag-condition-selector');
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

		if (searchBar.length > 0)
			init();
	};
})(jQuery);