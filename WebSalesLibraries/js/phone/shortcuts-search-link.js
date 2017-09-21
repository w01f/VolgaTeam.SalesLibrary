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
			var shortcutLinkTitle = shortcutData.options.headerOptions.title;
			var pageIdentifier = '#search-results-'+shortcutData.options.linkId;
			var searchResultsPage = $(pageIdentifier);
			searchResultsPage.find('.page-header .header-title').html(shortcutLinkTitle);

			$.mobile.pageContainer.pagecontainer("change", pageIdentifier, {
				transition: "slidefade"
			});

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

					new $.SalesPortal.SearchDataTable(
						data.dataset,
						searchShortcutOptions.conditions.sortSettings.columnTag,
						searchShortcutOptions.conditions.sortSettings.order,
						{
							id: pageIdentifier,
							name: shortcutLinkTitle
						}
					);
				}
			);
		};
	};
})(jQuery);