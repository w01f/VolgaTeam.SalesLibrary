(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		var that = this;

		var searchBar = undefined;

		this.selectedCarouselCategory = 1;
		this.selectedPageId = undefined;

		this.init = function (tabId)
		{
			var pageSelector = '#' + tabId + ' .enabled.shortcuts-page';
			$('.shortcuts-page').off('click');
			$(pageSelector).on('click', function ()
			{
				$(pageSelector).removeClass('sel');
				$(this).addClass('sel');
				loadShortcutsPage(tabId);
			});
			loadShortcutsPage(tabId);
			$(window).off('resize').on('resize', that.updateContentSize);
		};

		this.processSearchLink = function (content, objectId)
		{
			$.SalesPortal.ShortcutsSearchManager(content, objectId);
		};

		var loadShortcutsPage = function (tabId)
		{
			var pageIdSelector = '#' + tabId + ' .sel.shortcuts-page';
			var pageId = $(pageIdSelector).attr('id');
			if (that.selectedPageId != pageId)
				that.selectedCarouselCategory = 1;
			that.selectedPageId = pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getPage",
				data: {
					pageId: pageId
				},
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
					that.updateContentSize();
				},
				success: function (data)
				{
					var content = $('#content');
					content.html(data.content);

					initPageLogo(data);
					searchBar = new $.SalesPortal.ShortcutsSearchBar(pageId);

					switch (data.type)
					{
						case 'grid':
							content.find('.shortcuts-links-grid').cubeportfolio(data.displayParameters);
							$('.shortcuts-link.direct').off('click').on('click', function (e)
							{
								var isEmbedded = $(this).hasClass('embedded');
								e.preventDefault();
								var handler = getClickHandler('direct', $(this).find('.service-data').html(), isEmbedded);
								handler();
							});
							$('.shortcuts-link.download').off('click').on('click', function (e)
							{
								e.stopPropagation();
								e.preventDefault();
								var handler = getClickHandler('download', $(this).find('.service-data').html(), false);
								handler();
							});
							$('.shortcuts-link.preview').off('click').on('click', function ()
							{
								var handler = getClickHandler('video', $(this).find('.service-data').html(), false);
								handler();
							});
							$('.shortcuts-link.library-file').off('click').on('click', function ()
							{
								var handler = getClickHandler('library-file', $(this).find('.service-data').html(), false);
								handler();
							});
							$('.shortcuts-link.library-page').off('click').on('click', function ()
							{
								var handler = getClickHandler('library-page', $(this).find('.service-data').html(), false);
								handler();
							});
							break;
						case 'carousel':
							FWDU3DCarUtils.checkIfHasTransforms();
							data.displayParameters.startAtCategory = that.selectedCarouselCategory;
							var carousel = new FWDUltimate3DCarousel(data.displayParameters);
							data.displayParameters.predefinedDataList.forEach(function (category)
							{
								category.dataItems.forEach(function (dataItem)
								{
									if (dataItem.mediaType == 'func')
										dataItem.onClick = getClickHandler(dataItem.handlerType, dataItem.dataContent, dataItem.samePage);
								});
							});
							carousel.addListener(FWDUltimate3DCarousel.CATEGORY_CHANGE, function (ev)
							{
								that.updateContentSize();
								that.selectedCarouselCategory = ev.id + 1;
								var logo = data.displayParameters.predefinedDataList[ev.id].logo;
								var shortcutsTab = $('#' + tabId);
								shortcutsTab.find('.ribbon-tab-logo').hide();
								if (logo.length > 0)
								{
									var pageLogo = shortcutsTab.find('.ribbon-shortcut-custom-logo');
									pageLogo.attr('src', logo);
									pageLogo.show();
								}
								else
									shortcutsTab.find('.ribbon-shortcut-tab-logo').show();
							});
							break;
					}
				},
				async: true,
				dataType: 'json'
			});
		};

		var initPageLogo = function (data)
		{
			var content = $('#content');
			var pageContent = content.find('.shortcuts-page-content');
			var tabId = pageContent.attr('id').replace("shortcuts-page-content-", "");
			var shortcutsTab = $('#shortcuts-tab-' + tabId);
			var tabLogos = shortcutsTab.find('.ribbon-tab-logo');
			tabLogos.hide();
			var ribbonLogoPath = data.logo;
			if (ribbonLogoPath.length > 0)
			{
				var pageLogo = shortcutsTab.find('.ribbon-shortcut-custom-logo');
				pageLogo.attr('src', ribbonLogoPath);
				pageLogo.show();
			}
			else
				shortcutsTab.find('.ribbon-shortcut-tab-logo').show();
		};

		var getClickHandler = function (handlerType, dataContent, isEmbedded)
		{
			var dataObject = $('<div><div class="service-data">' + dataContent + '</div></div>');
			switch (handlerType)
			{
				case 'direct':
					if (isEmbedded)
					{
						return function ()
						{
							var content = $('#content');
							var pageContent = content.find('.shortcuts-page-content');
							var tabId = pageContent.attr('id').replace("shortcuts-page-content-", "");
							var shortcutsTab = $('#shortcuts-tab-' + tabId);

							var linkId = dataObject.find('.link-id').html();
							var linkType = dataObject.find('.link-type').html();
							var url = dataObject.find('.url').text();
							var ribbonLogoPath = dataObject.find('.ribbon-logo-path');
							shortcutsTab.find('.ribbon-tab-logo').hide();
							if (ribbonLogoPath.length > 0)
							{
								var linkLogo = shortcutsTab.find('.ribbon-shortcut-custom-logo');
								linkLogo.attr('src', ribbonLogoPath.html());
								linkLogo.show();
							}
							else
								shortcutsTab.find('.ribbon-shortcut-tab-logo').show();
							$.ajax({
								type: "POST",
								url: url,
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show(false);
								},
								complete: function ()
								{
									that.updateContentSize();
									$.SalesPortal.Overlay.hide();
								},
								success: function (msg)
								{
									searchBar.changeVisibility(false);
									var pageContent = content.find('.shortcuts-page-content');
									if (linkType == 'search')
									{
										pageContent.html(msg);
										that.processSearchLink(pageContent, linkId);
									}
									else if (linkType == 'window')
									{
										pageContent.html("<div class='padding'>" + msg + "</div>");
										$.SalesPortal.Wallbin.assignLinkEvents($('#content'));
									}
									else
										pageContent.html("<div class='padding'>" + msg + "</div>");
									content.animate({scrollTop: 0}, 1);
								},
								error: function ()
								{
								},
								async: true,
								dataType: 'html'
							});
						}
					}
					else
						return function ()
						{
							var linkName = dataObject.find('.link-name').html();
							var url = dataObject.find('.url').text();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "statistic/writeActivity",
								data: {
									type: 'Shortcuts',
									subType: 'Open',
									data: $.toJSON({
										File: linkName,
										'Original Format': 'url'
									})
								},
								async: true,
								dataType: 'html'
							});
							window.open(url.replace(/&amp;/g, '%26'), "_blank");
						};
				case 'download':
					return function ()
					{
						var linkId = dataObject.find('.link-id').text();
						var url = dataObject.find('.url').text();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "shortcuts/getDownloadDialog",
							data: {linkId: linkId},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(true);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								var content = $(msg);
								content.find('#accept-button').off('click');
								content.find('#accept-button').on('click', function ()
								{
									$.fancybox.close();
									window.open(url.replace(/&amp;/g, '%26'), "_self");
								});
								$.fancybox({
									content: content,
									title: 'Download',
									width: 300,
									autoSize: false,
									autoHeight: true,
									openEffect: 'none',
									closeEffect: 'none'
								});
							},
							async: true,
							dataType: 'html'
						});
					};
				case 'video':
					return function ()
					{
						$.SalesPortal.LinkManager.viewSelectedFormat(dataObject, false, true);
					};
					break;
				case 'library-file':
					return function ()
					{
						$.SalesPortal.LinkManager.requestViewDialog(dataObject.find('.link-id').html(), false);
					};
					break;
				case 'library-page':
					return function ()
					{
						$.cookie("selectedLibraryName", dataObject.find('.library-name').html(), {
							expires: (60 * 60 * 24 * 7)
						});
						$.cookie("selectedPageName", dataObject.find('.page-name').html(), {
							expires: (60 * 60 * 24 * 7)
						});
						$.cookie("selectedRibbonTabId", 'home-tab', {
							expires: (60 * 60 * 24 * 7)
						});
						window.location.reload();
					};
					break;
			}
			return null;
		};

		this.updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			var content = $('#content');
			var shortcutsPage = content.find('.shortcuts-page-content');
			var height = content.height() - content.find('.shortcuts-search-bar.open').height();
			shortcutsPage.css({
				'height': height + 'px'
			});

			var sideBar = $('#right-navbar');
			sideBar.find('.logo-list').css({
				'height': height + 'px'
			});
		};
	};
	$.SalesPortal.Shortcuts = new ShortcutsManager();
})(jQuery);