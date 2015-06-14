(function ($)
{
	$(document).ready(function ()
	{
		$('.tab-items').cubeportfolio({
			gridAdjustment: 'alignCenter'
		});

		$('.not-working').off('click').on('click', function (event)
		{
			$("#not-working-message").popup("open");
			event.preventDefault();
			event.stopPropagation();
		});

		$('.logout-button').off('click').on('click', function (e)
		{
			e.stopPropagation();
			e.preventDefault();
			$.SalesPortal.Auth.logout();
		})
	});
})(jQuery);