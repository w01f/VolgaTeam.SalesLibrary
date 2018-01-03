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
				var activityTag = data.find('.activity-data');
				if(activityTag.length>0)
				{
					var activityData = $.parseJSON(activityTag.text());
					that.trackActivity(activityData);
				}

				var hasCustomHandler = data.find('.has-custom-handler').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasCustomHandler === true && samePage === true)
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
					menu.find('.onemenu').click();
					menu.find('div[data-groupid="group-' + data.find('.bookmark-id').text() + '"]').trigger('click');
					return false;
				case 'youtube':
					$.SalesPortal.LinkManager.playYouTube(data.find('.youtube-title').text(), data.find('.youtube-id').text());
					break;
				case 'vimeo':
					$.SalesPortal.LinkManager.playVimeo(data.find('.player-title').text(), data.find('.player-url').text());
					break;
				default :
					if(shortcutId)
					{
						menu.find('.main-site-url').show();
						$.SalesPortal.HistoryManager.pushShortcut(data, customParameters);
						$.ajax({
							type: "POST",
							url: url,
							data: {
								linkId: shortcutId,
								parameters: customParameters
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (result)
							{
								openShortcutOnSamePage(result);
								if (customParameters && customParameters.scrollPosition)
									$.SalesPortal.Content.getContentObject().scrollTop(customParameters.scrollPosition);
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'json'
						});
					}
					break;
			}
			return true;
		};

		this.openStaticShortcutByType = function (type, parameters)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getShortcutDataByType",
				data: {
					shortcutType: type
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					that.openShortcutByMenuItemData($('<div>' + msg + '</div>'), parameters);
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
			details['Operation'] = operation === undefined ? 'Open' : operation;
			$.SalesPortal.LogHelper.write({
				type: 'Shortcut Tile',
				subType: action === undefined ? activityData.action : action,
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

		var openShortcutOnSamePage = function (parameters)
		{
			parameters.autoLoadLinkiCallback = undefined;
			if (parameters.options.autolLoadLinkId !== undefined)
			{
				parameters.autoLoadLinkiCallback = function () {
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: parameters.options.autolLoadLinkId,
						isQuickSite: false
					});
				}
			}
				
			switch (parameters.options.shortcutType)
			{
				case 'gridbundle':
					new $.SalesPortal.ShortcutsGrid().init(parameters);
					updatedAllContentNecessary = true;
					break;
				case 'carouselbundle':
					new $.SalesPortal.ShortcutsCarousel().init(parameters);
					updatedAllContentNecessary = true;
					break;
				case 'search':
					$.SalesPortal.ShortcutsSearchLink(parameters).runSearch(parameters.autoLoadLinkiCallback);
					break;
				case 'window':
					new $.SalesPortal.ShortcutsLibraryWindow().init(parameters);
					break;
				case 'page':
					new $.SalesPortal.ShortcutsLibraryPage().init(parameters);
					break;
				case 'pagebundle':
					new $.SalesPortal.ShortcutsLibraryPageBundle().init(parameters);
					break;
				case 'library':
					new $.SalesPortal.ShortcutsWallbin().init(parameters);
					break;
				case 'download':
					new $.SalesPortal.ShortcutsDownload().init(parameters);
					break;
				case 'searchapp':
					new $.SalesPortal.ShortcutsSearchApp().init(parameters);
					break;
				case 'qbuilder':
					new $.SalesPortal.ShortcutsQBuilder().init(parameters);
					break;
				case 'quizzes':
					new $.SalesPortal.ShortcutsQuizzes().init(parameters);
					break;
				case 'favorites':
					new $.SalesPortal.ShortcutsFavorites().init(parameters);
					break;
				case 'landing':
					new $.SalesPortal.ShortcutsLandingPage().init(parameters);
					updatedAllContentNecessary = true;
					break;
				default :
					$.SalesPortal.Content.fillContent({
						content: parameters.content,
						headerOptions: parameters.options.headerOptions,
						actions: parameters.actions,
						navigationPanel: parameters.navigationPanel,
						loadCallback: function ()
						{
							$.SalesPortal.ShortcutsManager.updateContentSize();
							$(window).off('resize.default-shortcut').on('resize.default-shortcut', $.SalesPortal.ShortcutsManager.updateContentSize);
						}
					});
					break;
			}
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsManager();
})(jQuery);