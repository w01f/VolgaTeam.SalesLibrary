(function ($)
{
	$.updateContentAreaDimensions = function ()
	{
		var height = $(window).height() - $('#ribbon').height() - $('#ribbon').offset().top - 10;
		$('body').css({
			'height': 'auto'
		});
		var content = $('#content');
		content.css({
			'height': height + 'px'
		});
		content.find('>div').css({
			'height': height + 'px'
		});
		updateTicker();
	};

	var updateTicker = function ()
	{
		var tickerWidth = $(window).width() - 5;
		var newsWidth = tickerWidth - $('.mt-label').width() - $('.mt-controls').width();
		$('.modern-ticker').css({
			'width': tickerWidth + 'px'
		});
		$('.mt-news').css({
			'width': newsWidth + 'px'
		});
	}
})(jQuery);