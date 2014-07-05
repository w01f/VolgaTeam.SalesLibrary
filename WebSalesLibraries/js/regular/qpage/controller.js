(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var LayoutManager = function()
	{
		var that = this;
		this.init = function()
		{
			$('#ribbon').ribbon();
			$('a#view-dialog-link').fancybox();
			$('#login-button').off('click').on('click', function ()
			{
				window.location = "getProtected?id="+$('#page-id').html();
			});
			$.SalesPortal.Overlay.hide();
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
	};
	$.SalesPortal.Layout = new LayoutManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Layout.init();
	});
})(jQuery);