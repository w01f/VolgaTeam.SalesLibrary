(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ShortcutsFavorites = function ()
	{
		var favoritesData = undefined;

		var dataTable = new $.SalesPortal.SearchDataTable(
			{
				saveState: false,
				deleteHandler: function (linkId)
				{
					deleteLink(linkId, currentFolderId);
				}
			}
		);

		var currentFolderId = undefined;

		this.init = function (data)
		{
			favoritesData = data;

			$.SalesPortal.Content.fillContent(
				favoritesData.content,
				{
					title: favoritesData.options.headerTitle,
					icon: favoritesData.options.headerIcon
				},
				favoritesData.actions
			);

			loadFolders(null);

			initActionButtons();

			$(window).off('resize.favorites').on('resize.favorites', updateContentSize);
			updateContentSize();
		};

		var initActionButtons = function ()
		{
			//var shortcutActionsContainer = $('#shortcut-action-container');
		};

		var loadFolders;
		loadFolders = function (selectedFolderId)
		{
			var isScrolling = false;
			getFolderLinks(selectedFolderId);
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
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Delete Folder',
					description: 'Are you SURE you want to delete this folder and its contents?',
					buttons: [
						{
							tag: 'yes',
							title: 'Yes',
							clickHandler: function ()
							{
								modalDialog.close();
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
										$.SalesPortal.ShortcutsManager.openShortcut($('<div>' + favoritesData.options.serviceData + '</div>'));
									},
									async: true,
									dataType: 'html'
								});
							}
						},
						{
							tag: 'no',
							title: 'No',
							clickHandler: function ()
							{
								modalDialog.close();
							}
						}
					]
				});
				modalDialog.show();
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
								$.SalesPortal.Content.clearContent();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								$.SalesPortal.Content.fillContent(
									msg,
									{
										title: 'Favorite Links',
										icon: 'icon-favorite'
									}
								);
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
			var folderId = listItem.children('.service-data').children('.folder-id').text();
			currentFolderId = folderId;
			getFolderLinks(folderId);
		};

		var getFolderLinks = function (folderId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/getLinks",
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
//					var linkGridBody = favoriteLinksPanel.find(".links-grid-body");
//					linkGridBody.find(".delete-link").off('click').on('click', function (e)
//					{
//						e.stopPropagation();
//						e.preventDefault();
//						var linkId = $(this).parent().parent().find('.link-id-column').html();
//						deleteLink(linkId, folderId);
//					});
//					var isScrolling = false;
//					linkGridBody.find(".delete-link").off('touchstart').off('touchmove').off('touchend').on('touchstart',
//						function ()
//						{
//							isScrolling = false;
//						}).on('touchmove',function ()
//						{
//							isScrolling = true;
//						}).on('touchend', function (e)
//						{
//							e.stopPropagation();
//							e.preventDefault();
//
//							if (!isScrolling)
//							{
//								var linkId = $(this).parent().parent().find('.link-id-column').html();
//								deleteLink(linkId, folderId);
//							}
//							return false;
//						});
				},
				success: function (result)
				{
					dataTable.init(result.links, result.viewOptions);
					updateContentSize();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		var deleteLink = function (linkId, folderId)
		{
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
					getFolderLinks(folderId);
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Content.updateSize();

			var height = $.SalesPortal.Content.getContentObject().height();
			$('#favorites-panel-folders').find('> div').css({
				'height': (height - 3) + 'px'
			});

			var favoriteLinks = $('#favorites-panel-links');
			favoriteLinks.css({
				'height': height + 'px'
			});

			dataTable.updateSize();
		};
	};
})(jQuery);
