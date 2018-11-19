(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsNavigationPanel = function (parameters) {
		if (parameters === undefined)
			parameters = {
				content: undefined,
				options: undefined,
				sizeChangedCallback: undefined
			};
		parameters.content = parameters.content !== undefined ? parameters.content : '';
		parameters.options = parameters.options !== undefined ? parameters.options : undefined;
		parameters.sizeChangedCallback = parameters.sizeChangedCallback !== undefined ? parameters.sizeChangedCallback : function () {
		};

		var init = function () {
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

			navigationPanelObject.find('.control-bar .button').each(function () {
				var img = $(this).find('img');
				if (img.length > 0)
				{
					var imgURL = img.attr('src');
					$.get(imgURL, function (data) {
						var svg = $(data).find('svg');
						svg = svg.removeAttr('xmlns:a');
						img.replaceWith(svg);
					}, 'xml');
				}
			});

			navigationItemList = navigationPanelObject.find('.navigation-item-list');

			if (parameters.options)
			{
				var currentStateKey = parameters.options.id;
				var currentSavedState = localStorage.getItem('navigation-panel-' + currentStateKey);
				if (currentSavedState)
					currentSavedState = JSON.parse(currentSavedState);
				navigationItemList.data('id', parameters.options.id);
			}

			if (parameters.content === '')
				navigationPanelObject.hide();
			else
			{
				var navigationPanelSate = $.cookie("navigationPanelState");
				if (navigationPanelSate === null)
				{
					if (parameters.options.expanded)
						navigationPanelSate = 'expanded';
					else
						navigationPanelSate = 'collapsed';
				}

				navigationPanelObject.show();
				switch (navigationPanelSate)
				{
					case 'expanded':
						expandPanel();
						break;
					case 'collapsed':
						collapsePanel();
						break;
					default:
						expandPanel();
						break;
				}

				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(navigationPanelObject);
				navigationPanelObject.find('li.disabled a').off('click').on('click', function (e) {
					e.preventDefault();
				});
			}

			navigationPanelObject.removeClass('hidden-xs');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.extraSmall === true)
				navigationPanelObject.addClass('hidden-xs');
			navigationPanelObject.removeClass('hidden-sm');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.small === true)
				navigationPanelObject.addClass('hidden-sm');
			navigationPanelObject.removeClass('hidden-md');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition.medium === true)
				navigationPanelObject.addClass('hidden-md');
			navigationPanelObject.removeClass('hidden-lg');
			if (parameters.options && parameters.options.hideCondition && parameters.options.hideCondition === true)
				navigationPanelObject.addClass('hidden-lg');

			navigationPanelObject.find('.control-bar .button').off('click').on('click', function () {
				var button = $(this);
				if (button.hasClass('button-collapse'))
				{
					collapsePanel();
					$.cookie("navigationPanelState", 'collapsed', {
						expires: (60 * 60 * 24 * 7)
					});
				}
				if (button.hasClass('button-expand'))
				{
					expandPanel();
					$.cookie("navigationPanelState", 'expanded', {
						expires: (60 * 60 * 24 * 7)
					});
				}
			});

			updateSize();

			if (currentSavedState)
				navigationItemList.scrollTop(currentSavedState.scrollPosition);

			$(window).off('resize.navigation-panel').on('resize.navigation-panel', updateSize);
		};

		var expandPanel = function () {
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			navigationPanelObject.addClass('expanded');
			navigationPanelObject.removeClass('collapsed');
			navigationPanelObject.find('.navigation-item-list-container').animate({
				width: '150px'
			}, 300);
			navigationPanelObject.find('.navigation-item-list').animate({
				width: '150px'
			}, 300, function () {
				parameters.sizeChangedCallback();
			});
		};

		var collapsePanel = function () {
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			navigationPanelObject.removeClass('expanded');
			navigationPanelObject.addClass('collapsed');
			navigationPanelObject.find('.navigation-item-list-container').animate({
				width: '47px'
			}, 300);
			navigationPanelObject.find('.navigation-item-list').animate({
				width: '66px'
			}, 300, function () {
				parameters.sizeChangedCallback();
			});
		};

		var updateSize = function () {
			var menu = $('#main-menu');
			var navigationPanelObject = $.SalesPortal.Content.getNavigationPanel();
			var height = $(window).height() - menu.outerHeight(true) - menu.offset().top - 48;//control bar button image height;
			navigationPanelObject.find('.navigation-item-list').css({
				'height': height + 'px'
			});
		};

		init();
	};
})(jQuery);