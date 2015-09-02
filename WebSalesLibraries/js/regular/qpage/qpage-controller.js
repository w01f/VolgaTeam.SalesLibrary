(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var QPageManager = function ()
	{
		this.init = function ()
		{
			$.SalesPortal.Content.init();
			assignLinkEvents($('#page-links-container'));
			initActionButtons();
			updateSize();
			$(window).on('resize', updateSize);
		};

		var updateSize = function ()
		{
			$.SalesPortal.Content.updateSize();
		};

		var assignLinkEvents = function (container)
		{
			$.mtReInit();

			container.find('.clickable').off('click').on('click', function (event)
			{
				if (checkEmail())
				{
					var linkId = $(this).attr('id').replace('link', '');
					recordActivity(linkId);
					$.SalesPortal.LinkManager.requestViewDialog(linkId, true);
				}
				event.stopPropagation();
			});
			container.find('.folder-link').off('click').on('click', function (event)
			{
				if (checkEmail())
				{
					loadFolderLinkContent($(this));
				}
				event.stopPropagation();
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

							$.mtReInit();

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
				url: window.BaseUrl + "recordActivity",
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

		var initActionButtons = function ()
		{
			$('#login-button').off('click').on('click', function ()
			{
				window.location = "getProtected?id=" + $('#page-id').html();
			});
		}
	};
	$.SalesPortal.QPage = new QPageManager();
	$(document).ready(function ()
	{
		$.SalesPortal.QPage.init();
	});
})(jQuery);