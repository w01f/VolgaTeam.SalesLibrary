(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		var that = this;
		this.init = function (tabId)
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
			$(window).off('resize').on('resize', updateContentSize);
		};

		this.processSearchLink = function (content)
		{
			var searchConditions = content.find('.search-conditions');
			var homeBar = content.find('.shortcuts-home-bar');
			var hideResults = searchConditions.find('.hide-results').length > 0;
			var shortcutTitle = content.find('.shortcut-title').html();
			var sortColumn = content.find('.sort-column').html();
			var sortDirection = content.find('.sort-direction').html();
			var searchGrid = new $.SalesPortal.LinkGrid();
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
					$.SalesPortal.Overlay.show(false);
				};

				var completeCallback = function ()
				{
					updateContentSize();
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
					$.SalesPortal.Overlay.hide();
				};

				var successCallback = function (msg)
				{
					content.html('');
					content.append(searchConditions);
					content.append(homeBar);
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
								$.SalesPortal.SearchHelper.requestSearchResultDialog(linkIds);
						});
				};
				$.SalesPortal.SearchHelper.runSearch(searchCondition, beforeSearch, completeCallback, successCallback);
			};
			getSearchResult(0);
		};

		var loadShortcutsPage = function (tabId)
		{
			var pageIdSelector = '#' + tabId + ' .sel.shortcuts-page';
			var pageId = $(pageIdSelector).attr('id');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getPage",
				data: {
					pageId: pageId
				},
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
					updateContentSize();
				},
				success: function (msg)
				{
					var content = $('#content');
					content.html(msg);
					var pageContent = content.find('.shortcuts-page-content');
					var tabId = pageContent.attr('id').replace("shortcuts-page-content-", "");
					var shortcutsTab = $('#shortcuts-tab-' + tabId);
					var pageLogo = shortcutsTab.find('.ribbon-tab-logo');
					pageLogo.show();
					content.find('.shortcuts-home-bar .logo-container img').on('click', function ()
					{
						pageLogo.trigger("click");
					});
					content.find('.shortcuts-home-bar .buttons-container img').on('click', function (e)
					{
						var isExpanded = $(this).hasClass('expanded');
						if (isExpanded)
						{
							$(this).removeClass('expanded');
							$(this).attr('src', $(this).attr('src').replace('collapse', 'expand'));
							changeSearchBarVisibility(false);
						}
						else
						{
							$(this).addClass('expanded');
							$(this).attr('src', $(this).attr('src').replace('expand', 'collapse'));
							changeSearchBarVisibility(true);
						}
						updateContentSize();
						e.stopPropagation();
					});
					var linkLogo = shortcutsTab.find('.ribbon-link-logo');
					linkLogo.hide();

					$('.file-filter-panel .file-selector input').off('change').on('change', function ()
					{
						updateSearchButtonState();
					});

					$('.tags-filter-panel-switcher').off('click').on('click', function ()
					{
						setTagsCondition();
					});

					var search = function ()
					{
						if ($('.btn.search-bar-run').hasClass('disabled')) return;
						var textCondition = $('.shortcuts-search-bar .search-bar-text').val();
						if ($('#search-exact-match').is(':checked'))
							textCondition = '"' + textCondition + '"';
						var searchConditions = $('.shortcuts-search-bar .search-conditions');
						var onlyFiles = $('#search-file-names-only').is(':checked') ? 1 : 0;
						var selectedFileTypes = [];
						if ($('#search-file-type-powerpoint').is(':checked'))
							selectedFileTypes.push("ppt");
						if ($('#search-file-type-video').is(':checked'))
						{
							selectedFileTypes.push("video");
							selectedFileTypes.push("mp4");
							selectedFileTypes.push("wmv");
						}
						if ($('#search-file-type-other').is(':checked'))
						{
							selectedFileTypes.push("doc");
							selectedFileTypes.push("xls");
							selectedFileTypes.push("pdf");
							selectedFileTypes.push("url");
							selectedFileTypes.push("png");
							selectedFileTypes.push("jpeg");
						}
						if (searchConditions.find('.same-page').html() === 'true')
						{
							changeSearchBarVisibility(false);
							$('.shortcuts-home-bar .buttons-container').hide();
							searchConditions.remove('.search-text');
							searchConditions.append('<div class="search-text">' + textCondition + '</div>');
							var searchByContent = searchConditions.find('.search-by-content');
							if (searchByContent.length > 0)
								searchByContent.html(onlyFiles != 1 ? 'true' : false);
							else
								searchConditions.append('<div class="search-by-content">' + (onlyFiles != 1) + '</div>');
							searchConditions.remove('.file-type');
							$.each(selectedFileTypes, function (index, value)
							{
								searchConditions.append('<div class="file-type">' + value + '</div>');
							});
							var content = $('#content').find('.shortcuts-page-content');
							content.html('<div class="search-conditions" style="display: none;">' + searchConditions.html() + '</div>');
							that.processSearchLink(content);
						}
						else
						{
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
							window.open("shortcuts/GetQuickSearchResult?pageId=" + pageId + "&text=" + textCondition + "&onlyFiles=" + onlyFiles + "&fileTypes=" + $.toJSON(selectedFileTypes) + "&superFilters=" + $.toJSON(superFilters) + "&categories=" + $.toJSON(categories));
						}
					};
					$('.shortcuts-search-bar .search-bar-text').keypress(function (e)
					{
						updateSearchButtonState();
						if (e.which == 13)
							search();
					});
					$('.shortcuts-search-bar .search-bar-run').on('click', search);
					pageContent.find('.shortcuts-links-container').cubeportfolio({
						defaultFilter: '*',
						animationType: 'slideDelay',
						gapHorizontal: 20,
						gapVertical: 20,
						gridAdjustment: 'responsive',
						caption: 'overlayBottomAlong',
						displayType: 'bottomToTop',
						displayTypeSpeed: 100
					});
					$('.shortcuts-link.direct').off('click').on('click', function ()
					{
						var linkName = $(this).find('.link-name').html();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
							data: {
								type: 'Shortcuts',
								subType: 'Open',
								data: $.toJSON({
									File: linkName,
									'Original Format': 'url'
								})
							},
							async: true,
							dataType: 'html'
						});
					});
					$('.shortcuts-link.preview').off('click').on('click', function ()
					{
						$.SalesPortal.LinkManager.viewSelectedFormat($(this), false, true);
					});
					$('.shortcuts-link.library-file').off('click').on('click', function ()
					{
						$.SalesPortal.LinkManager.requestViewDialog($(this).find('.link-id').html(), false);
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
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								updateContentSize();
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								changeSearchBarVisibility(false);
								$('.shortcuts-home-bar .buttons-container').hide();
								var pageContent = content.find('.shortcuts-page-content');
								if (link.hasClass('search'))
								{
									pageContent.html(msg);
									that.processSearchLink(pageContent)
								}
								else if (link.hasClass('window'))
								{
									pageContent.html("<div class='padding'>" + msg + "</div>");
									$.SalesPortal.Wallbin.assignLinkEvents($('#content'));
								}
								else
									pageContent.html("<div class='padding'>" + msg + "</div>");
								content.animate({scrollTop: 0}, 1);

								var ribbonLogoPath = link.find('.ribbon-logo-path');
								if (ribbonLogoPath.length > 0)
								{
									linkLogo.attr('src', ribbonLogoPath.html());
									pageLogo.hide();
									linkLogo.show();
								}

								var shortcutTitle = pageContent.find('.shortcut-title').html();
								$('.shortcuts-home-bar .title').html(shortcutTitle);
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					});

					updateSearchButtonState();

					updateSelectedCategories();
				},
				async: true,
				dataType: 'html'
			});
		};

		var changeSearchBarVisibility = function (show)
		{
			var searchBar = $('.shortcuts-search-bar');
			if (show)
				searchBar.addClass('open').show();
			else
				searchBar.removeClass('open').hide();
		};

		var updateSearchButtonState = function ()
		{
			var hasKeyword = $('.shortcuts-search-bar .search-bar-text').val() != "";
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var hasSuperFilters = searchConditions.find('.super-filter').length > 0;
			var hasCategories = searchConditions.find('.category').length > 0;
			var searchButton = $('.btn.search-bar-run');
			searchButton.removeClass('disabled');
			if (!(hasKeyword || hasSuperFilters || hasCategories) || !($('#search-file-type-powerpoint').is(':checked') || $('#search-file-type-video').is(':checked') || $('#search-file-type-other').is(':checked')))
				searchButton.addClass('disabled');
		};

		var updateSelectedCategories = function ()
		{
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var existedSuperFilters = searchConditions.find('.super-filter').map(function ()
			{
				return $(this).html();
			}).get();
			var existedCategories = searchConditions.find('.category').map(function ()
			{
				return $(this).html().split('------')[1];
			}).get();
			var categoryStr = $.merge(existedSuperFilters, existedCategories).join(', ');
			var selectedCategoryLabel = $('.tag-condition-selected');
			if (categoryStr != "")
				selectedCategoryLabel.html($('.tags-filter-panel-switcher').html() + ': ' + categoryStr);
			else
				selectedCategoryLabel.html('');
		};

		var setTagsCondition = function ()
		{
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var categorySelector = searchConditions.find('.tag-condition-selector');
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
					var categories = innerContent.find(".tag-list");
					var superFilters = innerContent.find(".super-filter-list");
					categories.accordion({
						heightStyle: "content",
						active: false,
						collapsible: true,
						icons: {
							header: "ui-icon-circle-arrow-e",
							activeHeader: "ui-icon-circle-arrow-s"
						}
					});

					$.each(searchConditions.find('.super-filter'), function ()
					{
						var value = $(this).html();
						superFilters.find('.btn:contains("' + value + '")').button('toggle');
					});

					$.each(searchConditions.find('.category'), function ()
					{
						var value = $(this).html();
						categories.find('.item-selector[value="' + value + '"]').prop('checked', true);
					});

					categories.find('.group-selector').off('change').on('change', function ()
					{
						var categoryGroup = $(this).parent().parent();
						categoryGroup.find('.item-selector').prop('checked', $(this).is(':checked'));
					});

					innerContent.find('.tags-clear-all').off('click').on('click', function ()
					{
						superFilters.find('.btn').removeClass('active').blur();
						categories.find(":checked").prop('checked', false);
					});

					superFilters.find('.btn').off('click').on('click', function ()
					{
						$(this).button('toggle').blur();
					});

					innerContent.find('.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});

					innerContent.find('.accept-button').on('click', function ()
					{
						searchConditions.find('.super-filter').remove();
						$.each(superFilters.find('.btn.active'), function ()
						{
							searchConditions.append($('<div class="super-filter">' + $(this).html() + '</div>'));
						});

						searchConditions.find('.category').remove();
						$.each(categories.find(".item-selector:checked"), function ()
						{
							searchConditions.append($('<div class="category">' + $(this).val() + '</div>'));
						});

						$.fancybox.close();

						updateSearchButtonState();

						updateSelectedCategories();
					});
				},
				afterClose: function ()
				{
				},
				onUpdate: function ()
				{
				}
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			var content = $('#content');
			var shortcutsPage = content.find('.shortcuts-page-content');
			var height = content.height() - content.find('.shortcuts-home-bar img').height() - content.find('.shortcuts-search-bar.open').height() - 20;
			shortcutsPage.css({
				'height': height + 'px'
			});

			var searchResult = $('#search-result');
			searchResult.find('> div').css({
				'height': height + 'px'
			});
			var gridHeader = searchResult.find('.links-grid-header');
			var searchResultBar = searchResult.find('.search-grid-info');
			searchResult.find('.links-grid-body-container').css({
				'height': (height - (searchResultBar.length > 0 ? (searchResultBar.height() + 12) : 0) - gridHeader.height()) + 'px'
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
	$.SalesPortal.Shortcuts = new ShortcutsManager();
})(jQuery);