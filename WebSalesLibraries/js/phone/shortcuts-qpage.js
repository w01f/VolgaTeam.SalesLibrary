(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsQPage = function (data)
	{
		this.init = function ()
		{
			$.SalesPortal.QPage.init();
			$.mobile.changePage("#quicksite", {
				transition: "slidefade"
			});
		};
	};
})(jQuery);