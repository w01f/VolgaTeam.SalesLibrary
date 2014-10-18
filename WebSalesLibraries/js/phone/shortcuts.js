(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		this.init = function ()
		{
			$('#shortcuts').on('pageshow', function ()
			{
				var shortcutsTitle = $('#shortcuts').find('.ui-title').html();
				$('#folders').find('.header-title').html(shortcutsTitle);
				$('#links').find('.header-title').html(shortcutsTitle);
				$('#gallery-page').find('.header-title').html(shortcutsTitle);
				return false;
			});
			$('#shortcuts-tab').on('change', function ()
			{
				tabChanged();
			});
			$('#shortcuts-page').on('change', function ()
			{
				pageChanged();
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getTabs",
				beforeSend: function ()
				{
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
					var tabSelector = $('#shortcuts-tab');
					tabSelector.html(msg).selectmenu().selectmenu('refresh', true);
					var itemsCount = tabSelector.find('option').length;
					if (itemsCount > 1)
						tabSelector.parent().parent().show();
					else
						tabSelector.parent().parent().hide();
					tabChanged();
				},
				async: true,
				dataType: 'html'
			});
		};

		var tabChanged = function ()
		{
			var selectedTabId = $("#shortcuts-tab").find(":selected").val();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getPages",
				data: {
					tabId: selectedTabId
				},
				beforeSend: function ()
				{
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
					var pageSelector = $('#shortcuts-page');
					pageSelector.html(msg).selectmenu().selectmenu('refresh', true);
					var itemsCount = pageSelector.find('option').length;
					if (itemsCount > 1)
						pageSelector.parent().parent().show();
					else
						pageSelector.parent().parent().hide();
					pageChanged();
				},
				async: true,
				dataType: 'html'
			});
		};

		var pageChanged = function ()
		{
			var selectedPageId = $("#shortcuts-page").find(":selected").val();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getPage",
				data: {
					pageId: selectedPageId
				},
				beforeSend: function ()
				{
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
					var shortcutsLinks = $('#shortcuts-links');
					shortcutsLinks.html(msg);
					shortcutsLinks.find('ul').listview();
					var shortcutsTitle = $('#shortcuts').find('.ui-title').html();

					$('.shortcuts-link.empty').off('click').on('click', function (e)
					{
						e.stopPropagation();
					});
					$('.shortcuts-link.direct').off('click').on('click', function (e)
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
					$('.shortcuts-link.library-file').off('click').on('click', function (e)
					{
						e.stopPropagation();
						$.SalesPortal.Wallbin.loadLink($(this).find('.link-id').html(), shortcutsTitle, '#shortcuts', false);
					});
					$('.shortcuts-link.window').off('click').on('click', function (e)
					{
						e.stopPropagation();
						$.SalesPortal.Wallbin.loadFolder($(this).find('.folder-id').html(), '#shortcuts');
					});
					$('.shortcuts-link.library-page').off('click').on('click', function (e)
					{
						e.stopPropagation();
						$.cookie("selectedLibraryName", $(this).find('.library-name').html(), {
							expires: 60 * 60 * 24 * 7
						});
						$.cookie("selectedPageName", $(this).find('.page-name').html(), {
							expires: 60 * 60 * 24 * 7
						});
						$.SalesPortal.Wallbin.loadPage();
					});
					$('.shortcuts-link.quicklist').off('click').on('click', function (e)
					{
						e.stopPropagation();
						e.preventDefault();
						$.ajax({
							type: "POST",
							url: $(this).attr('href'),
							beforeSend: function ()
							{
								$('#shortcut-quick-list').find('.page-content').html('');
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
								var quickListPage = $('#shortcut-quick-list');
								quickListPage.find('.page-content').html(msg);
								$.mobile.changePage("#shortcut-quick-list", {
									transition: "slidefade"
								});
								quickListPage.find('.page-content').children('ul').listview();
							},
							async: true,
							dataType: 'html'
						});
					});
					$('.shortcuts-link.search').off('click').on('click', function (e)
					{
						e.stopPropagation();
						e.preventDefault();
						$.ajax({
							type: "POST",
							url: $(this).attr('href'),
							beforeSend: function ()
							{
								$.mobile.loading('show', {
									textVisible: false,
									html: ""
								});
							},
							complete: function ()
							{
							},
							success: function (msg)
							{
								var searchConditions = $(msg);
								var sortColumn = searchConditions.find('.sort-column').html();
								var sortDirection = searchConditions.find('.sort-direction').html();

								var getSearchCondition = function (isSort)
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

									var datasetKey = $('#search-result').find('td.dataset-key').html();
									datasetKey = isSort == 0 || datasetKey == '' ? undefined : datasetKey;

									return {
										fileTypes: selectedFileTypes,
										condition: selectedCondition,
										startDate: startDate,
										endDate: endDate,
										dateFile: searchConditions.find('.use-file-date').length > 0,
										libraries: selectedLibraryIds.length > 0 ? $.toJSON(selectedLibraryIds) : null,
										superFilters: superFilters.length > 0 ? $.toJSON(superFilters) : null,
										categories: categories.length > 0 ? $.toJSON(categories) : null,
										categoriesExactMatch: false,
										onlyWithCategories: onlyWithCategories,
										hideDuplicated: searchConditions.find('.hide-duplicated').length > 0,
										onlyByName: onlyByName,
										onlyByContent: onlyByContent,
										sortColumn: $.cookie("sortColumnMobile"),
										sortDirection: $.cookie("sortDirectionMobile"),
										datasetKey: datasetKey,
										backLink: '#shortcuts'
									};
								};
								$.cookie("sortColumnMobile", sortColumn, {
									expires: (60 * 60 * 24 * 7)
								});
								$('#search-result-sort-column').off('change.custom').on('change.custom', function ()
								{
									$.cookie("sortColumnMobile", $('#search-result-sort-column').find(':selected').val(), {
										expires: (60 * 60 * 24 * 7)
									});
									$.SalesPortal.Search.runSearch(getSearchCondition(1));
								});
								$.cookie("sortDirectionMobile", sortDirection, {
									expires: (60 * 60 * 24 * 7)
								});
								$('#search-result-sort-order').off('change.custom').on('change.custom', function ()
								{
									$.cookie("sortDirectionMobile", $('#search-result-sort-order').find(':selected').val(), {
										expires: (60 * 60 * 24 * 7)
									});
									$.SalesPortal.Search.runSearch(getSearchCondition(1));
								});
								$.SalesPortal.Search.runSearch(getSearchCondition(0));
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
	};
	$.SalesPortal.Shortcuts = new ShortcutsManager();
})(jQuery);