window.salesDepot = window.salesDepot || { };

(function ($)
{
	$.runSearch = function (searchCondition, beforeSearch, completeCallback, successCallBack)
	{

		$.ajax({
			type: "POST",
			url: "search/searchByContent",
			data: searchCondition,
			beforeSend: beforeSearch,
			complete: completeCallback,
			success: successCallBack,
			error: undefined,
			async: true,
			dataType: 'html'
		});
	};

	$.requestSearchResultDialog = function (linkIds)
	{
		$.ajax({
			type: "POST",
			url: "preview/getSpecialDialog",
			data: {
				linkIds: linkIds,
				isSearchResults: true
			},
			beforeSend: function ()
			{
				$.showOverlayLight();
			},
			complete: function ()
			{
				$.hideOverlayLight();
			},
			success: function (msg)
			{
				if (msg != '')
				{
					var content = $(msg);
					$.showSpecialDialog(content, linkIds, undefined);
				}
			},
			error: function ()
			{
			},
			async: true,
			dataType: 'html'
		});
	};

})(jQuery);