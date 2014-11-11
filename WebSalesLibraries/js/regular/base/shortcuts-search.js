(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchManager = function (content, objectId, isPageSearch)
	{
		var that = this;
		var baseDatasetKey = undefined;

		var shortcutTitle = content.find('.shortcut-title').html();
		var sortColumn = content.find('.sort-column').html();
		var sortDirection = content.find('.sort-direction').html();

		var baseSearchConditionsHtml = $('<div></div>').append(content.find('.search-conditions')).html();
		var homeBarHtml = $('<div></div>').append(content.parent().find('.shortcuts-home-bar')).html();
		content.parent().find('.shortcuts-home-bar').remove();
		content.html('');
		content.append($(baseSearchConditionsHtml));
		content.append($(homeBarHtml));
		content.append($('<table id="search-container"><tr>' +
			'<td id="right-navbar" style="display: none; width: 15%; min-width: 240px;"></td>' +
			'<td id="search-result"><div></div></td>' +
			'</tr></table>'));
		baseSearchConditions = content.find('.search-conditions');
		homeBar = content.find('.shortcuts-home-bar');

		var hideResults = baseSearchConditions.find('.hide-results').length > 0;
		var enableSubSearch = baseSearchConditions.find('.enable-sub-search').length > 0;
		var defaultSubSearch = baseSearchConditions.find('.sub-search-default-view').html();

		if (enableSubSearch)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchBar",
				data: {
					pageId: isPageSearch ? objectId : undefined,
					linkId: isPageSearch ? undefined : objectId
				},
				success: function (msg)
				{
					if (homeBar != null)
					{
						var subSearchBar = homeBar.find('.sub-search-bar');
						subSearchBar.addClass('active').html(msg);
						homeBar.find('.logo-container').hide();
						initSubSearchButtons();
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		}

		$.SalesPortal.SearchHelper.runSearch(
			(function ()
			{
				var selectedCondition = '';
				var selectedConditionTag = baseSearchConditions.find('.search-text');
				if (selectedConditionTag.length > 0)
					selectedCondition = selectedConditionTag.html();

				var selectedFileTypes = [];
				$.each(baseSearchConditions.find('.file-type'), function ()
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

				var startDateTag = baseSearchConditions.find('.start-date');
				if (startDateTag.length > 0)
					var startDate = startDateTag.text();
				var endDateTag = baseSearchConditions.find('.end-date');
				if (endDateTag.length > 0)
					var endDate = endDateTag.text();

				var selectedLibraryIds = [];
				var libraryIds = baseSearchConditions.find('.library');
				if (libraryIds.length > 0)
					$.each(libraryIds, function ()
					{
						selectedLibraryIds.push($(this).html());
					});

				var superFilters = [];
				$.each(baseSearchConditions.find('.super-filter'), function ()
				{
					superFilters.push($(this).html());
				});

				var categories = [];
				$.each(baseSearchConditions.find('.category'), function ()
				{
					var substr = $(this).html().split('------');
					var category = {
						category: substr[0],
						tag: substr[1]
					};
					categories.push(category);
				});

				var onlyWithCategories = baseSearchConditions.find('.only-with-categories').html();
				var onlyByName = baseSearchConditions.find('.search-by-name').html();
				var onlyByContent = baseSearchConditions.find('.search-by-content').html();

				return {
					fileTypes: selectedFileTypes,
					condition: selectedCondition,
					startDate: startDate,
					endDate: endDate,
					dateFile: baseSearchConditions.find('.use-file-date').length > 0,
					libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
					superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
					categories: categories.length > 0 ? $.toJSON(categories) : null,
					categoriesExactMatch: false,
					onlyWithCategories: onlyWithCategories,
					hideDuplicated: baseSearchConditions.find('.hide-duplicated').length > 0,
					onlyByName: onlyByName,
					onlyByContent: onlyByContent,
					sortColumn: sortColumn,
					sortDirection: sortDirection,
					datasetKey: undefined
				};
			}()),
			function ()
			{
				$.SalesPortal.Overlay.show(false);
			},
			function ()
			{
				$.SalesPortal.Overlay.hide();
			},
			function (msg)
			{
				var datasetKeyNodes = $('<div>' + msg + '</div>').find('.dataset-key');
				baseDatasetKey = datasetKeyNodes.length > 0 ? datasetKeyNodes.html() : null;
				if (defaultSubSearch == 'all')
					SimpleSearch(content);
				else if (defaultSubSearch == 'search')
					SubSearchCustom(content);
				else if (defaultSubSearch == 'links')
					SubSearchByTemplates(content);
			});

		var SimpleSearch = function (content)
		{
			var searchGrid = new $.SalesPortal.LinkGrid();
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function (isSort)
			{
				var searchCondition = (function ()
				{
					var datasetKey = isSort == 0 || searchGrid.datasetKey == null ? undefined : searchGrid.datasetKey;
					sortColumn = searchGrid.sortColumn != undefined && searchGrid.sortColumn != null ? searchGrid.sortColumn : sortColumn;
					sortDirection = searchGrid.sortDirection != undefined && searchGrid.sortDirection != null ? searchGrid.sortDirection : sortDirection;
					return {
						dateFile: baseSearchConditions.find('.use-file-date').length > 0,
						hideDuplicated: baseSearchConditions.find('.hide-duplicated').length > 0,
						sortColumn: sortColumn,
						sortDirection: sortDirection,
						datasetKey: datasetKey,
						baseDatasetKey: baseDatasetKey
					};
				}());

				var beforeSearch = function ()
				{
					sideBar.hide();
					$.SalesPortal.Overlay.show(false);
				};

				var completeCallback = function ()
				{
					updateContentSize();
					searchGrid.init({
						content: content,
						refreshCallback: function ()
						{
							getSearchResults(1);
						},
						sortColumn: isSort == 1 ? null : sortColumn,
						sortDirection: isSort == 1 ? null : sortDirection,
						showDelete: false
					});
					$.SalesPortal.Overlay.hide();
				};

				var successCallback = function (msg)
				{
					var searchResults = $('#search-result');
					searchResults.find('> div').html('').append(msg);

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
			getSearchResults(0);
		};

		var SubSearchCustom = function (content)
		{
			var searchGrid = new $.SalesPortal.LinkGrid();
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function (isSort)
			{
				var searchCondition = (function ()
				{
					var datasetKey = isSort == 0 || searchGrid.datasetKey == null ? undefined : searchGrid.datasetKey;
					sortColumn = searchGrid.sortColumn != undefined && searchGrid.sortColumn != null ? searchGrid.sortColumn : sortColumn;
					sortDirection = searchGrid.sortDirection != undefined && searchGrid.sortDirection != null ? searchGrid.sortDirection : sortDirection;
					var textCondition = sideBar.find('.search-bar-text').val();
					if (sideBar.find('#search-exact-match').is(':checked'))
						textCondition = '"' + textCondition + '"';
					var onlyFiles = sideBar.find('#search-file-names-only').is(':checked') ? 1 : 0;
					var selectedFileTypes = [];
					if (sideBar.find('#search-file-type-powerpoint').is(':checked'))
						selectedFileTypes.push("ppt");
					if (sideBar.find('#search-file-type-video').is(':checked'))
					{
						selectedFileTypes.push("video");
						selectedFileTypes.push("mp4");
						selectedFileTypes.push("wmv");
					}
					if (sideBar.find('#search-file-type-other').is(':checked'))
					{
						selectedFileTypes.push("doc");
						selectedFileTypes.push("xls");
						selectedFileTypes.push("pdf");
						selectedFileTypes.push("url");
						selectedFileTypes.push("url365");
						selectedFileTypes.push("png");
						selectedFileTypes.push("jpeg");
					}

					var superFilters = [];
					$.each(sideBar.find('.search-conditions .super-filter'), function ()
					{
						superFilters.push($(this).html());
					});

					var categories = [];
					$.each(sideBar.find('.search-conditions .category'), function ()
					{
						var substr = $(this).html().split('------');
						var category = {
							category: substr[0],
							tag: substr[1]
						};
						categories.push(category);
					});

					return {
						condition: textCondition,
						fileTypes: selectedFileTypes,
						dateFile: baseSearchConditions.find('.use-file-date').length > 0,
						hideDuplicated: baseSearchConditions.find('.hide-duplicated').length > 0,
						onlyByName: true,
						onlyByContent: onlyFiles == 0,
						superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
						categories: categories.length > 0 ? $.toJSON(categories) : null,
						sortColumn: sortColumn,
						sortDirection: sortDirection,
						datasetKey: datasetKey,
						baseDatasetKey: baseDatasetKey
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
							getSearchResults(1);
						},
						sortColumn: isSort == 1 ? null : sortColumn,
						sortDirection: isSort == 1 ? null : sortDirection,
						showDelete: false
					});
					$.SalesPortal.Overlay.hide();
				};

				var successCallback = function (msg)
				{
					var searchResults = $('#search-result');
					searchResults.find('> div').html('').append(msg);

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

			var initPanel = function ()
			{
				sideBar.find('.tags-filter-panel-switcher').off('click').on('click', function ()
				{
					setTagsCondition();
				});
				sideBar.find('.search-bar-text').keypress(function (e)
				{
					if (e.which == 13)
						getSearchResults(0);
				});
				sideBar.find('.search-bar-run').on('click', function ()
				{
					getSearchResults(0);
				});
			};

			var setTagsCondition = function ()
			{
				var categoryConditions = sideBar.find('.search-conditions');
				var categorySelector = categoryConditions.find('.tag-condition-selector');
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

						$.each(categoryConditions.find('.super-filter'), function ()
						{
							var value = $(this).html();
							superFilters.find('.btn:contains("' + value + '")').button('toggle');
						});

						$.each(categoryConditions.find('.category'), function ()
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
							categoryConditions.find('.super-filter').remove();
							$.each(superFilters.find('.btn.active'), function ()
							{
								categoryConditions.append($('<div class="super-filter">' + $(this).html() + '</div>'));
							});

							categoryConditions.find('.category').remove();
							$.each(categories.find(".item-selector:checked"), function ()
							{
								categoryConditions.append($('<div class="category">' + $(this).val() + '</div>'));
							});

							$.fancybox.close();

							var existedSuperFilters = categoryConditions.find('.super-filter').map(function ()
							{
								return $(this).html();
							}).get();
							var existedCategories = categoryConditions.find('.category').map(function ()
							{
								return $(this).html().split('------')[1];
							}).get();
							var categoryStr = $.merge(existedSuperFilters, existedCategories).join(', ');
							var selectedCategoryLabel = sideBar.find('.tag-condition-selected');
							if (categoryStr != "")
								selectedCategoryLabel.html(sideBar.find('.tags-filter-panel-switcher').html() + ': ' + categoryStr);
							else
								selectedCategoryLabel.html('');
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

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchCustomPanel",
				data: {
					pageId: isPageSearch ? objectId : undefined,
					linkId: isPageSearch ? undefined : objectId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					updateContentSize();
					getSearchResults(0);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var SubSearchByTemplates = function (content)
		{
			var searchGrid = new $.SalesPortal.LinkGrid();
			var sideBar = content.find('#right-navbar');

			var getSearchResults = function (isSort)
			{
				var searchCondition = (function ()
				{
					var datasetKey = isSort == 0 || searchGrid.datasetKey == null ? undefined : searchGrid.datasetKey;
					sortColumn = searchGrid.sortColumn != undefined && searchGrid.sortColumn != null ? searchGrid.sortColumn : sortColumn;
					sortDirection = searchGrid.sortDirection != undefined && searchGrid.sortDirection != null ? searchGrid.sortDirection : sortDirection;

					var selectedTemplateConditions = sideBar.find('a.opened .search-conditions');

					var templateCondition = '';
					var templateConditionTag = selectedTemplateConditions.find('.search-text');
					if (templateConditionTag.length > 0)
						templateCondition = templateConditionTag.html();

					var templateFileTypes = [];
					$.each(selectedTemplateConditions.find('.file-type'), function ()
					{
						var fileType = $(this).html();
						templateFileTypes.push(fileType);
						if (fileType == 'video')
						{
							templateFileTypes.push("mp4");
							templateFileTypes.push("wmv");
						}
						else if (fileType == 'image')
						{
							templateFileTypes.push("png");
							templateFileTypes.push("jpeg");
						}
					});

					var templateStartDateTag = selectedTemplateConditions.find('.start-date');
					if (templateStartDateTag.length > 0)
						var templateStartDate = templateStartDateTag.text();
					var templateEndDateTag = selectedTemplateConditions.find('.end-date');
					if (templateEndDateTag.length > 0)
						var templateEndDate = templateEndDateTag.text();

					var templateLibraryIds = [];
					var libraryIds = selectedTemplateConditions.find('.library');
					if (libraryIds.length > 0)
						$.each(libraryIds, function ()
						{
							templateLibraryIds.push($(this).html());
						});

					var templateSuperFilters = [];
					$.each(selectedTemplateConditions.find('.super-filter'), function ()
					{
						templateSuperFilters.push($(this).html());
					});

					var templateCategories = [];
					$.each(selectedTemplateConditions.find('.category'), function ()
					{
						var substr = $(this).html().split('------');
						var category = {
							category: substr[0],
							tag: substr[1]
						};
						templateCategories.push(category);
					});

					var templateOnlyWithCategories = selectedTemplateConditions.find('.only-with-categories').length > 0 ? selectedTemplateConditions.find('.only-with-categories').html() : baseSearchConditions.find('.only-with-categories').html();
					var templateOnlyByName = selectedTemplateConditions.find('.search-by-name').length > 0 ? selectedTemplateConditions.find('.search-by-name').html() : baseSearchConditions.find('.search-by-name').html();
					var templateOnlyByContent = selectedTemplateConditions.find('.search-by-content').length > 0 ? selectedTemplateConditions.find('.search-by-content').html() : baseSearchConditions.find('.search-by-content').html();

					return {
						fileTypes: templateFileTypes,
						condition: templateCondition,
						startDate: templateStartDate,
						endDate: templateEndDate,
						dateFile: selectedTemplateConditions.find('.use-file-date').length > 0 || baseSearchConditions.find('.use-file-date').length > 0,
						libraries: templateLibraryIds.length > 0 ? $.toJSON(templateLibraryIds) : null,
						superFilters: templateSuperFilters.length > 0 ? $.toJSON(templateSuperFilters) : null,
						categories: templateCategories.length > 0 ? $.toJSON(templateCategories) : null,
						categoriesExactMatch: false,
						onlyWithCategories: templateOnlyWithCategories,
						hideDuplicated: selectedTemplateConditions.find('.hide-duplicated').length > 0 || baseSearchConditions.find('.hide-duplicated').length > 0,
						onlyByName: templateOnlyByName,
						onlyByContent: templateOnlyByContent,
						sortColumn: sortColumn,
						sortDirection: sortDirection,
						datasetKey: datasetKey,
						baseDatasetKey: baseDatasetKey
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
							getSearchResults(1);
						},
						sortColumn: isSort == 1 ? null : sortColumn,
						sortDirection: isSort == 1 ? null : sortDirection,
						showDelete: false
					});
					$.SalesPortal.Overlay.hide();
				};

				var successCallback = function (msg)
				{
					var searchResults = $('#search-result');
					searchResults.find('> div').html('').append(msg);

					var resultsBar = content.find('.search-grid-info');
					var linkIds = [];
					$.each(content.find(".links-grid-body").find('.link-id-column'), function ()
					{
						linkIds.push($(this).html());
					});
					if (linkIds.length == 0)
						content.find(".links-grid-body-container").html('<img src="' + window.BaseUrl + 'images/shortcuts/no_cats.png">');
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
							if (linkIds.length > 0)
								$.SalesPortal.SearchHelper.requestSearchResultDialog(linkIds);
						});
				};
				$.SalesPortal.SearchHelper.runSearch(searchCondition, beforeSearch, completeCallback, successCallback);
			};

			var initPanel = function ()
			{
				var logoSelector = sideBar.find('.logo-list ul');
				sideBar.find('.logo-list ul li img').tooltip({animation: false, trigger: 'hover', placement: 'bottom', delay: { show: 500, hide: 100 }});
				logoSelector.find('a').on('click', function ()
				{
					logoSelector.find('a').removeClass('opened');
					$(this).addClass('opened');
					getSearchResults(0);
				});
			};

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getSubSearchTemplatesPanel",
				data: {
					pageId: isPageSearch ? objectId : undefined,
					linkId: isPageSearch ? undefined : objectId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					sideBar.html(msg);
					sideBar.show();
					initPanel();
					updateContentSize();
					getSearchResults(0);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var initSubSearchButtons = function ()
		{
			var subSearchBar = homeBar.find('.sub-search-bar');
			subSearchBar.find('img').tooltip({
				animation: false,
				trigger: 'hover',
				placement: 'bottom',
				delay: {
					show: 500,
					hide: 100 },
				container: 'body'});
			subSearchBar.find('img').off('click.toggle').on('click.toggle',function (e)
			{
				var src = $(this).attr('src');
				if (/-off/i.test(src))
				{
					subSearchBar.find('img').attr('src', function ()
					{
						return $(this).attr('src').replace('-on', '-off');
					});
					$(this).attr('src', src.replace('-off', '-on'));
				}
				else
					e.stopPropagation();
			}).off('click.process').on('click.process', function ()
			{
				if ($(this).hasClass('no-filter'))
					SimpleSearch(content);
				else if ($(this).hasClass('custom-filter'))
					SubSearchCustom(content);
				else if ($(this).hasClass('predefined-filter'))
					SubSearchByTemplates(content);
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Shortcuts.updateContentSize();
		};
		return that;
	};
})(jQuery);