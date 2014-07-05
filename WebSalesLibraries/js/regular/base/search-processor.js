(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var SearchHelper = function ()
	{
		this.runSearch = function (searchCondition, beforeSearch, completeCallback, successCallBack)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "search/searchByContent",
				data: searchCondition,
				beforeSend: beforeSearch,
				complete: completeCallback,
				success: successCallBack,
				error: undefined,
				async: true,
				dataType: 'html'
			});
		};

		this.requestSearchResultDialog = function (linkIds)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getSpecialDialog",
				data: {
					linkIds: linkIds,
					isSearchResults: true
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					if (msg != '')
					{
						var content = $(msg);
						$.SalesPortal.LinkManager.showSpecialDialog(content, linkIds, undefined);
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};
	};
	$.SalesPortal.SearchHelper = new SearchHelper();
})(jQuery);