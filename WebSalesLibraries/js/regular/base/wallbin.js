(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var WallbinManager = function ()
	{
		var that = this;

		var storedTextSize = $.cookie("textSize");
		if (storedTextSize == null)
			storedTextSize = 12;

		var storedTextSpace = $.cookie("textSpace");
		if (storedTextSpace == null)
			storedTextSpace = 2;

		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getLibraryDropDownList",
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
					$('#libraries-selector-container').css({
						'visibility': 'hidden'
					});
				},
				complete: function ()
				{
					$('#libraries-selector-container').css({
						'visibility': 'visible'
					});
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					$('#select-library').html(msg).selectpicker('refresh');
					libraryChanged();
				},
				async: true,
				dataType: 'html'
			});
			$(window).off('resize').on('resize', updateContentSize);
			var librarySelector = $('#select-library');
			librarySelector.selectpicker();
			librarySelector.off('change').on('change', function ()
			{
				libraryChanged();
			});
			var pageSelector = $('#select-page');
			pageSelector.selectpicker();
			pageSelector.off('change').on('change', function ()
			{
				pageChanged();
			});
			$('#increase-text-space').off('click').on('click', function ()
			{
				if (storedTextSpace < 3)
				{
					storedTextSpace++;
					updateTextSpace(storedTextSpace);
				}
				$(this).blur();
			});
			$('#decrease-text-space').off('click').on('click', function ()
			{
				if (storedTextSpace > 1)
				{
					storedTextSpace--;
					updateTextSpace(storedTextSpace);
				}
				$(this).blur();
			});

			$('#increase-text-size').off('click').on('click', function ()
			{
				if (storedTextSize < 22)
					storedTextSize++;
				updateTextSize(storedTextSize);
				$(this).blur();
			});

			$('#decrease-text-size').off('click').on('click', function ()
			{
				if (storedTextSize > 8)
					storedTextSize--;
				updateTextSize(storedTextSize);
				$(this).blur();
			});

			$('#columns-view').off('click').on('click', function ()
			{
				$('#columns-view,#accordion-view').removeClass('sel').blur();
				$.cookie("wallbinView", "columns", {
					expires: 60 * 60 * 24 * 7
				});
				updateView();
			});

			$('#accordion-view').off('click').on('click', function ()
			{
				$('#columns-view,#accordion-view').removeClass('sel').blur();
				$.cookie("wallbinView", "accordion", {
					expires: 60 * 60 * 24 * 7
				});
				updateView();
			});

			$('#tabs-view').off('click').on('click', function ()
			{
				var tabToggle = $('#tabs-view');
				$.cookie("wallbinUseTabs", !tabToggle.hasClass('sel'), {
					expires: 60 * 60 * 24 * 7
				});
				updateView();
			});
		};

		this.assignLinkEvents = function (container)
		{
			updateTextSize(storedTextSize);
			updateTextSpace(storedTextSpace);
			updateContentSize();
			container.find('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', container: '#content', delay: { show: 500, hide: 100 }});
			container.find('.clickable').off('click').on('click', function (event)
			{
				var linkId = $(this).attr('id').replace('link', '');
				$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
				event.stopPropagation();
			});
			container.find('.folder-link').off('click').on('click', function (event)
			{
				loadFolderLinkContent($(this));
				event.stopPropagation();
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
			if (( /Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent) ))
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

		var libraryChanged = function ()
		{
			var librarySelector = $("#select-library");
			var selectedLibraryName = librarySelector.find(":selected").text();
			librarySelector.selectpicker('render');
			$.cookie("selectedLibraryName", selectedLibraryName, {
				expires: 60 * 60 * 24 * 7
			});
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
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
				url: window.BaseUrl + "wallbin/getPageDropDownList",
				beforeSend: function ()
				{
					$('#libraries-selector-container').css({
						'visibility': 'hidden'
					});
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
					$('#libraries-selector-container').css({
						'visibility': 'visible'
					});
				},
				success: function (msg)
				{
					$('#select-page').html(msg).selectpicker('refresh');
					$("#page-logo").attr('src', $("#select-library").val());
					pageChanged();
				},
				async: true,
				dataType: 'html'
			});
		};

		var pageChanged = function ()
		{
			var pageSelector = $("#select-page");
			var pageLogoUrl = pageSelector.selectpicker('val');
			var selectedPageName = pageSelector.find(":selected").text();
			pageSelector.selectpicker('render');
			$.cookie("selectedPageName", selectedPageName, {
				expires: 60 * 60 * 24 * 7
			});
			$("#page-logo").attr('src', pageLogoUrl);
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "statistic/writeActivity",
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
				url: window.BaseUrl + "wallbin/getColumnsView",
				data: {},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
					container.html('');
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					container.html('<div class="wallbin-content wallbin-container">' + msg + '</div>').find('img').load(function ()
					{
						updateContentSize();
					});
					that.assignLinkEvents(container);
				},
				error: function ()
				{
					container.html('');
				},
				async: true,
				dataType: 'html'
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
							$('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', container: '#content', delay: { show: 500, hide: 100 }});
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

		var loadAccordion = function (container)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getAccordionView",
				data: {},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
					container.html('');
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					container.html('<div class="wallbin-content">' + msg + '</div>');
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
			updateContentSize();
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
						$('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', container: '#content', delay: { show: 500, hide: 100 }});
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

		var loadTabs = function (container)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getTabsView",
				data: {
					wallbinView: $.cookie("wallbinView")
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(true);
					container.html('');
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					container.html('<div class="wallbin-content wallbin-tabs">' + msg + '</div>');
					var tabPages = container.find('.wallbin-tabs');
					tabPages.tabs({
						active: $("#select-page").prop("selectedIndex"),
						activate: function (event, ui)
						{
							var selectedPageName = $(ui.newTab).text();
							$.cookie("selectedPageName", selectedPageName, {
								expires: 60 * 60 * 24 * 7
							});
							var pageSelector = $("#select-page");
							pageSelector.find("option").each(function ()
							{
								if ($(this).text() == selectedPageName)
								{
									$("#page-logo").attr('src', $(this).val());
									$(this).prop("selected", "selected");
								}
								else
									$(this).prop("selected", false)
							});
							pageSelector.selectpicker('render');

							if ($.cookie("wallbinView") == "accordion")
								loadAccordion($(ui.newPanel));
							else
								loadColumns($(ui.newPanel));
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "statistic/writeActivity",
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
					tabPages.tabs('paging', { follow: true });
					if ($.cookie("wallbinView") == "accordion")
						assignAccordionEvents(container);
					else
						that.assignLinkEvents(container);
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
				pageselector.selectpicker('show');
				tabToggle.removeClass('sel').blur();
				if ($.cookie("wallbinView") == "accordion")
				{
					$('#accordion-view').addClass('sel');
					loadAccordion(container);
				}
				else
				{
					$('#columns-view').addClass('sel');
					loadColumns(container);
				}
			}
			else
			{
				pageselector.selectpicker('hide');
				tabToggle.addClass('sel');
				if ($.cookie("wallbinView") == "accordion")
					$('#accordion-view').addClass('sel');
				else
					$('#columns-view').addClass('sel');
				loadTabs(container);
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

			$.cookie("textSpace", textSpace, {
				expires: (60 * 60 * 24 * 7)
			});
		};

		var updateTextSize = function (textSize)
		{
			$('.link-text, .link-note').css('font-size', textSize + 'pt');

			$.cookie("textSize", textSize, {
				expires: (60 * 60 * 24 * 7)
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			var content = $('#content');
			var height = content.height();
			content.find('>div.wallbin-content').css({
				'height': height + 'px'
			});
			var tabPanelHeight = 0;
			var tabPanel = content.find('.ui-tabs-nav');
			if (tabPanel.length > 0)
			{
				tabPanelHeight = tabPanel.height();
				if (tabPanelHeight == 0)
					tabPanelHeight = 36;
			}
			$.each(content.find('.wallbin-container'), function ()
			{
				var pageHeaderHeight = 0;
				var pageHeader = $(this).find('.wallbin-header');
				if (pageHeader.length > 0)
					pageHeaderHeight = pageHeader.height() + 1;
				if (tabPanelHeight > 0)
					$(this).find('.wallbin-tab').css({
						'height': (height - pageHeaderHeight - tabPanelHeight - 5) + 'px'
					});
				else
					$(this).find('.wallbin-tab').css({
						'height': (height - pageHeaderHeight) + 'px'
					});
			});
		};
	};
	$.SalesPortal.Wallbin = new WallbinManager();
})(jQuery);