(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsWallbin = function (data)
	{
		this.init = function ()
		{
			$.SalesPortal.Wallbin.init();
			$.mobile.pageContainer.pagecontainer("change", "#library", {
				transition: "slidefade"
			});
		};
	};
})(jQuery);