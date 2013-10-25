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
				var pageLogo = $('#shortcuts-tab-' + tabId).find('.ribbon-tab-logo');
				content.find('.shortcuts-home-bar img').on('click', function ()
				{
					pageLogo.trigger("click");
				});

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
		var searchConditions = content.find('#search-conditions');
		var hideResults = searchConditions.find('.hide-results').length > 0;
		var shortcutTitle = content.find('.shortcut-title').html();
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

				var onlyByName = searchConditions.find('.search-by-name').html();
				var onlyByContent = searchConditions.find('.search-by-content').html();

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
					hideDuplicated: searchConditions.find('.hide-duplicated').html(),
					onlyByName: onlyByName,
					onlyByContent: onlyByContent,
					isSort: isSort
				};
			}());

			var beforeSearch = function ()
			{
				$.showOverlayLight();
			};

			var completeCallback = function ()
			{
				$.updateContentAreaDimensions();
				$.linkGrid.refreshData = function ()
				{
					getSearchResult(1);
				};
				$.linkGrid.showDelete = false;
				$.linkGrid.init();
				$.hideOverlayLight();
			};

			var successCallback = function (msg)
			{
				content.html('');
				content.append(searchConditions);
				content.append($('<div id="search-container"><div id="search-result" style="width: 100% !important; padding: 0;"><div></div></div></div>'));
				content.find('#search-result > div').append(msg);

				var resultsBar = content.find('.search-grid-info.has-result');
				if (hideResults)
				{
					$('.shortcuts-home-bar .title').html(shortcutTitle != '' ?
						shortcutTitle + ' (' + resultsBar.find('#search-links-info-count span').html() + ')' :
						resultsBar.find('#search-links-info-count span').html()
					);
					resultsBar.remove();
				}
				else
					resultsBar.off('click').on('click', function ()
					{
						var linkIds = [];
						$.each($("#links-grid-body").find('.link-id-column'), function ()
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