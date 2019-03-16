(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	let ShortcutsGroup = function () {
		let that = this;

		this.init = function () {
			let groupPage = $('.shortcut-group-page');

			let initGroup = function () {
				groupPage.find('.menu-item').off('click').on('click', function (e) {
					var data = $(this).find('.menu-item-data');
					var hasCustomHandler = data.find('.has-custom-handler').length > 0;
					var samePage = data.find('.same-page').length > 0;

					if (hasCustomHandler === true && samePage === true)
					{
						e.preventDefault();
						that.openShortcutByMenuItemData(data, '#' + groupPage.prop('id'));
					}
					else
						that.trackActivity(data);
				});

				groupPage.find('.navigation-items-container-main .shortcuts-link').off('click').on('click', function (e) {
					var data = $(this).find('>.service-data');
					var hasCustomHandler = data.find('.has-custom-handler').length > 0;
					var samePage = data.find('.same-page').length > 0;

					if (hasCustomHandler === true && samePage === true)
					{
						e.preventDefault();
						$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, '#' + groupPage.prop('id'));
					}
					else
						$.SalesPortal.ShortcutsManager.trackActivity(data);
				});

				$('.logout-button').off('click').on('click', function (e) {
					e.stopPropagation();
					e.preventDefault();
					$.SalesPortal.Auth.logout();
				});
			};

			$(window).off("pagecontainerchange.group").on("pagecontainerchange.group", function (event, ui) {
				if ((ui.toPage !== undefined && ui.toPage.prop('id') === groupPage.prop('id')) || ui.options.target === groupPage.prop('id'))
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
							groupPage.find('.menu-items').cubeportfolio('destroy', function () {
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

			$(window).on("pagecontainershow", function (event, ui) {
				let dataObject = $(ui.toPage).find('>.service-data .activity-data');
				if(dataObject.length > 0)
				{
					let activityData = $.parseJSON(dataObject.text());
					console.log(activityData);
					$.SalesPortal.LogHelper.write({
						type: activityData.type,
						subType: activityData.subType,
						data: activityData.data
					});
				}
			});
		};

		this.openShortcutByMenuItemData = function (data, parentShortcutId, customParameters) {
			let shortcutId = data.find('.link-id').text();
			let url = data.find('.url').text();
			let shortcutType = data.find('.link-type').text();

			switch (shortcutType)
			{
				case 'libraryfile':
					let shortcutLinkTitle = data.find('.link-header').text();
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
					$.SalesPortal.ShortcutsManager.trackActivity(data);
					if (parentShortcutId === undefined)
						$('#shortcuts-link-download-warning-popup').popup('open');
					else
						$(parentShortcutId + '-download-warning-popup').popup('open');
					break;
				case 'left_panel_mobile':
					$.SalesPortal.ShortcutsManager.trackActivity(data);
					break;
				default :
					$.ajax({
						type: "POST",
						url: url,
						data: {
							linkId: shortcutId,
							parameters: customParameters
						},
						beforeSend: function () {
							$.mobile.loading('show', {
								textVisible: false,
								html: ""
							});
						},
						complete: function () {
						},
						success: function (result) {
							cleanupPreviousInstance(parentShortcutId);

							let pageContent = $(result.content);

							let dynamicNavigationPanelContent = pageContent.find('.navigation-panels-dynamic').html();
							pageContent.append($(dynamicNavigationPanelContent));
							pageContent.find('.navigation-panels-dynamic').remove();

							$('body').append(pageContent);

							if (parentShortcutId !== undefined)
								pageContent.find('.main-content .content-header .back a').prop('href', parentShortcutId);

							let navigationToggleButton = pageContent.find('.navigation-panel-toggle');
							let navigationItemsContainerMain = pageContent.find('.navigation-items-container-main');
							if (!(result.navigationPanel && result.navigationPanel.content !== ''))
								navigationToggleButton.hide();
							navigationItemsContainerMain.html(result.navigationPanel ? result.navigationPanel.content : '');

							let allNavigationItemsContainers = pageContent.find('.navigation-items-container');
							$(window).one("pagecontainerchange.navigation-items", function () {
								allNavigationItemsContainers.find('.shortcuts-link').off('click').on('click', function (e) {
									var data = $(this).find('>.service-data');

									var hasCustomHandler = data.find('.has-custom-handler').length > 0;
									var samePage = data.find('.same-page').length > 0;

									if (hasCustomHandler === true && samePage === true)
									{
										e.preventDefault();
										$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(data, '#' + $('.shortcut-link-page.ui-page-active').prop('id'));
									}
									else
										$.SalesPortal.ShortcutsManager.trackActivity(data);
								});
								$('.logout-button').off('click').on('click', function (e) {
									e.stopPropagation();
									e.preventDefault();
									$.SalesPortal.Auth.logout();
								});
							});

							$.mobile.initializePage();

							if (result.options != undefined)
							{
								switch (result.options.shortcutType)
								{
									case 'gridbundle':
									case 'carouselbundle':
									case 'landing':
										new $.SalesPortal.ShortcutsBundle(result).init();
										break;
									case 'search':
										$.SalesPortal.ShortcutsManager.trackActivity(data);
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
									case 'pagebundle':
										new $.SalesPortal.ShortcutsWallbin(result).init(result);
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
										$.mobile.loading('hide', {
											textVisible: false,
											html: ""
										});
										break;
								}
							}
						},
						error: function () {
							$.mobile.loading('hide', {
								textVisible: false,
								html: ""
							});
						},
						async: true,
						dataType: 'json'
					});
					break;
			}
		};

		let cleanupPreviousInstance = function (parentShortcutId) {
			if (parentShortcutId === undefined)
				$('body .shortcut-link-page').remove();
			else
			{
				let parentIds = [];
				let currentParentId = parentShortcutId;
				while (currentParentId)
				{
					if (currentParentId != '#')
						parentIds.push(currentParentId);
					currentParentId = $(currentParentId).find('.back a').prop('href');
					if (currentParentId)
						currentParentId = currentParentId.substr(currentParentId.indexOf("#"))
				}
				$('body .shortcut-link-page').not(parentIds.join(', ')).remove();
			}
		};

		this.trackActivity = function (dataObject) {
			let activityData = $.parseJSON(dataObject.find('.activity-data').text());
			let loggerData = {
				type: 'Shortcut Tile',
				subType: activityData.action,
				data: activityData.details
			};
			console.log(loggerData);
			$.SalesPortal.LogHelper.write(loggerData);
		};
	};
	$.SalesPortal.ShortcutsManager = new ShortcutsGroup();
})(jQuery);