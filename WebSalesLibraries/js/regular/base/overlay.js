(function ($)
{
	$.SalesPortal = $.SalesPortal || { };
	var OverlayHelper = function ()
	{
		var activeOverlays = 0;
		this.show = function (withBackground)
		{
			if (activeOverlays == 0)
			{
				if (withBackground)
				{
					$('#content-overlay').css({
						'width': $(window).width() + 'px'
					}).css({
						'height': $(window).height() + 'px'
					}).fadeIn(0);
				}
				$('<div id="fancybox-loading"><div></div></div>').appendTo('body');
			}
			activeOverlays++;
		};

		this.hide = function ()
		{
			if (activeOverlays <= 1)
			{
				$('#fancybox-loading').remove();
				$('#content-overlay').fadeOut(0);
			}
			if (activeOverlays > 0)
				activeOverlays--;
		};
	};
	$.SalesPortal.Overlay = new OverlayHelper();
})(jQuery);