(function ($)
{
	var RateManager = function ()
	{
		this.init = function (linkId, relatedObject)
		{
//			$.ajax({
//				type: "POST",
//				url: window.BaseUrl + "rate/getRate",
//				data: {linkId: linkId},
//				success: function (msg)
//				{
//					if (msg != '')
//					{
//					}
//				},
//				error: function ()
//				{
//				},
//				async: true,
//				dataType: 'html'
//			});
		};
	};
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.Rate = new RateManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Rate.init();
	});
})(jQuery);