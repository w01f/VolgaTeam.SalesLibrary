(function ($)
{
	$.SalesPortal = $.SalesPortal || { };
	var OverlayHelper = function ()
	{
		this.show = function (withBackground)
		{
			if (withBackground)
			{
				$('#content-overlay').css({
					'width': $(window).width() + 'px'
				}).css({
					'height': $(window).height() + 'px'
				}).fadeIn(0);
			}
			if ($('#fancybox-loading').length == 0)
				$("<div id=\"fancybox-loading\"><div></div></div>").appendTo('body');
		};

		this.hide = function ()
		{
			$('#fancybox-loading').remove();
			$('#content-overlay').fadeOut(0);
		};
	};
	$.SalesPortal.Overlay = new OverlayHelper();
})(jQuery);