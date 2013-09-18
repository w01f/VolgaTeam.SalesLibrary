(function ($)
{
	var storedTextSize = $.cookie("textSize");
	if (storedTextSize == null)
		storedTextSize = 12;

	var storedTextSpace = $.cookie("textSpace");
	if (storedTextSpace == null)
		storedTextSpace = 2;

	var libraryChanged = function ()
	{
		var selectedLibraryName = $("#select-library").find(":selected").text();
		$.cookie("selectedLibraryName", selectedLibraryName, {
			expires: 60 * 60 * 24 * 7
		});
		$.ajax({
			type: "POST",
			url: "statistic/writeActivity",
			data: {
				type: 'Wallbin',
				subType: 'Library Changed',
				data: $.toJSON({
					Library: selectedLibraryName
				})
			},
			async: true,
			dataType: 'html'
		});
		$.ajax({
			type: "POST",
			url: "wallbin/getPageDropDownList",
			beforeSend: function ()
			{
				$('#libraries-selector-container').css({
					'visibility': 'hidden'
				});
				$.showOverlay();
			},
			complete: function ()
			{
				$.hideOverlay();
				$('#libraries-selector-container').css({
					'visibility': 'visible'
				});
			},
			success: function (msg)
			{
				$('#select-page').html(msg);
				$("#page-logo").attr('src', $("#select-library").val());
				pageChanged();
			},
			async: true,
			dataType: 'html'
		});
	};

	var pageChanged = function ()
	{
		var selectedPageName = $("#select-page").find(":selected").text();
		$.cookie("selectedPageName", selectedPageName, {
			expires: 60 * 60 * 24 * 7
		});
		$("#page-logo").attr('src', $("#select-page").val());
		$.ajax({
			type: "POST",
			url: "statistic/writeActivity",
			data: {
				type: 'Wallbin',
				subType: 'Page Changed',
				data: $.toJSON({
					Page: selectedPageName
				})
			},
			async: true,
			dataType: 'html'
		});
		updateView();
	};

	var loadColumns = function (container)
	{
		$.ajax({
			type: "POST",
			url: "wallbin/getColumnsView",
			data: {},
			beforeSend: function ()
			{
				$.showOverlay();
				container.html('');
			},
			complete: function ()
			{
				$.hideOverlay();
			},
			success: function (msg)
			{
				container.html('<div class="wallbin-container">' + msg + '</div>').find('img').load(function ()
				{
					$.updateContentAreaDimensions();
				});
				assignLinkEvents(container);
			},
			error: function ()
			{
				container.html('');
			},
			async: true,
			dataType: 'html'
		});
	};

	var assignLinkEvents = function (container)
	{
		$.updateTextSize(storedTextSize);
		$.updateTextSpace(storedTextSpace);
		$.updateContentAreaDimensions();
		container.find('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', delay: { show: 500, hide: 100 }});
		container.find('.clickable').off('click').on('click', function (event)
		{
			var linkId = $(this).attr('id').replace('link', '');
			$.requestViewDialog(linkId, false);
			event.stopPropagation();
		});
		container.find('.clickable, .link-container.line-break').off('mousedown.context').on('mousedown.context', function (eventDown)
		{
			if (eventDown.which == 3)
			{
				$(this).off('mouseup.context').on('mouseup.context', function (eventUp)
				{
					if (eventUp.which == 3)
					{
						var linkId = $(this).attr('id').replace('link', '');
						$.requestSpecialDialog([linkId], undefined);
						$(this).off('mouseup.context');
						eventUp.stopPropagation();
						eventUp.preventDefault();
					}
				});
			}
		});
		if (( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
		{
			container.find('.clickable, .link-container.line-break').hammer().on('doubletap', function (event)
			{
				var linkId = $(this).attr('id').replace('link', '');
				$.requestSpecialDialog([linkId], undefined);
				event.gesture.stopPropagation();
				event.gesture.preventDefault();
				event.stopPropagation();
				event.preventDefault();
			});
		}
		container.find('.folder-link').off('click').on('click', function (event)
		{
			loadFolderLinkContent($(this));
			event.stopPropagation();
		});
		container.find('.folder-link').off('mousedown.context').on('mousedown.context', function (eventDown)
		{
			if (eventDown.which == 3)
			{
				$(this).off('mouseup.context').on('mouseup.context', function (eventUp)
				{
					if (eventUp.which == 3)
					{
						var linkId = $(this).attr('id').replace('link', '');
						$.requestSpecialDialog([linkId], undefined);
						$(this).off('mouseup.context');
						eventUp.stopPropagation();
						eventUp.preventDefault();
					}
				});
				eventDown.stopPropagation();
			}
		});
		if (( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
		{
			container.find('.folder-link').hammer().on('doubletap', function (event)
			{
				var linkId = $(this).attr('id').replace('link', '');
				$.requestSpecialDialog([linkId], undefined);
				event.gesture.stopPropagation();
				event.gesture.preventDefault();
				event.stopPropagation();
				event.preventDefault();
			});
		}
		container.find('.folder-header-container').off('mousedown.context').on('mousedown.context', function (eventDown)
		{
			if (eventDown.which == 3)
			{
				$(this).off('mouseup.context').on('mouseup.context', function (eventUp)
				{
					if (eventUp.which == 3)
					{
						var folderId = $(this).attr('id').replace('folder', '');
						$.requestSpecialDialog(undefined, folderId);
						$(this).off('mouseup.context');
						eventUp.stopPropagation();
						eventUp.preventDefault();
					}
				});
				eventDown.stopPropagation();
			}
		});
		if (( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
		{
			container.find('.folder-header-container').hammer().on('doubletap', function (event)
			{
				var folderId = $(this).attr('id').replace('folder', '');
				$.requestSpecialDialog(undefined, folderId);
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
				var linkId = folderLinkContent.attr("id").replace('folder-link-content', '');
			if (!folderLinkContent.find('.link-container').length && linkId != null)
			{
				$.ajax({
					type: "POST",
					url: "wallbin/getLinkFolderContent",
					data: {
						linkId: linkId
					},
					beforeSend: function ()
					{
						$.showOverlayLight();
						folderLinkContent.html('');
					},
					complete: function ()
					{
						$.hideOverlayLight();
					},
					success: function (msg)
					{
						folderLinkContent.html(msg);
						$('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', delay: { show: 500, hide: 100 }});
						assignLinkEvents(folderLinkContent);
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
			linkObject.removeClass('active');
		}
	};

	var loadAccordion = function (container)
	{
		$.ajax({
			type: "POST",
			url: "wallbin/getAccordionView",
			data: {},
			beforeSend: function ()
			{
				$.showOverlay();
				container.html('');
			},
			complete: function ()
			{
				$.hideOverlay();
			},
			success: function (msg)
			{
				container.html('<div>' + msg + '</div>');
				assignAccordionEvents(container);
			},
			error: function ()
			{
				container.html('');
			},
			async: true,
			dataType: 'html'
		});
	};

	var assignAccordionEvents = function (container)
	{
		$.updateContentAreaDimensions();
		container.find('.folder-header')
			.off('click')
			.on('click', function ()
			{
				container.find('.folder-header.active').parent().find('.folder-links').hide("blind", {
					direction: "vertical"
				}, 500);
				var justCollapse = $(this).hasClass('active');
				container.find('.folder-header').removeClass('active');
				if (!justCollapse)
				{
					$(this).addClass('active');
					var folderContainer = $(this).parent();
					var folderId = $.trim($(this).attr("id").replace('folder-', ''));
					showAccordionFolder(folderContainer, folderId);
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
				url: "wallbin/getFolderLinksList",
				data: {
					folderId: folderId
				},
				beforeSend: function ()
				{
					$.showOverlayLight();
					folderLinks.html('');
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					folderLinks.html(msg);
					$('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', delay: { show: 500, hide: 100 }});
					assignLinkEvents(folderLinks);
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

	var loadTabs = function (container)
	{
		$.ajax({
			type: "POST",
			url: "wallbin/getTabsView",
			data: {
				wallbinView: $.cookie("wallbinView")
			},
			beforeSend: function ()
			{
				$.showOverlay();
				container.html('');
			},
			complete: function ()
			{
				$.hideOverlay();
			},
			success: function (msg)
			{
				container.html('<div class="wallbin-tabs">' + msg + '</div>');
				var tabPages = container.find('.wallbin-tabs')
				tabPages.tabs({
					selected: $("#select-page")[0].selectedIndex,
					select: function (event, ui)
					{
						var selectedPageName = $(ui.tab).text();
						$.cookie("selectedPageName", selectedPageName, {
							expires: 60 * 60 * 24 * 7
						});
						$("#select-page")[0].selectedIndex = ui.index;
						$("#page-logo").attr('src', $("#select-page").val());
						if ($.cookie("wallbinView") == "accordion")
							loadAccordion($(ui.panel));
						else
							loadColumns($(ui.panel));
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Wallbin',
								subType: 'Page Changed',
								data: $.toJSON({
									Page: selectedPageName
								})
							},
							async: true,
							dataType: 'html'
						});
					}
				});
				if ($.cookie("wallbinView") == "accordion")
					assignAccordionEvents(container);
				else
					assignLinkEvents(container);
			},
			error: function ()
			{
				container.html('');
			},
			async: true,
			dataType: 'html'
		});
	};

	var updateView = function ()
	{
		var pageselector = $("#select-page");
		var container = $('#content');
		var tabToggle = $('#tabs-view');

		if ($.cookie("wallbinUseTabs") != "true")
		{
			pageselector.show();
			tabToggle.removeClass('active');
			if ($.cookie("wallbinView") == "accordion")
			{
				$('#accordion-view').addClass('active');
				loadAccordion(container);
			}
			else
			{
				$('#columns-view').addClass('active');
				loadColumns(container);
			}
		}
		else
		{
			pageselector.hide();
			tabToggle.addClass('active');
			if ($.cookie("wallbinView") == "accordion")
				$('#accordion-view').addClass('active');
			else
				$('#columns-view').addClass('active');
			loadTabs(container);
		}
	};

	$.initWallbinView = function ()
	{
		$.ajax({
			type: "POST",
			url: "wallbin/getLibraryDropDownList",
			beforeSend: function ()
			{
				$('#content').html('');
				$.showOverlay();
				$('#libraries-selector-container').css({
					'visibility': 'hidden'
				});
			},
			complete: function ()
			{
				$('#libraries-selector-container').css({
					'visibility': 'visible'
				});
				$.hideOverlay();
			},
			success: function (msg)
			{
				$('#select-library').html(msg);
				libraryChanged();
			},
			async: true,
			dataType: 'html'
		});
	};

	$(document).ready(function ()
	{
		$('#select-library').on('change', function ()
		{
			libraryChanged();
		});
		$('#select-page').on('change', function ()
		{
			pageChanged();
		});

		$(window).on('resize', $.updateContentAreaDimensions);

		$('#increase-text-space').off('click').on('click', function ()
		{
			if (storedTextSpace < 3)
			{
				storedTextSpace++;
				$.updateTextSpace(storedTextSpace);
			}
		});
		$('#decrease-text-space').off('click').on('click', function ()
		{
			if (storedTextSpace > 1)
			{
				storedTextSpace--;
				$.updateTextSpace(storedTextSpace);
			}
		});

		$('#increase-text-size').off('click').on('click', function ()
		{
			if (storedTextSize < 22)
				storedTextSize++;
			$.updateTextSize(storedTextSize);
		});

		$('#decrease-text-size').off('click').on('click', function ()
		{
			if (storedTextSize > 8)
				storedTextSize--;
			$.updateTextSize(storedTextSize);
		});

		$('#columns-view').off('click').on('click', function ()
		{
			$('#columns-view,#accordion-view').removeClass('active');
			$.cookie("wallbinView", "columns", {
				expires: 60 * 60 * 24 * 7
			});
			updateView();
		});

		$('#accordion-view').off('click').on('click', function ()
		{
			$('#columns-view,#accordion-view').removeClass('active');
			$.cookie("wallbinView", "accordion", {
				expires: 60 * 60 * 24 * 7
			});
			updateView();
		});

		$('#tabs-view').off('click').on('click', function ()
		{
			var tabToggle = $('#tabs-view');
			$.cookie("wallbinUseTabs", !tabToggle.hasClass('active'), {
				expires: 60 * 60 * 24 * 7
			});
			updateView();
		});
	});
})
	(jQuery);