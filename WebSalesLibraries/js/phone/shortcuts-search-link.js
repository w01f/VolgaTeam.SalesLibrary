(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchLink = function (data)
	{
		var shortcutData = data;

		this.init = function ()
		{
			var searchShortcutOptions = new $.SalesPortal.SearchOptions(shortcutData.options.searchOptions);
			var shortcutLinkTitle = shortcutData.options.headerTitle;
			var searchResultsPage = $('#search-results');
			searchResultsPage.find('.page-header .header-title').html(shortcutLinkTitle);

			$.SalesPortal.SearchHelper.runSearch(
				{
					datasetKey: undefined,
					conditions: $.toJSON(searchShortcutOptions.conditions)
				},
				function ()
				{
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				function (data)
				{
					searchResultsPage.find('.entities-count span').html(data.dataset.length + ' Links');

					$.mobile.changePage("#search-results", {
						transition: "slidefade"
					});

					new $.SalesPortal.SearchDataTable(
						data.dataset,
						searchShortcutOptions.conditions.sortColumn,
						searchShortcutOptions.conditions.sortDirection,
						{
							id: '#search-results',
							name: shortcutLinkTitle
						}
					);
				}
			);
		};
	};

	$.SalesPortal.SearchOptions = function (data)
	{
		this.title = undefined;
		this.isPage = undefined;
		this.openInSamePage = undefined;

		this.enableSubSearch = undefined;
		this.subSearchDefaultView = undefined;

		this.conditions = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);