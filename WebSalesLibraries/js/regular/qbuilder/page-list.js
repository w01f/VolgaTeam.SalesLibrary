(function ($)
{
	$.pageList = {
		load: function (selectedPageId)
		{
			var pageList = $('#page-list-container');
			if (selectedPageId == undefined)
				selectedPageId = pageList.find('li').first().attr('id').replace('page', '');
			$.ajax({
				type: "POST",
				url: "qbuilder/getPageList",
				data: {
					selectedPageId: selectedPageId
				},
				beforeSend: function ()
				{
					pageList.html('');
					$.pageContent.clear();
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					pageList.html(msg);
					$.pageList.afterLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		afterLoad: function ()
		{
			var pageList = $('#page-list-container');
			if (pageList.find('a.selected').length > 0)
				$.pageList.openPage(pageList.find('a.selected').parent().attr('id').replace('page', ''));
			pageList.find('li')
				.off('mousedown.page-list-item touchstart.page-list-item')
				.on('mousedown.page-list-item touchstart.page-list-item', function (eventDown)
				{
					$(this).off('mouseup.page-list-item touchend.page-list-item').on('mouseup.page-list-item touchend.page-list-item', function (eventUp)
					{
						if (eventDown.which != 3)
						{
							pageList.find('li>a').removeClass('selected');
							pageList.find('li .icon-folder-open').removeClass('icon-folder-open').addClass('icon-folder-close');
							$(this).children('a').addClass('selected');
							$(this).children('a').children('.icon-folder-close').removeClass('icon-folder-close').addClass('icon-folder-open');
							var selectedPageId = $(this).attr('id').replace('page', '');
							$.pageList.openPage(selectedPageId);
							eventUp.stopPropagation();
						}
					});
					eventDown.stopPropagation();
				})
				.off('touchmove.page-list-item')
				.on('touchmove.page-list-item', function ()
				{
					$(this).off('touchend');
				});
			$.each(pageList.find('li'), function ()
			{
				var menu = {};
				menu['clone'] = {icon: ' icon-plus', text: 'CLONE this quickSITE', click: function ()
				{
					$.pageList.addPage(this.target.id.replace('page', ''));
				}};
				menu['separator1'] = '---';
				menu['delete'] = {icon: 'icon-remove', text: 'DELETE this quickSITE', click: function ()
				{
					$.pageList.deletePage(this.target.id.replace('page', ''));
				}};
				$(this).contextMenu(menu);
			});
		},
		init: function ()
		{
			var pageList = $('#page-list');
			var showPageList = $.cookie("showQPageList") != undefined ?
				$.cookie("showQPageList") == "true" :
				$('#page-list-button').hasClass('sel');
			if (showPageList)
				pageList.show();
			else
				pageList.hide();

			$('#page-list-clear').off('click').on('click', function ()
			{
				$.pageList.deletePages();
			});
		},
		openPage: function (selectedPageId)
		{
			$.pageContent.load(selectedPageId);
		},
		addPage: function (clonePageId)
		{
			$.ajax({
				type: "POST",
				url: "qbuilder/addPageDialog",
				data: {
					clone: clonePageId != undefined
				},
				beforeSend: function ()
				{
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					var addPageContent = $(msg);
					addPageContent.find('.btn.accept-button').on('click', function ()
					{
						var now = new Date();
						$.ajax({
							type: "POST",
							url: "qbuilder/addPage",
							data: {
								title: $('#add-page-name').val(),
								createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
								clonePageId: clonePageId
							},
							beforeSend: function ()
							{
								$.showOverlayLight();
							},
							complete: function ()
							{
								$.hideOverlayLight();
							},
							success: function (msg)
							{
								$.pageList.load(msg);
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					addPageContent.find('.btn.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: addPageContent,
						title: clonePageId == undefined ? "Add quickSITE" : "Clone quickSITE",
						scrolling: 'no',
						autoSize: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		addLitePage: function (libraryLinkId, title, fileName, fileType)
		{
			$.ajax({
				type: "POST",
				url: "qbuilder/addPageLiteDialog",
				data: {
					linkId: libraryLinkId
				},
				beforeSend: function ()
				{
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					var addPageContent = $(msg);
					var linkName = addPageContent.find('#add-page-name').val();
					addPageContent.find('#add-page-expires-in').find('.btn').on('click', function ()
					{
						addPageContent.find('#add-page-expires-in').find('.btn').removeClass('active');
						$(this).addClass('active');
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Email',
								subType: 'Email Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
					});
					var logoSelector = addPageContent.find('#add-page-tab-logo');
					logoSelector.find('ul a').on('click', function ()
					{
						logoSelector.find('ul a').removeClass('opened');
						$(this).addClass('opened');
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Email',
								subType: 'Email Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
					});
					addPageContent.find('#add-page-restricted, #add-page-show-link-to-main-site').on('change', function ()
					{
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Email',
								subType: 'Email Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
					});
					addPageContent.find('.btn.accept-button').on('click', function ()
					{
						var subtitle = $('#add-page-name').val();
						var now = new Date();
						$.ajax({
							type: "POST",
							url: "qbuilder/addPageLite",
							data: {
								linkId: libraryLinkId,
								createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
								subtitle: subtitle,
								logo: $('#add-page-tab-logo').find('a.opened').find('img').attr('src'),
								expiresInDays: $('#add-page-expires-in').find('.active').val(),
								restricted: $('#add-page-restricted').is(':checked'),
								showLinkToMainSite: $('#add-page-show-link-to-main-site').is(':checked')
							},
							beforeSend: function ()
							{
								$.showOverlayLight();
							},
							complete: function ()
							{
								$.hideOverlayLight();
							},
							success: function (msg)
							{
								window.open('mailto: ?subject=' + subtitle + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg, "_self");
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					addPageContent.find('.btn.cancel-button').on('click', function ()
					{
						$.fancybox.close();
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Email',
								subType: 'Email Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
					});
					$.fancybox({
						content: addPageContent,
						title: title,
						width: 430,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
					$('#add-page-tabs a:first').tab('show');
					$('#add-page-tabs a[data-toggle="tab"]').on('shown', function (e)
					{
						$.ajax({
							type: "POST",
							url: "statistic/writeActivity",
							data: {
								type: 'Email',
								subType: 'Email Activity',
								data: $.toJSON({
									Name: title,
									File: fileName,
									'Original Format': fileType
								})
							},
							async: true,
							dataType: 'html'
						});
					})
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		deletePage: function (pageId)
		{
			var pageList = $('#page-list-container');
			var selectedPageId = pageList.find('a.selected').parent().attr('id').replace('page', '');
			if (pageId != undefined)
				selectedPageId = pageId;
			if (selectedPageId != null)
			{
				$('body').append('<div id="delete-page-warning" title="Delete quickSITE">Are you SURE you want to delete selected quickSITE?</div>');
				$("#delete-page-warning").dialog({
					resizable: false,
					modal: true,
					buttons: {
						"Yes": function ()
						{
							$(this).dialog("close");
							$.ajax({
								type: "POST",
								url: "qbuilder/deletePage",
								data: {
									selectedPageId: selectedPageId
								},
								beforeSend: function ()
								{
									$.showOverlayLight();
								},
								complete: function ()
								{
									$.hideOverlayLight();
									$.pageList.load();
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
					close: function (event, ui)
					{
						$("#delete-page-warning").remove();
					}
				});
			}
		},
		deletePages: function ()
		{
			$.ajax({
				type: "POST",
				url: "qbuilder/deletePagesDialog",
				data: {    },
				beforeSend: function ()
				{
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					var deletePagesContent = $(msg);
					deletePagesContent.find('#delete-pages-select-all').on('click', function ()
					{
						deletePagesContent.find('.delete-pages-item').attr('checked', true);
					});
					deletePagesContent.find('#delete-pages-clear-all').on('click', function ()
					{
						deletePagesContent.find('.delete-pages-item').attr('checked', false);
					});
					deletePagesContent.find('.btn.accept-button').on('click', function ()
					{
						var selectedPageIds = [];
						deletePagesContent.find('.delete-pages-item:checked').each(function ()
						{
							selectedPageIds.push($(this).val());
						});

						$.ajax({
							type: "POST",
							url: "qbuilder/deletePages",
							data: {
								pageIds: $.toJSON(selectedPageIds)
							},
							beforeSend: function ()
							{
								$.showOverlayLight();
							},
							complete: function ()
							{
								$.hideOverlayLight();
								$.pageList.load();
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					deletePagesContent.find('.btn.cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});
					$.fancybox({
						content: deletePagesContent,
						title: "Clean up existed quickSITES",
						width: 430,
						scrolling: 'no',
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		savePage: function (onSuccessHandler)
		{
			var pageList = $('#page-list-container');
			var selectedPage = pageList.find('a.selected');
			var selectedPageId = selectedPage.parent().attr('id').replace('page', '');
			selectedPage.find('span').html($('#page-content-title').val());
			$.ajax({
				type: "POST",
				url: "qbuilder/savePage",
				data: {
					selectedPageId: selectedPageId,
					title: $('#page-content-title').val(),
					description: $('#page-content-description').val(),
					logo: $('#page-content-tab-logo').find('a.opened').find('img').attr('src'),
					expirationDate: $('#page-content-expiration-date').val(),
					requireLogin: $('#page-content-require-login').is(':checked'),
					showTicker: $('#page-content-show-ticker').is(':checked'),
					showLinkToMainSite: $('#page-content-show-link-to-main-site').is(':checked'),
					header: $('#page-content-header-text').val(),
					footer: $('#page-content-footer-text').val()
				},
				beforeSend: function ()
				{
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function ()
				{
					if (onSuccessHandler != null)
						onSuccessHandler();
					else
					{
						$('body').append('<div id="page-content-save-confirm" title="adSALESapps.com">quickSITE is SAVED!</div>');
						$("#page-content-save-confirm").dialog({
							resizable: false,
							modal: true,
							buttons: {
								"OK": function ()
								{
									$(this).dialog("close");
								}
							},
							close: function ()
							{
								$("#page-content-save-confirm").remove();
							}
						});
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		previewPage: function ()
		{
			window.open($('#page-content-url').html(), '_blank');
		},
		emailPageOutlook: function ()
		{
			window.open("mailto: ?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + $('#page-content-url').html(), "_self");
		}
	};

	$(document).ready(function ()
	{
		$('#page-list-button').off('click').on('click', function ()
		{
			if ($(this).hasClass('sel'))
				$(this).removeClass('sel');
			else
				$(this).addClass('sel');
			$.cookie("showQPageList", $(this).hasClass('sel'), {
				expires: (60 * 60 * 24 * 7)
			});
			$.pageList.init();
			$.updateContentAreaDimensions();
		});
		$('#page-add-button').off('click').on('click', function ()
		{
			$.pageList.addPage();
		});
		$('#page-delete-button').off('click').on('click', function ()
		{
			$.pageList.deletePage();
		});
		$('#page-save-button').off('click').on('click', function ()
		{
			$.pageList.savePage(null);
		});
		$('#page-preview-button').off('click').on('click', function ()
		{
			$.pageList.savePage($.pageList.previewPage);
		});
		$('#page-email-outlook-button').off('click').on('click', function ()
		{
			$.pageList.savePage($.pageList.emailPageOutlook);
		});
	});
})(jQuery);
