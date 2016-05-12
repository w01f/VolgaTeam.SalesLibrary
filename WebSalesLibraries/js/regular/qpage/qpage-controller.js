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

			container.find('.line-break').off('click.open').on('click.open', function (event)
			{
				event.stopPropagation();
				event.preventDefault();
			});

			container.find('.clickable').off('click.open').on('click.open', function (event)
			{
				if (checkEmail())
				{
					var linkId = $(this).attr('id').replace('link', '');
					recordActivity(linkId);
					$.SalesPortal.LinkManager.requestViewDialog(linkId, true);
				}
				event.stopPropagation();
				event.preventDefault();
			});

			container.find('.clickable, .url').off('dragstart').on('dragstart', function (event)
			{
				var header = $(this).find('.service-data .download-header').text();
				var url = $(this).find('.service-data .download-link').text();
				if (url != '')
					event.originalEvent.dataTransfer.setData(header, url.replace('site_base_url_placeholder', window.BaseUrl).replace(/\/\/+/g, '/'));
			});

			container.find('.log-activity').off('click.log').on('click.log', function ()
			{
				var data = $(this).children('.service-data');
				var activityData = $.parseJSON(data.find('.activity-data').text());

				$.SalesPortal.LogHelper.write({
					type: 'Link',
					subType: 'Open',
					data: {
						Name: activityData.title,
						File: activityData.fileName,
						'Original Format': activityData.format
					}
				});
			});
			container.find('.folder-link').off('click.open').on('click.open', function (event)
			{
				if (checkEmail())
					loadFolderLinkContent($(this));
				event.preventDefault();
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
					var modalDialog = new $.SalesPortal.ModalDialog({
						title: 'Email',
						description: 'Enter your email address to view this link...'
					});
					modalDialog.show();
					return false;
				}
			}
			return true;
		}

		var initActionButtons = function ()
		{
			$('#login-button').off('click.action').on('click.action', function ()
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