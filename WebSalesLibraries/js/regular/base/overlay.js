(function ($)
{
	$.SalesPortal = $.SalesPortal || {};
	var OverlayHelper = function ()
	{
		var activeOverlays = 0;

		var overlayBar = null;
		var overlayTimerId = null;

		this.show = function ()
		{
			if (activeOverlays == 0)
			{
				$('<div id="progress-bar"></div>').appendTo('body');
				$('#content').addClass('overlay');
				overlayBar = new ProgressBar.Circle('#progress-bar', {
					strokeWidth: 9,
					duration: 4800,
					color: '#919191',
					trailColor: '#919191',
					trailWidth: 1,
					svgStyle: null
				});
				overlayBar.animate(1.0);
				overlayTimerId = setInterval(function ()
				{
					if (overlayBar)
						try
						{
							overlayBar.destroy();
						}
						catch (ex)
						{
						}
						finally
						{
							overlayBar = null;
						}
					overlayBar = new ProgressBar.Circle('#progress-bar', {
						strokeWidth: 9,
						duration: 4800,
						color: '#919191',
						trailColor: '#919191',
						trailWidth: 1,
						svgStyle: null
					});
					overlayBar.animate(1.0);
				}, 4850);
			}
			activeOverlays++;
		};

		this.hide = function ()
		{
			if (activeOverlays <= 1)
			{
				if (overlayTimerId)
					clearTimeout(overlayTimerId);
				if (overlayBar)
					try
					{
						overlayBar.destroy();
					}
					catch (ex)
					{
					}
					finally
					{
						overlayBar = null;
					}
				$('#progress-bar').remove();
				$('#content').removeClass('overlay');
			}
			if (activeOverlays > 0)
				activeOverlays--;
		};
	};
	$.SalesPortal.Overlay = new OverlayHelper();
})(jQuery);