(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchApp = function (data)
	{
		var that = this;

		var currentSearchConditions = new $.SalesPortal.SearchConditions(function ()
		{
		});

		var searchConditionsPage = undefined;
		var searchResultsPage = undefined;

		this.init = function ()
		{
			$.mobile.pageContainer.pagecontainer("change", "#search", {
				transition: "slidefade"
			});

			initControls();
			applySearchConditions();
		};

		var initControls = function ()
		{
			searchConditionsPage = $('#search');
			searchResultsPage = $('#search-results-search');

			$('#search-tab-filters-text').off('input').on('input', function ()
			{
				var text = $(this).val();
				if (text !== '')
					currentSearchConditions.set('text', text);
				else
					currentSearchConditions.set('text', null);
			});

			var dateFormat = 'MM/DD/YYYY';
			$('#search-tab-filters-date-start').off('change').on('change', function ()
			{
				var date = moment($(this).val());
				if (date.isValid())
					currentSearchConditions.set('dateStart', date.format(dateFormat));
				else
					currentSearchConditions.set('dateStart', null);
			});
			$('#search-tab-filters-date-end').off('change').on('change', function ()
			{
				var date = moment($(this).val());
				if (date.isValid())
					currentSearchConditions.set('dateEnd', date.format(dateFormat));
				else
					currentSearchConditions.set('dateEnd', null);
			});

			$('#search-tab-filters-file-name-only').off('change').on('change', function ()
			{
				currentSearchConditions.set('onlyFileNames', $(this).prop('checked'));
			});

			$('#search-tab-filters-exact-search').off('change').on('change', function ()
			{
				currentSearchConditions.set('exactMatch', $(this).prop('checked'));
			});

			$('#search-tab-file-types').find('.search-tab-file-type-toggle').off('change').on('change', function ()
			{
				currentSearchConditions.setFileTypesSettings({
					showPowerPoint: $('#search-tab-file-types-power-point').prop('checked'),
					showVideo: $('#search-tab-file-types-video').prop('checked'),
					showPdf: $('#search-tab-file-types-pdf').prop('checked'),
					showWord: $('#search-tab-file-types-word').prop('checked'),
					showExcel: $('#search-tab-file-types-excel').prop('checked'),
					showImages: $('#search-tab-file-types-image').prop('checked'),
					showUrls: $('#search-tab-file-types-url').prop('checked')
				});
			});

			searchConditionsPage.find('.search-category-group-panel').panel({
				open: function ()
				{
					var categoryGroupPanel = $(this);

					categoryGroupPanel.find('.select-all').off('click').on('click', function ()
					{
						categoryGroupPanel.find('.search-category-tag-toggle').prop('checked', true).checkboxradio("refresh");
					});

					categoryGroupPanel.find('.clear-all').off('click').on('click', function ()
					{
						categoryGroupPanel.find('.search-category-tag-toggle').prop('checked', false).checkboxradio("refresh");
					});
				},
				close: function ()
				{
					var categoryGroupPanel = $(this);

					var categoryGroupCode = categoryGroupPanel.find('.service-data .category-group-code').text();

					var categoryGroupName = categoryGroupPanel.find('.group-name').text();
					var newCategories = [];
					var currentGroupCategories = [];
					$.each(categoryGroupPanel.find('.search-category-tag-toggle'), function ()
					{
						var tagCheckBox = $(this);
						if (tagCheckBox.prop('checked') === true)
							currentGroupCategories.push(categoryGroupPanel.find("label[for='" + tagCheckBox.attr('id') + "']").text());
					});
					if (currentGroupCategories.length > 0)
						newCategories.push({
							name: categoryGroupName,
							items: currentGroupCategories
						});
					var existedCategories = currentSearchConditions.getCategorySettings();
					$.each(existedCategories, function (groupIndex, group)
					{
						if (group.name !== categoryGroupName)
							newCategories.push(group);
					});
					currentSearchConditions.setCategorySettings(newCategories);

					var categoryGroupToggleButton = $('#search-category-group-toggle-' + categoryGroupCode);
					if (currentGroupCategories.length > 0)
						categoryGroupToggleButton.buttonMarkup({theme: 'd'});
					else
						categoryGroupToggleButton.buttonMarkup({theme: 'a'});
				}
			});

			$('#search-library-panel').panel({
				close: function ()
				{
					var libraryPanel = $(this);
					var libraries = [];
					$.each(libraryPanel.find('.search-filter-library-toggle'), function ()
					{
						var libraryCheckBox = $(this);
						if (libraryCheckBox.prop('checked') === true)
							libraries.push({
								id: libraryCheckBox.val(),
								name: libraryPanel.find("label[for='" + libraryCheckBox.attr('id') + "']").text()
							});
					});
					currentSearchConditions.setLibrarySettings(libraries);

					var libraryFilterToggleButton = $('#search-filter-library-button');
					if (libraries.length > 0)
						libraryFilterToggleButton.buttonMarkup({theme: 'd'});
					else
						libraryFilterToggleButton.buttonMarkup({theme: 'a'});
				}
			});

			$('#search-super-filter-panel').panel({
				close: function ()
				{
					var superFilterPanel = $(this);
					var superFilters = [];
					$.each(superFilterPanel.find('.search-super-filter-toggle'), function ()
					{
						var superFilterCheckBox = $(this);
						if (superFilterCheckBox.prop('checked') === true)
							superFilters.push(superFilterPanel.find("label[for='" + superFilterCheckBox.attr('id') + "']").text());
					});
					currentSearchConditions.setSuperFiltersSettings(superFilters);

					var superFilterToggleButton = $('#search-super-filter-button');
					if (superFilters.length > 0)
						superFilterToggleButton.buttonMarkup({theme: 'd'});
					else
						superFilterToggleButton.buttonMarkup({theme: 'a'});
				}
			});

			$("#search-button-run").off('click').on('click', function ()
			{
				runSearch();
			});

			$("#search-button-clear").off('click').on('click', function ()
			{
				clearSearchConditions();
				that.init();
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
					$.mobile.pageContainer.pagecontainer("change", "#search-results-search", {
						transition: "slidefade"
					});

					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				function (data)
				{
					searchResultsPage.find('.entities-count span').html(data.dataset.length + ' Links');

					new $.SalesPortal.SearchDataTable(
						data.dataset,
						undefined,
						undefined,
						{
							id: '#search-results-search',
							name: 'Search Results'
						}
					);
				}
			);
		};

		var applySearchConditions = function ()
		{
			$('#search-tab-filters-text').val(currentSearchConditions.get('text'));
			$('#search-tab-filters-date-start').val(currentSearchConditions.get('dateStart'));
			$('#search-tab-filters-date-end').val(currentSearchConditions.get('dateEnd'));

			$('#search-tab-filters-file-name-only').prop('checked', currentSearchConditions.get('onlyFileNames')).checkboxradio("refresh");
			$('#search-tab-filters-exact-search').prop('checked', currentSearchConditions.get('exactMatch')).checkboxradio("refresh");

			var fileSettings = currentSearchConditions.getFileTypesSettings();
			$('#search-tab-file-types-power-point').prop('checked', fileSettings.showPowerPoint).checkboxradio("refresh");
			$('#search-tab-file-types-video').prop('checked', fileSettings.showVideo).checkboxradio("refresh");
			$('#search-tab-file-types-pdf').prop('checked', fileSettings.showPdf).checkboxradio("refresh");
			$('#search-tab-file-types-word').prop('checked', fileSettings.showWord).checkboxradio("refresh");
			$('#search-tab-file-types-excel').prop('checked', fileSettings.showExcel).checkboxradio("refresh");
			$('#search-tab-file-types-image').prop('checked', fileSettings.showImages).checkboxradio("refresh");
			$('#search-tab-file-types-url').prop('checked', fileSettings.showUrls).checkboxradio("refresh");

			searchConditionsPage.find('.search-category-group-panel .search-category-tag-toggle').prop('checked', false).checkboxradio("refresh");
			searchConditionsPage.find('.category-group-button').buttonMarkup({theme: 'a'});

			searchConditionsPage.find('#search-library-panel .search-filter-library-toggle').prop('checked', false).checkboxradio("refresh");
			searchConditionsPage.find('#search-filter-library-button').buttonMarkup({theme: 'a'});

			searchConditionsPage.find('#search-super-filter-panel .search-super-filter-toggle').prop('checked', false).checkboxradio("refresh");
			searchConditionsPage.find('#search-super-filter-button').buttonMarkup({theme: 'a'});
		};

		var clearSearchConditions = function ()
		{
			currentSearchConditions.clear();
			applySearchConditions();
		};
	};
})(jQuery);