(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.WallbinManager = function (inputSettings) {
		var that = this;
		var updateResponsiveColumnsTimer = null;

		var wallbinSettings = new WallbinSettings(inputSettings);

		var contentContainerId = wallbinSettings.contentObject.prop('id');
		if (contentContainerId === '')
			contentContainerId = 'content';

		var storedTextSize = $.cookie("wallbinTextSize" + wallbinSettings.shortcutId);
		if (storedTextSize === null)
			storedTextSize = 12;
		else
		{
			storedTextSize = parseInt(storedTextSize);
			if (!(storedTextSize === 14 || storedTextSize === 12 || storedTextSize === 10))
				storedTextSize = 12
		}

		this.initContent = function () {

			switch (wallbinSettings.pageViewType)
			{
				case 'columns':
					updateTextSize(storedTextSize);
					assignLinkEvents(wallbinSettings.contentObject);
					break;
				case 'accordion':
					assignAccordionEvents(wallbinSettings.contentObject);
					break;
			}
		};

		this.initPageSelector = function () {
			var libraryHeader = wallbinSettings.contentObject.find('.wallbin-header');
			switch (wallbinSettings.pageSelectorMode)
			{
				case 'tabs':
					var tabContainer = libraryHeader.find('.page-selector-container .tab-pages');
					tabContainer.scrollTabs({
						click_callback: function () {
							tabContainer.find('.page-tab-header').removeClass('selected');
							$(this).addClass('selected');
							loadPageContent($(this).find('.service-data .encoded-data').text());
						}
					});
					var selectedTab = tabContainer.find('.selected');
					var left = tabContainer.scrollLeft();
					tabContainer.find('.scroll_tab_inner').animate({scrollLeft: left + selectedTab.position().left + 'px'}, 0);
					break;
				case 'combo':
					var comboSelector = libraryHeader.find('.selectpicker');
					comboSelector.selectpicker();
					comboSelector.off('change').on('change', function () {
						loadPageContent(atob(comboSelector.selectpicker('val').trim()));
						comboSelector.selectpicker('refresh');
					});
					break;
			}
		};

		this.updateContentSize = function () {
			if (wallbinSettings.processResponsiveColumns)
				updateResponsiveColumnsTimer = setTimeout(function () {
					$.SalesPortal.ScreenManager.processScreenSizeChange(updateResponsiveColumns);
				}, 500);

			if (wallbinSettings.fitWallbinToWholeScreen)
			{
				var content = $.SalesPortal.Content.getContentObject();

				var wallbinHeader = content.find('.wallbin-header-container');

				var contentHeight = content.outerHeight(true);
				var headerHeight = wallbinHeader.outerHeight(true);
				var wallbinHeight = contentHeight - headerHeight - 3;

				var pageContainers = content.find('.page-container');
				pageContainers.css({
					'height': wallbinHeight + 'px'
				});
				$.each(pageContainers, function () {
					var pageContainer = $(this);

					var headerHeight = pageContainer.find('.header-container').outerHeight(true);
					pageContainer.find('.content-container').css({
						'height': (wallbinHeight - headerHeight) + 'px'
					});
				});
			}
			fixColumnBorders();
		};

		this.zoomIn = function () {
			if (storedTextSize < 14)
			{
				storedTextSize += 2;
				updateTextSize(storedTextSize);
			}
		};

		this.zoomOut = function () {
			if (storedTextSize > 10)
			{
				storedTextSize -= 2;
				updateTextSize(storedTextSize);
			}
		};

		var loadPageContent = function (pageDataEncoded) {

			var pageData = $.parseJSON(pageDataEncoded);

			$.cookie("SelectedLibraryPageId-" + wallbinSettings.wallbinId, pageData.pageId, {
				expires: (60 * 60 * 24 * 7)
			});

			var libraryContent = wallbinSettings.contentObject.find('.wallbin-container');

			libraryContent.find('.service-data .encoded-data.selected-page-data').text(pageDataEncoded);

			libraryContent.find('.page-container').removeClass('selected').hide();

			var selectedPage = libraryContent.find('#page-' + pageData.pageId);
			if (selectedPage.length === 0)
			{
				var viewPath = undefined;
				switch (wallbinSettings.pageViewType)
				{
					case 'columns':
						viewPath = "wallbin/getColumnsView";
						break;
					case 'accordion':
						viewPath = "wallbin/getAccordionView";
						break;
				}
				$.ajax({
					type: "POST",
					url: window.BaseUrl + viewPath,
					data: {
						libraryId: pageData.libraryId,
						pageId: pageData.pageId,
						styleContainerType: pageData.styleContainerType,
						styleContainerId: pageData.styleContainerId,
						contentContainerId: contentContainerId,
						screenSettings: $.SalesPortal.ScreenManager.getScreenSettings()
					},
					beforeSend: function () {
						$.SalesPortal.Overlay.show();
					},
					complete: function () {
						$.SalesPortal.Overlay.hide();
					},
					success: function (result) {
						if (pageData.logoContent !== '')
							wallbinSettings.contentObject.find('.wallbin-logo-wrapper').html('<img class="wallbin-logo" src="' + pageData.logoContent + '">');
						else
							wallbinSettings.contentObject.find('.wallbin-logo-wrapper').html('');
						libraryContent.append($(result));
						selectedPage = libraryContent.find('#page-' + pageData.pageId);
						selectedPage.addClass('selected').show();
						that.initContent();
						that.updateContentSize();
					},
					error: function () {
					},
					async: true,
					dataType: 'html'
				});
			}
			else
			{
				if (pageData.logoContent !== '')
					wallbinSettings.contentObject.find('.wallbin-logo-wrapper').html('<img class="wallbin-logo" src="' + pageData.logoContent + '">');
				else
					wallbinSettings.contentObject.find('.wallbin-logo-wrapper').html('');
				selectedPage.addClass('selected').show();
				that.initContent();
				that.updateContentSize();
			}
			$.SalesPortal.LogHelper.write({
				type: 'Wallbin',
				subType: 'Page Open',
				data: {
					Name: wallbinSettings.wallbinName,
					pageName: pageData.pageName
				}
			});
		};

		var assignLinkEvents = function (container) {
			that.updateContentSize();

			if ($.SalesPortal.Content.isMobileDevice())
			{
				container.find('.line-break').off('click').on('click', function (event) {
					event.stopPropagation();
					event.preventDefault();
				});
				container.find('.line-break').hammer().on('tap', function (event) {
					$.SalesPortal.LinkManager.cleanupContextMenu();
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});


				container.find('.clickable').off('click').on('click', function (event) {
					event.stopPropagation();
					event.preventDefault();
				});
				container.find('.clickable').hammer().on('tap', function (event) {
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});

				container.find('.url').hammer().on('tap', function (event) {
					event.gesture.stopPropagation();
				});
				container.find('.clickable, .folder-link, .line-break, .url').hammer().on('hold', function (event) {
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, true, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});

				container.find('.folder-link').off('click').on('click', function (event) {
					event.preventDefault();
					event.stopPropagation();
				});
				container.find('.folder-link').off('click.open').hammer().on('tap', function (event) {
					$.SalesPortal.LinkManager.cleanupContextMenu();
					loadFolderLinkContent($(this));
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});

				container.find('.folder-header-container').off('click').on('click', function (event) {
					event.preventDefault();
					event.stopPropagation();
				});
				container.find('.folder-header-container').hammer().on('hold', function (event) {
					var folderId = $(this).attr('id').replace('folder', '');
					getWindowContextMenu(folderId, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				container.find('.line-break').off('click.open').on('click.open', function (event) {
					$.SalesPortal.LinkManager.cleanupContextMenu();
					event.stopPropagation();
					event.preventDefault();
				});

				container.find('.clickable').off('click.open').on('click.open', function (event) {
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
					event.stopPropagation();
					event.preventDefault();
				});

				container.find('.clickable, .folder-link, .line-break')
					.off('contextmenu').on('contextmenu', function (event) {
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, true, event.clientX, event.clientY);
					return false;
				});

				container.find('.url').off('click.open').on('click.open', function (event) {
					event.stopPropagation();
				});
				container.find('.url-internal').off('contextmenu').on('contextmenu', function (event) {
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, true, event.clientX, event.clientY);
					return false;
				});
				if (!$.SalesPortal.Content.isEOBrowser())
				{
					container.find('.url-external').off('contextmenu').on('contextmenu', function (event) {
						var linkId = $(this).attr('id').replace('link', '');
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, true, event.clientX, event.clientY);
						return false;
					});
				}

				container.find('.folder-link').off('click.open').on('click.open', function (event) {
					$.SalesPortal.LinkManager.cleanupContextMenu();
					loadFolderLinkContent($(this));
					event.preventDefault();
					event.stopPropagation();
				});

				container.find('.folder-header-container')
					.off('contextmenu').on('contextmenu', function (event) {
					var folderId = $(this).attr('id').replace('folder', '');
					getWindowContextMenu(folderId, event.clientX, event.clientY);
					return false;
				});
			}

			container.find('.clickable, .url').off('dragstart').on('dragstart', function (event) {
				var header = $(this).find('.service-data .download-header').text();
				var url = $(this).find('.service-data .download-link').text();
				if (url !== '')
					event.originalEvent.dataTransfer.setData(header, url.replace(/\/\/+/g, '/'));
			});

			container.find('.log-activity').off('click.log').on('click.log', function () {
				var data = $(this).children('.service-data');
				var activityData = $.parseJSON(data.find('.activity-data').text());

				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Open',
					linkId: activityData.id,
					data: {
						name: activityData.title,
						file: activityData.fileName,
						originalFormat: activityData.format
					}
				});
			});
		};

		var assignAccordionEvents = function (container) {
			container.find('.folder-header')
				.off('click')
				.on('click', function () {
					container.find('.folder-header.active').parent().find('.folder-links').hide("blind", {
						direction: "vertical"
					}, 500);
					var justCollapse = $(this).hasClass('active');
					container.find('.folder-header').removeClass('active').blur();
					if (!justCollapse)
					{
						$(this).addClass('active');
						var folderContainer = $(this).parent();
						var folderId = $.trim($(this).attr("id").replace('folder-', ''));
						showAccordionFolder(folderContainer, folderId);
						$.SalesPortal.LogHelper.write({
							type: 'Library Window',
							subType: 'Open Accordion'
						});
					}
				});
		};

		var loadFolderLinkContent = function (linkObject) {
			if (!linkObject.hasClass('active'))
			{
				linkObject.addClass('active');
				var folderLinkContent = linkObject.children('.folder-link-content');
				var linkId = null;
				if (folderLinkContent.attr("id") !== null)
					linkId = folderLinkContent.attr("id").replace('folder-link-content', '');
				if (!folderLinkContent.find('.link-container').length && linkId !== null)
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "wallbin/getLinkFolderContent",
						data: {
							linkId: linkId
						},
						beforeSend: function () {
							$.SalesPortal.Overlay.show();
							folderLinkContent.html('');
						},
						complete: function () {
							$.SalesPortal.Overlay.hide();
						},
						success: function (msg) {
							folderLinkContent.html(msg);
							assignLinkEvents(folderLinkContent);
							folderLinkContent.show("blind", {
								direction: "vertical"
							}, 500);
							linkObject.children('.link-text-container').find('.base-image').hide();
							linkObject.children('.link-text-container').find('.alternative-image').show();
						},
						error: function () {
							folderLinkContent.html('');
						},
						async: true,
						dataType: 'html'
					});
				}
				else
				{
					folderLinkContent.show("blind", {
						direction: "vertical"
					}, 500);
					linkObject.children('.link-text-container').find('.base-image').hide();
					linkObject.children('.link-text-container').find('.alternative-image').show();
				}
			}
			else
			{
				linkObject.children('.folder-link-content').hide("blind", {
					direction: "vertical"
				}, 500);
				linkObject.removeClass('active').blur();
				linkObject.children('.link-text-container').find('.alternative-image').hide();
				linkObject.children('.link-text-container').find('.base-image').show();
			}
		};

		var getWindowContextMenu = function (folderId, pointX, pointY) {
			$.SalesPortal.LinkManager.cleanupContextMenu();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getWindowContextMenu",
				data: {
					folderId: folderId
				},
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (content) {
					var menu = $(content);
					$('body').append(menu);
					menu
						.show()
						.css({
							position: "absolute",
							left: getMenuPosition(menu, pointX, 'width', 'scrollLeft'),
							top: getMenuPosition(menu, pointY, 'height', 'scrollTop')
						})
						.off('click')
						.on('click', 'a', function () {
							menu.hide();
							$.SalesPortal.QBuilder.LinkCart.addFolder(folderId);
						});
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};

		var getMenuPosition = function (menuObject, mouse, direction, scrollDir) {
			var win = $(window)[direction](),
				scroll = $(window)[scrollDir](),
				menu = menuObject[direction](),
				position = mouse + scroll;

			// opening menu would pass the side of the page
			if (mouse + menu > win && menu < mouse)
				position -= menu;

			return position;
		};

		var showAccordionFolder = function (folderContainer, folderId) {
			var folderLinks = folderContainer.find('.folder-links');
			if (folderLinks.html() === '')
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "wallbin/getFolderLinksList",
					data: {
						folderId: folderId
					},
					beforeSend: function () {
						$.SalesPortal.Overlay.show();
						folderLinks.html('');
					},
					complete: function () {
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg) {
						folderLinks.html(msg);

						assignLinkEvents(folderLinks);
						folderLinks.show("blind", {
							direction: "vertical"
						}, 500);
					},
					error: function () {
						folderLinks.html('');
					},
					async: true,
					dataType: 'html'
				});
			}
			else
				folderLinks.show("blind", {
					direction: "vertical"
				}, 500);
		};

		var updateTextSize = function (textSize) {
			wallbinSettings.contentObject.find('.link-text-container-sized').css('font-size', textSize + 'pt');

			$.cookie("wallbinTextSize" + wallbinSettings.shortcutId, textSize, {
				expires: (60 * 60 * 24 * 7)
			});
		};

		var fixColumnBorders = function () {
			var pageContent = wallbinSettings.contentObject.find('.page-container.selected');

			var column1Height = pageContent.find('.column0 .page-column-inner').outerHeight(true);
			var column2Height = pageContent.find('.column1 .page-column-inner').outerHeight(true);
			var column3Height = pageContent.find('.column2 .page-column-inner').outerHeight(true);

			if (column1Height > column2Height)
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column0 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column1 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}

			if (column2Height > column3Height)
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '1px'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '0'
				});
			}
			else
			{
				pageContent.find('.column1 .page-column-inner').css({
					'border-right-width': '0'
				});
				pageContent.find('.column2 .page-column-inner').css({
					'border-left-width': '1px'
				});
			}
		};

		var updateResponsiveColumns = function () {
			updateResponsiveColumnsTimer = null;

			var libraryContent = wallbinSettings.contentObject.find('.wallbin-container');
			libraryContent.find('.page-container').remove();

			var encodedSelectedPageData = libraryContent.find('.service-data .encoded-data.selected-page-data').text();
			loadPageContent(encodedSelectedPageData);

			updateResponsiveColumnsTimer = null;
		}
	};

	var WallbinSettings = function (data) {
		this.contentObject = undefined;
		this.shortcutId = undefined;
		this.wallbinId = undefined;
		this.wallbinName = undefined;
		this.pageViewType = undefined;
		this.pageSelectorMode = undefined;
		this.processResponsiveColumns = false;
		this.fitWallbinToWholeScreen = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);