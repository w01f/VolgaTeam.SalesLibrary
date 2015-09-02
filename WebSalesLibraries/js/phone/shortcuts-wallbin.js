(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsWallbin = function (data)
	{
		this.init = function ()
		{
			$.SalesPortal.Wallbin.init();
			$.mobile.changePage("#library", {
				transition: "slidefade"
			});
		};
	};
})(jQuery);