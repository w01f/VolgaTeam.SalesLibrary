(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var loadFolderContent = function (linkId, parentLinkId)
	{
		$.ajax({
			type: "POST",
			url: window.BaseUrl + "wallbin/getLinkFolderContent",
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
					$.SalesPortal.Wallbin.loadLink(selectedLink, mainPage.find('.header-title').html(), false, ('#link-folder-content-' + linkId));
				});
				linkFolderContent.find(".folder-content-link").on('click', function ()
				{
					var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
					loadFolderContent(selectedLink, linkId);
				});
				linkFolderContent.find(".file-link-detail").on('click', function (event)
				{
					var selectedLink = $.trim($(this).parent().attr("href").replace('#link', ''));
					$.SalesPortal.Wallbin.loadLinkDeatils(selectedLink, mainPage.find('.header-title').html(), ('#link-folder-content-' + linkId));
					event.stopPropagation();
				});
			},
			async: true,
			dataType: 'html'
		});
	};

	var recordActivity = function (linkId)
	{
		var pageId = $('#page-id').html();
		$.ajax({
			type: "POST",
			url: window.BaseUrl + "qpage/recordActivity",
			data: {
				pageId: pageId,
				userEmail: $('#user-email').val(),
				linkId: linkId
			},
			async: true,
			dataType: 'html'
		});
	};

	function checkEmail()
	{
		var emailControl = $('#user-email');
		if (emailControl.length > 0)
		{
			var emailValue = emailControl.val();
			var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			if (!regex.test(emailValue))
			{
				var infoDialog = $('#info-dialog');
				infoDialog.find('.dialog-description').text('Enter your email address to view this link...');
				infoDialog.find('.dialog-title').text('Email');
				$.mobile.changePage("#info-dialog");
				return false;
			}
		}
		return true;
	}

	$(document).ready(function ()
	{
		var mainPage = $('#main');
		mainPage.find('.page-content').children('ul').listview();
		mainPage.find(".file-link").on('click', function ()
		{
			if (checkEmail())
			{
				var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
				recordActivity(selectedLink);
				$.SalesPortal.Wallbin.loadLink(selectedLink, mainPage.find('.header-title').html(), false, '#main');
			}
		});
		mainPage.find(".folder-content-link").on('click', function ()
		{
			if (checkEmail())
			{
				var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
				recordActivity(selectedLink);
				loadFolderContent(selectedLink, null);
			}
		});
		mainPage.find(".file-link-detail").on('click', function (event)
		{
			if (checkEmail())
			{
				var selectedLink = $.trim($(this).parent().attr("href").replace('#link', ''));
				recordActivity(selectedLink);
				$.SalesPortal.Wallbin.loadLinkDeatils(selectedLink, mainPage.find('.header-title').html(), '#main');
				event.stopPropagation();
			}
		});

		$('#gallery-page').on('pageshow',function ()
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