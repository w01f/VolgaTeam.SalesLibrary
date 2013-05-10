(function ($)
{
	var loadMainPage = function ()
	{
		$.ajax({
			type: "POST",
			url: "qbuilder/getMainPage",
			beforeSend: function ()
			{
				$('#content').html('');
				$.showOverlay();
			},
			complete: function ()
			{
				$.hideOverlay();
			},
			success: function (msg)
			{
				$('#content').html(msg);

				$.pageList.afterLoad();
				$.pageList.init();

				$.linkCart.afterLoad();
				$.linkCart.init();

				$.updateContentAreaDimensions();
			},
			async: true,
			dataType: 'html'
		});
	};

	$(document).ready(function ()
	{
		loadMainPage();

		$(window).on('resize', $.updateContentAreaDimensions);
	});
})(jQuery);
