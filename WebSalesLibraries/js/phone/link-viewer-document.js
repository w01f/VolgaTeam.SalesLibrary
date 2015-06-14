(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var photoSwipeInstance = undefined;
	$.SalesPortal.DocumentViewer = function (parameters, parentPageData)
	{
		var viewerData = new $.SalesPortal.DocumentViewerData($.parseJSON(parameters.data));
		var sliderCurrentPosition = 0;

		this.show = function ()
		{
			cleanupPreviousInstance();

			var body = $('body');
			body.append($(parameters.content));

			$('.link-viewer-page .page-header .header-title').html(parentPageData.name);

			var baseLinkViewerPage = $('#link-viewer');
			baseLinkViewerPage.find('.main-content .content-header .back a').prop('href', parentPageData.id);

			var popupOpenFile = baseLinkViewerPage.find('#link-viewer-open-menu');
			popupOpenFile.find('.popup-open-action').off('click').on('click', function ()
			{
				popupOpenFile.popup('close');
			});

			var slidesCount = viewerData.pagesInPng != undefined ? viewerData.pagesInPng.length : 0;
			if (slidesCount > 0)
				baseLinkViewerPage.find('.page-footer .link-viewer-info strong').html('Slide 1 of ' + slidesCount);

			baseLinkViewerPage.find('.slider').slick({
				dots: true,
				arrows: false,
				infinite: true,
				speed: 300,
				onAfterChange: function (slider, i)
				{
					baseLinkViewerPage.find('.page-footer .link-viewer-info strong').html('Slide ' + (i + 1) + ' of ' + slidesCount);
					sliderCurrentPosition = i;
				}
			});
			$(window).on("resize", function ()
			{
				baseLinkViewerPage.find('.slider').slickGoTo(sliderCurrentPosition);
			});
			$(window).on("navigate", function ()
			{
				baseLinkViewerPage.find('.slider').slickGoTo(sliderCurrentPosition);
			});
			body.on("pagecontainerchange", function (event)
			{
				baseLinkViewerPage.find('.slider').slickGoTo(sliderCurrentPosition);
			});

			var galleryPage = $('#link-viewer-gallery');
			var options = {
				enableMouseWheel: false,
				enableKeyboard: false,
				captionAndToolbarAutoHideDelay: 0,
				jQueryMobile: true
			};
			photoSwipeInstance = galleryPage.find(".gallery-items").find("a").photoSwipe(options);
			photoSwipeInstance.addEventHandler(window.Code.PhotoSwipe.EventTypes.onToolbarTap, function (e)
			{
				if (e.toolbarAction === 'close')
				{
					baseLinkViewerPage.find('.slider').slickGoTo(sliderCurrentPosition);
				}
			});

			$('#link-viwer-open-full-screen').off('click').on('click', function (e)
			{
				e.preventDefault();
				photoSwipeInstance.show(sliderCurrentPosition);
			});

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			});

			$.mobile.initializePage();
			$.mobile.changePage("#link-viewer", {
				transition: "slidefade"
			});

			updateSliderSize();
			baseLinkViewerPage.find('.slider').slickGoTo(sliderCurrentPosition);
		};

		var cleanupPreviousInstance = function ()
		{
			if (typeof photoSwipeInstance != "undefined" && photoSwipeInstance != null)
			{
				window.Code.PhotoSwipe.unsetActivateInstance(photoSwipeInstance);
				window.Code.PhotoSwipe.detatch(photoSwipeInstance);
			}

			$('body .link-viewer-page').remove();
		};

		var updateSliderSize = function ()
		{
			var windowWidth = $(window).width();
			$('#link-viewer').find('.slider img, .slider .slick-track, .slider .slick-track .slick-slide').css({
				'width': '100%'
			});
		};
	};
})(jQuery);
