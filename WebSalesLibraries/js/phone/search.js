(function ($)
{
	$.initSearch = function ()
	{
		$.ajax({
			type:"POST",
			url:"search/getSearchView",
			beforeSend:function ()
			{
				$('#search-basic').find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible:false,
					html:""
				});
			},
			complete:function ()
			{
				$.mobile.loading('hide', {
					textVisible:false,
					html:""
				});
			},
			success:function (msg)
			{
				$('#search-basic').find('.page-content').html(msg);
				loadSearchPanel();
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"search/getFileTypesView",
			beforeSend:function ()
			{
				$('#search-file-types').find('.page-content').html('');
			},
			complete:function ()
			{
				loadFileTypesPanel();
			},
			success:function (msg)
			{
				$('#search-file-types').find('.page-content').html(msg);
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"search/getTagsView",
			beforeSend:function ()
			{
				$('#search-tags').find('.page-content').html('');
			},
			complete:function ()
			{
				loadTagsPanel();
			},
			success:function (msg)
			{
				$('#search-tags').find('.page-content').html(msg);
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"search/getDateView",
			beforeSend:function ()
			{
				$('#search-date').find('.page-content').html('');
			},
			complete:function ()
			{
				loadDatePanel();
			},
			success:function (msg)
			{
				$('#search-date').find('.page-content').html(msg);
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"search/getLibrariesView",
			beforeSend:function ()
			{
				$('#search-libraries').find('.page-content').html('');
			},
			complete:function ()
			{
			},
			success:function (msg)
			{
				$('#search-libraries').find('.page-content').html(msg);
				loadLibrariesPanel();
			},
			async:true,
			dataType:'html'
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
		$('#search-match-selector').find('a').removeClass('ui-btn-active');
		if ($.cookie("exactMatch") != null)
		{
			if ($.cookie("exactMatch") == "true")
				$("#search-match-exact").addClass('ui-btn-active');
			else
				$("#search-match-partial").addClass('ui-btn-active');
		}
		else
			$("#search-match-exact").addClass('ui-btn-active');
		$('#search-match-exact, #search-match-partial').off('click').on('click', function ()
		{
			$.cookie("exactMatch", $('#search-match-partial').hasClass('ui-btn-active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});

		var hideDuplicates = $('#hide-duplicated');
		if ($.cookie("hideDuplicated") != null && $.cookie("hideDuplicated") == "true")
			hideDuplicates.attr("checked", true);
		hideDuplicates.on('change', function ()
		{
			$.cookie("hideDuplicated", $('#hide-duplicated').attr("checked") == "checked", {
				expires:(60 * 60 * 24 * 7)
			});
		});

	};

	var initDateSelector = function ()
	{
		var searchDateContainer = $('#search-date-container');
		searchDateContainer.collapsible();
		$('#search-date-start, #search-date-end').scroller({
			preset:'date',
			theme:'jqm',
			display:'modal',
			mode:'clickpick',
			animate:'fade',
			dateOrder:'mmD ddy',
			dateFormat:'mm/dd/y'
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
			$("#search-date-file").attr("checked", true).checkboxradio("refresh");
		$('#search-date-file, #search-date-link').on('change', function ()
		{
			$.cookie("conditionDateByFile", $('#search-date-file').attr("checked") == "checked", {
				expires:(60 * 60 * 24 * 7)
			});
		});
	};

	var initFileTypeSelector = function ()
	{
		var updateFileTypes = function ()
		{
			$.cookie("fileTypePpt", $('#search-file-type-powerpoint').is(':checked'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeDoc", $('#search-file-type-word').is(':checked'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeXls", $('#search-file-type-excel').is(':checked'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypePdf", $('#search-file-type-pdf').is(':checked'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeVideo", $('#search-file-type-video').is(':checked'), {
				expires:(60 * 60 * 24 * 7)
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

		$('#file-types').find('input[type="checkbox"]').button({
			text:false
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
				expires:(60 * 60 * 24 * 7)
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
			expires:(60 * 60 * 24 * 7)
		});
		$('#search-result-sort-column').on('change', function ()
		{
			$.cookie("sortColumn", $('#search-result-sort-column').find(':selected').val(), {
				expires:(60 * 60 * 24 * 7)
			});
			runSearch(1);
		});
		$.cookie("sortDirection", 'asc', {
			expires:(60 * 60 * 24 * 7)
		});
		$('#search-result-sort-order').on('change', function ()
		{
			$.cookie("sortDirection", $('#search-result-sort-order').find(':selected').val(), {
				expires:(60 * 60 * 24 * 7)
			});
			runSearch(1);
		});
	};

	var initTagsSelector = function ()
	{
		$('.search-tags-group').collapsible();
		$('.search-tags-item').checkboxradio();

		var tagsCount = $('.search-tags-item').length;
		if (tagsCount < 1)
			$('.tab-search-tags').addClass('ui-disabled').attr('href', "#");

		$('#search-tags-clear-button').on('click', function ()
		{
			$('.search-tags-item').attr('checked', false).checkboxradio("refresh");
		});
	};

	var runSearch = function (isSort)
	{
		var selectedCondition = $('#search-keyword').val();
		if ($('#search-match-exact').hasClass('ui-btn-active'))
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
			selectedFileTypes.push("video");


		var startDateText = $('#search-date-start').val();
		var endDateText = $('#search-date-end').val();
		if (startDateText != null && endDateText != null && startDateText != '' && endDateText != '')
		{
			var startDate = startDateText;
			var endDate = endDateText;
		}

		var onlyFileCards = $('#search-only-filecards').is(':checked') ? 1 : 0;

		var categories = [];
		$.each($(".search-tags-item:checked"), function ()
		{
			var substr = $(this).attr('id').split('------');
			var category = {
				category:substr[0],
				tag:substr[1]
			};
			categories.push(category);
		});

		$.ajax({
			type:"POST",
			url:"search/searchByContent",
			data:{
				fileTypes:selectedFileTypes,
				condition:selectedCondition,
				startDate:startDate,
				endDate:endDate,
				dateFile:$('#search-date-file').attr("checked") == "checked",
				onlyFileCards:onlyFileCards,
				categories:categories.length > 0 ? $.toJSON(categories) : null,
				categoriesExactMatch:$('#search-tags-exact-match').find(':selected').val(),
				hideDuplicated:$('#hide-duplicated').attr("checked") == "checked",
				isSort:isSort
			},
			beforeSend:function ()
			{
				$('#search-result-body').html('');
				$.mobile.loading('show', {
					textVisible:false,
					html:""
				});
			},
			complete:function ()
			{
				$.mobile.loading('hide', {
					textVisible:false,
					html:""
				});
			},
			success:function (msg)
			{
				var searchResultBody = $('#search-result-body');
				searchResultBody.html(msg);
				$.mobile.changePage("#search-result", {
					transition:"slidefade"
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
					$.loadLink(selectedLink, true, false, '#search-result');
				});
				$(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					$.loadLinkDeatils(selectedLink, true);
					event.stopPropagation();
				});
			},
			error:function ()
			{
				$('#search-result-body').html('');
			},
			async:true,
			dataType:'html'
		});
	}
})(jQuery);