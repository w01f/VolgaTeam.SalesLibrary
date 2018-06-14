(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ContentManager = function () {
		var that = this;

		this.init = function () {
			$('a#view-dialog-link').fancybox();
		};

		this.fillContent = function (parameters) {
			if (parameters === undefined)
				parameters = {
					content: undefined,
					headerOptions: undefined,
					actions: undefined,
					navigationPanel: undefined,
					fixedPanels: undefined,
					loadCallback: undefined,
					resizeCallback: undefined
				};
			parameters.content = parameters.content !== undefined ? parameters.content : '';
			parameters.headerOptions = parameters.headerOptions !== undefined ? parameters.headerOptions : undefined;
			parameters.actions = parameters.actions !== undefined ? parameters.actions : undefined;
			parameters.navigationPanel = parameters.navigationPanel !== undefined ? parameters.navigationPanel : undefined;
			parameters.fixedPanels = parameters.fixedPanels !== undefined ? parameters.fixedPanels : null;
			parameters.loadCallback = parameters.loadCallback !== undefined ? parameters.loadCallback : function () {
			};
			parameters.resizeCallback = parameters.resizeCallback !== undefined ? parameters.resizeCallback : function () {
			};

			initHeader(parameters.headerOptions);
			initShortcutActions(parameters.actions);

			new $.SalesPortal.ShortcutsNavigationPanel({
				content: parameters.navigationPanel !== undefined && parameters.navigationPanel !== null ? parameters.navigationPanel.content : '',
				options: parameters.navigationPanel !== undefined && parameters.navigationPanel !== null ? parameters.navigationPanel.options : undefined,
				sizeChangedCallback: parameters.resizeCallback
			});

			var contentRoot = $('#content');
			var contentObject = that.getContentObject();

			var contentInner = contentRoot.find('.content-inner');
			contentInner.find('.fixed-panel').remove();
			contentInner.css({
				'padding': '0'
			});

			if (parameters.fixedPanels)
			{
				if (parameters.fixedPanels.top !== undefined)
				{
					contentObject.before(parameters.fixedPanels.top.content);
				}
				if (parameters.fixedPanels.bottom !== undefined)
				{
					contentRoot.find('.content-inner').append(parameters.fixedPanels.bottom.content);
					contentInner.css({
						'padding-bottom': parameters.fixedPanels.bottom.height + 'px'
					});
				}
			}

			contentObject.scrollTop(0);
			contentObject.html(parameters.content);


			var anchorImage = contentObject.find('img.wallbin-logo');
			if (anchorImage.length > 0)
				anchorImage.on('load', parameters.loadCallback);
			else
				parameters.loadCallback();

			contentRoot.css({
				'overflow': ''
			});

			contentObject.css({
				'overflow': ''
			});
		};

		this.clearContent = function () {
			that.fillContent();
		};

		this.getContentObject = function () {
			return $('#content').find('.content-inner .content-scrollable-area');
		};

		this.getNavigationPanel = function () {
			return $('#content').find('.navigation-panel');
		};

		this.getFixedPanels = function () {
			return $('#content').find('.fixed-panel');
		};

		this.updateSize = function () {
			$('body').css({
				'height': 'auto'
			});

			var contentRoot = $('#content');
			var content = that.getContentObject();

			var menu = $('#main-menu');


			var topFixedPanel = contentRoot.find('.fixed-panel-top');
			var bottomFixedPanel = contentRoot.find('.fixed-panel-bottom');

			var height = $(window).height() - menu.outerHeight(true) - menu.offset().top;
			if (topFixedPanel.length > 0)
				height = height - topFixedPanel.outerHeight(true);
			if (bottomFixedPanel.length > 0)
				height = height - bottomFixedPanel.outerHeight(true);

			content.css({
				'max-width': 'none',
				'width': '100%',
				'height': height + 'px'
			});
		};

		this.isMobileDevice = function () {
			return /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent);
		};

		this.isEOBrowser = function () {
			return /Essential Objects/i.test(navigator.userAgent);
		};

		var initHeader = function (headerOptions) {
			if (headerOptions !== undefined)
			{
				var mainMenu = $('#main-menu');
				var openMainMenuButton = mainMenu.find('.open-main-menu-button');
				var headerTitle = mainMenu.find('.header-text');
				var headerIcon = mainMenu.find('.header-icon');

				var actionMenu = $('#shortcut-action-menu');
				var openActionMenuButton = actionMenu.find('.open-action-menu-button');

				headerTitle.css({
					'color': (headerOptions.titleColor !== 'transparent' ? ('#' + headerOptions.titleColor) : headerOptions.titleColor) + '!important'
				});
				headerIcon.css({
					'color': (headerOptions.iconColor !== 'transparent' ? ('#' + headerOptions.iconColor) : headerOptions.iconColor) + '!important'
				});
				openMainMenuButton.css({
					'color': (headerOptions.openMainMenuButtonColor !== 'transparent' ? ('#' + headerOptions.openMainMenuButtonColor) : headerOptions.openMainMenuButtonColor) + '!important'
				});
				openActionMenuButton.css({
					'color': (headerOptions.openActionMenuButtonColor !== 'transparent' ? ('#' + headerOptions.openActionMenuButtonColor) : headerOptions.openActionMenuButtonColor) + '!important'
				});

				$('#main-menu-custom-page-style').html('#main-menu .menu-back-colored {background-color: ' + (headerOptions.barBackColor !== 'transparent' ? ('#' + headerOptions.barBackColor) : headerOptions.barBackColor) + ' !important;}' +
					' #main-menu .menu-back-colored .om-closenav {color: ' + (headerOptions.openMainMenuButtonColor !== 'transparent' ? ('#' + headerOptions.openMainMenuButtonColor) : headerOptions.openMainMenuButtonColor) + ' !important;}' +
					' #main-menu .menu-back-colored .shortcut-menu-group-item {color: ' + (headerOptions.barBackColor !== 'transparent' ? ('#' + headerOptions.shortcutGroupsColor) : headerOptions.shortcutGroupsColor) + ' !important;}' +
					' #shortcut-action-menu, #shortcut-action-menu .main-level {background-color: ' + (headerOptions.barBackColor !== 'transparent' ? ('#' + headerOptions.barBackColor) : headerOptions.barBackColor) + ' !important;}' +
					' #shortcut-action-menu .main-level .shortcut-menu-header:hover {background-color: ' + (headerOptions.barBackColor !== 'transparent' ? ('#' + headerOptions.barBackColor) : headerOptions.barBackColor) + ' !important;}');

				headerTitle.html('');
				headerIcon.removeClass(function (index, css) {
					return (css.match(/(^|\s)icon-\S+/g) || []).join(' ');
				});
				headerTitle.html(headerOptions.title);
				if (headerOptions.icon !== '')
				{
					headerIcon.addClass(headerOptions.icon);
				}

				headerIcon.removeClass('hidden-xs');
				if (headerOptions.hideIconCondition)
				{
					if (headerOptions.hideIconCondition.extraSmall === true)
						headerIcon.addClass('hidden-xs');
					headerIcon.removeClass('hidden-sm');
					if (headerOptions.hideIconCondition.small === true)
						headerIcon.addClass('hidden-sm');
					headerIcon.removeClass('hidden-md');
					if (headerOptions.hideIconCondition.medium === true)
						headerIcon.addClass('hidden-md');
					headerIcon.removeClass('hidden-lg');
					if (headerOptions.hideIconCondition.large === true)
						headerIcon.addClass('hidden-lg');
				}

				headerTitle.removeClass('hidden-xs');
				if (headerOptions.hideTextCondition)
				{
					if (headerOptions.hideTextCondition.extraSmall === true)
						headerTitle.addClass('hidden-xs');
					headerTitle.removeClass('hidden-sm');
					if (headerOptions.hideTextCondition.small === true)
						headerTitle.addClass('hidden-sm');
					headerTitle.removeClass('hidden-md');
					if (headerOptions.hideTextCondition.medium === true)
						headerTitle.addClass('hidden-md');
					headerTitle.removeClass('hidden-lg');
					if (headerOptions.hideTextCondition.large === true)
						headerTitle.addClass('hidden-lg');
				}
			}
		};

		var initShortcutActions = function (actionsContent) {
			var shortcutActionsMenu = $('#shortcut-action-menu');
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.html('');
			if (actionsContent !== undefined)
				shortcutActionsContainer.html(actionsContent);

			var actions = shortcutActionsContainer.find('.shortcut-action');
			if (actions.length > 0)
				shortcutActionsMenu.find('.shortcut-menu-header').show();
			else
				shortcutActionsMenu.find('.shortcut-menu-header').hide();

			shortcutActionsContainer.find('.logout').off('click.action').on('click.action', function () {
				$.SalesPortal.Auth.logout();
			});

			shortcutActionsContainer.find('.shortcut-action').off('click.log').on('click.log', function () {
				if (!$(this).hasClass('logout'))
				{
					var dataObject = $(this).find('.service-data');
					var activityDataEncoded = dataObject.find('.activity-data').text();
					if (activityDataEncoded !== '')
					{
						var activityData = $.parseJSON(activityDataEncoded);
						$.SalesPortal.LogHelper.write({
							type: 'Shortcut Tile',
							subType: activityData.shortcut,
							linkId: activityData.id,
							data: {
								file: activityData.file,
								operation: activityData.title
							}
						});
					}
				}
			});
		};
	};
	$.SalesPortal.Content = new ContentManager();
})(jQuery);