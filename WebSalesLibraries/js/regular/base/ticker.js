(function ($)
{
	$(document).ready(function ()
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
			transitionTime: 300,
			displayTime: 4000,
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
			$.requestViewDialog(linkId, false);
		});
		$('.modern-ticker .ticker-link.video').off('click').on('click', function (event)
		{
			event.preventDefault();
			event.stopPropagation();
			$.viewSelectedFormat($(this), false, false);
		});
	});
})
	(jQuery);