(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.Masonry = function (parameters) {
		var masonryId = parameters.containerId;
		var parentShortcutId = parameters.parentShortcutId;
		var querySettings = parameters.querySettings !== undefined ? new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings) : null;
		var viewSettings = new MasonryViewSettings(parameters.viewSettings);
		var masonryContainer = undefined;
		var updateResponsiveColumnsTimer = null;

		this.init = function () {
			masonryContainer = $('#masonry-container-' + masonryId);

			initMasonry();

			if (viewSettings.feedType !== 'masonry')
			{
				initControls();
				setTimeout(function () {
					reloadLinks(false);
				}, 900000);
			}
		};

		var reloadLinks = function (showProgress) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "landingPage/getMasonryLinkFeedItems",
				data: {
					feedId: masonryId,
					parentShortcutId: parentShortcutId,
					feedType: querySettings.feedType,
					querySettings: querySettings,
					viewSettings: viewSettings
				},
				beforeSend: function () {
					if (showProgress)
						$.SalesPortal.Overlay.show();
				},
				success: function (msg) {
					$.SalesPortal.Overlay.hide();

					masonryContainer.find('#masonry-grid-' + masonryId).html(msg);

					initMasonry();
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var initControls = function () {
			masonryContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function () {
				var dateRangeTitle = $(this).text();
				masonryContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				updateDetailsHoverTip();

				reloadLinks(true);
			});

			masonryContainer.find('.link-format-toggle').off('click').on('click', function () {
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormatsInclude = [];
				$.each(masonryContainer.find('.link-format-toggle.active'), function () {
					var button = $(this);
					querySettings.linkFormatsInclude.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			});

			updateDetailsHoverTip();
			masonryContainer.find('.date-range-toggle-group, .link-format-toggle').last().css("margin-right", "6px");
			masonryContainer.find('.feed-details-button').each(function () {
				var img = $(this).find('.svg');
				if (img.length > 0)
				{
					var imgClass = img.attr('class');
					var imgURL = img.attr('src');

					$.get(imgURL, function (data) {
						var svg = $(data).find('svg');
						if (typeof imgClass !== 'undefined')
							svg = svg.attr('class', imgClass + ' replaced-svg');
						svg = svg.removeAttr('xmlns:a');
						img.replaceWith(svg);
					}, 'xml');
				}
			});

			masonryContainer.find('.feed-details-button').off('click').on('click', function () {
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
						case "all-time":
							linkId = data.find('.all-time-link-id').text();
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
							$.SalesPortal.ShortcutsManager.openShortcutByMenuItemData(shortcutData);
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
						case "all-time":
							url = data.find('.all-time-url').text();
							break;
						default:
							url = data.find('.default-url').text();
							break;
					}
					window.open(url, "_blank");
				}
			});
		};

		var initMasonry = function () {
			var grid = masonryContainer.find('#masonry-grid-' + masonryId);
			try
			{
				var filterContainerId = '#masonry-filter-' + masonryId;

				grid.cubeportfolio({
					filters: filterContainerId,
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

				$(filterContainerId).find('.cbp-filter-item').off('click.masonry-filter').on('click.masonry-filter', function () {
					var filterTag = $(this).data('filter');
					$.cookie("DefaultFilterTags-" + parentShortcutId + "-" + masonryId, filterTag, {
						expires: (60 * 2)
					});
				});

				$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(grid);

				grid.find('.library-link-item.previewable').off('click').on('click', function (e) {
					e.stopPropagation();
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				grid.find('.library-link-item.direct-url').off('click').on('click', function (e) {
					var linkId = $(this).find('.service-data .link-id').text();
					var url = $(this).data('url');
					$.SalesPortal.LogHelper.write({
						type: 'Link',
						subType: 'Open',
						linkId: linkId,
						data: {
							file: url
						}
					});
				});

				if ($.SalesPortal.Content.isMobileDevice())
				{
					grid.find('.library-link-item').hammer().on('hold', function (event) {
						var linkId = $(this).find('.service-data .link-id').text();
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
						event.gesture.stopPropagation();
						event.gesture.preventDefault();
					});
				}
				else
				{
					grid.find('.library-link-item').off('contextmenu').on('contextmenu', function (event) {
						var linkId = $(this).find('.service-data .link-id').text();
						$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
						return false;
					});
				}

				grid.find('.draggable').off('dragstart').on('dragstart', function (e) {
					var urlHeader = $(this).data("url-header");
					var url = $(this).data('url');
					if (url !== '')
						e.originalEvent.dataTransfer.setData(urlHeader, url);
				});
			}
			catch (err)
			{
				grid.cubeportfolio('destroy', function () {
					initMasonry();
				});
			}
		};

		var updateDetailsHoverTip = function () {
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

		this.updateContentSize = function () {
			updateResponsiveColumnsTimer = setTimeout(function () {
				updateResponsiveColumnsTimer = null;
				$.SalesPortal.ScreenManager.processScreenSizeChange(function () {

					var grid = masonryContainer.find('#masonry-grid-' + masonryId);
					var filterContainerId = '#masonry-filter-' + masonryId;

					try
					{
						grid.cubeportfolio('destroy', function () {

							updateResponsiveColumnsTimer = null;
							grid.cubeportfolio({
								filters: filterContainerId,
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
							updateResponsiveColumnsTimer = null;

						});
					}
					catch (err)
					{
						grid.cubeportfolio({
							filters: filterContainerId,
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
						updateResponsiveColumnsTimer = null;
					}
				});
			}, 500);
		};
	};

	var MasonryViewSettings = function (data) {
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