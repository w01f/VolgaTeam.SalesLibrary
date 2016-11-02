(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var WallbinManager = function ()
	{
		var that = this;

		var storedTextSize = $.cookie("wallbinTextSize");
		if (storedTextSize == null)
			storedTextSize = 12;
		else
		{
			storedTextSize = parseInt(storedTextSize);
			if (!(storedTextSize == 14 || storedTextSize == 12 || storedTextSize == 10))
				storedTextSize = 12
		}

		this.assignLinkEvents = function (container)
		{
			updateTextSize(storedTextSize);
			that.updateContentSize();

			if ($.SalesPortal.Content.isMobileDevice())
			{
				container.find('.line-break').on('click', function (event)
				{
					event.stopPropagation();
					event.preventDefault();
				});
				container.find('.line-break').hammer().on('tap', function (event)
				{
					$.SalesPortal.LinkManager.cleanupContextMenu();
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});


				container.find('.clickable').on('click', function (event)
				{
					event.stopPropagation();
					event.preventDefault();
				});
				container.find('.clickable').hammer().on('tap', function (event)
				{
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});

				container.find('.url').hammer().on('tap', function (event)
				{
					event.gesture.stopPropagation();
				});
				container.find('.clickable, .folder-link, .line-break, .url').hammer().on('hold', function (event)
				{
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
				container.find('.url').hammer().on('tap', function (event)
				{
					event.gesture.stopPropagation();
				});

				container.find('.folder-link').on('click', function (event)
				{
					event.preventDefault();
					event.stopPropagation();
				});
				container.find('.folder-link').off('click.open').hammer().on('tap', function (event)
				{
					$.SalesPortal.LinkManager.cleanupContextMenu();
					loadFolderLinkContent($(this));
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});

				container.find('.folder-header-container').on('click', function (event)
				{
					event.preventDefault();
					event.stopPropagation();
				});
				container.find('.folder-header-container').hammer().on('hold', function (event)
				{
					var folderId = $(this).attr('id').replace('folder', '');
					getWindowContextMenu(folderId, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				container.find('.line-break').off('click.open').on('click.open', function (event)
				{
					$.SalesPortal.LinkManager.cleanupContextMenu();
					event.stopPropagation();
					event.preventDefault();
				});

				container.find('.clickable').off('click.open').on('click.open', function (event)
				{
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
					event.stopPropagation();
					event.preventDefault();
				});

				container.find('.clickable, .folder-link, .line-break')
					.off('contextmenu').on('contextmenu', function (event)
				{
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
					return false;
				});

				container.find('.url').off('click.open').on('click.open', function (event)
				{
					event.stopPropagation();
				});
				container.find('.url-internal').off('contextmenu').on('contextmenu', function (event)
				{
					var linkId = $(this).attr('id').replace('link', '');
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
					return false;
				});
				if (!$.SalesPortal.Content.isEOBrowser())
				{
					container.find('.url-external').off('contextmenu').on('contextmenu', function (event)
					{
						var linkId = $(this).attr('id').replace('link', '');
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, event.clientX, event.clientY);
						return false;
					});
				}

				container.find('.folder-link').off('click.open').on('click.open', function (event)
				{
					$.SalesPortal.LinkManager.cleanupContextMenu();
					loadFolderLinkContent($(this));
					event.preventDefault();
					event.stopPropagation();
				});

				container.find('.folder-header-container')
					.off('contextmenu').on('contextmenu', function (event)
				{
					var folderId = $(this).attr('id').replace('folder', '');
					getWindowContextMenu(folderId, event.clientX, event.clientY);
					return false;
				});
			}

			container.find('.clickable, .url').off('dragstart').on('dragstart', function (event)
			{
				var header = $(this).find('.service-data .download-header').text();
				var url = $(this).find('.service-data .download-link').text();
				if (url != '')
					event.originalEvent.dataTransfer.setData(header, url.replace('site_base_url_placeholder', window.BaseUrl).replace(/\/\/+/g, '/'));
			});

			container.find('.log-activity').off('click.log').on('click.log', function ()
			{
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

		var loadFolderLinkContent = function (linkObject)
		{
			if (!linkObject.hasClass('active'))
			{
				linkObject.addClass('active');
				var folderLinkContent = linkObject.children('.folder-link-content');
				var linkId = null;
				if (folderLinkContent.attr("id") != null)
					linkId = folderLinkContent.attr("id").replace('folder-link-content', '');
				if (!folderLinkContent.find('.link-container').length && linkId != null)
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "wallbin/getLinkFolderContent",
						data: {
							linkId: linkId
						},
						beforeSend: function ()
						{
							$.SalesPortal.Overlay.show(false);
							folderLinkContent.html('');
						},
						complete: function ()
						{
							$.SalesPortal.Overlay.hide();
						},
						success: function (msg)
						{
							folderLinkContent.html(msg);

							that.assignLinkEvents(folderLinkContent);
							folderLinkContent.show("blind", {
								direction: "vertical"
							}, 500);
						},
						error: function ()
						{
							folderLinkContent.html('');
						},
						async: true,
						dataType: 'html'
					});
				}
				else
					folderLinkContent.show("blind", {
						direction: "vertical"
					}, 500);

			}
			else
			{
				linkObject.children('.folder-link-content').hide("blind", {
					direction: "vertical"
				}, 500);
				linkObject.removeClass('active').blur();
			}
		};

		var getWindowContextMenu = function (folderId, pointX, pointY)
		{
			$.SalesPortal.LinkManager.cleanupContextMenu();

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getWindowContextMenu",
				data: {
					folderId: folderId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (content)
				{
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
						.on('click', 'a', function ()
						{
							menu.hide();
							$.SalesPortal.QBuilder.LinkCart.addFolder(folderId);
						});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var getMenuPosition = function (menuObject, mouse, direction, scrollDir)
		{
			var win = $(window)[direction](),
				scroll = $(window)[scrollDir](),
				menu = menuObject[direction](),
				position = mouse + scroll;

			// opening menu would pass the side of the page
			if (mouse + menu > win && menu < mouse)
				position -= menu;

			return position;
		};

		this.assignAccordionEvents = function (container)
		{
			that.updateContentSize();
			container.find('.folder-header')
				.off('click')
				.on('click', function ()
				{
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

		var showAccordionFolder = function (folderContainer, folderId)
		{
			var folderLinks = folderContainer.find('.folder-links');
			if (folderLinks.html() == '')
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "wallbin/getFolderLinksList",
					data: {
						folderId: folderId
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
						folderLinks.html('');
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function (msg)
					{
						folderLinks.html(msg);

						that.assignLinkEvents(folderLinks);
						folderLinks.show("blind", {
							direction: "vertical"
						}, 500);
					},
					error: function ()
					{
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

		this.zoomIn = function ()
		{
			if (storedTextSize < 14)
			{
				storedTextSize += 2;
				updateTextSize(storedTextSize);
			}
		};

		this.zoomOut = function ()
		{
			if (storedTextSize > 10)
			{
				storedTextSize -= 2;
				updateTextSize(storedTextSize);
			}
		};

		var updateTextSize = function (textSize)
		{
			$('.link-text-container-sized').css('font-size', textSize + 'pt');

			$.cookie("wallbinTextSize", textSize, {
				expires: (60 * 60 * 24 * 7)
			});
		};

		this.updateContentSize = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			var wallbinHeader = content.find('.wallbin-header');

			var contentHeight = content.outerHeight();
			var headerHeight = wallbinHeader.outerHeight();
			var wallbinHeight = contentHeight - headerHeight;

			var pageContainers = content.find('.page-container');
			pageContainers.css({
				'height': wallbinHeight + 'px'
			});
			$.each(pageContainers, function ()
			{
				var pageContainer = $(this);

				var headerHeight = pageContainer.find('.header-container').outerHeight();
				pageContainer.find('.content-container').css({
					'height': (wallbinHeight - headerHeight) + 'px'
				});
			});

			var libraryUpdateStamp = $('#library-update-stamp');
			libraryUpdateStamp.css({
				'left': ($('body').outerWidth() - libraryUpdateStamp.outerWidth()) + 'px'
			});

		};
	};
	$.SalesPortal.Wallbin = new WallbinManager();
})(jQuery);