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
				onOpen: function(){
					$('#content-overlay').removeClass('main-menu-opened').addClass('main-menu-opened');
					$('#shortcut-action-menu').find('.shortcut-menu-header').addClass('main-menu-active');
				},
				onClose: function(){
					$('#content-overlay').removeClass('main-menu-opened');
					$('#shortcut-action-menu').find('.shortcut-menu-header').removeClass('main-menu-active');
				}
			});
			$.SalesPortal.ShortcutsManager.assignShortcutHandlers(menuContainer.find('.om-itemholder .om-itemlist'));
		};
	};
	$.SalesPortal.MainMenu = new MenuManager();
})(jQuery);
