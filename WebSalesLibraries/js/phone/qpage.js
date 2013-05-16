(function ($)
{
	var loadLink = function (linkId, parentTitle, isAttachment, backLink)
	{
		$.ajax({
			type: "POST",
			url: isAttachment ? "preview/getAttachmentPreviewList" : "preview/getLinkPreviewList",
			data: {
				linkId: linkId
			},
			beforeSend: function ()
			{
				$('#preview').find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function ()
			{
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				var previewPage = $('#preview');
				previewPage.find('.page-content').html(msg);
				previewPage.find('.header-title').html(parentTitle);
				$('.email-tab .header-title').html(parentTitle);
				$('.favorites-tab .header-title').html(parentTitle);
				previewPage.find('.link.back').attr('href', backLink);
				$.mobile.changePage("#preview", {
					transition: "slidefade"
				});
				previewPage.find('.page-content').children('ul').listview();
				previewPage.find('.res-selector').navbar();

				$('.file-size.regular').hide();
				$('.file-size.phone').show();
				$('.res-selector a').on('click', function ()
				{
					if ($('a.low-res-button').hasClass('ui-btn-active'))
					{
						$('.file-size.regular').hide();
						$('.file-size.phone').show();

					}
					else
					{
						$('.file-size.regular').show();
						$('.file-size.phone').hide();
					}
				});

				$(".preview-link").on('click', function ()
				{
					var itemContent = $(this).find('.item-content');
					var viewFormat = itemContent.find('.view-type').html().toUpperCase();

					var resolution = 'hi';
					if ($('.res-selector .low-res-button').hasClass('ui-btn-active'))
						resolution = 'low';
					else if ($('.res-selector .hi-res-button').hasClass('ui-btn-active'))
						resolution = 'hi';

					if (viewFormat == 'PNG' || viewFormat == 'JPEG')
					{
						var galleryHeader = $('#preview').find('.link-container').first().clone();
						var previewInfo = '';
						if (resolution == 'hi')
							previewInfo += 'High Resolution - ';
						else if (resolution == 'low')
							previewInfo += 'Low Resolution - ';
						previewInfo += viewFormat + ' Images';
						galleryHeader.find('.file').html(previewInfo);

						$('#gallery-title').html('').append(galleryHeader);
					}

					$.viewSelectedFormat(itemContent, resolution);
				});
			},
			async: true,
			dataType: 'html'
		});
	};

	var loadLinkDeatils = function (linkId, parentTitle, backLink)
	{
		$.ajax({
			type: "POST",
			url: "preview/getLinkDetails",
			data: {
				linkId: linkId
			},
			beforeSend: function ()
			{
				$('#preview').find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function ()
			{
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				var linkDetailsPage = $('#link-details');
				linkDetailsPage.find('.page-content').html(msg);
				linkDetailsPage.find('.link.back').attr('href', backLink);
				$.mobile.changePage("#link-details", {
					transition: "slidefade"
				});
				linkDetailsPage.find('.page-content').children('ul').listview();
				$(".file-card-link").on('click', function ()
				{
					var linkId = $.trim($(this).attr("href").replace('#file-card', ''));
					$.viewFileCard(linkId);
				});
				$(".attachment-link").on('click', function ()
				{
					var attachmentId = $.trim($(this).attr("href").replace('#attachment', ''));
					loadLink(attachmentId, parentTitle, true, '#link-details');
				});
			},
			async: true,
			dataType: 'html'
		});
	};

	var loadFolderContent = function (linkId, parentLinkId)
	{
		$.ajax({
			type: "POST",
			url: "wallbin/getLinkFolderContent",
			data: {
				linkId: linkId
			},
			beforeSend: function ()
			{
				$('#link-folder-content-' + linkId).find('.page-content').html('');
				$.mobile.loading('show', {
					textVisible: false,
					html: ""
				});
			},
			complete: function ()
			{
				$.mobile.loading('hide', {
					textVisible: false,
					html: ""
				});
			},
			success: function (msg)
			{
				var mainPage = $('#main');
				var linkFolderContent = $('#link-folder-content-' + linkId);
				if (!linkFolderContent[0])
				{
					var linkFolderContentTemplate = $('#link-folder-content-template');
					linkFolderContent = linkFolderContentTemplate.clone(true)
						.insertAfter(linkFolderContentTemplate)
						.attr('id', 'link-folder-content-' + linkId);
					linkFolderContent.find('.link.back').attr('href', parentLinkId == null ? '#main' : ('#link-folder-content-' + parentLinkId));
				}
				linkFolderContent.find('.page-content').html(msg);
				$.mobile.changePage('#link-folder-content-' + linkId, {
					transition: "slidefade"
				});
				linkFolderContent.find('.page-content').children('ul').listview();
				linkFolderContent.find(".file-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					loadLink(selectedLink, mainPage.find('.header-title').html(), false, ('#link-folder-content-' + linkId));
				});
				linkFolderContent.find(".folder-content-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					loadFolderContent(selectedLink, linkId);
				});
				linkFolderContent.find(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					loadLinkDeatils(selectedLink, mainPage.find('.header-title').html(), ('#link-folder-content-' + linkId));
					event.stopPropagation();
				});
			},
			async: true,
			dataType: 'html'
		});
	};

	$(document).ready(function ()
	{
		var mainPage = $('#main');
		mainPage.find('.page-content').children('ul').listview();
		mainPage.find(".file-link").on('click', function ()
		{
			var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
			loadLink(selectedLink, mainPage.find('.header-title').html(), false, '#main');
		});
		mainPage.find(".folder-content-link").on('click', function ()
		{
			var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
			loadFolderContent(selectedLink, null);
		});
		mainPage.find(".file-link-detail").on('click', function (event)
		{
			var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
			loadLinkDeatils(selectedLink, mainPage.find('.header-title').html(), '#main');
			event.stopPropagation();
		});

		$('#gallery-page').on('pageshow',function (e)
		{
			var options = {
				enableMouseWheel: false,
				enableKeyboard: false,
				captionAndToolbarAutoHideDelay: 0,
				jQueryMobile: true
			};
			$("#gallery").find("a").photoSwipe(options);
			return true;
		}).on('pagehide', function (e)
			{
				var currentPage = $(e.target),
					photoSwipeInstance = window.Code.PhotoSwipe.getInstance(currentPage.attr('id'));
				if (typeof photoSwipeInstance != "undefined" && photoSwipeInstance != null)
				{
					window.Code.PhotoSwipe.detatch(photoSwipeInstance);
				}
				return true;
			});
	});
})(jQuery);