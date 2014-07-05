(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function ()
	{
		this.init = function ()
		{
			$.mobile.changePage("#libraries", {
				transition: "slidefade",
				direction: "reverse"
			});

			$.SalesPortal.Shortcuts.init();
			$.SalesPortal.Wallbin.init();
			$.SalesPortal.Search.init();
			$.SalesPortal.Favorites.init();

			$('.logout-button').off('click').on('click', function ()
			{
				$.SalesPortal.Auth.logout();
			});

			$('#gallery-page').on('pageshow',function ()
			{
				var options = {
					enableMouseWheel: false,
					enableKeyboard: false,
					captionAndToolbarAutoHideDelay: 0,
					jQueryMobile: true
				};
				var galleryImages = $("#gallery").find("a");
				galleryImages.photoSwipe(options);
				return true;
			}).on('pagehide', function (e)
			{
				var currentPage = $(e.target);
				var photoSwipeInstance = window.Code.PhotoSwipe.getInstance(currentPage.attr('id'));
				if (typeof photoSwipeInstance != "undefined" && photoSwipeInstance != null)
				{
					window.Code.PhotoSwipe.detatch(photoSwipeInstance);
				}
				return true;
			});
		};
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);