(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.MenuStripe = function (parameters) {
		var menuStripeId = parameters.containerId;
		var menuStripe = undefined;

		this.init = function () {
			menuStripe = $('#menu-stripe-' + menuStripeId);
			initMenuStripe();
		};

		var initMenuStripe = function () {
			if (menuStripe.hasClass('expand-on-click'))
			{
				menuStripe.find('.menu-stripe-top-item>a, .menu-stripe-item-submenu>a').off('click.menu-stripe-expand').on('click.menu-stripe-expand', function (eclick) {
					var menuItem = $(this).parent();
					if (menuItem.hasClass('active-item'))
					{
						menuItem.find('.menu-stripe-top-item, .menu-stripe-item-submenu').removeClass('active-item');
						menuItem.removeClass('active-item')
					}
					else
					{
						$.each(menuItem.siblings(), function () {
							var sibling = $(this);
							sibling.removeClass('active-item');
							sibling.find('.menu-stripe-top-item, .menu-stripe-item-submenu').removeClass('active-item');
						});
						menuItem.addClass('active-item');
						$(document)
							.off('mousedown.menu-stripe-expand' + menuStripeId)
							.on('mousedown.menu-stripe-expand+menuStripeId-' + menuStripeId, function (emouse) {

								var targetItem = $(emouse.target);
								if (!((targetItem.hasClass('menu-stripe-top-item') || targetItem.parent().hasClass('menu-stripe-top-item') ||
											targetItem.hasClass('menu-stripe-item-submenu') || targetItem.parent().hasClass('menu-stripe-item-submenu')) &&
										targetItem.closest('#menu-stripe-' + menuStripeId).length > 0
									))
									$('.menu-stripe-top-item, .menu-stripe-item-submenu').removeClass('active-item');
							});
					}
				})
			}
		};
	};
})(jQuery);