(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var SearchManager = function ()
	{
		var searchGrid = new $.SalesPortal.LinkGrid();

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
					initControlPanel();
					updateContentSize();
					searchGrid.init({
						content: $('#search-result'),
						refreshCallback: function ()
						{
							getSearchResult(1);
						},
						showDelete: false
					});
					if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true")
						getSearchResult(1);
				},
				success: function (msg)
				{
					$('#content').html(msg);
				},
				error: function ()
				{
					$('#content').html('');
				},
				async: true,
				dataType: 'html'
			});
			$(window).off('resize').on('resize', updateContentSize);
		};

		var getSearchResult = function (isSort)
		{
			var searchCondition = (function ()
			{
				var textCondition = $('#condition-content-value');
				var dateCondition = $('#condition-date-range');

				var selectedCondition = textCondition.val();
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
				{
					selectedFileTypes.push("video");
					selectedFileTypes.push("mp4");
					selectedFileTypes.push("wmv");
				}
				if ($('#search-file-type-url').hasClass('active'))
					selectedFileTypes.push("url");
				if ($('#search-file-type-image').hasClass('active'))
				{
					selectedFileTypes.push("png");
					selectedFileTypes.push("jpeg");
				}

				var dateString = dateCondition.find('input').val().split(" - ");
				if (dateString.length == 2)
				{
					var startDate = dateString[0];
					var endDate = dateString[1];
				}

				var onlyFileCards = 0;

				var selectedLibraryIds = [];
				$('#libraries').find(':checked').each(function ()
				{
					selectedLibraryIds.push($(this).val());
				});

				var tagsOptions = $('#search-options-tags');
				var superFilters = [];
				$.each(tagsOptions.find('.super-filter-list .btn.active'), function ()
				{
					superFilters.push($(this).html());
				});

				var categories = [];
				$.each(tagsOptions.find(".tag-list .item-selector:checked"), function ()
				{
					var substr = $(this).val().split('------');
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

				var datasetKey = isSort == 0 || searchGrid.datasetKey == null ? undefined : searchGrid.datasetKey;

				//Save search state to recover while tabs are switching
				$.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId"), true, {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("textCondition" + $.cookie("selectedRibbonTabId"), textCondition.val(), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("dateCondition" + $.cookie("selectedRibbonTabId"), dateCondition.find('input').val(), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("selectedSuperFilters" + $.cookie("selectedRibbonTabId"), $.toJSON(superFilters), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("selectedCategories" + $.cookie("selectedRibbonTabId"), $.toJSON(categories), {
					expires: (60 * 60 * 24 * 7)
				});
				//-----------------------------------------------------

				return {
					fileTypes: selectedFileTypes,
					condition: selectedCondition,
					startDate: startDate,
					endDate: endDate,
					dateFile: $('#condition-date-file').hasClass('active'),
					onlyFileCards: onlyFileCards,
					libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
					superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
					categories: categories.length > 0 ? $.toJSON(categories) : null,
					categoriesExactMatch: false,
					onlyWithCategories: false,
					hideDuplicated: $('#hide-duplicated').hasClass('active'),
					onlyByName: onlyByName,
					onlyByContent: onlyByContent,
					sortColumn: searchGrid.sortColumn,
					sortDirection: searchGrid.sortDirection,
					datasetKey: datasetKey
				};
			}());

			var beforeSearch = function ()
			{
				$.SalesPortal.Overlay.show(false);
			};

			var completeCallback = function ()
			{
				updateContentSize();
				searchGrid.init({
					content: $('#search-result'),
					refreshCallback: function ()
					{
						getSearchResult(1);
					},
					showDelete: false
				});
				$.SalesPortal.Overlay.hide();
			};

			var successCallback = function (msg)
			{
				var searchResult = $('#search-result');
				searchResult.find('>div').html('').append(msg);
				searchResult.find('.search-grid-info.has-result').off('click').on('click', function ()
				{
					var linkIds = [];
					$.each(searchResult.find('.links-grid-body').find('.link-id-column'), function ()
					{
						linkIds.push($(this).html());
					});
					if (linkIds.length > 0)
						$.SalesPortal.SearchHelper.requestSearchResultDialog(linkIds);
				});
			};

			$.SalesPortal.SearchHelper.runSearch(searchCondition, beforeSearch, completeCallback, successCallback);
		};

		var initSearchButtons = function ()
		{
			$("#run-search-full").off('click').on('click', function ()
			{
				getSearchResult(0);
			});
		};

		var initSideBarToggle = function ()
		{
			$(".side-bar-toggle").off('click').on('click', function ()
			{
				var button = $(this);
				button.removeClass('.ribbon-hot');
				var sideBar = $('#right-navbar');
				var sideBarVisible = true;
				if (button.hasClass('sel'))
				{
					button.removeClass('sel');
					sideBar.hide("slide", { direction: "left" });
					sideBarVisible = false;
				}
				else
				{
					button.addClass('sel');
					sideBar.show("slide", { direction: "right" });
					sideBarVisible = true;
				}
				if (searchGrid.hasData)
					searchGrid.refreshData();
				else
					updateContentSize();
				$.cookie("sideBarVisible", sideBarVisible, {
					expires: (60 * 60 * 24 * 7)
				});
			});
		};

		var initTabControl = function ()
		{
			var conditionType = 0;
			if ($.cookie("search-control-panel") != null)
				conditionType = parseInt($.cookie("search-control-panel"));

			var disabled = [];

			var tagsCount = $('#search-options-tags').find('.tag-list').find('input[type="checkbox"]').length;
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
				active: conditionType,
				disabled: disabled,
				activate: function (event, ui)
				{
					$.cookie("search-control-panel", ui.newTab.index(), {
						expires: (60 * 60 * 24 * 7)
					});
				}});
		};

		var initKeywordFiled = function ()
		{
			var rightPanel = $("#right-navbar");
			var textCondition = $('#condition-content-value');
			if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true" && $.cookie("textCondition" + $.cookie("selectedRibbonTabId")) != null)
				textCondition.val($.cookie("textCondition" + $.cookie("selectedRibbonTabId")));
			$("#clear-content-value").off('click').on('click', function ()
			{
				textCondition.val('');
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
					$('#content-compare-type').find('.btn').removeClass('active').blur();
					$(this).addClass('active');
				}
				$.cookie("exactMatch", $('#content-compare-exact').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
			});
			rightPanel.find("input").keypress(function (e)
			{
				return e.which != 13;
			});
			textCondition.keypress(function (e)
			{
				if (e.which == 13)
				{
					getSearchResult(0);
				}
			});

			var hideDuplicated = $("#hide-duplicated");
			if ($.cookie("hideDuplicated") != null && $.cookie("hideDuplicated") == "true")
				hideDuplicated.button('toggle');
			hideDuplicated.off('click').on('click', function ()
			{
				if ($(this).hasClass('active'))
					$(this).removeClass('active').blur();
				else
					$(this).addClass('active');
				$.cookie("hideDuplicated", hideDuplicated.hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
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
					rightPanel.find(".search-fields-option").removeClass('active').blur();
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
					expires: (60 * 60 * 24 * 7)
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

			if ($.cookie("fileTypeUrl") != null)
			{
				if ($.cookie("fileTypeUrl") == "true")
					$("#search-file-type-url").button('toggle');
			}
			else
				$("#search-file-type-url").button('toggle');

			if ($.cookie("fileTypeImage") != null)
			{
				if ($.cookie("fileTypeImage") == "true")
					$("#search-file-type-image").button('toggle');
			}
			else
				$("#search-file-type-image").button('toggle');

			var fileTypesPane = $('#file-types');
			fileTypesPane.find('input[type="checkbox"]').button({
				text: false
			});
			fileTypesPane.find('.search-file-type').off('click').on('click', function ()
			{
				if ($(this).hasClass('active'))
					$(this).removeClass('active').blur();
				else
					$(this).addClass('active');
				$.cookie("fileTypePpt", $('#search-file-type-powerpoint').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeDoc", $('#search-file-type-word').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeXls", $('#search-file-type-excel').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypePdf", $('#search-file-type-pdf').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeVideo", $('#search-file-type-video').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeUrl", $('#search-file-type-url').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
				$.cookie("fileTypeImage", $('#search-file-type-image').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
			});
		};

		var initDateRange = function ()
		{
			var dateFormat = 'MM/DD/YYYY';
			var dateCondition = $('#condition-date-range');
			dateCondition.find('input').daterangepicker(
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
				}
			);

			if ($.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId")) == "true" && $.cookie("dateCondition" + $.cookie("selectedRibbonTabId")) != null)
				dateCondition.find('input').val($.cookie("dateCondition" + $.cookie("selectedRibbonTabId")));

			$("#clear-date-range").off('click').on('click', function ()
			{
				dateCondition.find('input').val('');
			});

			if ($.cookie("conditionDateByFile") != null)
			{
				if ($.cookie("conditionDateByFile") == "true")
					$("#condition-date-file").button('toggle');
				else
					$("#condition-date-link").button('toggle');
			}
			else
				$("#condition-date-link").button('toggle');
			$('#condition-date-file, #condition-date-link').off('click').on('click', function ()
			{
				if (!$(this).hasClass('active'))
				{
					$('#condition-date-file, #condition-date-link').removeClass('active').blur();
					$(this).addClass('active');
				}
				$.cookie("conditionDateByFile", $('#condition-date-file').hasClass('active'), {
					expires: (60 * 60 * 24 * 7)
				});
			});

			$("#clear-date-value").off('click').on('click', function ()
			{
				dateCondition.find('input').val('');
			});
		};

		var initTags = function ()
		{
			var tagsOptions = $('#search-options-tags');
			var categories = tagsOptions.find(".tag-list");
			var superFilters = tagsOptions.find(".super-filter-list");

			categories.accordion({
				heightStyle: "content",
				active: false,
				collapsible: true,
				icons: {
					header: "ui-icon-circle-arrow-e",
					activeHeader: "ui-icon-circle-arrow-s"
				}
			});

			categories.find('.group-selector').off('change').on('change', function ()
			{
				var categoryGroup = $(this).parent().parent();
				categoryGroup.find('.item-selector').prop('checked', $(this).is(':checked'));
			});

			tagsOptions.find('.tags-clear-all').off('click').on('click', function ()
			{
				superFilters.find('.btn').removeClass('active').blur();
				categories.find(":checked").prop('checked', false);
			});

			superFilters.find('.btn').off('click').on('click', function ()
			{
				$(this).button('toggle').blur();
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
					expires: (60 * 60 * 24 * 7)
				});
			};

			var librariesPane = $('#libraries');
			var groupsCount = librariesPane.find('h3').length;

			$("#libraries").accordion({
				heightStyle: "content",
				active: groupsCount > 1 ? false : 0,
				collapsible: groupsCount > 1,
				icons: {
					header: "ui-icon-circle-arrow-e",
					activeHeader: "ui-icon-circle-arrow-s"
				}
			});

			librariesPane.find('input[type="checkbox"]').on('change', function ()
			{
				saveSelectedLibraries();
			});
			$('#library-select-all').off('click').on('click', function ()
			{
				$('#libraries').find('input[type="checkbox"]').prop('checked', true);
				saveSelectedLibraries();
			});

			$('#library-clear-all').off('click').on('click', function ()
			{
				$("#libraries").find(":checked").prop('checked', false);
				saveSelectedLibraries();
			});

			saveSelectedLibraries();
		};

		var initControlPanel = function ()
		{
			initSearchButtons();
			initSideBarToggle();
			initKeywordFiled();
			initFileTypes();
			initDateRange();
			initTags();
			initLibraries();
			initTabControl();

			$('.clear-button').off('click').on('click', function ()
			{
				$.cookie("recoverSearchState" + $.cookie("selectedRibbonTabId"), false, {
					expires: (60 * 60 * 24 * 7)
				});
				var searchCondition = (function ()
				{
					return {isClear: 1};
				}());

				var beforeSearch = function ()
				{
					$.SalesPortal.Overlay.show(false);
				};

				var completeCallback = function ()
				{
					$.SalesPortal.Overlay.hide();
					updateContentSize();
					searchGrid.init({
						content: $('#search-result'),
						refreshCallback: function ()
						{
							getSearchResult(1);
						},
						showDelete: false
					});
				};

				var successCallback = function (msg)
				{
					$('#search-result').find('>div').html('').append(msg);
				};
				$.SalesPortal.SearchHelper.runSearch(searchCondition, beforeSearch, completeCallback, successCallback);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			var height = $('#content').height();
			$('#right-navbar').find('> div').css({
				'height': height + 'px'
			});
			$('#search-options-tags').find('.tag-list-container').css({
				'height': (height - 198) + 'px'
			});
			$('#libraries-container').css({
				'height': (height - 142) + 'px'
			});

			$('#file-types-container').css({
				'height': (height - 47) + 'px'
			});

			var searchResult = $('#search-result');

			height = $('#search-container').parent().height();
			searchResult.find('> div').css({
				'height': height + 'px'
			});
			var gridHeader = searchResult.find('.links-grid-header');
			var serchResultBar = searchResult.find('.search-grid-info');
			searchResult.find('.links-grid-body-container').css({
				'height': (searchResult.find('> div').height() - (serchResultBar.length > 0 ? (serchResultBar.height() + 12) : 0) - gridHeader.height()) + 'px'
			});

			var linkDateWidth = 100;

			var linkNameHeaderWidth = searchResult.width() -
				gridHeader.find('td.details-button').width() -
				gridHeader.find('td.library-column').width() -
				gridHeader.find('td.link-type-column').width() -
				gridHeader.find('td.link-tag-column').width() -
				linkDateWidth;
			gridHeader.find('td.link-name-column').css({
				'width': linkNameHeaderWidth + 'px'
			});

			var gridBody = searchResult.find('.links-grid-body');
			var linkNameBodyWidth = searchResult.width() -
				gridBody.find('td.details-button').width() -
				gridBody.find('td.library-column').width() -
				gridBody.find('td.link-type-column').width() -
				gridBody.find('td.link-tag-column').width() -
				linkDateWidth;
			gridBody.find('td.link-name-column').css({
				'width': linkNameBodyWidth + 'px'
			});
		};
	};
	$.SalesPortal.Search = new SearchManager();
})(jQuery);