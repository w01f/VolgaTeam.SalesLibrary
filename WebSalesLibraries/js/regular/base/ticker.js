(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var TickerManager = function ()
	{
		var that = this;
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
			$('.modern-ticker .ticker-link.link').off('click').on('click', function ()
			{
				var linkId = $(this).attr('href').replace('#', '');
				$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
			});
			$('.modern-ticker .ticker-link.video').off('click').on('click', function (event)
			{
				event.preventDefault();
				event.stopPropagation();
				$.SalesPortal.LinkManager.playVideo($.parseJSON($(this).find('.links')));
			});

			that.updateContentSize();
		};

		this.updateContentSize = function ()
		{
			var tickerWidth = $(window).width() - 4;
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