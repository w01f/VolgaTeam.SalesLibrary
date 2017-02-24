(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var ShortcutsGroup = function ()
	{
		var that = this;

		this.init = function ()
		{
			var groupPage = $('#shortcut-group');

			var initGroup = function ()
			{
				groupPage.find('.menu-item').off('click').on('click', function (e)
				{
					var data = $(this).find('.service-data');
					that.trackActivity(data);

					var hasCustomHandler = data.find('.has-custom-handler').length > 0;
					var samePage = data.find('.same-page').length > 0;

					if (hasCustomHandler == true && samePage == true)
					{
						e.preventDefault();
						that.openShortcutByMenuItemData(data, '#' + groupPage.prop('id'));
					}
				});

				$('.logout-button').off('click').on('click', function (e)
				{
					e.stopPropagation();
					e.preventDefault();
					$.SalesPortal.Auth.logout();
				});
			};

			$(window).off("pagecontainerchange.group").on("pagecontainerchange.group", function (event, ui)
			{
				if ((ui.toPage != undefined && ui.toPage.prop('id') == groupPage.prop('id')) || ui.options.target == groupPage.prop('id'))
				{
					if (groupPage.find('.cbp-l-grid-masonry').length > 0)
					{
						try
						{
							groupPage.find('.menu-items').cubeportfolio({
								gridAdjustment: 'alignCenter'
							});
							initGroup();
						}
						catch (err)
						{
							groupPage.find('.menu-items').cubeportfolio('destroy', function ()
							{
								groupPage.find('.menu-items').cubeportfolio({
									gridAdjustment: 'alignCenter'
								});
								initGroup();
							});
						}
					}
					else
						initGroup();
				}
			});
		};

		this.openShortcutByMenuItemData = function (data, parentShortcutId, customParameters)
		{
			var shortcutId = data.find('.link-id').text();
			var url = data.find('.url').text();
			var shortcutType = data.find('.link-type').text();

			switch (shortcutType)
			{
				case 'libraryfile':
					var shortcutLinkTitle = data.find('.link-header').text();
					$.SalesPortal.LinkManager.requestViewDialog(
						data.find('.library-link-id').text(),
						{
							id: parentShortcutId,
							name: shortcutLinkTitle
						},
						false
					);
					break;
				case 'download':
					if (parentShortcutId == undefined)
						$('#shortcuts-link-download-warning-popup').popup('open');
					else
						$(parentShortcutId + '-download-warning-popup').popup('open');
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
							$.mobile.loading('show', {
								textVisible: false,
								html: ""
							});
						},
						complete: function ()
						{
							$.mobile.loading('hide', {
								textVisible: false,
								html: ""
							});
						},
						success: function (result)
						{
							cleanupPreviousInstance(parentShortcutId);

							var pageContent = $(result.content);
							$('body').append(pageContent);

							if (parentShortcutId !== undefined)
								pageContent.find('.main-content .content-header .back a').prop('href', parentShortcutId);

							var navigationToggleButton = pageContent.find('.navigation-panel-toggle');
							var navigationItemsConteiner = pageContent.find('.navigation-items-container');
							if (result.navigationPanel == '')
								navigationToggleButton.hide();
							navigationItemsConteiner.html(result.navigationPanel);
							$(window).one("pagecontainerchange.navigation-items", function (event, ui)
							{
								navigationItemsConteiner.find('.shortcuts-link').off('click').on('click', function (e)
								{
									var data = $(this).find('.service-data');
									$.SalesPortal.ShortcutsManager.trackActivity(data);

									var hasCustomHandler = data.find('.has-custom-handler').length > 0;
									var samePage = data.find('.same-page').length > 0;

									if (hasCustomHandler == true && samePage == true)
									{
										e.preventDefault();
										$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, '#' + $('.shortcut-link-page.ui-page-active').prop('id'));
									}
								});
							});

							$.mobile.initializePage();

							switch (result.options.shortcutType)
							{
								case 'gridbundle':
								case 'carouselbundle':
								case 'landing':
									new $.SalesPortal.ShortcutsBundle(result).init();
									break;
								case 'search':
									new $.SalesPortal.ShortcutsSearchLink(result).init();
									break;
								case 'window':
									new $.SalesPortal.ShortcutsLibraryWindow(result).init();
									break;
								case 'qpage':
									new $.SalesPortal.ShortcutsQPage(result).init();
									break;
								case 'page':
									new $.SalesPortal.ShortcutsLibraryPage(result).init();
									break;
								case 'library':
									new $.SalesPortal.ShortcutsWallbin().init(result);
									break;
								case 'searchapp':
									new $.SalesPortal.ShortcutsSearchApp().init(result);
									break;
								case 'favorites':
									new $.SalesPortal.ShortcutsFavorites().init(result);
									break;
								default :
									$.mobile.pageContainer.pagecontainer("change", "#shortcut-link-page-" + result.options.linkId, {
										transition: "slidefade"
									});
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

		var cleanupPreviousInstance = function (parentShortcutId)
		{
			if (parentShortcutId == undefined)
				$('body .shortcut-link-page').remove();
			else
				$('body .shortcut-link-page').not(parentShortcutId).remove();
		};

		this.trackActivity = function (dataObject)
		{
			var activityData = $.parseJSON(dataObject.find('.activity-data').text());
			$.SalesPortal.LogHelper.write({
				type: 'Shortcut Tile',
				subType: activityData.action,
				data: {
					file: activityData.title
				}
			});
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsGroup();
})(jQuery);