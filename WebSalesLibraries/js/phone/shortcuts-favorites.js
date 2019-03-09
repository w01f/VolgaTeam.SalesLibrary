(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsFavorites = function (data)
	{
		this.init = function ()
		{
			$.mobile.pageContainer.pagecontainer("change", "#favorites-view", {
				transition: "slidefade"
			});
			$.SalesPortal.Favorites.initViewPage();

			$.mobile.loading('hide', {
				textVisible: false,
				html: ""
			});
		};
	};
})(jQuery);