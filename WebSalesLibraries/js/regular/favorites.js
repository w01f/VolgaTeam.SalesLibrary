(function ($)
{
	$.initFavoritesView = function ()
	{
		$.ajax({
			type:"POST",
			url:"favorites/getFavoritesView",
			beforeSend:function ()
			{
				$('#content').html('');
				$.showOverlay();
			},
			complete:function ()
			{
				$.hideOverlay();
			},
			success:function (msg)
			{
				$('#content').html('<div>' + msg + '</div>');
				loadFolders(null);
			},
			async:true,
			dataType:'html'
		});
	};

	var loadFolders = function (selectedFolderId)
	{
		getFolderLinks(selectedFolderId, 0);
		var foldersPanel = $('#favorites-panel-folders');
		foldersPanel.find('li')
			.on('click', function (event)
			{
				openFolder($(this));
				event.stopPropagation();
			})
			.on('touchstart', function ()
			{
				isScrolling = false;
			})
			.on('touchmove', function ()
			{
				isScrolling = true;
			})
			.on('touchend', function (e)
			{
				if (!isScrolling)
					openFolder($(this));
				e.stopPropagation();
				e.preventDefault();
				return false;
			});
		foldersPanel.find('.draggable-folder').hover(
			function (event)
			{
				$(this).children('.delete-folder').fadeIn(200);
				event.stopPropagation();
			},
			function (event)
			{
				$(this).children('.delete-folder').fadeOut(100);
				event.stopPropagation();
			}
		);
		foldersPanel.find('.delete-folder').hide();
		foldersPanel.find('.delete-folder').off('click').on('click', function (event)
		{
			var folderId = $(this).parent().parent().children('.service-data').children('.folder-id').html();
			$("#delete-folder-warning").dialog({
				resizable:false,
				modal:true,
				buttons:{
					"Yes":function ()
					{
						$(this).dialog("close");
						$.ajax({
							type:"POST",
							url:"favorites/deleteFolder",
							data:{
								folderId:folderId
							},
							beforeSend:function ()
							{
								$.showOverlayLight();
							},
							complete:function ()
							{
								$.hideOverlayLight();
								$.initFavoritesView();
							},
							async:true,
							dataType:'html'
						});
					},
					"No":function ()
					{
						$(this).dialog("close");
					}
				}
			});
			event.stopPropagation();
		});


		foldersPanel.find('.draggable-folder').draggable({
				delay:300,
				revert:"invalid",
				helper:function (event)
				{
					var folderId = $(this).parent().children('.service-data').children('.folder-id').html();
					return  $('<i id="' + folderId + '" class="icon-folder-close"></i>');
				},
				cursorAt:{ left:1, top:1 }
			}
		);
		foldersPanel.find('.droppable').droppable({
			greedy:true,
			accept:".draggable-folder, .draggable-link",
			hoverClass:"droppable-hover",
			drop:function (event, ui)
			{
				var parentFolder = $(this).parent();
				var parentId = parentFolder.children('.service-data').children('.folder-id').html();
				if (ui.helper.hasClass('icon-file'))
				{
					var linkId = ui.helper.attr('id');
					var oldParentId = $('#favorites-panel-folders').find('li a.opened').parent().children('.service-data').children('.folder-id').html();
					$.ajax({
						type:"POST",
						url:"favorites/putLinkToFolder",
						data:{
							newfolderId:parentId,
							oldfolderId:oldParentId,
							linkId:linkId
						},
						beforeSend:function ()
						{
							$.showOverlayLight();
						},
						complete:function ()
						{
							$.hideOverlayLight();
							openFolder(parentFolder);
						},
						async:true,
						dataType:'html'
					});
				}
				else if (ui.helper.hasClass('icon-folder-close'))
				{
					var folderId = ui.helper.attr('id');
					$.ajax({
						type:"POST",
						url:"favorites/putFolderToFolder",
						data:{
							folderId:folderId,
							parentId:parentId
						},
						beforeSend:function ()
						{
							$.showOverlayLight();
							$('#content').html('');
						},
						complete:function ()
						{
							$.hideOverlayLight();
						},
						success:function (msg)
						{
							$('#content').html('<div>' + msg + '</div>');
							loadFolders(folderId);
						},
						error:function ()
						{
						},
						async:true,
						dataType:'html'
					});
				}
			}
		});
		$.updateContentAreaDimensions();
	};

	var openFolder = function (listItem)
	{
		var foldersPanel = $('#favorites-panel-folders');
		foldersPanel.find('li a').removeClass('opened');
		foldersPanel.find('li .icon-folder-open').removeClass('icon-folder-open').addClass('icon-folder-close');
		listItem.children('a').addClass('opened');
		listItem.children('a').children('.icon-folder-close').removeClass('icon-folder-close').addClass('icon-folder-open');
		var folderId = listItem.children('.service-data').children('.folder-id').html();
		getFolderLinks(folderId, 0);
	};

	var getFolderLinks = function (folderId, isSort)
	{
		$.ajax({
			type:"POST",
			url:"favorites/getLinks",
			data:{
				folderId:folderId,
				isSort:isSort
			},
			beforeSend:function ()
			{
				$.showOverlayLight();
			},
			complete:function ()
			{
				$.hideOverlayLight();
				$.linkGrid.refreshData = function ()
				{
					getFolderLinks(folderId, 1);
				};
				$.linkGrid.showDelete = true;
				$.linkGrid.init();
				$.updateContentAreaDimensions();

				var linkGridBody = $("#links-grid-body");
				linkGridBody.find(".delete-link").off('click').on('click', function (e)
				{
					e.stopPropagation();
					e.preventDefault();
					var linkId = $(this).parent().parent().find('.link-id-column').html();
					deleteLink(linkId,folderId);
				});
				linkGridBody.find(".delete-link").off('touchstart').off('touchmove').off('touchend').on('touchstart',
					function ()
					{
						isScrolling = false;
					}).on('touchmove',function ()
					{
						isScrolling = true;
					}).on('touchend', function (e)
					{
						e.stopPropagation();
						e.preventDefault();

						if (!isScrolling)
						{
							var linkId = $(this).parent().parent().find('.link-id-column').html();
							deleteLink(linkId,folderId);
						}
						return false;
					});
			},
			success:function (msg)
			{
				$('#favorites-panel-links').find('>div').html('').append(msg);
			},
			error:function ()
			{
				$('#favorites-panel-links').find('>div').html('');
			},
			async:true,
			dataType:'html'
		});
	};

	var deleteLink = function (linkId,folderId)
	{
		$("#delete-link-warning").dialog({
			resizable:false,
			modal:true,
			buttons:{
				"Yes":function ()
				{
					$(this).dialog("close");
					$.ajax({
						type:"POST",
						url:"favorites/deleteLink",
						data:{
							linkId:linkId,
							folderId:folderId
						},
						beforeSend:function ()
						{
							$.showOverlayLight();
						},
						complete:function ()
						{
							$.hideOverlayLight();
							getFolderLinks(folderId, 0);
						},
						async:true,
						dataType:'html'
					});
				},
				"No":function ()
				{
					$(this).dialog("close");
				}
			}
		});
	};

	$(document).ready(function ()
	{
	});
})(jQuery);