(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || { };
	var PageListManager = function ()
	{
		var that = this;
		this.selectedPage = undefined;

		this.init = function ()
		{
			if ($.cookie("showQPageList") == "true")
				that.show();
			else
				that.hide();

			var pageList = $('#page-list-container');
			if (pageList.find('tr').length > 0)
				openPage(pageList.find('tr.selected').find('.link-id-column').html());
			else
			{
				$('#shortcut-action-container').find('.qbuilder-qsite-preview').attr('href', '#');
				$('#page-content').html('');
			}
			pageList.find('tr').off('click').on('click', function (event)
			{
				if (!$(this).hasClass('selected'))
				{
					pageList.find('tr').removeClass('selected');
					$(this).addClass('selected');
					var selectedPageId = $(this).find('.link-id-column').html();
					openPage(selectedPageId);
				}
				event.stopPropagation();
			});
			pageList.find('.link-delete').off('click').on('click', function (event)
			{
				event.stopPropagation();
				deletePage($(this).parent().find('.link-id-column').html());
			});
			pageList.find('.link-clone').off('click').on('click', function (event)
			{
				event.stopPropagation();
				addPage($(this).parent().find('.link-id-column').html());
			});
			pageList.find('.link-up').off('click').on('click', function (event)
			{
				event.stopPropagation();
				upPage($(this).parent());
			});
			pageList.find('.link-down').off('click').on('click', function (event)
			{
				event.stopPropagation();
				downPage($(this).parent());
			});
			$('#page-list-clear').off('click').on('click', function ()
			{
				deletePages();
			});
		};

		this.show = function ()
		{
			$.cookie("showQPageList", true, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#page-list').show();
		};

		this.hide = function ()
		{
			$.cookie("showQPageList", false, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#page-list').hide();
		};

		this.addLitePage = function (libraryLinkId, title, fileName, fileType)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addPageLiteDialog",
				data: {
					linkId: libraryLinkId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var addPageContent = $(msg);
					addPageContent.find('#add-page-expires-in').find('.btn').on('click', function ()
					{
						addPageContent.find('#add-page-expires-in').find('.btn').removeClass('active').blur();
						$(this).addClass('active');
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
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
							url: window.BaseUrl + "statistic/writeActivity",
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
							url: window.BaseUrl + "statistic/writeActivity",
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
							url: window.BaseUrl + "qbuilder/addPageLite",
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
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								if (subtitle != '')
									window.open('mailto:?subject=' + subtitle.replace(/&/g, '%26').replace(' ', '%20') + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
								else
									window.open('mailto:?body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
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
							url: window.BaseUrl + "statistic/writeActivity",
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
					var addPageTabs = $('#add-page-tabs');
					addPageTabs.find('a:first').tab('show');
					addPageTabs.find('a[data-toggle="tab"]').on('shown', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "statistic/writeActivity",
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
		};

		this.addPage = function (clonePageId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addPageDialog",
				data: {
					clone: clonePageId != undefined
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var addPageContent = $(msg);
					addPageContent.find('.btn.accept-button').on('click', function ()
					{
						var now = new Date();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "qbuilder/addPage",
							data: {
								title: $('#add-page-name').val(),
								createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
								clonePageId: clonePageId
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								load(msg);
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
		};

		this.deletePage = function (pageId)
		{
			var selectedPageId = that.selectedPage.pageId;
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
								url: window.BaseUrl + "qbuilder/deletePage",
								data: {
									selectedPageId: selectedPageId
								},
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show(false);
								},
								complete: function ()
								{
									$.SalesPortal.Overlay.hide();
									load();
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
						$("#delete-page-warning").remove();
					}
				});
			}
		};

		this.savePage = function (onSuccessHandler)
		{
			var pageList = $('#page-list-container');
			var selectedPageRow = pageList.find('tr.selected');
			var selectedPageId = that.selectedPage.pageId;
			var title = $('#page-content-title').val();
			selectedPageRow.find('td.link-name-column').html(title);
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/savePage",
				data: {
					selectedPageId: selectedPageId,
					title: title,
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
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function ()
				{
					if (onSuccessHandler != null)
						onSuccessHandler();
					else
					{
						$('body').append('<div id="page-content-save-confirm" title="adSALESapps.com">quickSITE is SAVED!</div>');
						$.fancybox({
							content: $('<div class="row" style="margin: 0;">' +
								'<div class="col-xs-3"><img src="' +
								window.BaseUrl +
								'images/qpages/save.png">' +
								'</div>' +
								'<div class="col-xs-8 col-xs-offset-1">' +
								'<h3>Boo Yah!</h3>' +
								'<p class="text-muted">Your QuickSite is Saved</p>' +
								'</div>' +
								'</div>' +
								'<div class="row" style="margin: 0;"><div class="col-xs-12 text-center"><button type="button" class="btn btn-default" style="width: 80px; margin-top: 20px" onclick="$.fancybox.close()">OK</button></div></div>'),
							title: 'QuickSite',
							width: 400,
							autoSize: false,
							autoHeight: true,
							openEffect: 'none',
							closeEffect: 'none',
							helpers: {
								title: false
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
		};

		this.emailPage = function ()
		{
			that.savePage(emailPageOutlook);
		};

		this.updateContentSize = function ()
		{
			var height = $('#content').height() - $('#page-list-buttons').outerHeight() - 10;
			$('#page-list-container').css({
				'height': height + 'px'
			});
			if (that.selectedPage != undefined)
				that.selectedPage.updateContentSize();
		};

		var load = function (pageToSelectId)
		{
			var pageList = $('#page-list-container');
			if (pageToSelectId == undefined && that.selectedPage != undefined)
				pageToSelectId = that.selectedPage.pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/getPageList",
				data: {
					selectedPageId: pageToSelectId
				},
				beforeSend: function ()
				{
					pageList.html('');
					if (that.selectedPage != undefined)
						that.selectedPage.clear();
					that.selectedPage = undefined;
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					pageList.html(msg);
					that.init();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var openPage = function (selectedPageId)
		{
			that.selectedPage = new $.SalesPortal.QBuilder.PageContent(selectedPageId);
		};

		var deletePages = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/deletePagesDialog",
				data: {    },
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var deletePagesContent = $(msg);
					deletePagesContent.find('#delete-pages-select-all').on('click', function ()
					{
						deletePagesContent.find('.delete-pages-item').prop('checked', true);
					});
					deletePagesContent.find('#delete-pages-clear-all').on('click', function ()
					{
						deletePagesContent.find('.delete-pages-item').prop('checked', false);
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
							url: window.BaseUrl + "qbuilder/deletePages",
							data: {
								pageIds: $.toJSON(selectedPageIds)
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show(false);
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
								load();
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
		};

		var upPage = function (row)
		{
			var pageId = row.find('.link-id-column').html();
			var rowIndex = $('#page-list').find('tr.page-list-item').index(row);
			if (rowIndex > 0)
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qbuilder/setPageOrder",
					data: {
						pageId: pageId,
						order: (rowIndex - 1)
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
						load();
					},
					async: true,
					dataType: 'html'
				});
			}
		};

		var downPage = function (row)
		{
			var nextRow = row.next();
			if (nextRow.length > 0)
			{
				var pageId = nextRow.find('.link-id-column').html();
				var rowIndex = $('#page-list').find('tr.page-list-item').index(nextRow);
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qbuilder/setPageOrder",
					data: {
						pageId: pageId,
						order: rowIndex > 0 ? (rowIndex - 1) : 0
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
						load();
					},
					async: true,
					dataType: 'html'
				});
			}
		};

		var emailPageOutlook = function ()
		{
			var pinCode = $('#page-content-access-code').val();
			if (pinCode.length > 0)
				window.open("mailto:?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + $('#page-content-url').html() + "%0D%0APin-code: " + pinCode, "_self");
			else
				window.open("mailto:?body=" + "%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A" + $('#page-content-url').html(), "_self");
		}
	};
	$.SalesPortal.QBuilder.PageList = new PageListManager();
})(jQuery);
