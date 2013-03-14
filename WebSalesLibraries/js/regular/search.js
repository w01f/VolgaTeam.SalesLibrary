(function ($)
{
	var runSearch = function (isSort)
	{
		var selectedCondition = $('#condition-content-value').val();
		if ($('#content-compare-exact').hasClass('active'))
			selectedCondition = '"' + selectedCondition + '"';

		var selectedFileTypes = [];
		if ($('#search-file-type-powerpoint').hasClass('active'))
			selectedFileTypes.push("ppt");
		if ($('#search-file-type-word').hasClass('active'))
			selectedFileTypes.push("doc");
		if ($('#search-file-type-excel').hasClass('active'))
			selectedFileTypes.push("xls");
		if ($('#search-file-type-pdf').hasClass('active'))
			selectedFileTypes.push("pdf");
		if ($('#search-file-type-video').hasClass('active'))
			selectedFileTypes.push("video");

		var dateString = $('#condition-date-range').find('input').val().split(" - ");
		if (dateString.length == 2)
		{
			var startDate = dateString[0];
			var endDate = dateString[1];
		}

		var selectedTabId = $.cookie("selectedRibbonTabId");
		var onlyFileCards = 0;
		if (selectedTabId == 'search-file-card-tab')
			onlyFileCards = 1;
		else if ($.cookie("onlyFileCards") != null)
			onlyFileCards = parseInt($.cookie("onlyFileCards"));

		var categories = [];
		$.each($("#categories").find(":checked"), function ()
		{
			var substr = $(this).val().split('------');
			var category = {
				category:substr[0],
				tag:substr[1]
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

		//Save search state to recover while tabs are switching
		$.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId"), true, {
			expires:(60 * 60 * 24 * 7)
		});
		$.cookie("textCondition" + $.cookie("selectedRibbonTabId"), $('#condition-content-value').val(), {
			expires:(60 * 60 * 24 * 7)
		});
		$.cookie("dateCondition" + $.cookie("selectedRibbonTabId"), $('#condition-date-range').find('input').val(), {
			expires:(60 * 60 * 24 * 7)
		});
		$.cookie("selectedCategories" + $.cookie("selectedRibbonTabId"), $.toJSON(categories), {
			expires:(60 * 60 * 24 * 7)
		});
		//-----------------------------------------------------

		$.ajax({
			type:"POST",
			url:"search/searchByContent",
			data:{
				fileTypes:selectedFileTypes,
				condition:selectedCondition,
				startDate:startDate,
				endDate:endDate,
				dateFile:$('#condition-date-file').hasClass('active'),
				onlyFileCards:onlyFileCards,
				categories:categories.length > 0 ? $.toJSON(categories) : null,
				categoriesExactMatch:$('#tags-compare-exact').hasClass('active'),
				hideDuplicated:$('#hide-duplicated').hasClass('active'),
				onlyByName:onlyByName,
				onlyByContent:onlyByContent,
				isSort:isSort
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
				$('#search-links-number').find('>span').html('');
			},
			complete:function ()
			{
				$.hideOverlayLight();
				$.updateContentAreaDimensions();
				$.linkGrid.refreshData = function ()
				{
					runSearch(1);
				};
				$.linkGrid.showDelete = false;
				$.linkGrid.init();
			},
			success:function (msg)
			{
				$('#search-result').find('>div').html('').append(msg);
			},
			error:function ()
			{
				$('#search-result').find('>div').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	var initSearchButtons = function ()
	{
		$("#run-search-full").off('click').on('click', function ()
		{
			runSearch(0);
		});
		$("#run-search-file-card").off('click').on('click', function ()
		{
			runSearch(0);
		});
	};

	var initTabControl = function ()
	{
		var conditionType = 0;
		if ($.cookie("search-control-panel") != null)
			conditionType = parseInt($.cookie("search-control-panel"));

		var disabled = [];

		var tagsCount = $('#categories').find('input[type="checkbox"]').length;
		if (tagsCount < 1)
		{
			if (conditionType == 1)
				conditionType = 0;
			disabled.push(1);
		}

		var librariesCount = $('#libraries').find('input[type="checkbox"]').length;
		if (librariesCount < 2)
		{
			if (conditionType == 4)
				conditionType = 0;
			disabled.push(4);
		}

		$("#search-control-panel").tabs({
			selected:conditionType,
			disabled:disabled
		}).on('tabsselect', function (event, ui)
			{
				$.cookie("search-control-panel", ui.index, {
					expires:(60 * 60 * 24 * 7)
				});
			})
	};

	var initFileCard = function ()
	{
		var toggleSearchFileCard = function (toggleState)
		{
			if (toggleState == 1)
				$('#search-file-card-button').addClass('sel');
			else
				$('#search-file-card-button').removeClass('sel');

			$.cookie("onlyFileCards", toggleState, {
				expires:(60 * 60 * 24 * 7)
			});
		};

		var selectedTabId = $.cookie("selectedRibbonTabId");
		if (selectedTabId == 'search-full-tab')
		{
			var onlyFileCards = 0;
			if ($.cookie("onlyFileCards") != null)
				onlyFileCards = parseInt($.cookie("onlyFileCards"));
			toggleSearchFileCard(onlyFileCards);
			$("#search-file-card-button").off('click').on('click', function ()
			{
				var onlyFileCards = 0;
				if ($.cookie("onlyFileCards") != null)
					onlyFileCards = parseInt($.cookie("onlyFileCards"));
				if (onlyFileCards == 0)
					onlyFileCards = 1;
				else
					onlyFileCards = 0;
				toggleSearchFileCard(onlyFileCards);
			});
		}
	};

	var initKeywordFiled = function ()
	{
		var rightPanel = $("#right-navbar");

		if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true" && $.cookie("textCondition" + $.cookie("selectedRibbonTabId")) != null)
			$('#condition-content-value').val($.cookie("textCondition" + $.cookie("selectedRibbonTabId")));

		$("#clear-content-value").off('click').on('click', function ()
		{
			$('#condition-content-value').val('');
		});

		if ($.cookie("exactMatch") != null)
		{
			if ($.cookie("exactMatch") == "true")
				$("#content-compare-exact").button('toggle');
			else
				$("#content-compare-partial").button('toggle');
		}
		else
			$("#content-compare-exact").button('toggle');
		$("#content-compare-exact, #content-compare-partial").off('click').on('click', function ()
		{
			if (!$(this).hasClass('active'))
			{
				$('#content-compare-type').find('.btn').removeClass('active');
				$(this).addClass('active');
			}
			$.cookie("exactMatch", $('#content-compare-exact').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});
		rightPanel.find("input").keypress(function (e)
		{
			if (e.which == 13)
			{
				return false;
			}
		});
		$('#condition-content-value').keypress(function (e)
		{
			if (e.which == 13)
			{
				runSearch(0);
			}
		});

		var hideDuplicated = $("#hide-duplicated");
		if ($.cookie("hideDuplicated") != null && $.cookie("hideDuplicated") == "true")
			hideDuplicated.button('toggle');
		hideDuplicated.off('click').on('click', function ()
		{
			if ($(this).hasClass('active'))
				$(this).removeClass('active');
			else
				$(this).addClass('active');
			$.cookie("hideDuplicated", hideDuplicated.hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});

		if ($.cookie("searchFields") != null)
		{
			if ($.cookie("searchFields") == "all")
				$("#content-full").button('toggle');
			else if ($.cookie("searchFields") == "name")
				$("#content-only-file").button('toggle');
			else if ($.cookie("searchFields") == "text")
				$("#content-only-text").button('toggle');
		}
		else
			$("#content-full").button('toggle');

		rightPanel.find(".search-fields-option").off('click').on('click', function ()
		{
			if (!$(this).hasClass('active'))
			{
				rightPanel.find(".search-fields-option").removeClass('active');
				$(this).addClass('active');
				var id = $(this).attr('id');
				var value = "all";
				switch (id)
				{
					case "content-full":
						value = "all";
						break;
					case "content-only-file":
						value = "name";
						break;
					case "content-only-text":
						value = "text";
						break;
				}
			}
			$.cookie("searchFields", value, {
				expires:(60 * 60 * 24 * 7)
			});
		});

		$("#clear-all-content-value").off('click').on('click', function ()
		{
			$('#condition-content-value').val('');
		});
	};

	var initFileTypes = function ()
	{
		if ($.cookie("fileTypePpt") != null)
		{
			if ($.cookie("fileTypePpt") == "true")
				$("#search-file-type-powerpoint").button('toggle');
		}
		else
			$("#search-file-type-powerpoint").button('toggle');

		if ($.cookie("fileTypeDoc") != null)
		{
			if ($.cookie("fileTypeDoc") == "true")
				$("#search-file-type-word").button('toggle');
		}
		else
			$("#search-file-type-word").button('toggle');

		if ($.cookie("fileTypeXls") != null)
		{
			if ($.cookie("fileTypeXls") == "true")
				$("#search-file-type-excel").button('toggle');
		}
		else
			$("#search-file-type-excel").button('toggle');

		if ($.cookie("fileTypePdf") != null)
		{
			if ($.cookie("fileTypePdf") == "true")
				$("#search-file-type-pdf").button('toggle');
		}
		else
			$("#search-file-type-pdf").button('toggle');

		if ($.cookie("fileTypeVideo") != null)
		{
			if ($.cookie("fileTypeVideo") == "true")
				$("#search-file-type-video").button('toggle');
		}
		else
			$("#search-file-type-video").button('toggle');

		$('#file-types').find('input[type="checkbox"]').button({
			text:false
		});
		$('#file-types').find('.search-file-type').off('click').on('click', function ()
		{
			if ($(this).hasClass('active'))
				$(this).removeClass('active');
			else
				$(this).addClass('active');
			$.cookie("fileTypePpt", $('#search-file-type-powerpoint').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeDoc", $('#search-file-type-word').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeXls", $('#search-file-type-excel').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypePdf", $('#search-file-type-pdf').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
			$.cookie("fileTypeVideo", $('#search-file-type-video').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});
	};

	var initDateRange = function ()
	{
		var dateFormat = 'MM/dd/yyyy';
		$('#condition-date-range').daterangepicker(
			{
				format:dateFormat,
				ranges:{
					'Last day':['yesterday', 'today'],
					'Last 15 days':[Date.today().add({
						days:-14
					}), 'today'],
					'Last 30 days':[Date.today().add({
						days:-29
					}), 'today']
				}
			},
			function (start, end)
			{
				$('#condition-date-range').find('input').val(start.toString(dateFormat) + ' - ' + end.toString(dateFormat));
			}
		);

		if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true" && $.cookie("dateCondition" + $.cookie("selectedRibbonTabId")) != null)
			$('#condition-date-range').find('input').val($.cookie("dateCondition" + $.cookie("selectedRibbonTabId")));

		$("#clear-date-range").off('click').on('click', function ()
		{
			$('#condition-date-range').find('input').val('');
		});

		if ($.cookie("conditionDateByFile") != null)
		{
			if ($.cookie("conditionDateByFile") == "true")
				$("#condition-date-file").button('toggle');
			else
				$("#condition-date-link").button('toggle');
		}
		else
			$("#condition-date-file").button('toggle');
		$('#condition-date-file, #condition-date-link').off('click').on('click', function ()
		{
			if (!$(this).hasClass('active'))
			{
				$('#condition-date-file, #condition-date-link').removeClass('active');
				$(this).addClass('active');
			}
			$.cookie("conditionDateByFile", $('#condition-date-file').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});

		$("#clear-date-value").off('click').on('click', function ()
		{
			$('#condition-date-range').find('input').val('');
		});
	};

	var initTags = function ()
	{
		$("#categories").accordion({
			autoHeight:false,
			active:false,
			collapsible:true,
			icons:{
				header:"ui-icon-circle-arrow-e",
				activeHeader:"ui-icon-circle-arrow-s"
			}
		});
		$('#tags-clear-all').off('click').on('click', function ()
		{
			$("#categories").find(":checked").attr('checked', false);
		});

		if ($.cookie("tagsExactMatch") != null)
		{
			if ($.cookie("tagsExactMatch") == "true")
				$("#tags-compare-exact").button('toggle');
			else
				$("#tags-compare-partial").button('toggle');
		}
		else
			$("#tags-compare-exact").button('toggle');
		$("#tags-compare-exact,#tags-compare-partial").off('click').on('click', function ()
		{
			if (!$(this).hasClass('active'))
			{
				$('#tags-compare-type').find('.btn').removeClass('active');
				$(this).addClass('active');
			}
			$.cookie("tagsExactMatch", $('#tags-compare-exact').hasClass('active'), {
				expires:(60 * 60 * 24 * 7)
			});
		});
	};

	var initLibraries = function ()
	{
		var saveSelectedLibraries = function ()
		{
			var selectedLibraryIds = [];
			$('#libraries').find(':checked').each(function ()
			{
				selectedLibraryIds.push($(this).val());
			});
			$.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
				expires:(60 * 60 * 24 * 7)
			});
		};

		var groupsCount = $('#libraries').find('h3').length;

		$("#libraries").accordion({
			autoHeight:false,
			active:groupsCount > 1 ? false : 0,
			collapsible:!!(groupsCount > 1),
			icons:{
				header:"ui-icon-circle-arrow-e",
				activeHeader:"ui-icon-circle-arrow-s"
			}
		});

		$('#libraries').find('input[type="checkbox"]').on('change', function ()
		{
			saveSelectedLibraries();
		});
		$('#library-select-all').off('click').on('click', function ()
		{
			$('#libraries').find('input[type="checkbox"]').attr('checked', true);
			saveSelectedLibraries();
		});

		$('#library-clear-all').off('click').on('click', function ()
		{
			$("#libraries").find(":checked").attr('checked', false);
			saveSelectedLibraries();
		});

		saveSelectedLibraries();
	};

	var initControlPanel = function ()
	{
		initSearchButtons();
		initFileCard();
		initKeywordFiled();
		initFileTypes();
		initDateRange();
		initTags();
		initLibraries();
		initTabControl();

		$('.clear-button').off('click').on('click', function ()
		{
			$.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId"), false, {
				expires:(60 * 60 * 24 * 7)
			});
			$.ajax({
				type:"POST",
				url:"search/searchByContent",
				data:{
					isClear:1
				},
				beforeSend:function ()
				{
					$.showOverlayLight();
					$('#search-links-number').find('>span').html('');
				},
				complete:function ()
				{
					$.hideOverlayLight();
					$.updateContentAreaDimensions();
					$.linkGrid.refreshData = function ()
					{
						runSearch(1);
					};
					$.linkGrid.showDelete = false;
					$.linkGrid.init();
				},
				success:function (msg)
				{
					$('#search-result').find('>div').html('').append(msg);
				},
				error:function ()
				{
					$('#search-result').find('>div').html('');
				},
				async:true,
				dataType:'html'
			});
		});
	};

	$.initSearchView = function ()
	{
		$.ajax({
			type:"POST",
			url:"search/getSearchView",
			beforeSend:function ()
			{
				$('#content').html('');
				$.showOverlay();
			},
			complete:function ()
			{
				$.hideOverlay();
				initControlPanel();
				$.updateContentAreaDimensions();
				$.linkGrid.refreshData = function ()
				{
					runSearch(1);
				};
				$.linkGrid.showDelete = false;
				$.linkGrid.init();
				if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true")
					runSearch(1);
			},
			success:function (msg)
			{
				$('#content').html(msg);
			},
			error:function ()
			{
				$('#content').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
	});
})(jQuery);