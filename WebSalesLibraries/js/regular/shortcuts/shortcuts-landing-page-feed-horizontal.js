(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.LandingPage = $.SalesPortal.LandingPage || {};
	$.SalesPortal.LandingPage.HorizontalFeed = function (parameters) {
		var containerId = parameters.containerId;
		var querySettings = new $.SalesPortal.LandingPage.LinkFeedQuerySettings(parameters.querySettings);
		var viewSettings = new FeedViewSettings(parameters.viewSettings);
		var feedContainer = undefined;

		this.init = function () {
			feedContainer = $('#horizontal-feed-' + containerId);

			initControls();
			initCarouselControls();
			initSlider();

			if (viewSettings.feedType !== 'shortcut-slider')
			{
				setTimeout(function () {
					reloadLinks(false);
				}, 900000);
			}
		};

		var reloadLinks = function (showProgress) {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "landingPage/getHorizontalLinkFeedItems",
				data: {
					feedId: containerId,
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

					feedContainer.find('.carousel-container').html(msg);

					initSlider();
				},
				error: function () {
					$.SalesPortal.Overlay.hide();
				},
				async: true,
				dataType: 'html'
			});
		};

		var initControls = function () {
			feedContainer.find('.date-range-toggle a').off('click.link-feed').on('click.link-feed', function () {
				var dateRangeTitle = $(this).text();
				feedContainer.find('.date-range-toggle-group>button .title').text(dateRangeTitle);
				querySettings.dateRangeType = $(this).closest('.date-range-toggle').find('>.service-data .date-range-tag').text();

				updateDetailsHoverTip();

				reloadLinks(true);
			});

			feedContainer.find('.link-format-toggle').off('click').on('click', function () {
				$(this).blur();
				if ($(this).hasClass('active'))
					$(this).removeClass('active');
				else
					$(this).addClass('active');

				querySettings.linkFormatsInclude = [];
				$.each(feedContainer.find('.link-format-toggle.active'), function () {
					var button = $(this);
					querySettings.linkFormatsInclude.push(button.find('.service-data .link-format-tag').text());
				});

				reloadLinks(true);
			});

			updateDetailsHoverTip();
			feedContainer.find('.date-range-toggle-group, .link-format-toggle').last().css("margin-right", "6px");
			feedContainer.find('.feed-details-button').each(function () {
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

			feedContainer.find('.feed-details-button').off('click').on('click', function () {
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

		var initCarouselControls = function () {
			var leftButton = feedContainer.find('.portfolio_utube_carousel_control_left');
			var rightButton = feedContainer.find('.portfolio_utube_carousel_control_right');

			leftButton.prop('href', '#');
			leftButton.on('click', function () {
				var titleFind = "Trending(" + $('.feed-controls-container .title').text() + ")";
				ga('send', {
					hitType: 'pageview',
					title: titleFind,
					location: window.BaseUrl,
					page: "Landing Page/carousel/leftHorizontal"
				});
				$(this).blur();

				feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.manualAnimationSpeed + 's');
				feedContainer.find('.carousel').carousel('prev');
				setTimeout(function () {
					feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.autoAnimationSpeed + 's');
				}, 1500);
			});

			rightButton.prop('href', '#');
			rightButton.off('click').on('click', function () {
				var titleFind = "Trending(" + $('.feed-controls-container .title').text() + ")";
				ga('send', {
					hitType: 'pageview',
					title: titleFind,
					location: window.BaseUrl,
					page: "Landing Page/carousel/rightHorizontal"
				});
				$(this).blur();

				feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.manualAnimationSpeed + 's');
				feedContainer.find('.carousel').carousel('next');
				setTimeout(function () {
					feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.autoAnimationSpeed + 's');
				}, 1500);
			});
		};

		var initSlider = function () {
			feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.autoAnimationSpeed + 's');

			feedContainer.find('.carousel-slide-show').carousel();

			feedContainer.find('.carousel .carousel-inner').swipe({
				swipeLeft: function () {
					$(this).parent().carousel('next');
				},
				swipeRight: function () {
					$(this).parent().carousel('prev');
				},
				threshold: 0
			});

			var oneMoveItems = feedContainer.find('.carousel.one-link-move .item');
			oneMoveItems.each(function () {
				var itemToClone = $(this);
				for (var i = 1; i < (oneMoveItems.length > viewSettings.linksPerSlide ? viewSettings.linksPerSlide : oneMoveItems.length); i++)
				{
					itemToClone = itemToClone.next();
					if (!itemToClone.length)
					{
						itemToClone = $(this).siblings(':first');
					}
					itemToClone.children(':first-child').clone()
						.addClass("cloneditem-" + (i))
						.appendTo($(this));
				}
			});


			if ($.SalesPortal.Content.isMobileDevice())
			{
				feedContainer.find('.carousel-links .item .previewable').hammer().on('tap', function (e) {
					e.stopPropagation();
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				feedContainer.find('.carousel-links .item .direct-url').hammer().on('tap', function (e) {
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

				feedContainer.find('.carousel-links .item .library-link-item').hammer().on('hold', function (event) {
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.gesture.center.pageX, event.gesture.center.pageY);
					event.gesture.stopPropagation();
					event.gesture.preventDefault();
				});
			}
			else
			{
				feedContainer.find('.carousel-links .item .previewable').off('click').on('click', function (e) {
					e.stopPropagation();
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestViewDialog({
						linkId: linkId,
						isQuickSite: false
					});
				});

				feedContainer.find('.carousel-links .item .direct-url').off('click').on('click', function (e) {
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

				feedContainer.find('.carousel-links .item .library-link-item').off('contextmenu').on('contextmenu', function (event) {
					var linkId = $(this).find('.service-data .link-id').text();
					$.SalesPortal.LinkManager.requestLinkContextMenu(linkId, false, false, event.clientX, event.clientY);
					return false;
				});
			}

			feedContainer.find('.carousel-links .item .draggable').off('dragstart').on('dragstart', function (e) {
				var urlHeader = $(this).data("url-header");
				var url = $(this).data('url');
				if (url !== '')
					e.originalEvent.dataTransfer.setData(urlHeader, url);
			});

			feedContainer.find('.carousel').hover(function () {
				$(this).carousel('pause')
			}, function () {
				$(this).carousel('cycle')
			});

			if (viewSettings.enableMouseWheel)
			{
				feedContainer.on('mousewheel DOMMouseScroll', function (e) {
					e.stopPropagation();
					e.preventDefault();

					if (e.originalEvent.wheelDelta / 120 > 0)
					{
						feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.scrollAnimationSpeed + 's');
						feedContainer.find('.carousel').carousel('next');
						setTimeout(function () {
							feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.autoAnimationSpeed + 's');
						}, 1500);
					}
					else
					{
						feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.scrollAnimationSpeed + 's');
						feedContainer.find('.carousel').carousel('prev');
						setTimeout(function () {
							feedContainer.find('.carousel-inner .item').css('transition-duration', viewSettings.autoAnimationSpeed + 's');
						}, 1500);
					}
				});
			}

			$.SalesPortal.ShortcutsManager.assignShortcutItemHandlers(feedContainer);
		};

		var updateDetailsHoverTip = function () {
			var hoverTipButton = feedContainer.find('.feed-details-button');
			var hoverTipTemplateObject = hoverTipButton.find('.service-data .hover-tip-template');
			if (hoverTipTemplateObject.length > 0)
			{
				var hoverTipTemplate = hoverTipTemplateObject.text();
				if (feedContainer.find('.date-range-toggle').length > 0)
					hoverTipTemplate += (' by ' + querySettings.dateRangeType);
				hoverTipButton.prop('title', hoverTipTemplate);
			}
		};
	};

	var FeedViewSettings = function (data) {
		this.feedType = undefined;
		this.linksPerSlide = undefined;
		this.linksScrollMode = undefined;
		this.slideShow = undefined;
		this.slideShowInterval = undefined;
		this.maxThumbnailHeight = undefined;
		this.enableMouseWheel = undefined;
		this.dataItemSettings = undefined;
		this.controlSettings = undefined;
		this.controlsStyle = undefined;
		this.autoAnimationSpeed = undefined;
		this.manualAnimationSpeed = undefined;
		this.scrollAnimationSpeed = undefined;

		for (var property in data)
			if (data.hasOwnProperty(property))
				this[property] = data[property];
	};
})(jQuery);