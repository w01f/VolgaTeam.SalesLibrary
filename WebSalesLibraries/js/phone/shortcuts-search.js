(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsSearchManager = function (shortcutData)
	{
		var that = this;

		var searchShortcutOptions = undefined;

		var shortcutLinkTitle = shortcutData.find('.link-header').text();

		var searchResultsPage = $('#search-results');
		searchResultsPage.find('.page-header .header-title').html(shortcutLinkTitle);

		$.ajax({
			type: "POST",
			url: shortcutData.find('.url').text(),
			beforeSend: function ()
			{
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				searchShortcutOptions = new $.SalesPortal.SearchOptions($.parseJSON($(msg).find('.encoded-object').text()));

				$.SalesPortal.SearchHelper.runSearch(
					{
						datasetKey: undefined,
						conditions: $.toJSON(searchShortcutOptions.conditions)
					},
					function ()
					{
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
			},
			error: function ()
			{
			},
			async: true,
			dataType: 'html'
		});
		return that;
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