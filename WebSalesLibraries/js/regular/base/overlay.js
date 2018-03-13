(function ($)
{
	$.SalesPortal = $.SalesPortal || {};
	var OverlayHelper = function ()
	{
		var activeOverlays = 0;

		this.show = function ()
		{
			if (activeOverlays === 0)
			{
				$('<div id="progress-bar"></div>').appendTo('body');
				$('#content').addClass('overlay');
			}
			activeOverlays++;
		};

		this.hide = function ()
		{
			if (activeOverlays <= 1)
			{
				$('#progress-bar').remove();
				$('#content').removeClass('overlay');
			}
			if (activeOverlays > 0)
				activeOverlays--;
		};
	};
	$.SalesPortal.Overlay = new OverlayHelper();
})(jQuery);