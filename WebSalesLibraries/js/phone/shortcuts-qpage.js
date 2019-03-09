(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsQPage = function (data)
	{
		this.init = function ()
		{
			$.SalesPortal.QPage.init();
			$.mobile.pageContainer.pagecontainer("change", "#quicksite", {
				transition: "slidefade"
			});
			$.mobile.loading('hide', {
				textVisible: false,
				html: ""
			});
		};
	};
})(jQuery);