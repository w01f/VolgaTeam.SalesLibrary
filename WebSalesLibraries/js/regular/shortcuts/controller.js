(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function ()
	{
		var that = this;
		this.init = function ()
		{
			var content = $('#content');
			var objectId = content.find('.object-id').html();
			var isPage = content.find('.is-page').length > 0;
			$('#ribbon').ribbon();
			$('a#view-dialog-link').fancybox();
			if (!that.isMobileDevice())
				$.MetroTooltipInit({
					animation: "fadeInDown fast",
					position: "top",
					color: "#063BB3"
				});

			if (content.find('.search-conditions').length > 0)
			{
				$.SalesPortal.Overlay.show(true);
				$.SalesPortal.ShortcutsSearchManager(content, objectId);
				$.SalesPortal.Overlay.hide();
			}

			that.updateContentSize();
			$(window).on('resize', that.updateContentSize);
		};

		this.updateContentSize = function ()
		{
			var ribbon = $('#ribbon');
			var height = $(window).height() - ribbon.height() - ribbon.offset().top - 10;
			$('body').css({
				'height': 'auto'
			});
			var content = $('#content');
			content.css({
				'height': height + 'px'
			});
		};

		this.isMobileDevice = function ()
		{
			return /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent);
		};
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);