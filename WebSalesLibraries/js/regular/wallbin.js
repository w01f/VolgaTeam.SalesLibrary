(function ($)
{
	var storedTextSize = $.cookie("textSize");
	if (storedTextSize == null)
		storedTextSize = 12;

	var storedTextSpace = $.cookie("textSpace");
	if (storedTextSpace == null)
		storedTextSpace = 2;

	var libraryChanged = function ()
	{
		var selectedLibraryName = $("#select-library").find(":selected").text();
		$.cookie("selectedLibraryName", selectedLibraryName, {
			expires:60 * 60 * 24 * 7
		});
		$.ajax({
			type:"POST",
			url:"statistic/writeActivity",
			data:{
				type:'Wallbin',
				subType:'Library Changed',
				data:$.toJSON({
					Library:selectedLibraryName
				})
			},
			async:true,
			dataType:'html'
		});
		$.ajax({
			type:"POST",
			url:"wallbin/getPageDropDownList",
			beforeSend:function ()
			{
				$('#libraries-selector-container').css({
					'visibility':'hidden'
				});
				$.showOverlay();
			},
			complete:function ()
			{
				$.hideOverlay();
				$('#libraries-selector-container').css({
					'visibility':'visible'
				});
			},
			success:function (msg)
			{
				$('#select-page').html(msg);
				pageChanged();
				$("#page-logo").attr('src', $("#select-library").val());
			},
			async:true,
			dataType:'html'
		});
	};

	var pageChanged = function ()
	{
		var selectedPageName = $("#select-page").find(":selected").text();
		$.cookie("selectedPageName", selectedPageName, {
			expires:60 * 60 * 24 * 7
		});
		$("#page-logo").attr('src', $("#select-page").val());
		$.ajax({
			type:"POST",
			url:"statistic/writeActivity",
			data:{
				type:'Wallbin',
				subType:'Page Changed',
				data:$.toJSON({
					Page:selectedPageName
				})
			},
			async:true,
			dataType:'html'
		});
		updateView();
	};

	var loadColumns = function ()
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getColumnsView",
			data:{},
			beforeSend:function ()
			{
				$.showOverlay();
				$('#content').html('');
			},
			complete:function ()
			{
				$.hideOverlay();
			},
			success:function (msg)
			{
				$('#content').html('<div>' + msg + '</div>');
				$.updateTextSize(storedTextSize);
				$.updateTextSpace(storedTextSpace);
				$.updateContentAreaDimensions();
				$('.link-text, .banner-container').tooltip({animation:false, trigger:'hover', placement:'top', delay:{ show:500, hide:100 }})
				$('.clickable')
					.off('click')
					.on('click', $.openViewDialogEmbedded);
				$('.folder-link')
					.off('click')
					.on('click', function (event)
					{
						loadFolderLinkContent($(this));
						event.stopPropagation();
					});
			},
			error:function ()
			{
				$('#content').html('');
			},
			async:true,
			dataType:'html'
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
				$.ajax({
					type:"POST",
					url:"wallbin/getLinkFolderContent",
					data:{
						linkId:linkId
					},
					beforeSend:function ()
					{
						$.showOverlayLight();
						folderLinkContent.html('');
					},
					complete:function ()
					{
						$.hideOverlayLight();
					},
					success:function (msg)
					{
						folderLinkContent.html(msg);
						$.updateTextSize(storedTextSize);
						$.updateTextSpace(storedTextSpace);
						$.updateContentAreaDimensions();
						$('.link-text, .banner-container').tooltip({animation:false, trigger:'hover', placement:'top', delay:{ show:500, hide:100 }})
						$('.clickable')
							.off('click')
							.on('click', $.openViewDialogEmbedded);
						$('.folder-link')
							.off('click')
							.on('click', function (event)
							{
								loadFolderLinkContent($(this));
								event.stopPropagation();
							});
						folderLinkContent.show("blind", {
							direction:"vertical"
						}, 500);

					},
					error:function ()
					{
						folderLinkContent.html('');
					},
					async:true,
					dataType:'html'
				});
			}
			else
				folderLinkContent.show("blind", {
					direction:"vertical"
				}, 500);

		}
		else
		{
			linkObject.children('.folder-link-content').hide("blind", {
				direction:"vertical"
			}, 500);
			linkObject.removeClass('active');
		}
	};

	var loadAccordion = function ()
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getAccordionView",
			data:{},
			beforeSend:function ()
			{
				$.showOverlay();
				$('#content').html('');
			},
			complete:function ()
			{
				$.hideOverlay();
			},
			success:function (msg)
			{
				$('#content').html('<div>' + msg + '</div>');
				$.updateContentAreaDimensions();
				$('.folder-header')
					.off('click')
					.on('click', function ()
					{
						$('.folder-header.active').parent().find('.folder-links').hide("blind", {
							direction:"vertical"
						}, 500);
						var justCollapse = $(this).hasClass('active');
						$('.folder-header').removeClass('active');
						if (!justCollapse)
						{
							$(this).addClass('active');
							var folderContainer = $(this).parent();
							var folderId = $.trim($(this).attr("id").replace('folder-', ''));
							showAccordionFolder(folderContainer, folderId);
						}
					});
			},
			error:function ()
			{
				$('#content').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	var showAccordionFolder = function (folderContainer, folderId)
	{
		var folderLinks = folderContainer.find('.folder-links');
		if (folderLinks.html() == '')
		{
			$.ajax({
				type:"POST",
				url:"wallbin/getFolderLinksList",
				data:{
					folderId:folderId
				},
				beforeSend:function ()
				{
					$.showOverlayLight();
					folderLinks.html('');
				},
				complete:function ()
				{
					$.hideOverlayLight();
				},
				success:function (msg)
				{
					folderLinks.html(msg);
					$.updateTextSize(storedTextSize);
					$.updateTextSpace(storedTextSpace);
					$.updateContentAreaDimensions();
					$('.link-text, .banner-container').tooltip({animation:false, trigger:'hover', placement:'top', delay:{ show:500, hide:100 }})
					$('.clickable')
						.off('click')
						.on('click', $.openViewDialogEmbedded);
					$('.folder-link')
						.off('click')
						.on('click', function (event)
						{
							loadFolderLinkContent($(this));
							event.stopPropagation();
						});
					folderLinks.show("blind", {
						direction:"vertical"
					}, 500);
				},
				error:function ()
				{
					folderLinks.html('');
				},
				async:true,
				dataType:'html'
			});
		}
		else
			folderLinks.show("blind", {
				direction:"vertical"
			}, 500);
	};

	var updateView = function ()
	{
		$('#minibar').find('li').removeClass('active');
		if ($.cookie("accordionView") != null && $.cookie("accordionView") == "true")
		{
			$('#accordion-view').addClass('active');
			loadAccordion();
		}
		else
		{
			$('#columns-view').addClass('active');
			loadColumns();
		}
	};

	$.initWallbinView = function ()
	{
		$.ajax({
			type:"POST",
			url:"wallbin/getLibraryDropDownList",
			beforeSend:function ()
			{
				$('#content').html('');
				$.showOverlay();
				$('#libraries-selector-container').css({
					'visibility':'hidden'
				});
			},
			complete:function ()
			{
				$('#libraries-selector-container').css({
					'visibility':'visible'
				});
				$.hideOverlay();
			},
			success:function (msg)
			{
				$('#select-library').html(msg);
				libraryChanged();
			},
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
		$('#select-library').on('change', function ()
		{
			libraryChanged();
		});
		$('#select-page').on('change', function ()
		{
			pageChanged();
		});

		$(window).on('resize', $.updateContentAreaDimensions);

		$('#increase-text-space').off('click').on('click', function ()
		{
			if (storedTextSpace < 3)
			{
				storedTextSpace++;
				$.updateTextSpace(storedTextSpace);
			}
		});
		$('#decrease-text-space').off('click').on('click', function ()
		{
			if (storedTextSpace > 1)
			{
				storedTextSpace--;
				$.updateTextSpace(storedTextSpace);
			}
		});

		$('#increase-text-size').off('click').on('click', function ()
		{
			if (storedTextSize < 22)
				storedTextSize++;
			$.updateTextSize(storedTextSize);
		});

		$('#decrease-text-size').off('click').on('click', function ()
		{
			if (storedTextSize > 8)
				storedTextSize--;
			$.updateTextSize(storedTextSize);
		});

		$('#columns-view').off('click').on('click', function ()
		{
			$.cookie("accordionView", false, {
				expires:60 * 60 * 24 * 7
			});
			updateView();
		});

		$('#accordion-view').off('click').on('click', function ()
		{
			$.cookie("accordionView", true, {
				expires:60 * 60 * 24 * 7
			});
			updateView();
		});
	});
})(jQuery);