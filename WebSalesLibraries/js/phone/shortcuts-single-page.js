(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var ShortcutsSinglePageManager = function ()
	{
		this.init = function ()
		{
			var defaultShortcutData = $('.default-shortcut-data');
			if (defaultShortcutData.length > 0)
				$.SalesPortal.ShortcutsManager.openShortcut(defaultShortcutData);

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});
		};
	};

	$.SalesPortal.ShortcutsSinglePage = new ShortcutsSinglePageManager();
})(jQuery);
