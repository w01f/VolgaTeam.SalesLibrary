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
	};
})(jQuery);