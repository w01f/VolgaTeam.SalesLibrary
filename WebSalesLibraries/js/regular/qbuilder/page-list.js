(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || {};
	var PageListManager = function ()
	{
		var that = this;
		this.selectedPage = undefined;
		this.qBuilderData = undefined;

		this.load = function (pageToSelectId)
		{
			var pageList = $('#page-list-container');
			if (pageToSelectId == undefined && that.selectedPage != undefined)
				pageToSelectId = that.selectedPage.pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/getPageList",
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
					init();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.addPage = function (parameters)
		{
			if (parameters == undefined)
				parameters = {};
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/addPageDialog",
				data: {
					isCloning: parameters.templatePageId != undefined
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

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.qBuilderData.options.headerTitle},
						formContent: addPageContent
					});

					addPageContent.find('.btn.accept-button').on('click.qbuilder', function ()
					{
						var now = new Date();
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "qBuilder/addPage",
							data: {
								title: $('#add-page-name').val(),
								createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
								templatePageId: parameters.templatePageId,
								populateFromLinkCart: parameters.populateFromLinkCart != undefined && parameters.populateFromLinkCart ? parameters.populateFromLinkCart : false
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
								that.load(msg);
								if (parameters.populateFromLinkCart != undefined && parameters.populateFromLinkCart)
									$.SalesPortal.QBuilder.LinkCart.load();
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					addPageContent.find('.btn.cancel-button').on('click.qbuilder', function ()
					{
						$.fancybox.close();
					});

					$.fancybox({
						content: addPageContent,
						title: parameters.templatePageId == undefined ? "Add quickSITE" : "Clone quickSITE",
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
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Delete quickSITE',
					description: 'Are you SURE you want to delete selected quickSITE?',
					buttons: [
						{
							tag: 'yes',
							title: 'Yes',
							clickHandler: function ()
							{
								modalDialog.close();
								$.ajax({
									type: "POST",
									url: window.BaseUrl + "qBuilder/deletePage",
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
										that.load();
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
				url: window.BaseUrl + "qBuilder/savePage",
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
								'<div class="col-xs-3"><img src="' + window.BaseUrl + 'images/qpages/save.png">' +
								'</div>' +
								'<div class="col-xs-8 col-xs-offset-1">' +
								'<h3 style="margin-left: 0">Boo Yah!</h3>' +
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
				async: false,
				dataType: 'html'
			});
		};

		this.emailPage = function ()
		{
			that.savePage(emailPageOutlook);
		};

		this.updateContentSize = function ()
		{
			var height = $.SalesPortal.Content.getContentObject().height() - $('#service-panel').find('.headers').outerHeight(true) - $('#page-list-buttons').outerHeight(true) - 5;
			$('#page-list-container').css({
				'height': height + 'px'
			});
			if (that.selectedPage != undefined)
				that.selectedPage.updateContentSize();
		};

		var init = function ()
		{
			var pageList = $('#page-list-container');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: that.qBuilderData.options.headerTitle},
				formContent: pageList
			});

			if (pageList.find('tr.selected').length == 0)
				pageList.find('tr').first().addClass('selected');

			if (pageList.find('tr').length > 0)
				openPage(pageList.find('tr.selected').find('.link-id-column').html());
			else
			{
				$('#shortcut-action-container').find('.qbuilder-qsite-preview').attr('href', '#');
				$('#page-content').html('');
			}
			pageList.find('tr').off('click.qbuilder').on('click.qbuilder', function (event)
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
			pageList.find('.link-delete').off('click.qbuilder').on('click.qbuilder', function (event)
			{
				event.stopPropagation();
				that.deletePage($(this).parent().find('.link-id-column').html());
			});
			pageList.find('.link-clone').off('click.qbuilder').on('click.qbuilder', function (event)
			{
				event.stopPropagation();
				that.addPage({
						templatePageId: $(this).parent().find('.link-id-column').text()
					}
				);
			});
			pageList.find('.link-up').off('click.qbuilder').on('click.qbuilder', function (event)
			{
				event.stopPropagation();
				upPage($(this).parent());
			});
			pageList.find('.link-down').off('click.qbuilder').on('click.qbuilder', function (event)
			{
				event.stopPropagation();
				downPage($(this).parent());
			});
			$('#page-list-clear').off('click.qbuilder').on('click.qbuilder', function ()
			{
				deletePages();
			});
		};

		var openPage = function (selectedPageId)
		{
			that.selectedPage = new $.SalesPortal.QBuilder.PageContent({
				selectedPageId: selectedPageId,
				qBuilderData: that.qBuilderData
			});
		};

		var deletePages = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/deletePagesDialog",
				data: {},
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

					var formLogger = new $.SalesPortal.FormLogger();
					formLogger.init({
						logObject: {name: that.qBuilderData.options.headerTitle},
						formContent: deletePagesContent
					});

					deletePagesContent.find('#delete-pages-select-all').on('click.qbuilder', function ()
					{
						deletePagesContent.find('.delete-pages-item').prop('checked', true);
					});
					deletePagesContent.find('#delete-pages-clear-all').on('click.qbuilder', function ()
					{
						deletePagesContent.find('.delete-pages-item').prop('checked', false);
					});
					deletePagesContent.find('.btn.accept-button').on('click.qbuilder', function ()
					{
						var selectedPageIds = [];
						deletePagesContent.find('.delete-pages-item:checked').each(function ()
						{
							selectedPageIds.push($(this).val());
						});

						$.ajax({
							type: "POST",
							url: window.BaseUrl + "qBuilder/deletePages",
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
								that.load();
							},
							error: function ()
							{
							},
							async: true,
							dataType: 'html'
						});
						$.fancybox.close();
					});
					deletePagesContent.find('.btn.cancel-button').on('click.qbuilder', function ()
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
					url: window.BaseUrl + "qBuilder/setPageOrder",
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
						that.load();
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
					url: window.BaseUrl + "qBuilder/setPageOrder",
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
						that.load();
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
