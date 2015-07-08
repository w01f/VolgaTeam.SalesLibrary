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
						$.mobile.changePage("#link-viewer",
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
		};

		this.showAddPage = function ()
		{
			$.mobile.changePage("#favorites-add-page", {
				transition: "slidefade"
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