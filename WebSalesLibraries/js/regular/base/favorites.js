(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var FavoritesManager = function ()
	{
		var that = this;
		var favoritesGrid = null;
		favoritesGrid = new $.SalesPortal.LinkGrid();
		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/getFavoritesView",
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					$('#content').html(msg);
					loadFolders(null);
				},
				async: true,
				dataType: 'html'
			});
			$(window).off('resize').on('resize', updateContentSize);
		};

		var loadFolders;
		loadFolders = function (selectedFolderId)
		{
			var isScrolling = false;
			getFolderLinks(selectedFolderId, 0, favoritesGrid.sortColumn, favoritesGrid.sortDirection);
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
				$('body').append('<div id="delete-folder-warning" title="Delete Folder"><p>Are you SURE you want to delete this folder and its contents?</p></div>');
				$("#delete-folder-warning").dialog({
					resizable: false,
					modal: true,
					buttons: {
						"Yes": function ()
						{
							$(this).dialog("close");
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "favorites/deleteFolder",
								data: {
									folderId: folderId
								},
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show(false);
								},
								complete: function ()
								{
									$.SalesPortal.Overlay.hide();
									that.init();
								},
								async: true,
								dataType: 'html'
							});
						},
						"No": function ()
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
						$("#delete-folder-warning").remove();
					}
				});

				event.stopPropagation();
			});
			foldersPanel.find('.draggable-folder').draggable({
					delay: 300,
					revert: "invalid",
					helper: function ()
					{
						var folderId = $(this).parent().children('.service-data').children('.folder-id').html();
						return  $('<span id="' + folderId + '" class="glyphicon glyphicon-folder-close"></span>');
					},
					cursorAt: { left: 1, top: 1 }
				}
			);
			foldersPanel.find('.droppable').droppable({
				greedy: true,
				accept: ".draggable-folder, .draggable-link",
				hoverClass: "droppable-hover",
				drop: function (event, ui)
				{
					var parentFolder = $(this).parent();
					var parentId = parentFolder.children('.service-data').children('.folder-id').html();
					if (ui.helper.hasClass('glyphicon-file'))
					{
						var linkId = ui.helper.attr('id');
						var oldParentId = $('#favorites-panel-folders').find('li a.opened').parent().children('.service-data').children('.folder-id').html();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "favorites/putLinkToFolder",
							data: {
								newfolderId: parentId,
								oldfolderId: oldParentId,
								linkId: linkId
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
								openFolder(parentFolder);
							},
							async: true,
							dataType: 'html'
						});
					}
					else if (ui.helper.hasClass('glyphicon-folder-close'))
					{
						var folderId = ui.helper.attr('id');
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "favorites/putFolderToFolder",
							data: {
								folderId: folderId,
								parentId: parentId
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
								$('#content').html('');
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								$('#content').html(msg);
								loadFolders(folderId);
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					}
				}
			});
			updateContentSize();
		};

		var openFolder = function (listItem)
		{
			var foldersPanel = $('#favorites-panel-folders');
			foldersPanel.find('li a').removeClass('opened');
			foldersPanel.find('li .glyphicon-folder-open').removeClass('glyphicon-folder-open').addClass('glyphicon-folder-close');
			listItem.children('a').addClass('opened');
			listItem.children('a').children('.glyphicon-folder-close').removeClass('glyphicon-folder-close').addClass('glyphicon-folder-open');
			var folderId = listItem.children('.service-data').children('.folder-id').html();
			getFolderLinks(folderId, 0, favoritesGrid.sortColumn, favoritesGrid.sortDirection);
		};

		var getFolderLinks = function (folderId, isSort, sortColumn, sortDirection)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/getLinks",
				data: {
					folderId: folderId,
					isSort: isSort,
					sortColumn: sortColumn,
					sortDirection: sortDirection
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					var favoriteLinksPanel = $('#favorites-panel-links');
					$.SalesPortal.Overlay.hide();
					favoritesGrid.init({
						content: favoriteLinksPanel,
						refreshCallback: function ()
						{
							getFolderLinks(folderId, 1, favoritesGrid.sortColumn, favoritesGrid.sortDirection);
						},
						showDelete: true
					});
					updateContentSize();

					var linkGridBody = favoriteLinksPanel.find(".links-grid-body");
					linkGridBody.find(".delete-link").off('click').on('click', function (e)
					{
						e.stopPropagation();
						e.preventDefault();
						var linkId = $(this).parent().parent().find('.link-id-column').html();
						deleteLink(linkId, folderId);
					});
					var isScrolling = false;
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
								deleteLink(linkId, folderId);
							}
							return false;
						});
				},
				success: function (msg)
				{
					$('#favorites-panel-links').find('>div').html('').append(msg);
				},
				error: function ()
				{
					$('#favorites-panel-links').find('>div').html('');
				},
				async: true,
				dataType: 'html'
			});
		};

		var deleteLink = function (linkId, folderId)
		{
			$('body').append('<div id="delete-link-warning" title="Delete Link">Are you sure want to delete this link?</div>');
			$("#delete-link-warning").dialog({
				resizable: false,
				modal: true,
				buttons: {
					"Yes": function ()
					{
						$(this).dialog("close");
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "favorites/deleteLink",
							data: {
								linkId: linkId,
								folderId: folderId
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
								getFolderLinks(folderId, 0, favoritesGrid.sortColumn, favoritesGrid.sortDirection);
							},
							async: true,
							dataType: 'html'
						});
					},
					"No": function ()
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
					$('body').remove("#delete-link-warning");
				}
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();

			var height = $('#content').height();
			$('#favorites-panel-folders').find('> div').css({
				'height': (height - 3) + 'px'
			});

			var favoriteLinks = $('#favorites-panel-links');
			favoriteLinks.find('> div').css({
				'height': height + 'px'
			});
			var gridHeader = favoriteLinks.find('.links-grid-header');
			favoriteLinks.find('.links-grid-body-container').css({
				'height': (favoriteLinks.find('> div').height() - gridHeader.height()) + 'px'
			});

			var linkDateWidth = 140;

			var linkNameHeaderWidth = favoriteLinks.width() -
				gridHeader.find('td.library-column').width() -
				gridHeader.find('td.link-type-column').width() -
				gridHeader.find('td.link-tag-column').width() -
				gridHeader.find('td.link-rate-column').width() -
				linkDateWidth;
			gridHeader.find('td.link-name-column').css({
				'width': linkNameHeaderWidth + 'px'
			});

			var gridBody = favoriteLinks.find('.links-grid-body');
			var linkNameBodyWidth = favoriteLinks.width() -
				gridBody.find('td.library-column').width() -
				gridBody.find('td.link-type-column').width() -
				gridBody.find('td.link-tag-column').width() -
				gridBody.find('td.link-rate-column').width() -
				linkDateWidth;
			gridBody.find('td.link-name-column').css({
				'width': linkNameBodyWidth + 'px'
			});
		};
	};
	$.SalesPortal.Favorites = new FavoritesManager();
})(jQuery);