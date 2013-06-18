(function ($)
{
	var assignLinkEvents = function (container)
	{
		$.updateContentAreaDimensions();
		container.find('.clickable')
			.off('click')
			.on('click', function (event)
			{
				event.stopPropagation();
				var linkId = $(this).attr('id').replace('link', '');
				recordActivity(linkId);
				$.requestViewDialog(linkId, false);
			});
		container.find('.folder-link')
			.off('click')
			.on('click', function (event)
			{
				loadFolderLinkContent($(this));
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
				var linkId = folderLinkContent.attr("id").replace('folder-link-content', '');
			if (!folderLinkContent.find('.link-container').length && linkId != null)
			{
				recordActivity(linkId);
				$.ajax({
					type: "POST",
					url: "wallbin/getLinkFolderContent",
					data: {
						linkId: linkId
					},
					beforeSend: function ()
					{
						$.showOverlayLight();
						folderLinkContent.html('');
					},
					complete: function ()
					{
						$.hideOverlayLight();
					},
					success: function (msg)
					{
						folderLinkContent.html(msg);
						$.updateContentAreaDimensions();
						$('.link-text, .banner-container').tooltip({animation: false, trigger: 'hover', placement: 'top', delay: { show: 500, hide: 100 }});
						$('.clickable')
							.off('click')
							.on('click', function (event)
							{
								event.stopPropagation();
								var linkId = $(this).attr('id').replace('link', '');
								recordActivity(linkId);
								$.requestViewDialog(linkId, false);
							});
						$('.folder-link')
							.off('click')
							.on('click', function (event)
							{
								loadFolderLinkContent($(this));
								event.stopPropagation();
							});
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
			linkObject.removeClass('active');
		}
	};

	var recordActivity = function(linkId)
	{
		var pageId =$('#page-id').html();
		$.ajax({
			type: "POST",
			url: "qpage/recordActivity",
			data: {
				pageId: pageId,
				linkId: linkId
			},
			async: true,
			dataType: 'html'
		});
	};

	$(document).ready(function ()
	{
		assignLinkEvents($('#page-links-container'));
	});
})
	(jQuery);