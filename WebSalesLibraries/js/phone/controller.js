(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function ()
	{
		this.init = function ()
		{
		};
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);