window.salesDepot = window.salesDepot || { };

(function ($)
{
	var loadShortcutsPage = function (tabId)
	{
		var pageIdSelector = '#' + tabId + ' .sel.shortcuts-page';
		var pageId = $(pageIdSelector).attr('id');
		$.ajax({
			type: "POST",
			url: "shortcuts/getPage",
			data: {
				pageId: pageId
			},
			beforeSend: function ()
			{
				$('#content').html('');
				$.showOverlay();
			},
			complete: function ()
			{
				$.hideOverlay();
				$.updateContentAreaDimensions();
			},
			success: function (msg)
			{
				var content = $('#content');
				content.html(msg);

				var tabId = content.find('.shortcuts-page-content').attr('id').replace("shortcuts-page-content-", "");
				var shortcutsTab = $('#shortcuts-tab-' + tabId);
				var pageLogo = shortcutsTab.find('.ribbon-tab-logo');
				pageLogo.show();
				content.find('.shortcuts-home-bar img').on('click', function ()
				{
					pageLogo.trigger("click");
				});
				var linkLogo = shortcutsTab.find('.ribbon-link-logo');
				linkLogo.hide();

				$('.file-filter-panel-switcher').off('click').on('click', function (e)
				{
					e.preventDefault();
					var fileFilterPanel = $('.file-filter-panel');
					if (fileFilterPanel.hasClass('open'))
					{
						fileFilterPanel.removeClass('open');
						fileFilterPanel.hide(200, function ()
						{
							$.updateContentAreaDimensions();
						});
					}
					else
					{
						fileFilterPanel.addClass('open');
						fileFilterPanel.show(1000, 'swing', function ()
						{
							$.updateContentAreaDimensions();
						});
					}
					$.updateContentAreaDimensions();
				});
				$('.file-filter-panel .btn').on('click', function ()
				{
					$(this).button('toggle');
				});


				var search = function ()
				{
					var searchConditions = $('.shortcuts-search-bar .search-conditions');
					var textCondition = $('.shortcuts-search-bar .search-bar-text').val();
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

					if (searchConditions.find('.same-page').html() === 'true')
					{
						$('.shortcuts-search-bar').removeClass('open').hide();
						searchConditions.append('<div class="search-text">' + textCondition + '</div>');
						$.each(selectedFileTypes, function (index, value)
						{
							searchConditions.append('<div class="file-type">' + value + '</div>');
						});
						var content = $('#content .shortcuts-page-content');
						content.html('<div class="search-conditions" style="display: none;">' + searchConditions.html() + '</div>');
						$.processSearchLink(content);
					}
					else
						window.open("shortcuts/GetQuickSearchResult?pageId=" + pageId + "&text=" + textCondition + "&fileTypes=" + $.toJSON(selectedFileTypes));
				};
				$('.shortcuts-search-bar .search-bar-text').keypress(function (e)
				{
					if (e.which == 13)
						search();
				});
				$('.shortcuts-search-bar .search-bar-run').on('click', search);

				$('.shortcuts-link img').tooltip({animation: false, trigger: 'hover', delay: { show: 500, hide: 100 }});

				$('.shortcuts-link.preview').off('click').on('click', function ()
				{
					$.viewSelectedFormat($(this), false, true);
				});
				$('.shortcuts-link.library-file').off('click').on('click', function ()
				{
					$.requestViewDialog($(this).find('.link-id').html(), false);
				});
				$('.shortcuts-link.library-page').off('click').on('click', function ()
				{
					$.cookie("selectedLibraryName", $(this).find('.library-name').html(), {
						expires: (60 * 60 * 24 * 7)
					});
					$.cookie("selectedPageName", $(this).find('.page-name').html(), {
						expires: (60 * 60 * 24 * 7)
					});
					$.cookie("selectedRibbonTabId", 'home-tab', {
						expires: (60 * 60 * 24 * 7)
					});
					window.location.reload();
				});
				$('.shortcuts-link.embedded').off('click').on('click', function (e)
				{
					var link = $(this);
					e.preventDefault();
					$.ajax({
						type: "POST",
						url: $(this).attr('href'),
						beforeSend: function ()
						{
							$.showOverlayLight();
						},
						complete: function ()
						{
							$.updateContentAreaDimensions();
							$.hideOverlayLight();
						},
						success: function (msg)
						{
							$('.shortcuts-search-bar').removeClass('open').hide();
							var content = $('#content .shortcuts-page-content');

							if (link.hasClass('search'))
							{
								content.html(msg);
								$.processSearchLink(content)
							}
							else if (link.hasClass('window'))
							{
								content.html("<div class='padding'>" + msg + "</div>");
								$.assignLinkEvents($('#content'));
							}
							else
								content.html("<div class='padding'>" + msg + "</div>");

							var ribbonLogoPath = link.find('.ribbon-logo-path');
							if (ribbonLogoPath.length > 0)
							{
								linkLogo.attr('src', ribbonLogoPath.html());
								pageLogo.hide();
								linkLogo.show();
							}

							var shortcutTitle = content.find('.shortcut-title').html();
							$('.shortcuts-home-bar .title').html(shortcutTitle);
						},
						error: function ()
						{
						},
						async: true,
						dataType: 'html'
					});
				});
			},
			async: true,
			dataType: 'html'
		});
	};

	$.processSearchLink = function (content)
	{
		var searchConditions = content.find('.search-conditions');
		var hideResults = searchConditions.find('.hide-results').length > 0;
		var shortcutTitle = content.find('.shortcut-title').html();
		var sortColumn = content.find('.sort-column').html();
		var sortDirection = content.find('.sort-direction').html();
		var searchGrid = new $.LinkGrid();
		var getSearchResult = function (isSort)
		{
			var searchCondition = (function ()
			{
				var selectedCondition = '';
				var selectedConditionTag = searchConditions.find('.search-text');
				if (selectedConditionTag.length > 0)
					selectedCondition = selectedConditionTag.html();

				var selectedFileTypes = [];
				$.each(searchConditions.find('.file-type'), function ()
				{
					var fileType = $(this).html();
					selectedFileTypes.push(fileType);
					if (fileType == 'video')
					{
						selectedFileTypes.push("mp4");
						selectedFileTypes.push("wmv");
					}
					else if (fileType == 'image')
					{
						selectedFileTypes.push("png");
						selectedFileTypes.push("jpeg");
					}
				});

				var startDateTag = searchConditions.find('.start-date');
				if (startDateTag.length > 0)
					var startDate = startDateTag.html();
				var endDateTag = searchConditions.find('.end-date');
				if (endDateTag.length > 0)
					var endDate = endDateTag.html();

				var onlyFileCards = 0;

				var selectedLibraryIds = [];
				var libraryIds = searchConditions.find('.library');
				if (libraryIds.length > 0)
					$.each(libraryIds, function ()
					{
						selectedLibraryIds.push($(this).html());
					});

				var superFilters = [];
				$.each(searchConditions.find('.super-filter'), function ()
				{
					superFilters.push($(this).html());
				});

				var categories = [];
				$.each(searchConditions.find('.category'), function ()
				{
					var substr = $(this).html().split('------');
					var category = {
						category: substr[0],
						tag: substr[1]
					};
					categories.push(category);
				});

				var onlyWithCategories = searchConditions.find('.only-with-categories').html();
				var onlyByName = searchConditions.find('.search-by-name').html();
				var onlyByContent = searchConditions.find('.search-by-content').html();

				var datasetKey = isSort == 0 || searchGrid.datasetKey == null ? undefined : searchGrid.datasetKey;

				return {
					fileTypes: selectedFileTypes,
					condition: selectedCondition,
					startDate: startDate,
					endDate: endDate,
					dateFile: searchConditions.find('.use-file-date').html(),
					onlyFileCards: onlyFileCards,
					libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
					superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
					categories: categories.length > 0 ? $.toJSON(categories) : null,
					categoriesExactMatch: false,
					onlyWithCategories: onlyWithCategories,
					hideDuplicated: searchConditions.find('.hide-duplicated').html(),
					onlyByName: onlyByName,
					onlyByContent: onlyByContent,
					sortColumn: searchGrid.sortColumn != undefined && searchGrid.sortColumn != null ? searchGrid.sortColumn : sortColumn,
					sortDirection: searchGrid.sortDirection != undefined && searchGrid.sortDirection != null ? searchGrid.sortDirection : sortDirection,
					datasetKey: datasetKey
				};
			}());

			var beforeSearch = function ()
			{
				$.showOverlayLight();
			};

			var completeCallback = function ()
			{
				$.updateContentAreaDimensions();
				searchGrid.init({
					content: content,
					refreshCallback: function ()
					{
						getSearchResult(1);
					},
					sortColumn: isSort == 1 ? null : sortColumn,
					sortDirection: isSort == 1 ? null : sortDirection,
					showDelete: false
				});
				$.hideOverlayLight();
				$.hideOverlay();
			};

			var successCallback = function (msg)
			{
				content.html('');
				content.append(searchConditions);
				content.append($('<div id="search-container"><div id="search-result" style="width: 100% !important; padding: 0;"><div></div></div></div>'));
				content.find('#search-result > div').append(msg);

				var resultsBar = content.find('.search-grid-info');
				if (hideResults)
				{
					var linksFoundTag = resultsBar.find('#search-links-info-count span');
					var linksFound = linksFoundTag.length > 0 ? linksFoundTag.html() : 'No Tagged Files Exist';
					$('.shortcuts-home-bar .title').html(shortcutTitle != '' ? shortcutTitle + ' (' + linksFound + ')' : linksFound);
					resultsBar.remove();
				}
				else
					resultsBar.off('click').on('click', function ()
					{
						var linkIds = [];
						$.each(content.find(".links-grid-body").find('.link-id-column'), function ()
						{
							linkIds.push($(this).html());
						});
						if (linkIds.length > 0)
							$.requestSearchResultDialog(linkIds);
					});
			};
			$.runSearch(searchCondition, beforeSearch, completeCallback, successCallback);
		};
		getSearchResult(0);
	};

	$.initShortcutsView = function (tabId)
	{
		var pageSelector = '#' + tabId + ' .enabled.shortcuts-page';
		$('.shortcuts-page').off('click');
		$(pageSelector).on('click', function ()
		{
			$(pageSelector).removeClass('sel');
			$(this).addClass('sel');
			loadShortcutsPage(tabId);
		});
		loadShortcutsPage(tabId);
	};

	$(document).ready(function ()
	{
	});
})(jQuery);