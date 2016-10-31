(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsSuperGroup = function ()
	{
		this.init = function (superGroupTag)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/setSuperGroup",
				data: {
					superGroupTag: superGroupTag
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
					$.SalesPortal.MainMenu.updateShortcutsMenu(function ()
					{
						$('#main-menu').find('.onemenu').click();
					})
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};
	};
})(jQuery);
