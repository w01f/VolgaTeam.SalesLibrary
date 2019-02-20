(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.WallbinManager = function (id)
	{
		var that = this;
		var wallbinId = id;

		this.init = function ()
		{
			var wallbinPage = $('#wallbin-' + wallbinId);

			$('#wallbin-' + wallbinId + '-popup-panel-pages').find('.page-item a').off('click').on('click', function ()
			{
				var pageId = $(this).find('.service-data .page-id').text();
				wallbinPage.find('.content-header .title .page-name').html($(this).find('span').html());
				pageChanged(pageId);
			});

			that.initPageContent(wallbinPage.find('.content-data'), '#wallbin-' + wallbinId);

			$('.logout-button').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				$.SalesPortal.Auth.logout();
			})
		};

		var pageChanged = function (selectedPageId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getPageContent",
				data: {
					pageId: selectedPageId
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
					var pageContent = $('#wallbin-' + wallbinId).find('.content-data');
					pageContent.html(msg).find('div[data-role=collapsible]').collapsible();
					that.initPageContent(pageContent, '#wallbin-' + wallbinId);
				},
				async: true,
				dataType: 'html'
			});
		};

		this.initPageContent = function (pageContent, parentId)
		{
			var folders = pageContent.find('div[data-role=collapsible]');
			folders.on("collapsibleexpand", function ()
			{
				if ($(this).find('.folder-content').html() === '')
					loadLibraryFolderLinks($(this), parentId);
			});
		};

		var loadLibraryFolderLinks = function (libraryFolderElement, parentId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getFolderLinksList",
				data: {
					folderId: libraryFolderElement.find('.folder-id').text()
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
					libraryFolderElement.find('.folder-content').html(msg).find('div[data-role=collapsible]').collapsible();
					that.initFolderLinks(libraryFolderElement, parentId);
				},
				async: true,
				dataType: 'html'
			});
		};

		this.initFolderLinks = function (libraryFolderElement, parentId)
		{
			var collapsibleLinks = libraryFolderElement.find('.collapsible-link');
			collapsibleLinks.collapsible('disable');

			var regularLinks = libraryFolderElement.find('.regular-link');
			regularLinks.off('click').on('click', function (e)
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

			var directLinks = libraryFolderElement.find('.direct-link');
			directLinks.off('click').on('click', function ()
			{
				$.SalesPortal.LinkManager.openFile($(this).find('.url').text(), "_blank");
			});

			var folderLinks = libraryFolderElement.find('.folder-link');
			folderLinks.on("collapsibleexpand", function ()
			{
				if ($(this).find('.link-folder-content').html() === '')
					loadLinkFolderLinks($(this), parentId);
			});
		};

		var loadLinkFolderLinks = function (linkFolderElement, parentId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "wallbin/getLinkFolderContent",
				data: {
					linkId: linkFolderElement.find('.link-id').text()
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
					linkFolderElement.find('.link-folder-content').html(msg).find('div[data-role=collapsible]').collapsible();
					that.initFolderLinks(linkFolderElement, parentId);
				},
				async: true,
				dataType: 'html'
			});
		};
	};
})(jQuery);