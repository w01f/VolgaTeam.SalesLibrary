(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var WallbinManager = function ()
	{
		var that = this;

		var storedTextSize = $.cookie("wallbinTextSize");
		if (storedTextSize == null)
			storedTextSize = 14;
		else
			storedTextSize = parseInt(storedTextSize);

		var storedTextSpace = $.cookie("wallbinTextSpace");
		if (storedTextSpace == null)
			storedTextSpace = 2;
		else
			storedTextSpace = parseInt(storedTextSpace);

		this.assignLinkEvents = function (container)
		{
			updateTextSize(storedTextSize);
			updateTextSpace(storedTextSpace);
			that.updateContentSize();

			$.mtReInit();

			container.find('.line-break').off('click.open').on('click.open', function (event)
			{
				event.stopPropagation();
				event.preventDefault();
			});
			container.find('.clickable').off('click.open').on('click.open', function (event)
			{
				var linkId = $(this).attr('id').replace('link', '');
				$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
				event.stopPropagation();
				event.preventDefault();
			});
			container.find('.folder-link').off('click.open').on('click.open', function (event)
			{
				loadFolderLinkContent($(this));
				event.preventDefault();
			});
			container.find('.log-activity').off('click.log').on('click.log', function ()
			{
				var data = $(this).children('.service-data');
				var activityData = $.parseJSON(data.find('.activity-data').text());

				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Open',
					data: {
						Name: activityData.title,
						File: activityData.fileName,
						'Original Format': activityData.format
					}
				});
			});
			container.find('.folder-header-container').off('mousedown.context').on('mousedown.context', function (eventDown)
			{
				if (eventDown.which == 3)
				{
					$(this).off('mouseup.context').on('mouseup.context', function (eventUp)
					{
						if (eventUp.which == 3)
						{
							var folderId = $(this).attr('id').replace('folder', '');
							$.SalesPortal.LinkManager.requestSpecialDialog(undefined, folderId);
							$(this).off('mouseup.context');
							eventUp.stopPropagation();
							eventUp.preventDefault();
						}
					});
					eventDown.stopPropagation();
				}
			});
			if ($.SalesPortal.Content.isMobileDevice())
			{
				container.find('.folder-header-container').hammer().on('doubletap', function (event)
				{
					var folderId = $(this).attr('id').replace('folder', '');
					$.SalesPortal.LinkManager.requestSpecialDialog(undefined, folderId);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
					event.stopPropagation();
					event.preventDefault();
				});
			}
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

							$.mtReInit();

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
							subType: 'Open Accordion',
							data: {
								Name: parameters.data.name,
								File: parameters.data.fileName,
								'Original Format': parameters.format
							}
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

						$.mtReInit();

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
			if (storedTextSpace < 3)
			{
				storedTextSpace++;
				updateTextSpace(storedTextSpace);
			}
			if (storedTextSize < 17)
			{
				storedTextSize += 3;
				updateTextSize(storedTextSize);
			}
		};

		this.zoomOut = function ()
		{
			if (storedTextSpace > 1)
			{
				storedTextSpace--;
				updateTextSpace(storedTextSpace);
			}
			if (storedTextSize > 11)
			{
				storedTextSize -= 3;
				updateTextSize(storedTextSize);
			}
		};

		var updateTextSpace = function (textSpace)
		{
			if (textSpace == 1)
			{
				$('.link-container').css('margin-bottom', '5px');
			}
			else if (textSpace == 2)
			{
				$('.link-container').css('margin-bottom', '9px');
			}
			else if (textSpace == 3)
			{
				$('.link-container').css('margin-bottom', '14px');
			}

			$.cookie("wallbinTextSpace", textSpace, {
				expires: (60 * 60 * 24 * 7)
			});
		};

		var updateTextSize = function (textSize)
		{
			$('.link-text, .link-note').css('font-size', textSize + 'pt');

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
		};
	};
	$.SalesPortal.Wallbin = new WallbinManager();
})(jQuery);