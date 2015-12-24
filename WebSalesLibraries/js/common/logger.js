(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LogHelper = function ()
	{
		this.write = function (actionData)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: actionData,
				async: true,
				dataType: 'json'
			});
		}
	};
	$.SalesPortal.LogHelper = new LogHelper();
})(jQuery);
