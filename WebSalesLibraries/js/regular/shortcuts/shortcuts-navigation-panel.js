(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsNavigationPanel = function (parameters)
	{
		if (parameters == undefined)
			parameters = {
				content: undefined,
				sizeChangedCallback: undefined
			};
		parameters.content = parameters.content != undefined ? parameters.content : '';
		parameters.sizeChangedCallback = parameters.sizeChangedCallback != undefined ? parameters.sizeChangedCallback : function ()
		{
		};

		var init = function ()
		{
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			navigationPanelObject.html(parameters.content);
			if (parameters.content == '')
				navigationPanelObject.hide();
			else
			{
				var navigationPanelSate = $.cookie("navigationPanelState");
				if (navigationPanelSate == null)
					navigationPanelSate = 'expanded';
				navigationPanelObject.addClass(navigationPanelSate);
				navigationPanelObject.show();
				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(navigationPanelObject);
			}

			navigationPanelObject.find('.control-bar .button').off('click').on('click', function ()
			{
				var button = $(this);
				if (button.hasClass('button-collapse'))
				{
					$.SalesPortal.Content.getNavigationPanel().removeClass('expanded').addClass('collapsed');
					$.cookie("navigationPanelState", 'collapsed', {
						expires: (60 * 60 * 24 * 7)
					});
				}
				if (button.hasClass('button-expand'))
				{
					$.SalesPortal.Content.getNavigationPanel().removeClass('collapsed').addClass('expanded');
					$.cookie("navigationPanelState", 'expanded', {
						expires: (60 * 60 * 24 * 7)
					});
				}
				parameters.sizeChangedCallback();
			});

			updateSize();
			$(window).off('resize.navigation-panel').on('resize.navigation-panel', updateSize);
		};

		var updateSize = function ()
		{
			var menu = $('#main-menu');
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			var controlBar = navigationPanelObject.find('.control-bar');
			var height = $(window).height() - menu.outerHeight(true) - menu.offset().top - controlBar.outerHeight(true);
			navigationPanelObject.find('ul').css({
				'height': height + 'px'
			});
			//parameters.sizeChangedCallback();
		};

		init();
	};
})(jQuery);