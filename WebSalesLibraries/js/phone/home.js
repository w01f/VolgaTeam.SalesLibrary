(function ($)
{
	$(document).ready(function ()
	{
		$('.tab-items').cubeportfolio({
			gridAdjustment: 'alignCenter'
		});

		$('.logout-button').off('click').on('click', function (e)
		{
			e.stopPropagation();
			e.preventDefault();
			$.SalesPortal.Auth.logout();
		})
	});
})(jQuery);