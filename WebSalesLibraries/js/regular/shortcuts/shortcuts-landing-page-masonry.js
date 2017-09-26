(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.Masonry = function (parameters)
	{
		var masonryId = parameters.containerId;
		var querySettings = parameters.querySettings !== undefined ? new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings) : null;
		var viewSettings = new MasonryViewSettings(parameters.viewSettings);
		var masonryContainer = undefined;

		this.init = function ()
		{
			masonryContainer = $('#masonry-container-' + masonryId);

			initMasonry();

			if (viewSettings.feedType !== 'masonry')
			{
				initControls();
				setTimeout(function ()
				{
					reloadLinks(false);
				}, 900000);
			}
		};

		var reloadLinks = function (showProgress)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "landingPage/getMasonryLinkFeedItems",
				data: {
					feedId: masonryId,
					feedType: querySettings.feedType,
					querySettings: querySettings,
					viewSettings: viewSettings
				},
				beforeSend: function ()
				{
					if (showProgress)
						$.SalesPortal.Overlay.show();
				},
				success: function (msg)
				{
					$.SalesPortal.Overlay.hide();

					masonryContainer.find('#masonry-grid-' + masonryId).html(msg);

					initMasonry();
				},
				error: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var initControls = function ()
		{
			masonryContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function ()
			{
				var dateRangeTitle = $(this).text();
				masonryContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				updateDetailsHoverTip();

				reloadLinks(true);
			});

			masonryContainer.find('.link-format-toggle').off('click').on('click', function ()
			{
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormats = [];
				$.each(masonryContainer.find('.link-format-toggle.active'), function ()
				{
					var button = $(this);
					querySettings.linkFormats.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			});

			updateDetailsHoverTip();
			masonryContainer.find('.date-range-toggle-group, .link-format-toggle').last().css("margin-right", "6px");
			masonryContainer.find('.feed-details-button').each(function ()
			{
				var img = $(this).find('.svg');
				if (img.length > 0)
				{
					var imgClass = img.attr('class');
					var imgURL = img.attr('src');

					$.get(imgURL, function (data)
					{
						var svg = $(data).find('svg');
						if (typeof imgClass !== 'undefined')
							svg = svg.attr('class', imgClass + ' replaced-svg');
						svg = svg.removeAttr('xmlns:a');
						img.replaceWith(svg);
					}, 'xml');
				}
			});

			masonryContainer.find('.feed-details-button').off('click').on('click', function ()
			{
				$(this).blur();

				var data = $(this).find('.service-data');
				var samePage = data.find('.same-page').length > 0;
				if (samePage === true)
				{
					var linkId = '';
					switch (querySettings.dateRangeType)
					{
						case "today":
							linkId = data.find('.today-link-id').text();
							break;
						case "week":
							linkId = data.find('.week-link-id').text();
							break;
						case "month":
							linkId = data.find('.month-link-id').text();
							break;
						default:
							linkId = data.find('.default-link-id').text();
							break;
					}
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "shortcuts/getShortcutDataById",
						data: {
							linkId: linkId
						},
						beforeSend: function () {
							$.SalesPortal.Overlay.show();
						},
						complete: function () {
							$.SalesPortal.Overlay.hide();
						},
						success: function (msg) {
							var shortcutData = $('<div>' + msg + '</div>');
							var url = shortcutData.find('.url').text();
							var parameters = {
								pushHistory: true
							};
							$.ajax({
								type: "POST",
								url: url,
								data: {
									linkId: linkId,
									parameters: parameters
								},
								beforeSend: function () {
									$.SalesPortal.Overlay.show();
								},
								complete: function () {
									$.SalesPortal.Overlay.hide();
								},
								success: function (result) {
									$.SalesPortal.ShortcutsSearchLink(result).runSearch(function (data)
									{
										if (data.dataset.length === 0)
										{
											var modalDialog = new $.SalesPortal.ModalDialog({
												title: 'Site Update',
												description: 'This section is not yet updated today.<br><br>' +
												'Check back later and maybe this page will be ready…',
												width: 300,
												buttons: [
													{
														tag: 'ok',
														title: 'OK',
														clickHandler: function ()
														{
															modalDialog.close();
														}
													}
												]
											});
											modalDialog.show();
										}
										else
											$.SalesPortal.HistoryManager.pushShortcut(shortcutData, parameters);
									});
								},
								error: function () {
								},
								async: true,
								dataType: 'json'
							});
						},
						error: function () {
							$.SalesPortal.Overlay.hide();
						},
						async: true,
						dataType: 'html'
					});
				}
				else
				{
					var url = '#';
					switch (querySettings.dateRangeType)
					{
						case "today":
							url = data.find('.today-url').text();
							break;
						case "week":
							url = data.find('.week-url').text();
							break;
						case "month":
							url = data.find('.month-url').text();
							break;
						default:
							url = data.find('.default-url').text();
							break;
					}
					window.open(url, "_blank");
				}
			});
		};

		var initMasonry = function ()
		{
			var grid = masonryContainer.find('#masonry-grid-' + masonryId);
			try
			{
				grid.cubeportfolio({
					filters: '#masonry-filter-' + masonryId,
					layoutMode: 'grid',
					defaultFilter: viewSettings.defaultFilter ? ('.' + viewSettings.defaultFilter.tags.join(', .')) : '*',
					animationType: 'quicksand',
					gapHorizontal: parseInt(viewSettings.itemsPadding.left) + parseInt(viewSettings.itemsPadding.right),
					gapVertical: parseInt(viewSettings.itemsPadding.top) + parseInt(viewSettings.itemsPadding.bottom),
					gridAdjustment: 'responsive',
					caption: viewSettings.enableCaptionZoom === true ? 'zoom' : '',
					displayType: 'fadeIn',
					displayTypeSpeed: 100
				});
				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(grid);
				grid.find('.library-link-item.previewable').off('click').on('click', function (e)
				{
					e.stopPropagation();
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});
				grid.find('.draggable').off('dragstart').on('dragstart', function (e)
				{
					var urlHeader = $(this).data("url-header");
					var url = $(this).data('url');
					if (url !== '')
						e.originalEvent.dataTransfer.setData(urlHeader, url);
				});
			}
			catch (err)
			{
				grid.cubeportfolio('destroy', function ()
				{
					initMasonry();
				});
			}
		};

		var updateDetailsHoverTip = function ()
		{
			var hoverTipButton = masonryContainer.find('.feed-details-button');
			var hoverTipTemplateObject = hoverTipButton.find('.service-data .hover-tip-template');
			if (hoverTipTemplateObject.length > 0)
			{
				var hoverTipTemplate = hoverTipTemplateObject.text();
				if (masonryContainer.find('.date-range-toggle').length > 0)
					hoverTipTemplate += (' by ' + querySettings.dateRangeType);
				hoverTipButton.prop('title', hoverTipTemplate);
			}
		};
	};

	var MasonryViewSettings = function (data)
	{
		this.itemsPadding = undefined;
		this.enableCaptionZoom = undefined;
		this.captionZoomScale = undefined;

		this.imageWidth = undefined;
		this.imageHeight = undefined;
		this.controlsStyle = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;

		this.filters = undefined;
		this.defaultFilter = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);