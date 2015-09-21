(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		var that = this;

		this.assignShortcutHandlers = function (shortcutsContainer)
		{
			shortcutsContainer.find('.shortcuts-link').off('click.shortcut').on('click.shortcut', function (e)
			{
				var data = $(this).find('.service-data');
				that.trackActivity(data);

				var hasPageContent = data.find('.has-page-content').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasPageContent == true && samePage == true)
				{
					e.preventDefault();
					that.openShortcut(data, {}, e);
				}
			});
		};

		this.openShortcut = function (data, customParameters, context)
		{
			var shortcutId = data.find('.link-id').text();
			var url = data.find('.url').text();
			var shortcutType = data.find('.link-type').text();

			switch (shortcutType)
			{
				case 'video':
					$.SalesPortal.LinkManager.playVideo($.parseJSON(data.find('.links').text()));
					break;
				case 'libraryfile':
					$.SalesPortal.LinkManager.requestViewDialog(data.find('.library-link-id').html(), false);
					break;
				case 'gbookmark':
					var menu = $('#main-menu');
					menu.find('div[data-groupid="group-' + data.find('.bookmark-id').text() + '"]').trigger('click');
					context.stopPropagation();
					break;
				default :
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
						success: function (result)
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
									break;
								case 'carouselbundle':
									$.SalesPortal.Content.fillContent(result.content,
										{
											title: result.options.headerTitle,
											icon: result.options.headerIcon
										},
										result.actions);
									new $.SalesPortal.ShortcutsCarousel().init(result.options);
									break;
								case 'search':
									$.SalesPortal.Content.fillContent(result.content,
										{
											title: result.options.headerTitle,
											icon: result.options.headerIcon
										},
										result.actions);
									$.SalesPortal.ShortcutsSearchLink($.SalesPortal.Content.getContentObject(), shortcutId);
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
						},
						error: function ()
						{
						},
						async: true,
						dataType: 'json'
					});
					break;
			}
		};

		this.trackActivity = function (dataObject)
		{
			var activityData = $.parseJSON(dataObject.find('.activity-data').text());
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
				data: {
					type: 'Shortcuts',
					subType: activityData.action,
					data: $.toJSON({
						File: activityData.title
					})
				},
				async: true,
				dataType: 'html'
			});
		};

		this.updateContentSize = function ()
		{
			$.SalesPortal.Content.updateSize();
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsManager();
})(jQuery);