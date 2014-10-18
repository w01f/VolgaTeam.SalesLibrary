(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var FavoritesManager = function ()
	{
		this.init = function ()
		{
			$('#favorite-folder-select-button').off('click').on('click', function ()
			{
				$.mobile.changePage('#favorites-folder-list-dialog', {
					transition: "pop"
				});
			});
			$('.tab-favorites').off('click').on('click', function ()
			{
				loadFolder(null);
			});
		};

		this.addFavoriteLink = function (linkId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/addLink",
				data: {
					linkId: linkId,
					linkName: $('#favorite-link-name').val(),
					folderName: $('#favorite-folder-name').val()
				},
				beforeSend: function ()
				{
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
				success: function ()
				{
					$.mobile.changePage('#favorites-success-popup', {
						transition: "pop"
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var loadFolder = function (folderId, reload)
		{
			if (reload == undefined)
				reload = false;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/getFoldersAndLinks",
				data: {
					folderId: folderId
				},
				beforeSend: function ()
				{
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
					var newPageId = 'favorites-folder-' + (folderId != null ? folderId : 'root');
					var oldPageId = folderId != null ? $.mobile.activePage.data('url') : newPageId;
					var folderContent = $('#' + newPageId);
					if (!folderContent[0])
					{
						var favoritesTemplate = $('#favorites-template');
						folderContent = favoritesTemplate.clone(true)
							.insertAfter(favoritesTemplate)
							.attr('id', newPageId);
						var linkBack = folderContent.find('.link.back');
						if (folderId != null)
							linkBack.attr('href', '#' + oldPageId);
						else
							linkBack.remove();
					}
					folderContent.find('.page-content').html(msg);
					if (!reload)
						$.mobile.changePage('#' + newPageId, {
							transition: "slidefade"
						});
					folderContent.find('.page-content').find('ul').listview();
					$(".favorite-folder-link").off('click').on('click', function ()
					{
						var selectedFolderId = $.trim($(this).attr("href").replace('#folder', ''));
						loadFolder(selectedFolderId);
					});
					$(".favorite-folder-link-delete").off('click').on('click', function (event)
					{
						event.stopPropagation();
						var selectedFolderId = $.trim($(this).attr("href").replace('#folder', ''));
						var parentFolderId = $.mobile.activePage.data('url').replace('favorites-folder-', '');
						if (parentFolderId == 'root')
							parentFolderId = null;
						var confirmationDialog = $("#confirmation-dialog");
						confirmationDialog.find('.dialog-description').text('You are going to exclude folder from favorites');
						confirmationDialog.find('.dialog-title').text('Are you sure?');
						confirmationDialog.find('.dialog-confirm').on("click.confirm", function ()
						{
							deleteFolder(selectedFolderId);
							confirmationDialog.dialog("close");
							loadFolder(parentFolderId, true);
							$(this).off("click.confirm");
						});
						confirmationDialog.find('.dialog-cancel').on("click.cancel", function ()
						{
							confirmationDialog.dialog("close");
							$(this).off("click.cancel");
						});
						$.mobile.changePage("#confirmation-dialog");
					});
					$(".favorite-file-link").off('click').on('click', function ()
					{
						var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
						$.SalesPortal.Wallbin.loadLink(selectedLink, 'Favorites', '#' + $.mobile.activePage.data('url'), false);
					});
					$(".favorite-file-link-delete").off('click').on('click', function (event)
					{
						event.stopPropagation();
						var selectedLinkNode = $(this).closest('li');
						var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
						var selectedLinkFolder = $.mobile.activePage.data('url').replace('favorites-folder-', '');
						if (selectedLinkFolder == 'root')
							selectedLinkFolder = null;
						var confirmationDialog = $("#confirmation-dialog");
						confirmationDialog.find('.dialog-description').text('You are going to exclude link from favorites');
						confirmationDialog.find('.dialog-title').text('Are you sure?');
						confirmationDialog.find('.dialog-confirm').on("click.confirm", function ()
						{
							deleteLink(selectedLink, selectedLinkFolder);
							selectedLinkNode.remove();
							confirmationDialog.dialog("close");
							$(this).off("click.confirm");
						});
						confirmationDialog.find('.dialog-cancel').on("click.cancel", function ()
						{
							confirmationDialog.dialog("close");
							$(this).off("click.cancel");
						});
						$.mobile.changePage("#confirmation-dialog");
					});
				},
				async: true,
				dataType: 'html'
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
				async: true,
				dataType: 'html'
			});
		};

		var deleteFolder = function (folderId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/deleteFolder",
				data: {
					folderId: folderId
				},
				beforeSend: function ()
				{
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
				async: true,
				dataType: 'html'
			});
		};
	};
	$.SalesPortal.Favorites = new FavoritesManager();
})(jQuery);