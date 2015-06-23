(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var WallbinManager = function ()
	{
		var that = this;

		this.init = function ()
		{
			var libraryPage = $('#library');

			$('#library-popup-panel-pages').find('.page-item a').off('click').on('click', function ()
			{
				var pageId = $(this).find('.service-data .page-id').text();
				libraryPage.find('.content-header .title').html($(this).find('span').html());
				libraryPage.find('.content-header .logo img').prop('src', $(this).find('.service-data .page-logo').text());
				pageChanged(pageId);
			});

			that.initPageContent(libraryPage.find('.content-data'), '#library');

			var addTabPagePopup = $('#library-popup-add-tab-page');
			addTabPagePopup.find('.accept-action').off('click').on('click', function ()
			{
				addTabPagePopup.popup('close');
				addLibraryAsTabPage();
			});

			var deleteTabPagePopup = $('#library-popup-delete-tab-page');
			deleteTabPagePopup.find('.accept-action').off('click').on('click', function ()
			{
				deleteTabPagePopup.popup('close');
				deleteLibraryAsTabPage();
			});

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
					var pageContent = $('#library').find('.content-data');
					pageContent.html(msg).find('div[data-role=collapsible]').collapsible();
					that.initPageContent(pageContent, '#library');
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
				if ($(this).find('.folder-content').html() == '')
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
			var regularLinks = libraryFolderElement.find('.regular-link');
			regularLinks.collapsible('disable');
			regularLinks.off('click').on('click', function ()
			{
				$.SalesPortal.LinkManager.requestViewDialog(
					$(this).find('.link-id').text(),
					{
						id: parentId,
						name: $('#library').find('.header-title').text()
					},
					false
				);
			});

			var folderLinks = libraryFolderElement.find('.folder-link');
			folderLinks.on("collapsibleexpand", function ()
			{
				if ($(this).find('.link-folder-content').html() == '')
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

		var addLibraryAsTabPage = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "userTab/addLibraryTab",
				data: {
					libraryId: $('#library').find('.service-data .library-id').text()
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
					var libraryPage = $('#library');
					libraryPage.find('.main-footer .library-action').removeClass('active-action');
					libraryPage.find('.main-footer .library-action.delete-library-page').addClass('active-action');
				},
				error: undefined,
				async: true,
				dataType: 'json'
			});
		};

		var deleteLibraryAsTabPage = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "userTab/deleteLibraryTab",
				data: {
					libraryId: $('#library').find('.service-data .library-id').text()
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
					var libraryPage = $('#library');
					libraryPage.find('.main-footer .library-action').removeClass('active-action');
					libraryPage.find('.main-footer .library-action.add-library-page').addClass('active-action');
				},
				error: undefined,
				async: true,
				dataType: 'json'
			});
		};
	};
	$.SalesPortal.Wallbin = new WallbinManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Wallbin.init();
	});
})(jQuery);