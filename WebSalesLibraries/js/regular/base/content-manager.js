(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ContentManager = function ()
	{
		var that = this;

		this.init = function ()
		{
			$('a#view-dialog-link').fancybox();
			if (!that.isMobileDevice())
				$.MetroTooltipInit({
					animation: "fadeInDown fast",
					position: "top",
					color: "#063BB3"
				});
		};

		this.fillContent = function (content, headerOptions, actions, loadCallback)
		{
			if (loadCallback == undefined)
				loadCallback = function ()
				{
				};
			$('body').find('.mtContent').remove();
			that.getContentObject().html(content).find('img').on('load', loadCallback);
			initHeader(headerOptions);
			initShortcutActions(actions);
		};

		this.clearContent = function ()
		{
			that.fillContent('');
		};

		this.getContentObject = function ()
		{
			return $('#content');
		};

		this.updateSize = function ()
		{
			var menu = $('#main-menu');
			var height = $(window).height() - menu.height() - menu.offset().top;
			$('body').css({
				'height': 'auto'
			});
			var content = that.getContentObject();
			content.css({
				'height': height + 'px'
			});
		};

		this.isMobileDevice = function ()
		{
			return /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent);
		};

		this.isEOBrowser = function ()
		{
			return /Essential Objects/i.test(navigator.userAgent);
		};

		var initHeader = function (headerOptions)
		{
			var mainMenu = $('#main-menu');
			var headerTitle = mainMenu.find('.header-text');
			var headerIcon = mainMenu.find('.header-icon');

			if (headerOptions != undefined)
			{
				headerTitle.html('');
				headerIcon.removeClass(function (index, css)
				{
					return (css.match(/(^|\s)icon-\S+/g) || []).join(' ');
				});
				headerTitle.html(headerOptions.title);
				if (headerOptions.icon != '')
				{
					headerIcon.addClass(headerOptions.icon);
				}
			}
		};

		var initShortcutActions = function (actionsContent)
		{
			var shortcutActionsMenu = $('#shortcut-action-menu');
			var shortcutActionsContainer = $('#shortcut-action-container');
			shortcutActionsContainer.html('');
			if (actionsContent != undefined)
				shortcutActionsContainer.html(actionsContent);

			var actions = shortcutActionsContainer.find('.shortcut-action');
			if (actions.length > 0)
				shortcutActionsMenu.find('.shortcut-menu-header').show();
			else
				shortcutActionsMenu.find('.shortcut-menu-header').hide();

			shortcutActionsContainer.find('.logout').off('click.action').on('click.action', function ()
			{
				$.SalesPortal.Auth.logout();
			});

			shortcutActionsContainer.find('.shortcut-action').off('click.log').on('click.log', function ()
			{
				if (!$(this).hasClass('logout'))
				{
					var dataObject = $(this).find('.service-data');
					var activityDataEncoded = dataObject.find('.activity-data').text();
					if (activityDataEncoded != '')
					{
						var activityData = $.parseJSON(activityDataEncoded);
						$.SalesPortal.LogHelper.write({
							type: 'Shortcut Tile',
							subType: activityData.shortcut,
							data: {
								File: activityData.file,
								Operation: activityData.title
							}
						});
					}
				}
			});
		};
	};
	$.SalesPortal.Content = new ContentManager();
})(jQuery);