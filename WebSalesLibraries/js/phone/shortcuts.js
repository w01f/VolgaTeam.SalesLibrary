(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ShortcutsManager = function ()
	{
		this.init = function ()
		{
			$('#shortcuts-popup-panel-pages').find('.page-item a').off('click').on('click', function ()
			{
				var pageId = $(this).find('.service-data .page-id').text();
				$('#shortcuts').find('.content-header a').html($(this).find('span').html());
				pageChanged(pageId);
			});

			initPageContent();

			$.mobile.changePage($('#shortcuts'));

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
				url: window.BaseUrl + "shortcuts/getPage",
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
				success: function (data)
				{
					var shortcutsLinks = $('#shortcuts-links');
					if (shortcutsLinks.find('ul').length > 0)
						shortcutsLinks.cubeportfolio('destroy', function ()
						{
							shortcutsLinks.html(data.content);
							initPageContent();
						});
					else
					{
						shortcutsLinks.html(data.content);
						initPageContent();
					}
				},
				async: true,
				dataType: 'json'
			});
		};

		var initPageContent = function ()
		{
			var shortcutsLinks = $('#shortcuts-links');

			if (shortcutsLinks.find('ul').length > 0)
				shortcutsLinks.cubeportfolio({
					gridAdjustment: 'alignCenter',
					caption: ''
				});

			shortcutsLinks.find('.shortcuts-link.empty').off('click').on('click', function (e)
			{
				e.stopPropagation();
			});

			shortcutsLinks.find('.shortcuts-link.mp4').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();

				var shortcutData = $(this).find('.service-data');
				var url = shortcutData.find('.url').text();

				openPage(
					shortcutData,
					function ()
					{
						var pageContainer = $('#shortcut-link-page');
						var videoContent = pageContainer.find('.content-data');
						videoContent.html('<video ' +
							'id="video-player" ' +
							'class="video-js vjs-default-skin" ' +
							'controls preload="auto" ' +
							'width="100%" ' +
							'data-setup="{"autoplay":false}">' +
							'<source src="' + url + '" type="video/mp4"/>' +
							'</video>');
						$.mobile.changePage("#shortcut-link-page", {
							transition: "slidefade"
						});
						$.mobile.loading('hide', {
							textVisible: false,
							html: ""
						});
					}
				);
			});

			shortcutsLinks.find('.shortcuts-link.search').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();

				var shortcutData = $(this).find('.service-data');
				openPage(
					shortcutData,
					function ()
					{
						$.SalesPortal.ShortcutsSearchManager(shortcutData);
					}
				);
			});

			shortcutsLinks.find('.shortcuts-link.library-file').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();
				var shortcutData = $(this).find('.service-data');
				var shortcutLinkTitle = shortcutData.find('.link-header').text();
				$.SalesPortal.LinkManager.requestViewDialog(
					shortcutData.find('.link-id').text(),
					{
						id: '#shortcuts',
						name: shortcutData.find('.mobile-header').length > 0 ? shortcutData.find('.mobile-header').text() : shortcutLinkTitle
					},
					false
				);
			});

			shortcutsLinks.find('.shortcuts-link.quicklist').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();

				var shortcutData = $(this).find('.service-data');
				var url = shortcutData.find('.url').text();
				openPage(
					shortcutData,
					function ()
					{
						$.ajax({
							type: "POST",
							url: url,
							beforeSend: function ()
							{
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
								var pageContainer = $('#shortcut-link-page');
								pageContainer.find('.content-data').empty().html(msg);

								$.mobile.changePage("#shortcut-link-page", {
									transition: "slidefade"
								});
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					}
				);
			});

			shortcutsLinks.find('.shortcuts-link.library-page').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();

				var shortcutData = $(this).find('.service-data');
				var linkId = shortcutData.find('.link-id').text();
				openPage(
					shortcutData,
					function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "shortcuts/getLibraryPageShortcut",
							data: {
								linkId: linkId
							},
							beforeSend: function ()
							{
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
								var pageContainer = $('#shortcut-link-page');

								var libraryPageContent = pageContainer.find('.content-data');
								libraryPageContent.html(msg);
								libraryPageContent.find('div[data-role=collapsible]').collapsible();
								$.SalesPortal.Wallbin.initPageContent(libraryPageContent, '#shortcut-link-page');
								$.mobile.changePage("#shortcut-link-page", {
									transition: "slidefade"
								});
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					}
				);
			});

			shortcutsLinks.find('.shortcuts-link.window').off('click').on('click', function (e)
			{
				e.stopPropagation();
				e.preventDefault();

				var shortcutData = $(this).find('.service-data');
				var url = shortcutData.find('.url').text();
				openPage(
					shortcutData,
					function ()
					{
						$.ajax({
							type: "POST",
							url: url,
							beforeSend: function ()
							{
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
								var pageContainer = $('#shortcut-link-page');

								var folderContent = pageContainer.find('.content-data');
								folderContent.html(msg);
								folderContent.find('div[data-role=collapsible]').collapsible();
								$.SalesPortal.Wallbin.initFolderLinks(folderContent, '#shortcut-link-page');
								$.mobile.changePage("#shortcut-link-page", {
									transition: "slidefade"
								});
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
					}
				);
			});
		};

		var openPage = function (shortcutData, linkInitializer)
		{
			cleanupPreviousInstance();

			$.ajax({
				type: "POST",
				url: window.BaseUrl + "shortcuts/getLinkContentWrapper",
				data: {
					linkId: shortcutData.find('.link-id').text()
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
				},
				success: function (msg)
				{
					$('body').append($(msg));
					$.mobile.initializePage();
					linkInitializer();
				},
				async: true,
				dataType: 'html'
			});
		};

		var cleanupPreviousInstance = function ()
		{
			$('body .shortcut-link-page').remove();
		};
	};
	$.SalesPortal.Shortcuts = new ShortcutsManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Shortcuts.init();
	});
})(jQuery);