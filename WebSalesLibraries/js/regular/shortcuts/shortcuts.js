(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ShortcutsManager = function ()
	{
		var that = this;

		var updatedAllContentNecessary = false;

		this.assignShortcutGroupHandlers = function (groupsContainer)
		{
			groupsContainer.find('.om-ctrlitem').off('click.shortcut-group').on('click.shortcut-group', function ()
			{
				var data = $(this).find('.service-data');
				var activityData = $.parseJSON(data.find('.activity-data').text());

				$.SalesPortal.LogHelper.write({
					type: 'Navigation',
					subType: 'MenuBar Group',
					data: {
						file: activityData.title
					}
				});
			});
		};

		this.assignShortcutItemHandlers = function (shortcutsContainer)
		{
			shortcutsContainer.find('.shortcuts-link').off('click.shortcut').on('click.shortcut', function (e)
			{
				var data = $(this).find('.service-data');
				var activityData = $.parseJSON(data.find('.activity-data').text());
				that.trackActivity(activityData);

				var hasPageContent = data.find('.has-page-content').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasPageContent == true && samePage == true)
				{
					e.preventDefault();
					return that.openShortcutByMenuItemData(data, {pushHistory: true});
				}
				return true;
			});
		};

		this.openShortcutByMenuItemData = function (data, customParameters)
		{
			var shortcutId = data.find('.link-id').text();
			var url = data.find('.url').text();
			var shortcutType = data.find('.link-type').text();

			updatedAllContentNecessary = false;
			var menu = $('#main-menu');
			switch (shortcutType)
			{
				case 'video':
					$.SalesPortal.LinkManager.playVideo($.parseJSON(data.find('.links').text()));
					break;
				case 'libraryfile':
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: data.find('.library-link-id').html(),
						isQuickSite: false
					});
					break;
				case 'user_preferences':
					new $.SalesPortal.ShortcutsUserPreferences().init();
					break;
				case 'supergroup':
					new $.SalesPortal.ShortcutsSuperGroup().init(data.find('.super-group-tag').text());
					break;
				case 'gbookmark':
					menu.find('div[data-groupid="group-' + data.find('.bookmark-id').text() + '"]').trigger('click');
					return false;
				case 'youtube':
					$.SalesPortal.LinkManager.playYouTube(data.find('.youtube-title').text(), data.find('.youtube-id').text());
					break;
				default :
					menu.find('.main-site-url').show();
					$.ajax({
						type: "POST",
						url: url,
						data: {
							linkId: shortcutId,
							parameters: customParameters
						},
						beforeSend: function ()
						{
							$.SalesPortal.Overlay.show(false);
						},
						complete: function ()
						{
							$.SalesPortal.Overlay.hide();
						},
						success: openShortcutOnSamePage,
						error: function ()
						{
						},
						async: true,
						dataType: 'json'
					});
					break;
			}

			$.SalesPortal.ShortcutsHistory.pushState(data, customParameters);

			return true;
		};

		this.openStaticShortcutByType = function (type)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getShortcutData",
				data: {
					shortcutType: type
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					that.openShortcutByMenuItemData($('<div>' + msg + '</div>'));
				},
				error: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		this.trackActivity = function (activityData, action, operation)
		{
			var details = activityData.details;
			details['Operation'] = operation == undefined ? 'Open' : operation;
			$.SalesPortal.LogHelper.write({
				type: 'Shortcut Tile',
				subType: action == undefined ? activityData.action : action,
				data: details
			});
		};

		this.updateContentSize = function ()
		{
			$.SalesPortal.Content.updateSize();
		};

		this.updateCurrentShortcut = function ()
		{
			if (updatedAllContentNecessary)
			{
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Site Notification',
					description: 'The site database was recently updated. Click the button below to refresh your page',
					buttons: [
						{
							tag: 'ok',
							title: 'Refresh this page',
							width: 160,
							clickHandler: function ()
							{
								modalDialog.close();
								location.reload();
							}
						}
					]
				});
				modalDialog.show();
			}
		};

		var openShortcutOnSamePage = function (result)
		{
			switch (result.options.shortcutType)
			{
				case 'gridbundle':
					$.SalesPortal.Content.fillContent(result.content,
						{
							title: result.options.headerTitle,
							icon: result.options.headerIcon
						},
						result.actions);
					new $.SalesPortal.ShortcutsGrid().init(result.options);
					updatedAllContentNecessary = true;
					break;
				case 'carouselbundle':
					$.SalesPortal.Content.fillContent(result.content,
						{
							title: result.options.headerTitle,
							icon: result.options.headerIcon
						},
						result.actions);
					new $.SalesPortal.ShortcutsCarousel().init(result.options);
					updatedAllContentNecessary = true;
					break;
				case 'search':
					$.SalesPortal.Content.fillContent(result.content,
						{
							title: result.options.headerTitle,
							icon: result.options.headerIcon
						},
						result.actions);
					$.SalesPortal.ShortcutsSearchLink($.SalesPortal.Content.getContentObject(), result.options.linkId).runSearch();
					break;
				case 'window':
					new $.SalesPortal.ShortcutsLibraryWindow().init(result);
					break;
				case 'page':
					new $.SalesPortal.ShortcutsLibraryPage().init(result);
					break;
				case 'library':
					new $.SalesPortal.ShortcutsWallbin().init(result);
					break;
				case 'download':
					new $.SalesPortal.ShortcutsDownload().init(result);
					break;
				case 'searchapp':
					new $.SalesPortal.ShortcutsSearchApp().init(result);
					break;
				case 'qbuilder':
					new $.SalesPortal.ShortcutsQBuilder().init(result);
					break;
				case 'quizzes':
					new $.SalesPortal.ShortcutsQuizzes().init(result);
					break;
				case 'favorites':
					new $.SalesPortal.ShortcutsFavorites().init(result);
					break;
				default :
					$.SalesPortal.Content.fillContent(result.content,
						{
							title: result.options.headerTitle,
							icon: result.options.headerIcon
						},
						result.actions);
					break;
			}
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsManager();
})(jQuery);