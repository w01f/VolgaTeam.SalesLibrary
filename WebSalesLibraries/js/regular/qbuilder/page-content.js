(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || {};
	$.SalesPortal.QBuilder.PageContent = function (parameters)
	{
		var that = this;
		var dateFormat = 'MM/DD/YY';

		this.pageId = parameters.selectedPageId;
		var qBuilderData = parameters.qBuilderData;

		this.loadLinks = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/getPageLinks",
				data: {
					selectedPageId: that.pageId
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (result)
				{
					pageLinksTable.init({
						dataset: result.links,
						dataOptions: result.viewOptions
					});
					afterLinksLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'json'
			});
		};

		this.clear = function ()
		{
			$('#page-content').html('');
			$('#shortcut-action-container').find('.qbuilder-qsite-preview').attr('href', '#');
		};

		this.updateContentSize = function ()
		{
			updatePageLinks();
			updatePageLogos();
			updateEditors();
		};

		this.deleteAllLinks = function ()
		{
			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Delete All Links',
				description: 'Are you SURE you want to delete All Links from Page?',
				buttons: [
					{
						tag: 'yes',
						title: 'Yes',
						clickHandler: function ()
						{
							modalDialog.close();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "qBuilder/deleteAllLinksFromPage",
								data: {
									pageId: that.pageId
								},
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show();
								},
								complete: function ()
								{
									$.SalesPortal.Overlay.hide();
									that.loadLinks();
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
		};

		var load = function ()
		{
			var pageContent = $('#page-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/getPageContent",
				data: {
					selectedPageId: that.pageId
				},
				beforeSend: function ()
				{
					that.clear();
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					pageContent.html(msg);
					afterLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var updatePageLinks = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			var navigationPanel = $.SalesPortal.Content.getNavigationPanel();
			var pageContent = $('#page-content');
			var servicePanel = $('#service-panel');
			var tabLinks = $('#page-content-tab-links');
			var width = $(window).width() - navigationPanel.outerWidth(true) - servicePanel.outerWidth(true) - 25;
			var height = content.height() -
				pageContent.find('.page-title').outerHeight(true) -
				pageContent.find('#page-content-tabs-headers').outerHeight(true) -
				tabLinks.find('.header').outerHeight(true) - 20;

			pageContent.css({
				'max-width': width + 'px'
			});

			$('#page-content-links-container').css({
				'width': width + 'px',
				'height': height + 'px'
			});

			pageLinksTable.updateSize();
		};

		var updatePageLogos = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			var pageContent = $('#page-content');
			var tabLogo = $('#page-content-tab-logo');
			var height = content.height() -
				pageContent.find('.page-title').outerHeight(true) -
				pageContent.find('#page-content-tabs-headers').outerHeight(true) -
				tabLogo.find('.header').outerHeight(true) - 50;
			tabLogo.find('.logo-list').css({
				'height': height + 'px'
			});
		};

		var updateEditors = function ()
		{
			var content = $.SalesPortal.Content.getContentObject();
			var pageContent = $('#page-content');
			var containerHeight = content.height() -
				pageContent.find('.page-title').outerHeight(true) -
				pageContent.find('#page-content-tabs-headers').outerHeight(true);

			var descriptionHeight = containerHeight - $('#page-content-tab-title').find('.checkbox').outerHeight(true) - 70;
			$('#page-content-description').editable('option', 'height', descriptionHeight);

			var headerHeight = containerHeight - $('#page-content-tab-header').find('.checkbox').outerHeight(true) - 70;
			$('#page-content-header-text').editable('option', 'height', headerHeight);

			var footerHeight = containerHeight - $('#page-content-tab-footer').find('.checkbox').outerHeight(true) - 70;
			$('#page-content-footer-text').editable('option', 'height', footerHeight);
		};

		var dropableOptions = {
			greedy: true,
			accept: ".link-cart-data-table-content-row",
			hoverClass: "dt-rowReorder-moving",
			drop: function (event, ui)
			{
				var rowData = pageLinksTable.getTable().row($(this)).data();
				var order = rowData != undefined ? rowData.extended_data.listOrder : -1;
				var linkInCartId = ui.helper.attr('id');
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qBuilder/addLinkToPage",
					data: {
						pageId: that.pageId,
						linkInCartId: linkInCartId,
						order: order
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show();
					},
					complete: function ()
					{
						$.SalesPortal.Overlay.hide();
					},
					success: function ()
					{
						$.SalesPortal.QBuilder.LinkCart.load();
						that.loadLinks();
					},
					async: true,
					dataType: 'html'
				});
			}
		};

		var afterLoad = function ()
		{
			var pageUrlControl = $('#page-content-url');
			var pageUrl = pageUrlControl.attr('href');

			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {name: qBuilderData.options.headerTitle},
				formContent: $('#page-content')
			});

			$('#shortcut-action-container').find('.qbuilder-qsite-preview').attr('href', pageUrl);
			pageUrlControl.off('click.qbuilder').on('click.qbuilder', function (e)
			{
				e.preventDefault();
				$.SalesPortal.QBuilder.PageList.savePage(function ()
				{
					window.open(pageUrl, "_blank");
				});
			});

			var tabContainer = $('#page-content-tabs-headers');
			tabContainer.scrollTabs({
				click_callback: function ()
				{
					tabContainer.find('.page-tab-header').removeClass('selected');
					$(this).addClass('selected');
					var relatedContentId = $(this).find('.service-data .tab-id').text();
					$('#page-content-tabs-content').find('>div').removeClass('selected');
					$(relatedContentId).addClass('selected');
					qBuilderData.options.trackActivityDelegate();
				}
			});

			var pageDescription = $('#page-content-description');
			pageDescription.editable({
				inlineMode: false,
				height: '100%'
			});
			if (pageDescription.val() == '')
				pageDescription.editable('disable');
			pageDescription.on('editable.click', function ()
			{
				qBuilderData.options.trackActivityDelegate();
			});
			$('#page-content-show-description').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					pageDescription.editable('enable');
				else
				{
					pageDescription.editable("setHTML", '');
					pageDescription.editable('disable');
				}
			});

			var pageHeader = $('#page-content-header-text');
			pageHeader.editable({
				inlineMode: false
			});
			if (pageHeader.val() == '')
				pageHeader.editable('disable');
			pageHeader.on('editable.click', function ()
			{
				qBuilderData.options.trackActivityDelegate();
			});
			$('#page-content-show-header').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					pageHeader.editable('enable');
				else
				{
					pageHeader.editable("setHTML", '');
					pageHeader.editable('disable');
				}
			});

			var pageFooter = $('#page-content-footer-text');
			pageFooter.editable({
				inlineMode: false
			});
			if (pageFooter.val() == '')
				pageFooter.editable('disable');
			pageFooter.on('editable.click', function ()
			{
				qBuilderData.options.trackActivityDelegate();
			});
			$('#page-content-show-footer').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					pageFooter.editable('enable');
				else
				{
					pageFooter.editable("setHTML", '');
					pageFooter.editable('disable');
				}
			});

			var expirationDatePickerContainer = $('#page-content-expiration-date-container');
			var expirationDatePicker = $('#page-content-expiration-date');
			expirationDatePickerContainer.daterangepicker(
				{
					format: dateFormat,
					singleDatePicker: true,
					startDate: expirationDatePicker.val(),
					endDate: expirationDatePicker.val()
				}, markExpiredDate);

			$('#page-content-use-expiration-date').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					expirationDatePickerContainer.show();
				else
				{
					expirationDatePickerContainer.hide();
					expirationDatePicker.val('');
				}
			});

			$('#page-content-access-code-enabled').off('change').on('change', function ()
			{
				var accessCode = $('#page-content-access-code');
				if ($(this).is(':checked'))
					accessCode.show();
				else
				{
					accessCode.hide();
					accessCode.val('');
				}
			});

			$("#page-content-access-code").keydown(function (event)
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

			$('#page-content-record-activity').off('change').on('change', function ()
			{
				var ccEmail = $('#page-content-activity-email-copy');
				if ($(this).is(':checked'))
					ccEmail.removeAttr('disabled');
				else
				{
					ccEmail.attr('disabled', 'disabled');
					ccEmail.val('');
				}
			});

			var logoSelector = $('#page-content-tab-logo').find('.logo-list');
			$('#page-content-show-logo').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
				{
					logoSelector.removeClass('disabled');
					logoSelector.find('ul a').first().addClass('opened');
				}
				else
				{
					logoSelector.addClass('disabled');
					logoSelector.find('ul a').removeClass('opened');
				}
			});
			logoSelector.find('ul a').on('click.qbuilder', function ()
			{
				logoSelector.find('ul a').removeClass('opened');
				if (!logoSelector.hasClass('disabled'))
					$(this).addClass('opened');
			});

			that.loadLinks();

			that.updateContentSize();
		};

		var afterLinksLoad = function ()
		{
			pageLinksTable.getTable().on('row-reorder', reorderLink);

			var pageLinks = $('#page-links-data-table-content_wrapper');
			pageLinks.find('.dataTables_scrollBody').droppable(dropableOptions);
			pageLinks.find('tr').droppable(dropableOptions);

			updatePageLinks();
		};

		var deleteLink = function (linkInPageId)
		{
			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Delete Link',
				description: 'Are you SURE you want to delete selected link from Page?',
				buttons: [
					{
						tag: 'yes',
						title: 'Yes',
						clickHandler: function ()
						{
							modalDialog.close();
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "qBuilder/deleteLinkFromPage",
								data: {
									linkInPageId: linkInPageId
								},
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show();
								},
								complete: function ()
								{
									$.SalesPortal.Overlay.hide();
									that.loadLinks();
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
		};

		var reorderLink = function (e, diff, edit)
		{
			var targetLinkInPageId = edit.triggerRow.data().extended_data.linkInPageId;
			for (var i = 0, ien = diff.length; i < ien; i++)
			{
				var linkInPageId = pageLinksTable.getTable().row(diff[i].node).data().extended_data.linkInPageId;
				if (targetLinkInPageId != linkInPageId)
					continue;

				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qBuilder/setPageLinkOrder",
					data: {
						pageId: that.pageId,
						linkInPageId: linkInPageId,
						order: parseInt(diff[i].newData) - (diff[i].newPosition < diff[i].oldPosition ? 1 : 0)
					},
					beforeSend: function ()
					{
					},
					complete: function ()
					{
					},
					async: true,
					dataType: 'json'
				});

				break;
			}
		};

		var markExpiredDate = function ()
		{
			var expiredDateContainer = $('#page-content-expiration-date-container');
			var expiredDateString = $('#page-content-expiration-date').val();
			var today = moment().endOf('day');
			var expiredDate = expiredDateString != '' ? moment(expiredDateString, dateFormat).endOf('day') : today;
			if (expiredDate < today)
				expiredDateContainer.addClass('has-error');
			else
				expiredDateContainer.removeClass('has-error');
		};

		var trackActivity = function ()
		{
			var activityData = $.parseJSON($('<div>' + qBuilderData.options.serviceData + '</div>').find('.activity-data').text());
			$.SalesPortal.ShortcutsManager.trackActivity(
				activityData,
				'QBuilder',
				'QBuilder Activity');
		};

		var pageLinksTable = new $.SalesPortal.SearchDataTable(
			{
				tableIdentifier: 'page-links-data-table-content',
				tableContainerSelector: '#page-content-links-container',
				parentContainerSelector: '#page-content-links-container',
				saveState: false,
				deleteHandler: function (linkInfo)
				{
					deleteLink(linkInfo.extended_data.linkInPageId);
				},
				logHandler: trackActivity
			}
		);

		load();
	};
})(jQuery);
