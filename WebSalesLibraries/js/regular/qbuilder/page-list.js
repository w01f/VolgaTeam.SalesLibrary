(function ($)
{
	$.pageList = {
		load: function (selectedPageId)
		{
			var pageList = $('#page-list-container');
			if (selectedPageId == undefined)
				selectedPageId = $.pageList.getSelectedPageId();
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
			if (pageList.find('tr').length > 0)
				$.pageList.openPage($.pageList.getSelectedPageId());
			pageList.find('tr').off('click').on('click', function (event)
			{
				if (!$(this).hasClass('selected'))
				{
					pageList.find('tr').removeClass('selected');
					$(this).addClass('selected');
					var selectedPageId = $.pageList.getSelectedPageId();
					$.pageList.openPage(selectedPageId);
				}
				event.stopPropagation();
			});
			pageList.find('.link-delete').off('click').on('click', function (event)
			{
				event.stopPropagation();
				$.pageList.deletePage($(this).parent().find('.link-id-column').html());
			});
			pageList.find('.link-clone').off('click').on('click', function (event)
			{
				event.stopPropagation();
				$.pageList.addPage($(this).parent().find('.link-id-column').html());
			});
			pageList.find('.link-up').off('click').on('click', function (event)
			{
				event.stopPropagation();
				$.pageList.upPage($(this).parent());
			});
			pageList.find('.link-down').off('click').on('click', function (event)
			{
				event.stopPropagation();
				$.pageList.downPage($(this).parent());
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
		getSelectedPageId: function ()
		{
			var pageList = $('#page-list');
			return pageList.find('tr.selected').find('.link-id-column').html();
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
					addPageContent.find('#add-page-name-enabled').on('change', function ()
					{
						if (!addPageContent.find('#add-page-name-enabled').is(':checked'))
							addPageContent.find('#add-page-name').val('').attr('disabled', 'disabled');
						else
							addPageContent.find('#add-page-name').removeAttr('disabled');
					});
					addPageContent.find('#add-page-access-code-enabled').off('change').on('change', function ()
					{
						var accessCode = $('#add-page-access-code');
						if ($(this).is(':checked'))
							accessCode.show();
						else
						{
							accessCode.hide();
							accessCode.val('');
						}
					});
					addPageContent.find("#add-page-access-code").keydown(function (event)
					{
						if (event.keyCode == 46 || event.keyCode == 8)
						{
						}
						else
						{
							if (event.keyCode < 48 || event.keyCode > 57)
								event.preventDefault();
						}
					});
					addPageContent.find('#add-page-record-activity').off('change').on('change', function ()
					{
						var ccEmail = $('#add-page-activity-email-copy');
						if ($(this).is(':checked'))
							ccEmail.removeAttr('disabled');
						else
						{
							ccEmail.attr('disabled', 'disabled');
							ccEmail.val('');
						}
					});
					addPageContent.find('#add-page-name-enabled, #add-page-restricted,#add-page-access-code,#add-page-disable-widgets,#add-page-disable-banners,#add-page-record-activity,#add-page-show-links-as-url').on('change', function ()
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
						var pinCode = $('#add-page-access-code').val();
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
								pinCode: pinCode,
								disableWidgets: $('#add-page-disable-widgets').is(':checked'),
								disableBanners: $('#add-page-disable-banners').is(':checked'),
								showLinksAsUrl: $('#add-page-show-links-as-url').is(':checked'),
								recordActivity: $('#add-page-record-activity').is(':checked'),
								activityEmailCopy: $('#add-page-activity-email-copy').val()
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
								if (subtitle != '')
									window.open('mailto: ?subject=' + subtitle.replace(/&/g, '%26') + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
								else
									window.open('mailto: ?body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
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
						width: 530,
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
			var selectedPageId = $.pageList.getSelectedPageId();
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
			var selectedPage = pageList.find('tr.selected');
			var selectedPageId = $.pageList.getSelectedPageId();
			selectedPage.find('td.link-name-column').html($('#page-content-title').val());
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
					pinCode: $('#page-content-access-code').val(),
					disableBanners: $('#page-content-disable-banners').is(':checked'),
					disableWidgets: $('#page-content-disable-widgets').is(':checked'),
					showLinksAsUrl: $('#page-content-show-links-as-url').is(':checked'),
					recordActivity: $('#page-content-record-activity').is(':checked'),
					activityEmailCopy: $('#page-content-activity-email-copy').val(),
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
		upPage: function (row)
		{
			var pageId = row.find('.link-id-column').html();
			var rowIndex = $('#page-list tr.page-list-item').index(row);
			if (rowIndex > 0)
			{
				$.ajax({
					type: "POST",
					url: "qbuilder/setPageOrder",
					data: {
						pageId: pageId,
						order: (rowIndex - 1)
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
			}
		},
		downPage: function (row)
		{
			var nextRow = row.next();
			if (nextRow.length > 0)
			{
				var pageId = nextRow.find('.link-id-column').html();
				var rowIndex = $('#page-list tr.page-list-item').index(nextRow);
				$.ajax({
					type: "POST",
					url: "qbuilder/setPageOrder",
					data: {
						pageId: pageId,
						order: rowIndex > 0 ? (rowIndex - 1) : 0
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
			}
		},
		emailPageOutlook: function ()
		{
			var pinCode = $('#page-content-access-code').val();
			if (pinCode.length > 0)
				window.open("mailto: ?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + $('#page-content-url').html() + "%0D%0APin-code: " + pinCode, "_self");
			else
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
