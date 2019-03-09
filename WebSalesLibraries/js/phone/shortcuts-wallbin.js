(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsWallbin = function (data)
	{
		var shortcutData = data;

		this.init = function ()
		{
			var wallbinManager = new $.SalesPortal.WallbinManager(shortcutData.options.linkId);
			wallbinManager.init();

			$.mobile.pageContainer.pagecontainer("change", "#wallbin-" + shortcutData.options.linkId, {
				transition: "slidefade"
			});

			$.mobile.loading('hide', {
				textVisible: false,
				html: ""
			});
		};
	};
})(jQuery);