(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var FavoritesManager = function ()
	{
		this.initAddPage = function ()
		{
			var selectFoldersPopup = $('#favorites-add-select-folder-popup');
			selectFoldersPopup.find('.favorites-folder-item').off('click').on('click', function ()
			{
				$('#favorites-add-page-folder-name').val($(this).find('a').text());
				selectFoldersPopup.popup('close');
			});

			var favoritesAddPage = $('#favorites-add-page');
			favoritesAddPage.find('.page-footer .buttons .accept').off('click').on('click', function ()
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "favorites/addLink",
					data: {
						linkId: favoritesAddPage.find('.service-data .link-id').text(),
						linkName: $('#favorites-add-page-link-name').val(),
						folderName: $('#favorites-add-page-folder-name').val()
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
						$.mobile.pageContainer.pagecontainer("change", "#link-viewer",
							{
								transition: "slidefade",
								direction: "reverse"
							});
					},
					error: function ()
					{
					},
					async: true,
					dataType: 'html'
				});
			});
		};

		this.initViewPage = function ()
		{
			initFoldersAndLinks($('#favorites-view').find('.main-content'), '#favorites-view');
		};

		this.showAddPage = function ()
		{
			$.mobile.pageContainer.pagecontainer("change", "#favorites-add-page", {
				transition: "slidefade"
			});
		};

		var loadFolderContent = function (folderElement, parentId)
		{
			var folderIdElement = folderElement.find('.ui-collapsible-content').children('.service-data').find('.folder-id');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "favorites/getFoldersAndLinks",
				data: {
					folderId: folderIdElement.length > 0 ? folderIdElement.text() : undefined
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
					var folderContent = folderElement.find('.folder-content');
					folderContent.html(msg);
					folderContent.find('div[data-role=collapsible]').collapsible();
					folderContent.find('.delete-button').buttonMarkup();
					initFoldersAndLinks(folderElement, parentId);
				},
				async: true,
				dataType: 'html'
			});
		};

		var initFoldersAndLinks = function (folderElement, parentId)
		{
			var links = folderElement.find('.link');
			links.collapsible('disable');
			links.off('click').on('click', function (e)
			{
				$.SalesPortal.LinkManager.requestViewDialog(
					$(this).find('.link-id').text(),
					{
						id: parentId,
						name: $(parentId).find('.header-title').text()
					},
					false
				);
				e.preventDefault();
				e.stopPropagation();
			});
			$.each(links, function ()
			{
				var link = $(this);
				link.find('.delete-button').off('click').on('click', function (e)
				{
					e.stopPropagation();
					e.preventDefault();
					deleteLink(
						link.find('.service-data .link-id').text(),
						folderElement.find('.ui-collapsible-content').children('.service-data').find('.folder-id').text(),
						function ()
						{
							folderElement.find('.folder-content').html('');
							loadFolderContent(folderElement, parentId);
						});
				});
			});

			var folderLinks = folderElement.find('.folder');
			folderLinks.on("collapsibleexpand", function ()
			{
				if ($(this).find('.folder-content').html() == '')
					loadFolderContent($(this), parentId);
			});

			$.each(folderLinks, function ()
			{
				var folderLink = $(this);
				folderLink.find('.delete-button').off('click').on('click', function (e)
				{
					e.stopPropagation();
					e.preventDefault();
					deleteFolder(folderLink.find('.service-data .folder-id').text(), function ()
					{
						folderElement.find('.folder-content').html('');
						loadFolderContent(folderElement, parentId);
					});
				});
			});
		};

		var deleteLink = function (linkId, folderId, successCallback)
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
				success: successCallback,
				async: true,
				dataType: 'html'
			});
		};

		var deleteFolder = function (folderId, successCallback)
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
				success: successCallback,
				async: true,
				dataType: 'html'
			});
		};
	};
	$.SalesPortal.Favorites = new FavoritesManager();
})(jQuery);