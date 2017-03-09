(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsNavigationPanel = function (parameters)
	{
		if (parameters == undefined)
			parameters = {
				content: undefined,
				options: undefined,
				sizeChangedCallback: undefined
			};
		parameters.content = parameters.content != undefined ? parameters.content : '';
		parameters.options = parameters.options != undefined ? parameters.options : undefined;
		parameters.sizeChangedCallback = parameters.sizeChangedCallback != undefined ? parameters.sizeChangedCallback : function ()
			{
			};

		var init = function ()
		{
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			var navigationItemList = navigationPanelObject.find('.navigation-item-list');

			var previousStateKey = navigationItemList.data('id');
			if (previousStateKey)
			{
				var previousSavedState = {
					scrollPosition: navigationItemList.scrollTop()
				};
				localStorage.setItem('navigation-panel-' + previousStateKey, JSON.stringify(previousSavedState));
				navigationItemList.data('id', null);
			}

			navigationPanelObject.html(parameters.content);
			navigationItemList = navigationPanelObject.find('.navigation-item-list');

			if (parameters.options)
			{
				var currentStateKey = parameters.options.id;
				var currentSavedState = localStorage.getItem('navigation-panel-' + currentStateKey);
				if (currentSavedState)
					currentSavedState = JSON.parse(currentSavedState);
				navigationItemList.data('id', parameters.options.id);
			}

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

			navigationPanelObject.removeClass('hidden-xs');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.extraSmall == true)
				navigationPanelObject.addClass('hidden-xs');
			navigationPanelObject.removeClass('hidden-sm');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.small == true)
				navigationPanelObject.addClass('hidden-sm');
			navigationPanelObject.removeClass('hidden-md');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.medium == true)
				navigationPanelObject.addClass('hidden-md');
			navigationPanelObject.removeClass('hidden-lg');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition == true)
				navigationPanelObject.addClass('hidden-lg');

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

			if (currentSavedState)
				navigationItemList.scrollTop(currentSavedState.scrollPosition);

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