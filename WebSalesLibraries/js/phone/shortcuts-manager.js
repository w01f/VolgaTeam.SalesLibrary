(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsGroup = function ()
	{
		var that = this;

		this.init = function ()
		{
			var groupPage = $('#shortcut-group');

			groupPage.find('.menu-items').cubeportfolio({
				gridAdjustment: 'alignCenter'
			});

			$(window).on("pagechange", function (event, data)
			{
				if (data.toPage.prop('id') == groupPage.prop('id') && (data.prevPage == undefined || data.prevPage.prop('id') != groupPage.prop('id')))
				{
					groupPage.find('.menu-items').cubeportfolio('destroy');
					groupPage.find('.menu-items').cubeportfolio({
						gridAdjustment: 'alignCenter'
					});
				}
			});

			groupPage.find('.menu-item').off('click').on('click', function (e)
			{
				var data = $(this).find('.service-data');
				that.trackActivity(data);

				var hasPageContent = data.find('.has-page-content').length > 0;
				var samePage = data.find('.same-page').length > 0;

				if (hasPageContent == true && samePage == true)
				{
					e.preventDefault();
					that.openShortcut(data);
				}
			});

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});
		};

		this.openShortcut = function (data, parentShortcutId, customParameters)
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
							id: parentShortcutId == undefined ? '#shortcut-group' : ('#shortcut-link-page-' + parentShortcutId),
							name: shortcutLinkTitle
						},
						false
					);
					break;
				case 'download':
					if (parentShortcutId == undefined)
						$('#shortcuts-link-download-warning-popup').popup('open');
					else
						$('#shortcuts-link-download-warning-popup-' + parentShortcutId).popup('open');
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

							$('body').append($(result.content));
							$.mobile.initializePage();

							switch (result.options.shortcutType)
							{
								case 'gridbundle':
								case 'carouselbundle':
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
				$('body .shortcut-link-page').not('#shortcut-link-page-' + parentShortcutId).remove();
		};

		this.trackActivity = function (dataObject)
		{
			var activityData = $.parseJSON(dataObject.find('.activity-data').text());
			$.SalesPortal.LogHelper.write({
				type: 'Shortcut Tile',
				subType: activityData.action,
				data: {
					File: activityData.title
				}
			});
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsGroup();
})(jQuery);