(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	var assignLinkEvents = function (container)
	{
		$.SalesPortal.Layout.updateContentSize();
		container.find('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', delay: { show: 500, hide: 100 }});
		container.find('.clickable').off('click').on('click', function (event)
		{
			if (checkEmail())
			{
				var linkId = $(this).attr('id').replace('link', '');
				recordActivity(linkId);
				$.SalesPortal.LinkManager.requestViewDialog(linkId, false);
			}
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
						if (checkEmail())
						{
							var linkId = $(this).attr('id').replace('link', '');
							recordActivity(linkId);
							$.requestSpecialDialog([linkId], undefined);
							$(this).off('mouseup.context');
						}
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
				if (checkEmail())
				{
					var linkId = $(this).attr('id').replace('link', '');
					recordActivity(linkId);
					$.requestSpecialDialog([linkId], undefined);
				}
				event.gesture.stopPropagation();
				event.gesture.preventDefault();
				event.stopPropagation();
				event.preventDefault();
			});
		}
		container.find('.folder-link').off('click').on('click', function (event)
		{
			if (checkEmail())
			{
				loadFolderLinkContent($(this));
			}
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
						if (checkEmail())
						{
							var linkId = $(this).attr('id').replace('link', '');
							recordActivity(linkId);
							$.requestSpecialDialog([linkId], undefined);
							$(this).off('mouseup.context');
						}
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
				if (checkEmail())
				{
					var linkId = $(this).attr('id').replace('link', '');
					recordActivity(linkId);
					$.requestSpecialDialog([linkId], undefined);
				}
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
				linkId = folderLinkContent.attr("id").replace('folder-link-content', '');
			if (!folderLinkContent.find('.link-container').length && linkId != null)
			{
				recordActivity(linkId);
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
			linkObject.removeClass('active').blur();
		}
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
				$('body').append('<div id="user-email-warning" title="Email">Enter your email address to view this link...<br></div>');
				$("#user-email-warning").dialog({
					resizable: false,
					modal: true,
					buttons: {
						"OK": function ()
						{
							$(this).dialog("close");
						}
					},
					open: function ()
					{
						$(this).closest(".ui-dialog")
							.find(".ui-dialog-titlebar-close")
							.html("<span class='ui-icon ui-icon-closethick'></span>");
					},
					close: function ()
					{
						$("#user-email-warning").remove();
					}
				});
				return false;
			}
		}
		return true;
	}

	$(document).ready(function ()
	{
		assignLinkEvents($('#page-links-container'));
	});
})(jQuery);