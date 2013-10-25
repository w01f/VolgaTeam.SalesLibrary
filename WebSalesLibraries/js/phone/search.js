(function ($)
{
	$.initSearch = function ()
	{
		$.ajax({
			type: "POST",
			url: "search/getSearchView",
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
			url: "search/getFileTypesView",
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
			url: "search/getTagsView",
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
			url: "search/getDateView",
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
			url: "search/getLibrariesView",
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
			runSearch(0);
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
				$("#search-match-exact").attr("checked", true).checkboxradio("refresh");
			else
				$("#search-match-partial").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-match-exact").attr("checked", true).checkboxradio("refresh");

		$('#search-match-exact, #search-match-partial').off('click').on('change', function ()
		{
			$.cookie("exactMatch", $('#search-match-exact').attr("checked") == "checked", {
				expires: (60 * 60 * 24 * 7)
			});
		});

		var hideDuplicates = $('#hide-duplicated');
		if ($.cookie("hideDuplicated") != null && $.cookie("hideDuplicated") == "true")
			hideDuplicates.attr("checked", true);
		hideDuplicates.on('change', function ()
		{
			$.cookie("hideDuplicated", $('#hide-duplicated').attr("checked") == "checked", {
				expires: (60 * 60 * 24 * 7)
			});
		});

		$("#search-fields-options-container").find('input[type="radio"]').checkboxradio();
		if ($.cookie("searchFields") != null)
		{
			if ($.cookie("searchFields") == "all")
				$("#content-full").attr("checked", true).checkboxradio("refresh");
			else if ($.cookie("searchFields") == "name")
				$("#content-only-file").attr("checked", true).checkboxradio("refresh");
			else if ($.cookie("searchFields") == "text")
				$("#content-only-text").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#content-full").attr("checked", true).checkboxradio("refresh");

		$("#search-fields-options-container").find('input[type="radio"]').on('change', function ()
		{
			var value = "all";
			if ($('#content-only-file').attr("checked") == "checked")
				value = "name";
			else if ($('#content-only-text').attr("checked") == "checked")
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

		searchDateContainer.find('input[type="radio"]').checkboxradio();
		if ($.cookie("conditionDateByFile") != null)
		{
			if ($.cookie("conditionDateByFile") == "true")
				$("#search-date-file").attr("checked", true).checkboxradio("refresh");
			else
				$("#search-date-link").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-date-link").attr("checked", true).checkboxradio("refresh");
		$('#search-date-file, #search-date-link').on('change', function ()
		{
			$.cookie("conditionDateByFile", $('#search-date-file').attr("checked") == "checked", {
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

		$('#search-file-type-container').find('input[type="checkbox"]').checkboxradio();

		if ($.cookie("fileTypePpt") != null)
		{
			if ($.cookie("fileTypePpt") == "true")
				$("#search-file-type-powerpoint").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-powerpoint").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypeDoc") != null)
		{
			if ($.cookie("fileTypeDoc") == "true")
				$("#search-file-type-word").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-word").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypeXls") != null)
		{
			if ($.cookie("fileTypeXls") == "true")
				$("#search-file-type-excel").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-excel").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypePdf") != null)
		{
			if ($.cookie("fileTypePdf") == "true")
				$("#search-file-type-pdf").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-pdf").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypeVideo") != null)
		{
			if ($.cookie("fileTypeVideo") == "true")
				$("#search-file-type-video").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-video").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypeUrl") != null)
		{
			if ($.cookie("fileTypeUrl") == "true")
				$("#search-file-type-url").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-url").attr("checked", true).checkboxradio("refresh");

		if ($.cookie("fileTypeImage") != null)
		{
			if ($.cookie("fileTypeImage") == "true")
				$("#search-file-type-image").attr("checked", true).checkboxradio("refresh");
		}
		else
			$("#search-file-type-image").attr("checked", true).checkboxradio("refresh");

		$('#file-types').find('input[type="checkbox"]').button({
			text: false
		});

		$('#search-file-types-select-button').off('click').on('click', function ()
		{
			$('#search-file-type-container').find('input[type="checkbox"]').attr('checked', true).checkboxradio("refresh");
			updateFileTypes();
		});
		$('#search-file-types-button').off('click');
		$('#search-file-types-clear-button').on('click', function ()
		{
			$('#search-file-type-container').find('input[type="checkbox"]').attr('checked', false).checkboxradio("refresh");
			updateFileTypes();
		});

		$('#search-file-type-container').find('input[type="checkbox"]').off('change').on('change', function ()
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
		$('.search-libraries-item').checkboxradio();
		updateLibraries();

		var librariesCount = $('.search-libraries-item').length;
		if (librariesCount < 2)
			$('.tab-search-libraries').addClass('ui-disabled').attr('href', "#");

		$('.search-libraries-item').off('change').on('change', function ()
		{
			updateLibraries();
		});
		$('#search-libraries-select-button').off('click').on('click', function ()
		{
			$('.search-libraries-item').attr('checked', true).checkboxradio("refresh");
			updateLibraries();
		});
		$('#search-libraries-clear-button').off('click').on('click', function ()
		{
			$('.search-libraries-item').attr('checked', false).checkboxradio("refresh");
			updateLibraries();
		});
	};

	var initSearchSortSelectors = function ()
	{
		$.cookie("sortColumn", 'link-name', {
			expires: (60 * 60 * 24 * 7)
		});
		$('#search-result-sort-column').on('change', function ()
		{
			$.cookie("sortColumn", $('#search-result-sort-column').find(':selected').val(), {
				expires: (60 * 60 * 24 * 7)
			});
			runSearch(1);
		});
		$.cookie("sortDirection", 'asc', {
			expires: (60 * 60 * 24 * 7)
		});
		$('#search-result-sort-order').on('change', function ()
		{
			$.cookie("sortDirection", $('#search-result-sort-order').find(':selected').val(), {
				expires: (60 * 60 * 24 * 7)
			});
			runSearch(1);
		});
	};

	var initTagsSelector = function ()
	{
		var tagsCount = $('.search-tags-item').length;
		if (tagsCount < 1)
			$('.tab-search-tags').addClass('ui-disabled').attr('href', "#");

		$('#search-tags-clear-button').on('click', function ()
		{
			$('.super-filter-item').attr('checked', false).checkboxradio("refresh");
			$('.search-tags-item').attr('checked', false).checkboxradio("refresh");
		});

		$('.search-tags-item.group-selector').off('change').on('change', function ()
		{
			var categoryGroup = $(this).parent().parent();
			categoryGroup.find('.search-tags-item.item-selector').attr('checked', $(this).is(':checked')).checkboxradio("refresh");
		});

		$('.search-tags-group').collapsible();
		$('.search-tags-item').checkboxradio();
	};

	var runSearch = function (isSort)
	{
		var selectedCondition = $('#search-keyword').val();
		if ($('#search-match-exact').attr("checked") == "checked")
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
		}
		if ($('#search-file-type-url').is(':checked'))
			selectedFileTypes.push("url");
		if ($('#search-file-type-image').is(':checked'))
		{
			selectedFileTypes.push("png");
			selectedFileTypes.push("jpeg");
		}

		var startDateText = $('#search-date-start').val();
		var endDateText = $('#search-date-end').val();
		if (startDateText != null && endDateText != null && startDateText != '' && endDateText != '')
		{
			var startDate = startDateText;
			var endDate = endDateText;
		}

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

		$.ajax({
			type: "POST",
			url: "search/searchByContent",
			data: {
				fileTypes: selectedFileTypes,
				condition: selectedCondition,
				startDate: startDate,
				endDate: endDate,
				dateFile: $('#search-date-file').attr("checked") == "checked",
				onlyFileCards: onlyFileCards,
				libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
				superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
				categories: categories.length > 0 ? $.toJSON(categories) : null,
				categoriesExactMatch: false,
				hideDuplicated: $('#hide-duplicated').attr("checked") == "checked",
				onlyByName: onlyByName,
				onlyByContent: onlyByContent,
				isSort: isSort
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
				var searchResultBody = $('#search-result-body');
				searchResultBody.html(msg);
				$.mobile.changePage("#search-result", {
					transition: "slidefade"
				});
				searchResultBody.listview('refresh');

				var itemsNumber = searchResultBody.find('li').length;
				if (itemsNumber > 0)
				{
					$('#search-result-links-number').html('Results: ' + itemsNumber);
					$('#search-result-sort-column-container').show();
					$('#search-result-sort-order-container').show();
				}
				else
				{
					$('#search-result-links-number').html('Files was not found');
					$('#search-result-sort-column-container').hide();
					$('#search-result-sort-order-container').hide();
				}

				$(".file-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLink(selectedLink, 'Search', false, '#search-result');
				});
				$(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).parent().attr("href").replace('#link', ''));
					$.loadLinkDeatils(selectedLink, 'Search', '#search-result');
					event.stopPropagation();
				});
			},
			error: function ()
			{
				$('#search-result-body').html('');
			},
			async: true,
			dataType: 'html'
		});
	}
})(jQuery);