(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var TickerManager = function ()
	{
		this.init = function ()
		{
			var ticker = $(".modern-ticker");
			var effect = "scroll";
			if (ticker.hasClass('scroll'))
				effect = "scroll";
			else if (ticker.hasClass('fade'))
				effect = "fade";
			else if (ticker.hasClass('type'))
				effect = "type";
			else if (ticker.hasClass('slide'))
				effect = "slide";
			ticker.modernTicker({
				effect: effect,
				scrollInterval: 20,
				transitionTime: 500,
				displayTime: 5500,
				typeInterval: 10,
				slideDistance: 100,
				autoplay: true
			});
			$(".mt-label").css({
				width: 'auto'
			});
			updateContentSize();
			$(window).off('resize.ticker').on('resize.ticker', updateContentSize);
		};

		var updateContentSize = function ()
		{
			var tickerWidth = $(window).width()-4;
			var newsWidth = tickerWidth - $('.mt-label').width() - $('.mt-controls').width();
			$('.modern-ticker').css({
				'width': tickerWidth + 'px'
			});
			$('.mt-news').css({
				'width': newsWidth + 'px'
			});
		};
	};
	$.SalesPortal.Ticker = new TickerManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Ticker.init();
	});
})(jQuery);