(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var MenuManager = function ()
	{
		this.init = function ()
		{
			var menuContainer = $('#main-menu');
			menuContainer.find('.onemenu').onemenu({
				animEffect: 'fade',
				onOpen: function ()
				{
					$('#content-overlay').removeClass('main-menu-opened').addClass('main-menu-opened');
					$('#shortcut-action-menu').find('.shortcut-menu-header').addClass('main-menu-active');
				},
				onClose: function ()
				{
					$('#content-overlay').removeClass('main-menu-opened');
					$('#shortcut-action-menu').find('.shortcut-menu-header').removeClass('main-menu-active');
				}
			});
			$.SalesPortal.ShortcutsManager.assignShortcutHandlers(menuContainer.find('.om-itemholder .om-itemlist'));

			setTimeout(checkIfShortcutsUpdated, 60000);
		};

		var checkIfShortcutsUpdated = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/checkShortcutsUpdated",
				data: {
					menuDate: $('#om-nav').data('last-update')
				},
				beforeSend: function ()
				{
				},
				complete: function ()
				{
				},
				success: function (result)
				{
					if (result.needUpdate)
					{
						$('#om-nav').data('last-update', result.lastUpdate);
						updateShortcutsMenu();
						$.SalesPortal.ShortcutsManager.updateCurrentShortcut();
					}
					setTimeout(checkIfShortcutsUpdated, 60000);
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		var updateShortcutsMenu = function ()
		{
			var menuContainer = $('#main-menu');
			var menuItemsContainer = $('#om-nav');

			if (menuItemsContainer.hasClass('opened'))
				menuItemsContainer.find('.om-closenav').click();

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "site/getMenu",
				data: {},
				beforeSend: function ()
				{
				},
				complete: function ()
				{
				},
				success: function (result)
				{
					menuItemsContainer.html(result);
					$.SalesPortal.ShortcutsManager.assignShortcutHandlers(menuContainer.find('.om-itemholder .om-itemlist'));
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};
	};
	$.SalesPortal.MainMenu = new MenuManager();
})(jQuery);
