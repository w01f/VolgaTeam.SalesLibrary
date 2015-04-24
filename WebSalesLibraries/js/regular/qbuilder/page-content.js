(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || { };
	$.SalesPortal.QBuilder.PageContent = function (selectedPageId)
	{
		var that = this;
		var editorDescription;
		var editorHeader;
		var editorFooter;
		var dateFormat = 'MM/DD/YY';

		this.pageId = selectedPageId;

		this.loadLinks = function ()
		{
			var pageLinksContainer = $('#page-content-links-container');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/getPageLinks",
				data: {
					selectedPageId: that.pageId
				},
				beforeSend: function ()
				{
					pageLinksContainer.html('');
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					pageLinksContainer.html(msg);
					afterLinksLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.clear = function ()
		{
			$('#page-content').html('');
			$('#page-preview-button').attr('href', '#');
			destroyEditors();
		};

		this.updateContentSize = function ()
		{
			var content = $('#content');
			var pageContent = $('#page-content').children('div');
			var height = content.height() - 5;
			pageContent.css({
				'height': height + 'px'
			});

			updatePageLinks();
			updatePageLogos();
			updateEditors();
		};

		var load = function ()
		{
			var pageContent = $('#page-content');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/getPageContent",
				data: {
					selectedPageId: that.pageId
				},
				beforeSend: function ()
				{
					that.clear();
					$.SalesPortal.Overlay.show(false);
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
			var content = $('#content');
			var pageContent = $('#page-content');
			var tabLinks = $('#page-content-tab-links');
			var height = content.height() - 5 -
				pageContent.find('.page-title').height() -
				pageContent.find('.ui-tabs-nav').height() -
				tabLinks.find('.header').height() - 30;
			$('#page-content-links-container').css({
				'height': height + 'px'
			});
		};

		var updatePageLogos = function ()
		{
			var content = $('#content');
			var pageContent = $('#page-content');
			var tabLogo = $('#page-content-tab-logo');
			var height = content.height() - 5 -
				pageContent.find('.page-title').height() -
				pageContent.find('.ui-tabs-nav').height() -
				tabLogo.find('.header').height() - 40;
			tabLogo.find('.logo-list').css({
				'height': height + 'px'
			});
		};

		var updateEditors = function ()
		{
			var content = $('#content');
			var pageContent = $('#page-content');
			var containerHeight = content.height() -
				pageContent.find('.page-title').height() -
				pageContent.find('.ui-tabs-nav').height();

			var descriptionHeight = containerHeight - $('#page-content-tab-title').find('.checkbox').height() - 50;
			if (editorDescription != undefined)
			{
				editorDescription.$main.height(descriptionHeight + "px");
				editorDescription.refresh();
			}

			var headerHeight = containerHeight - $('#page-content-tab-header').find('.checkbox').height() - 50;
			if (editorHeader != undefined)
			{
				editorHeader.$main.height(headerHeight + "px");
				editorHeader.refresh();
			}

			var footerHeight = containerHeight - $('#page-content-tab-footer').find('.checkbox').height() - 50;
			if (editorFooter != undefined)
			{
				editorFooter.$main.height(footerHeight + "px");
				editorFooter.refresh();
			}
		};

		var dropableOptions = {
			greedy: true,
			accept: ".draggable-link",
			hoverClass: "droppable-hover",
			drop: function (event, ui)
			{
				var order = $('#page-content-links-container').find('tr.page-link').index($(this));
				var linkInCartId = ui.helper.attr('id');
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qbuilder/addLinkToPage",
					data: {
						pageId: that.pageId,
						linkInCartId: linkInCartId,
						order: order
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
			var pageUrl = $('#page-content-url');
			$('#page-preview-button').attr('href', pageUrl.attr('href'));
			pageUrl.off('click').on('click', function ()
			{
				$.SalesPortal.QBuilder.PageList.savePage(function ()
				{
				});
			});
			$("#page-content-tabs").tabs({
				activate: that.updateContentSize
			});
			var pageDescription = $('#page-content-description');
			editorDescription = pageDescription.cleditor({
				width: '96%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageDescription.val() == '')
				editorDescription.disable(true);
			$('#page-content-show-description').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					editorDescription.disable(false);
				else
				{
					editorDescription.clear();
					editorDescription.disable(true);
				}
				editorDescription.refresh();
			});

			var pageHeader = $('#page-content-header-text');
			editorHeader = pageHeader.cleditor({
				width: '96%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageHeader.val() == '')
				editorHeader.disable(true);
			$('#page-content-show-header').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					editorHeader.disable(false);
				else
				{
					editorHeader.clear();
					editorHeader.disable(true);
				}
				editorHeader.refresh();
			});

			var pageFooter = $('#page-content-footer-text');
			editorFooter = pageFooter.cleditor({
				width: '96%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageFooter.val() == '')
				editorFooter.disable(true);
			$('#page-content-show-footer').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					editorFooter.disable(false);
				else
				{
					editorFooter.clear();
					editorFooter.disable(true);
				}
				editorFooter.refresh();
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

			var logoSelector = $('#page-content-tab-logo');
			logoSelector.find('ul a').on('click', function ()
			{
				logoSelector.find('ul a').removeClass('opened');
				$(this).addClass('opened');
			});

			$('#page-content-links-container, #page-content-links-container tr.page-link').droppable(dropableOptions);

			afterLinksLoad();

			that.updateContentSize();
		};
		var afterLinksLoad = function ()
		{
			var pageLinks = $('#page-content-links-container');
			pageLinks.find('.link-delete').off('click').on('click', deleteLink);
			pageLinks.find('.link-up').off('click').on('click', upLink);
			pageLinks.find('.link-down').off('click').on('click', downLink);
			$('#page-content-links-number').html(pageLinks.find('.link-delete').length);
			pageLinks.find('tr.page-link').droppable(dropableOptions);

			var clickableLinks = pageLinks.find("td.click-no-mobile");
			clickableLinks.off('click').on('click', function ()
			{
				var ids = $(this).parent().find('.link-id-column').html().split('---');
				var linkId = ids[1].replace('link', '');
				$.SalesPortal.LinkManager.requestViewDialog(linkId, true);
			});

			pageLinks.find("td.click-mobile").off('touchstart').off('touchmove').off('touchend').on('touchstart',function ()
			{
				isScrolling = false;
			}).on('touchmove',function ()
			{
				isScrolling = true;
			}).on('touchend', function (e)
			{
				if (!isScrolling)
				{
					var ids = $(this).parent().find('.link-id-column').html().split('---');
					var linkId = ids[1].replace('link', '');
					$.SalesPortal.LinkManager.requestViewDialog(linkId, true);
				}
				e.stopPropagation();
				e.preventDefault();
				return false;
			});
		};
		var deleteLink = function ()
		{
			var ids = $(this).parent().find('.link-id-column').html().split('---');
			var linkInPageId = ids[0].replace('id', '');
			if (linkInPageId != null)
			{
				$('body').append('<div id="delete-link-warning" title="Delete Link">Are you SURE you want to delete selected link from Page?</div>');
				$("#delete-link-warning").dialog({
					resizable: false,
					modal: true,
					buttons: {
						"Yes": function ()
						{
							$(this).dialog("close");
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "qbuilder/deleteLinkFromPage",
								data: {
									linkInPageId: linkInPageId
								},
								beforeSend: function ()
								{
									$.SalesPortal.Overlay.show(false);
								},
								complete: function ()
								{
									$.SalesPortal.Overlay.hide();
									that.loadLinks();
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
						$("#delete-link-warning").remove();
					}
				});
			}
		};
		var upLink = function ()
		{
			var ids = $(this).parent().find('.link-id-column').html().split('---');
			var linkInPageId = ids[0].replace('id', '');
			var rowIndex = $('#page-content-links-container').find('tr.page-link').index($(this).parent());
			if (linkInPageId != null && rowIndex > 0)
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qbuilder/setPageLinkOrder",
					data: {
						pageId: that.pageId,
						linkInPageId: linkInPageId,
						order: (rowIndex - 1)
					},
					beforeSend: function ()
					{
						$.SalesPortal.Overlay.show(false);
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
		};
		var downLink = function ()
		{
			var nextRow = $(this).parent().next();
			if (nextRow.length > 0)
			{
				var ids = nextRow.find('.link-id-column').html().split('---');
				var linkInPageId = ids[0].replace('id', '');
				var rowIndex = $('#page-content-links-container').find('tr.page-link').index(nextRow);
				if (linkInPageId != null)
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "qbuilder/setPageLinkOrder",
						data: {
							pageId: that.pageId,
							linkInPageId: linkInPageId,
							order: rowIndex > 0 ? (rowIndex - 1) : 0
						},
						beforeSend: function ()
						{
							$.SalesPortal.Overlay.show(false);
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
			}
		};

		var destroyEditors = function ()
		{
			editorDescription = undefined;
			editorHeader = undefined;
			editorFooter = undefined;
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

		load();
	};
})(jQuery);
