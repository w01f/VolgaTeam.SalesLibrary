(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var SearchManager = function ()
	{
		var that = this;

		this.init = function ()
		{
			$('#search-basic').on('pageshow', function ()
			{
				initSearchSortSelectors();
				return false;
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getSearchView",
				beforeSend: function ()
				{
					$('#search-basic').find('.page-content').html('');
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				complete: function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				success: function (msg)
				{
					$('#search-basic').find('.page-content').html(msg);
					loadSearchPanel();
				},
				async: true,
				dataType: 'html'
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getFileTypesView",
				beforeSend: function ()
				{
					$('#search-file-types').find('.page-content').html('');
				},
				complete: function ()
				{
					loadFileTypesPanel();
				},
				success: function (msg)
				{
					$('#search-file-types').find('.page-content').html(msg);
				},
				async: true,
				dataType: 'html'
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getTagsView",
				beforeSend: function ()
				{
					$('#search-tags').find('.page-content').html('');
				},
				complete: function ()
				{
					loadTagsPanel();
				},
				success: function (msg)
				{
					$('#search-tags').find('.page-content').html(msg);
				},
				async: true,
				dataType: 'html'
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getDateView",
				beforeSend: function ()
				{
					$('#search-date').find('.page-content').html('');
				},
				complete: function ()
				{
					loadDatePanel();
				},
				success: function (msg)
				{
					$('#search-date').find('.page-content').html(msg);
				},
				async: true,
				dataType: 'html'
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/getLibrariesView",
				beforeSend: function ()
				{
					$('#search-libraries').find('.page-content').html('');
				},
				complete: function ()
				{
				},
				success: function (msg)
				{
					$('#search-libraries').find('.page-content').html(msg);
					loadLibrariesPanel();
				},
				async: true,
				dataType: 'html'
			});
		};

		this.runSearch = function (conditions)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/searchByContent",
				data: {
					fileTypes: conditions.fileTypes,
					condition: conditions.condition,
					startDate: conditions.startDate,
					endDate: conditions.endDate,
					dateFile: conditions.dateFile,
					libraries: conditions.libraries,
					superFilters: conditions.superFilters,
					categories: conditions.categories,
					categoriesExactMatch: conditions.categoriesExactMatch,
					onlyWithCategories: conditions.onlyWithCategories,
					hideDuplicated: conditions.hideDuplicated,
					onlyByName: conditions.onlyByName,
					onlyByContent: conditions.onlyByContent,
					sortColumn: conditions.sortColumn,
					sortDirection: conditions.sortDirection,
					datasetKey: conditions.datasetKey
				},
				beforeSend: function ()
				{
					$('#search-result-body').html('');
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				complete: function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				success: function (msg)
				{
					var searchResultPage = $('#search-result');
					searchResultPage.find('.link.back').attr('href', conditions.backLink);

					var searchResultBody = $('#search-result-body');
					searchResultBody.html(msg);
					var datasetKeyTag = searchResultBody.find('div.dataset-key');
					if (datasetKeyTag.length > 0)
					{
						searchResultPage.find('td.dataset-key').html(datasetKeyTag.html());
						datasetKeyTag.remove();
					}
					$.mobile.changePage("#search-result", {
						transition: "slidefade"
					});
					searchResultBody.listview('refresh');

					var itemsNumber = searchResultBody.find('li').length;
					var sortColumnSelectorContainer = $('#search-result-sort-column-container');
					var sortColumnSelector = $('#search-result-sort-column');
					var sortDirectionSelectorContainer = $('#search-result-sort-order-container');
					var sortDirectionSelector = $('#search-result-sort-order');
					if (itemsNumber > 0)
					{
						$('#search-result-links-number').html('Results: ' + itemsNumber);
						sortColumnSelectorContainer.show();
						sortColumnSelector.val(conditions.sortColumn).attr('selected', true).siblings('option').removeAttr('selected');
						sortColumnSelector.selectmenu().selectmenu("refresh", true);
						sortDirectionSelectorContainer.show();
						sortDirectionSelector.val(conditions.sortDirection).attr('selected', true).siblings('option').removeAttr('selected');
						sortDirectionSelector.slider().slider("refresh");
					}
					else
					{
						$('#search-result-links-number').html('Files was not found');
						sortColumnSelectorContainer.hide();
						sortDirectionSelectorContainer.hide();
					}

					$(".file-link").on('click', function ()
					{
						var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
						$.SalesPortal.Wallbin.loadLink(selectedLink, 'Search', '#search-result', false);
					});
				},
				error: function ()
				{
					$('#search-result-body').html('');
				},
				async: true,
				dataType: 'html'
			});
		};

		var loadSearchPanel = function ()
		{
			initMatchSelector();
			initSearchSortSelectors();

			$('#search-basic, #search-file-types, #search-tags, #search-date, #search-libraries').on('pageshow', function (e)
			{
				var currentPage = $(e.target);
				$('#search-result').find('.link.back').attr('href', '#' + currentPage.attr('id'));
				return true;
			});

			$('.search-button').on('click', function ()
			{
				that.runSearch(getSearchCondition(0));
			});
		};

		var loadDatePanel = function ()
		{
			initDateSelector();
		};

		var loadFileTypesPanel = function ()
		{
			initFileTypeSelector();
		};

		var loadLibrariesPanel = function ()
		{
			initLibrariesSelector();
		};

		var loadTagsPanel = function ()
		{
			initTagsSelector();
		};

		var initMatchSelector = function ()
		{
			$("#search-match-selector").find('input[type="radio"]').checkboxradio();
			if ($.cookie("exactMatch") != null)
			{
				if ($.cookie("exactMatch") == "true")
					$("#search-match-exact").prop("checked", true).checkboxradio("refresh");
				else
					$("#search-match-partial").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-match-exact").prop("checked", true).checkboxradio("refresh");

			$('#search-match-exact, #search-match-partial').off('click').on('change', function ()
			{
				$.cookie("exactMatch", $('#search-match-exact').prop("checked") == "checked", {
					expires: (60 * 60 * 24 * 7)
				});
			});

			var hideDuplicates = $('#hide-duplicated');
			if ($.cookie("hideDuplicated") != null && $.cookie("hideDuplicated") == "true")
				hideDuplicates.prop("checked", true);
			hideDuplicates.on('change', function ()
			{
				$.cookie("hideDuplicated", $('#hide-duplicated').prop("checked") == "checked", {
					expires: (60 * 60 * 24 * 7)
				});
			});

			var searchFieldsContainer = $("#search-fields-options-container");
			searchFieldsContainer.find('input[type="radio"]').checkboxradio();
			if ($.cookie("searchFields") != null)
			{
				if ($.cookie("searchFields") == "all")
					$("#content-full").prop("checked", true).checkboxradio("refresh");
				else if ($.cookie("searchFields") == "name")
					$("#content-only-file").prop("checked", true).checkboxradio("refresh");
				else if ($.cookie("searchFields") == "text")
					$("#content-only-text").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#content-full").prop("checked", true).checkboxradio("refresh");

			searchFieldsContainer.find('input[type="radio"]').on('change', function ()
			{
				var value = "all";
				if ($('#content-only-file').prop("checked") == "checked")
					value = "name";
				else if ($('#content-only-text').prop("checked") == "checked")
					value = "text";
				$.cookie("searchFields", value, {
					expires: (60 * 60 * 24 * 7)
				});
			});
		};

		var initDateSelector = function ()
		{
			var searchDateContainer = $('#search-date-container');
			searchDateContainer.collapsible();
			$('#search-date-start, #search-date-end').scroller({
				preset: 'date',
				theme: 'jqm',
				display: 'modal',
				mode: 'clickpick',
				animate: 'fade',
				dateOrder: 'mmD ddy',
				dateFormat: 'mm/dd/y'
			});
			$('#search-clear-date-button').off('click').on('click', function ()
			{
				$('#search-date-start, #search-date-end').val('');
				updateDateRange();
			});

			var searchDateFile = $("#search-date-file");
			searchDateFile.checkboxradio();
			var searchDateLink = $("#search-date-link");
			searchDateLink.checkboxradio();
			if ($.cookie("conditionDateByFile") != null)
			{
				if ($.cookie("conditionDateByFile") == "true")
					searchDateFile.prop("checked", true).checkboxradio("refresh");
				else
					searchDateLink.prop("checked", true).checkboxradio("refresh");
			}
			else
				searchDateLink.prop("checked", true).checkboxradio("refresh");
			$('#search-date-file, #search-date-link').on('change', function ()
			{
				$.cookie("conditionDateByFile", $('#search-date-file').prop("checked") == "checked", {
					expires: (60 * 60 * 24 * 7)
				});
			});
		};

		var initFileTypeSelector = function ()
		{
			var updateFileTypes = function ()
			{
				$.cookie("fileTypePpt", $('#search-file-type-powerpoint').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeDoc", $('#search-file-type-word').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeXls", $('#search-file-type-excel').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypePdf", $('#search-file-type-pdf').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeVideo", $('#search-file-type-video').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeUrl", $('#search-file-type-url').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeImage", $('#search-file-type-image').is(':checked'), {
					expires: (60 * 60 * 24 * 7)
				});
			};

			var searchFileTypeContainer = $('#search-file-type-container');
			searchFileTypeContainer.find('input[type="checkbox"]').checkboxradio();

			if ($.cookie("fileTypePpt") != null)
			{
				if ($.cookie("fileTypePpt") == "true")
					$("#search-file-type-powerpoint").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-powerpoint").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypeDoc") != null)
			{
				if ($.cookie("fileTypeDoc") == "true")
					$("#search-file-type-word").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-word").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypeXls") != null)
			{
				if ($.cookie("fileTypeXls") == "true")
					$("#search-file-type-excel").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-excel").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypePdf") != null)
			{
				if ($.cookie("fileTypePdf") == "true")
					$("#search-file-type-pdf").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-pdf").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypeVideo") != null)
			{
				if ($.cookie("fileTypeVideo") == "true")
					$("#search-file-type-video").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-video").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypeUrl") != null)
			{
				if ($.cookie("fileTypeUrl") == "true")
					$("#search-file-type-url").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-url").prop("checked", true).checkboxradio("refresh");

			if ($.cookie("fileTypeImage") != null)
			{
				if ($.cookie("fileTypeImage") == "true")
					$("#search-file-type-image").prop("checked", true).checkboxradio("refresh");
			}
			else
				$("#search-file-type-image").prop("checked", true).checkboxradio("refresh");

			$('#file-types').find('input[type="checkbox"]').button({
				text: false
			});

			$('#search-file-types-select-button').off('click').on('click', function ()
			{
				searchFileTypeContainer.find('input[type="checkbox"]').prop('checked', true).checkboxradio("refresh");
				updateFileTypes();
			});
			$('#search-file-types-button').off('click');
			$('#search-file-types-clear-button').on('click', function ()
			{
				searchFileTypeContainer.find('input[type="checkbox"]').prop('checked', false).checkboxradio("refresh");
				updateFileTypes();
			});

			searchFileTypeContainer.find('input[type="checkbox"]').off('change').on('change', function ()
			{
				updateFileTypes();
			});
		};

		var initLibrariesSelector = function ()
		{
			var updateLibraries = function ()
			{
				var selectedLibraryIds = [];
				$('.search-libraries-item:checked').each(function ()
				{
					selectedLibraryIds.push(this.id);
				});
				$.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
					expires: (60 * 60 * 24 * 7)
				});
			};
			$('.search-libraries-group').collapsible();
			var searchLibrariesItem = $('.search-libraries-item');
			searchLibrariesItem.checkboxradio();
			updateLibraries();

			var librariesCount = searchLibrariesItem.length;
			if (librariesCount < 2)
				$('.tab-search-libraries').addClass('ui-disabled').attr('href', "#");

			searchLibrariesItem.off('change').on('change', function ()
			{
				updateLibraries();
			});
			$('#search-libraries-select-button').off('click').on('click', function ()
			{
				$('.search-libraries-item').prop('checked', true).checkboxradio("refresh");
				updateLibraries();
			});
			$('#search-libraries-clear-button').off('click').on('click', function ()
			{
				$('.search-libraries-item').prop('checked', false).checkboxradio("refresh");
				updateLibraries();
			});
		};

		var initSearchSortSelectors = function ()
		{
			$.cookie("sortColumnMobile", 'name', {
				expires: (60 * 60 * 24 * 7)
			});
			$('#search-result-sort-column').off('change.custom').on('change.custom', function ()
			{
				$.cookie("sortColumnMobile", $('#search-result-sort-column').find(':selected').val(), {
					expires: (60 * 60 * 24 * 7)
				});
				that.runSearch(getSearchCondition(1));
			});
			$.cookie("sortDirectionMobile", 'asc', {
				expires: (60 * 60 * 24 * 7)
			});
			$('#search-result-sort-order').off('change.custom').on('change.custom', function ()
			{
				$.cookie("sortDirectionMobile", $('#search-result-sort-order').find(':selected').val(), {
					expires: (60 * 60 * 24 * 7)
				});
				that.runSearch(getSearchCondition(1));
			});
		};

		var initTagsSelector = function ()
		{
			var searchTagsItems = $('.search-tags-item');
			var tagsCount = searchTagsItems.length;
			if (tagsCount < 1)
				$('.tab-search-tags').addClass('ui-disabled').attr('href', "#");

			$('#search-tags-clear-button').on('click', function ()
			{
				$('.super-filter-item').prop('checked', false).checkboxradio("refresh");
				searchTagsItems.prop('checked', false).checkboxradio("refresh");
			});

			$('.search-tags-item.group-selector').off('change').on('change', function ()
			{
				var categoryGroup = $(this).parent().parent();
				categoryGroup.find('.search-tags-item.item-selector').prop('checked', $(this).is(':checked')).checkboxradio("refresh");
			});

			$('.search-tags-group').collapsible();
			searchTagsItems.checkboxradio();
		};

		var getSearchCondition = function (isSort)
		{
			var selectedCondition = $('#search-keyword').val();
			if ($('#search-match-exact').prop("checked") == "checked")
				selectedCondition = '"' + selectedCondition + '"';

			var selectedFileTypes = [];
			if ($('#search-file-type-powerpoint').is(':checked'))
				selectedFileTypes.push("ppt");
			if ($('#search-file-type-word').is(':checked'))
				selectedFileTypes.push("doc");
			if ($('#search-file-type-excel').is(':checked'))
				selectedFileTypes.push("xls");
			if ($('#search-file-type-pdf').is(':checked'))
				selectedFileTypes.push("pdf");
			if ($('#search-file-type-video').is(':checked'))
			{
				selectedFileTypes.push("video");
				selectedFileTypes.push("mp4");
				selectedFileTypes.push("wmv");
				selectedFileTypes.push("mp3");
			}
			if ($('#search-file-type-url').is(':checked'))
			{
				selectedFileTypes.push("url");
				selectedFileTypes.push("url365");
			}
			if ($('#search-file-type-image').is(':checked'))
			{
				selectedFileTypes.push("png");
				selectedFileTypes.push("jpeg");
			}

			var startDateText = $('#search-date-start').val();
			var endDateText = $('#search-date-end').val();
			var startDate = undefined;
			var endDate = undefined;
			if (startDateText != null && endDateText != null && startDateText != '' && endDateText != '')
			{
				startDate = startDateText;
				endDate = endDateText;
			}

			var dateFileSelector = $('#search-date-file');
			var dateFileCondition = dateFileSelector.length > 0 ? dateFileSelector.prop("checked") == "checked" : true;

			var onlyFileCards = $('#search-only-filecards').is(':checked') ? 1 : 0;

			var selectedLibraryIds = [];
			$('.search-libraries-item:checked').each(function ()
			{
				selectedLibraryIds.push(this.id);
			});

			var superFilters = [];
			$.each($(".super-filter-item:checked"), function ()
			{
				superFilters.push($(this).val());
			});

			var categories = [];
			$.each($(".search-tags-item.item-selector:checked"), function ()
			{
				var substr = $(this).attr('id').split('------');
				var category = {
					category: substr[0],
					tag: substr[1]
				};
				categories.push(category);
			});

			var onlyByName = false;
			var onlyByContent = false;
			if ($.cookie("searchFields") != null)
			{
				if ($.cookie("searchFields") == "name")
					onlyByName = true;
				else if ($.cookie("searchFields") == "text")
					onlyByContent = true;
			}

			var datasetKey = $('#search-result').find('td.dataset-key').html();
			datasetKey = isSort == 0 || datasetKey == '' ? undefined : datasetKey;

			return {
				fileTypes: selectedFileTypes,
				condition: selectedCondition,
				startDate: startDate,
				endDate: endDate,
				dateFile: dateFileCondition,
				onlyFileCards: onlyFileCards,
				libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
				superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
				categories: categories.length > 0 ? $.toJSON(categories) : null,
				categoriesExactMatch: false,
				onlyWithCategories: false,
				hideDuplicated: $('#hide-duplicated').prop("checked") == "checked",
				onlyByName: onlyByName,
				onlyByContent: onlyByContent,
				sortColumn: $.cookie("sortColumnMobile"),
				sortDirection: $.cookie("sortDirectionMobile"),
				datasetKey: datasetKey,
				backLink: '#search-basic'
			};
		};
	};
	$.SalesPortal.Search = new SearchManager();
})(jQuery);