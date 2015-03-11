(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		var that = this;
		var searchShortcutManager = undefined;
		this.selectedCarouselCategory = 1;

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

		this.processSearchLink = function (content, objectId, isPageSearch)
		{
			searchShortcutManager = $.SalesPortal.ShortcutsSearchManager(content, objectId, isPageSearch);
		};

		var loadShortcutsPage = function (tabId)
		{
			var pageIdSelector = '#' + tabId + ' .sel.shortcuts-page';
			var pageId = $(pageIdSelector).attr('id');
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
					initHomeBar();
					initSearchBar(pageId);

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

		var initHomeBar = function ()
		{
			var content = $('#content');
			var pageContent = content.find('.shortcuts-page-content');
			var tabId = pageContent.attr('id').replace("shortcuts-page-content-", "");
			var tabPageLogo = $('#shortcuts-tab-' + tabId).find('.ribbon-tab-logo');
			content.find('.shortcuts-home-bar .logo-container img').on('click', function ()
			{
				tabPageLogo.trigger("click");
			});

			content.find('.shortcuts-home-bar .buttons-container img').on('click', function (e)
			{
				var isExpanded = $(this).hasClass('expanded');
				if (isExpanded)
				{
					$(this).removeClass('expanded');
					$(this).attr('src', $(this).attr('src').replace('collapse', 'expand'));
					changeSearchBarVisibility(false);
				}
				else
				{
					$(this).addClass('expanded');
					$(this).attr('src', $(this).attr('src').replace('expand', 'collapse'));
					changeSearchBarVisibility(true);
				}
				that.updateContentSize();
				e.stopPropagation();
			});
		};

		var initSearchBar = function (currentPageId)
		{
			var search = function ()
			{
				if ($('.btn.search-bar-run').hasClass('disabled')) return;
				var textCondition = $('.shortcuts-search-bar .search-bar-text').val();
				if ($('#search-exact-match').is(':checked'))
					textCondition = '"' + textCondition + '"';
				var searchConditions = $('.shortcuts-search-bar .search-conditions');
				var onlyFiles = $('#search-file-names-only').is(':checked') ? 1 : 0;
				var selectedFileTypes = [];
				if ($('#search-file-type-powerpoint').is(':checked'))
					selectedFileTypes.push("ppt");
				if ($('#search-file-type-video').is(':checked'))
				{
					selectedFileTypes.push("video");
					selectedFileTypes.push("mp4");
					selectedFileTypes.push("wmv");
				}
				if ($('#search-file-type-other').is(':checked'))
				{
					selectedFileTypes.push("doc");
					selectedFileTypes.push("xls");
					selectedFileTypes.push("pdf");
					selectedFileTypes.push("url");
					selectedFileTypes.push("url365");
					selectedFileTypes.push("png");
					selectedFileTypes.push("jpeg");
					selectedFileTypes.push("mp3");
				}
				if (searchConditions.find('.same-page').html() === 'true')
				{
					changeSearchBarVisibility(false);
					$('.shortcuts-home-bar .buttons-container').remove();
					searchConditions.remove('.search-text');
					searchConditions.append('<div class="search-text">' + textCondition + '</div>');
					var searchByContent = searchConditions.find('.search-by-content');
					if (searchByContent.length > 0)
						searchByContent.html(onlyFiles != 1 ? 'true' : false);
					else
						searchConditions.append('<div class="search-by-content">' + (onlyFiles != 1) + '</div>');
					searchConditions.remove('.file-type');
					$.each(selectedFileTypes, function (index, value)
					{
						searchConditions.append('<div class="file-type">' + value + '</div>');
					});
					var content = $('#content').find('.shortcuts-page-content');
					content.html('<div class="search-conditions" style="display: none;">' + searchConditions.html() + '</div>');
					that.processSearchLink(content, currentPageId, true);
				}
				else
				{
					var superFilters = [];
					$.each(searchConditions.find('.super-filter'), function ()
					{
						superFilters.push($(this).html());
					});

					var categories = [];
					$.each(searchConditions.find('.category'), function ()
					{
						var substr = $(this).html().split('------');
						var category = {
							category: substr[0],
							tag: substr[1]
						};
						categories.push(category);
					});
					window.open("shortcuts/GetQuickSearchResult?pageId=" + currentPageId +
						"&text=" + textCondition +
						"&onlyFiles=" + onlyFiles +
						"&fileTypes=" + $.toJSON(selectedFileTypes) +
						"&superFilters=" + $.toJSON(superFilters) +
						"&categories=" + $.toJSON(categories));
				}
			};

			$('.shortcuts-search-bar .search-bar-text').keypress(function (e)
			{
				updateSearchButtonState();
				if (e.which == 13)
					search();
			});
			$('.shortcuts-search-bar .search-bar-run').on('click', search);

			$('.file-filter-panel .file-selector input').off('change').on('change', function ()
			{
				updateSearchButtonState();
			});

			$('.tags-filter-panel-switcher').off('click').on('click', function ()
			{
				setTagsCondition();
			});

			updateSearchButtonState();
			updateSelectedCategories();
		};

		var changeSearchBarVisibility = function (show)
		{
			var searchBar = $('.shortcuts-search-bar');
			if (show)
				searchBar.addClass('open').show();
			else
				searchBar.removeClass('open').hide();
		};

		var updateSearchButtonState = function ()
		{
			var hasKeyword = $('.shortcuts-search-bar .search-bar-text').val() != "";
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var hasSuperFilters = searchConditions.find('.super-filter').length > 0;
			var hasCategories = searchConditions.find('.category').length > 0;
			var searchButton = $('.btn.search-bar-run');
			searchButton.removeClass('disabled');
			if (!(hasKeyword || hasSuperFilters || hasCategories) || !($('#search-file-type-powerpoint').is(':checked') || $('#search-file-type-video').is(':checked') || $('#search-file-type-other').is(':checked')))
				searchButton.addClass('disabled');
		};

		var updateSelectedCategories = function ()
		{
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var existedSuperFilters = searchConditions.find('.super-filter').map(function ()
			{
				return $(this).html();
			}).get();
			var existedCategories = searchConditions.find('.category').map(function ()
			{
				return $(this).html().split('------')[1];
			}).get();
			var categoryStr = $.merge(existedSuperFilters, existedCategories).join(', ');
			var selectedCategoryLabel = $('.tag-condition-selected');
			if (categoryStr != "")
				selectedCategoryLabel.html($('.tags-filter-panel-switcher').html() + ': ' + categoryStr);
			else
				selectedCategoryLabel.html('');
		};

		var setTagsCondition = function ()
		{
			var searchConditions = $('.shortcuts-search-bar .search-conditions');
			var categorySelector = searchConditions.find('.tag-condition-selector');
			$.fancybox({
				content: categorySelector.html(),
				title: $('.tags-filter-panel-switcher').html(),
				width: 550,
				autoSize: false,
				autoHeight: true,
				openEffect: 'none',
				closeEffect: 'none',
				afterShow: function ()
				{
					var innerContent = $('.fancybox-inner');
					var categories = innerContent.find(".tag-list");
					var superFilters = innerContent.find(".super-filter-list");
					categories.accordion({
						heightStyle: "content",
						active: false,
						collapsible: true,
						icons: {
							header: "ui-icon-circle-arrow-e",
							activeHeader: "ui-icon-circle-arrow-s"
						}
					});

					$.each(searchConditions.find('.super-filter'), function ()
					{
						var value = $(this).html();
						superFilters.find('.btn:contains("' + value + '")').button('toggle');
					});

					$.each(searchConditions.find('.category'), function ()
					{
						var value = $(this).html();
						categories.find('.item-selector[value="' + value + '"]').prop('checked', true);
					});

					categories.find('.group-selector').off('change').on('change', function ()
					{
						var categoryGroup = $(this).parent().parent();
						categoryGroup.find('.item-selector').prop('checked', $(this).is(':checked'));
					});

					innerContent.find('.tags-clear-all').off('click').on('click', function ()
					{
						superFilters.find('.btn').removeClass('active').blur();
						categories.find(":checked").prop('checked', false);
					});

					superFilters.find('.btn').off('click').on('click', function ()
					{
						$(this).button('toggle').blur();
					});

					innerContent.find('.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});

					innerContent.find('.accept-button').on('click', function ()
					{
						searchConditions.find('.super-filter').remove();
						$.each(superFilters.find('.btn.active'), function ()
						{
							searchConditions.append($('<div class="super-filter">' + $(this).html() + '</div>'));
						});

						searchConditions.find('.category').remove();
						$.each(categories.find(".item-selector:checked"), function ()
						{
							searchConditions.append($('<div class="category">' + $(this).val() + '</div>'));
						});

						$.fancybox.close();

						updateSearchButtonState();

						updateSelectedCategories();
					});
				},
				afterClose: function ()
				{
				},
				onUpdate: function ()
				{
				}
			});
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
									changeSearchBarVisibility(false);
									$('.shortcuts-home-bar .buttons-container').remove();
									var pageContent = content.find('.shortcuts-page-content');
									if (linkType == 'search')
									{
										pageContent.html(msg);
										that.processSearchLink(pageContent, linkId, false);
									}
									else if (linkType == 'window')
									{
										pageContent.html("<div class='padding'>" + msg + "</div>");
										$.SalesPortal.Wallbin.assignLinkEvents($('#content'));
									}
									else
										pageContent.html("<div class='padding'>" + msg + "</div>");
									content.animate({scrollTop: 0}, 1);

									var shortcutTitle = pageContent.find('.shortcut-title').html();
									$('.shortcuts-home-bar .title').html(shortcutTitle);
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
			var height = content.height() - content.find('.shortcuts-home-bar').height() - content.find('.shortcuts-search-bar.open').height();
			shortcutsPage.css({
				'height': height + 'px'
			});

			var searchResult = $('#search-result');
			searchResult.find('> div').css({
				'height': height + 'px'
			});
			var gridHeader = searchResult.find('.links-grid-header');
			var searchResultBar = searchResult.find('.search-grid-info');
			searchResult.find('.links-grid-body-container').css({
				'height': (height - (searchResultBar.length > 0 ? (searchResultBar.height() + 12) : 0) - gridHeader.height()) + 'px'
			});

			var linkDateWidth = 100;

			var linkNameHeaderWidth = searchResult.width() -
				gridHeader.find('td.library-column').width() -
				gridHeader.find('td.link-type-column').width() -
				gridHeader.find('td.link-tag-column').width() -
				gridHeader.find('td.link-rate-column').width() -
				linkDateWidth;
			gridHeader.find('td.link-name-column').css({
				'width': linkNameHeaderWidth + 'px'
			});

			var gridBody = searchResult.find('.links-grid-body');
			var linkNameBodyWidth = searchResult.width() -
				gridBody.find('td.library-column').width() -
				gridBody.find('td.link-type-column').width() -
				gridBody.find('td.link-tag-column').width() -
				gridBody.find('td.link-rate-column').width() -
				linkDateWidth;
			gridBody.find('td.link-name-column').css({
				'width': linkNameBodyWidth + 'px'
			});

			var sideBar = $('#right-navbar');
			sideBar.find('.logo-list').css({
				'height': height + 'px'
			});
		};
	};
	$.SalesPortal.Shortcuts = new ShortcutsManager();
})(jQuery);