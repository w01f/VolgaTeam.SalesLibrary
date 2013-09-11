(function ($)
{
	$.pageContent = {
		load: function (selectedPageId)
		{
			var pageContent = $('#page-content');
			$.ajax({
				type: "POST",
				url: "qbuilder/getPageContent",
				data: {
					selectedPageId: selectedPageId
				},
				beforeSend: function ()
				{
					$.pageContent.destroyEditors();
					pageContent.html('');
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					pageContent.html(msg);
					$.pageContent.afterLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		editorDescription: null,
		editorHeader: null,
		editorFooter: null,
		dropableOptions: {
			greedy: true,
			accept: ".draggable-link",
			hoverClass: "droppable-hover",
			drop: function (event, ui)
			{
				var selectedPageId = $.pageList.getSelectedPageId();
				var order = $('#page-content-links-container tr.page-link').index($(this));
				var linkInCartId = ui.helper.attr('id');
				$.ajax({
					type: "POST",
					url: "qbuilder/addLinkToPage",
					data: {
						pageId: selectedPageId,
						linkInCartId: linkInCartId,
						order: order
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
						$.linkCart.load();
						$.pageContent.loadLinks();
					},
					async: true,
					dataType: 'html'
				});
			}
		},
		afterLoad: function ()
		{
			$('#page-content-url').off('click').on('click', function (even)
			{
				even.stopPropagation();
				even.preventDefault();
				$.pageList.savePage($.pageList.previewPage);
			});
			$("#page-content-tabs").tabs({
				show: function ()
				{
					$.pageContent.updateEditors();
				}
			});

			var pageDescription = $('#page-content-description');
			$.pageContent.editorDescription = pageDescription.cleditor({
				width: '99%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageDescription.val() == '')
				$.pageContent.editorDescription.disable(true);
			$('#page-content-show-description').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					$.pageContent.editorDescription.disable(false);
				else
				{
					$.pageContent.editorDescription.clear();
					$.pageContent.editorDescription.disable(true);
				}
				$.pageContent.editorDescription.refresh();
			});

			var pageHeader = $('#page-content-header-text');
			$.pageContent.editorHeader = pageHeader.cleditor({
				width: '99%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageHeader.val() == '')
				$.pageContent.editorHeader.disable(true);
			$('#page-content-show-header').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					$.pageContent.editorHeader.disable(false);
				else
				{
					$.pageContent.editorHeader.clear();
					$.pageContent.editorHeader.disable(true);
				}
				$.pageContent.editorHeader.refresh();
			});

			var pageFooter = $('#page-content-footer-text');
			$.pageContent.editorFooter = pageFooter.cleditor({
				width: '99%',
				controls: "bold italic underline subscript superscript size color font | undo redo"
			})[0];
			if (pageFooter.val() == '')
				$.pageContent.editorFooter.disable(true);
			$('#page-content-show-footer').off('change').on('change', function ()
			{
				if ($(this).is(':checked'))
					$.pageContent.editorFooter.disable(false);
				else
				{
					$.pageContent.editorFooter.clear();
					$.pageContent.editorFooter.disable(true);
				}
				$.pageContent.editorFooter.refresh();
			});

			$('#page-content-expiration-date-container').find('.input-append')
				.datepicker()
				.on('changeDate', function ()
				{
					$(this).datepicker('hide');
					markExpiredDate();
				});
			$('#page-content-use-expiration-date').off('change').on('change', function ()
			{
				var dateContainer = $('#page-content-expiration-date-container');
				var dateField = $('#page-content-expiration-date');
				if ($(this).is(':checked'))
					dateContainer.show();
				else
				{
					dateContainer.hide();
					dateField.val('');
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

			$('#page-content-links-container, #page-content-links-container tr.page-link').droppable($.pageContent.dropableOptions);

			$.pageContent.afterLinksLoad();

			$.updateContentAreaDimensions();
		},
		loadLinks: function ()
		{
			var pageLinksContainer = $('#page-content-links-container');
			var selectedPageId = $.pageList.getSelectedPageId();
			$.ajax({
				type: "POST",
				url: "qbuilder/getPageLinks",
				data: {
					selectedPageId: selectedPageId
				},
				beforeSend: function ()
				{
					pageLinksContainer.html('');
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					pageLinksContainer.html(msg);
					$.pageContent.afterLinksLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		afterLinksLoad: function ()
		{
			var pageLinks = $('#page-content-links-container');
			pageLinks.find('.link-delete').off('click').on('click', $.pageContent.deleteLink);
			pageLinks.find('.link-up').off('click').on('click', $.pageContent.upLink);
			pageLinks.find('.link-down').off('click').on('click', $.pageContent.downLink);
			$('#page-content-links-number').html(pageLinks.find('.link-delete').length);
			$('#page-content-links-container tr.page-link').droppable($.pageContent.dropableOptions);

			var clickableLinks = pageLinks.find("td.click-no-mobile");
			clickableLinks.off('click').on('click', function ()
			{
				var ids = $(this).parent().find('.link-id-column').html().split('---');
				var linkId = ids[1].replace('link', '');
				$.requestViewDialog(linkId, false);
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
						$.requestViewDialog(linkId, false);
					}
					e.stopPropagation();
					e.preventDefault();
					return false;
				});
		},
		addLink: function (linkInCartId)
		{
			var selectedPageId = $.pageList.getSelectedPageId();

			$.ajax({
				type: "POST",
				url: "qbuilder/addLinkToPage",
				data: {
					pageId: selectedPageId,
					linkInCartId: linkInCartId,
					order: -1
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
					$.linkCart.load();
					$.pageContent.loadLinks();
				},
				async: true,
				dataType: 'html'
			});
		},
		deleteLink: function ()
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
								url: "qbuilder/deleteLinkFromPage",
								data: {
									linkInPageId: linkInPageId
								},
								beforeSend: function ()
								{
									$.showOverlayLight();
								},
								complete: function ()
								{
									$.hideOverlayLight();
									$.pageContent.loadLinks();
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
					close: function ()
					{
						$("#delete-link-warning").remove();
					}
				});
			}
		},
		upLink: function ()
		{
			var selectedPageId = $.pageList.getSelectedPageId();
			var ids = $(this).parent().find('.link-id-column').html().split('---');
			var linkInPageId = ids[0].replace('id', '');
			var rowIndex = $('#page-content-links-container tr.page-link').index($(this).parent());
			if (linkInPageId != null && rowIndex > 0)
			{
				$.ajax({
					type: "POST",
					url: "qbuilder/setPageLinkOrder",
					data: {
						pageId: selectedPageId,
						linkInPageId: linkInPageId,
						order: (rowIndex - 1)
					},
					beforeSend: function ()
					{
						$.showOverlayLight();
					},
					complete: function ()
					{
						$.hideOverlayLight();
						$.pageContent.loadLinks();
					},
					async: true,
					dataType: 'html'
				});
			}
		},
		downLink: function ()
		{
			var selectedPageId = $.pageList.getSelectedPageId();
			var nextRow = $(this).parent().next();
			if (nextRow.length > 0)
			{
				var ids = nextRow.find('.link-id-column').html().split('---');
				var linkInPageId = ids[0].replace('id', '');
				var rowIndex = $('#page-content-links-container tr.page-link').index(nextRow);
				if (linkInPageId != null)
				{
					$.ajax({
						type: "POST",
						url: "qbuilder/setPageLinkOrder",
						data: {
							pageId: selectedPageId,
							linkInPageId: linkInPageId,
							order: rowIndex > 0 ? (rowIndex - 1) : 0
						},
						beforeSend: function ()
						{
							$.showOverlayLight();
						},
						complete: function ()
						{
							$.hideOverlayLight();
							$.pageContent.loadLinks();
						},
						async: true,
						dataType: 'html'
					});
				}
			}
		},
		clear: function ()
		{
			$('#page-content').html('');
		},
		updateEditors: function ()
		{
			var content = $('#content');
			var containerHeight = content.height() - $('#page-content .page-title').height() - $('#page-content .page-url').height() - $('#page-content .ui-tabs-nav').height();

			var descriptionHeight = containerHeight - $('#page-content-tab-title .checkbox').height() - 50;
			if ($.pageContent.editorDescription != undefined)
			{
				$.pageContent.editorDescription.$main.height(descriptionHeight + "px");
				$.pageContent.editorDescription.refresh();
			}

			var headerHeight = containerHeight - $('#page-content-tab-header .checkbox').height() - 50;
			if ($.pageContent.editorHeader != undefined)
			{
				$.pageContent.editorHeader.$main.height(headerHeight + "px");
				$.pageContent.editorHeader.refresh();
			}

			var footerHeight = containerHeight - $('#page-content-tab-footer .checkbox').height() - 50;
			if ($.pageContent.editorFooter != undefined)
			{
				$.pageContent.editorFooter.$main.height(footerHeight + "px");
				$.pageContent.editorFooter.refresh();
			}

			var logoHeight = content.height() - 5 - $('#page-content .page-title').height() - $('#page-content .page-url').height() - $('#page-content .ui-tabs-nav').height() - $('#page-content-tab-logo .header').height() - 45;
			$('#page-content-tab-logo').find('.logo-list').css({
				'height': logoHeight + 'px'
			});
		},
		destroyEditors: function ()
		{
			$.pageContent.editorDescription = null;
			$.pageContent.editorHeader = null;
			$.pageContent.editorFooter = null;
		}
	};

	var markExpiredDate = function ()
	{
		var expiredDateContainer = $('#page-content-expiration-date-container');
		var expiredDateString = $('#page-content-expiration-date').val();
		var today = new Date();
		var expiredDate = expiredDateString != '' ? $.datepicker.parseDate('mm/dd/yy', expiredDateString) : today;
		if (expiredDate < today)
			expiredDateContainer.addClass('error');
		else
			expiredDateContainer.removeClass('error');
	}

	var processAccessCode = function ()
	{
		var expiredDateContainer = $('#page-content-expiration-date-container');
		var expiredDateString = $('#page-content-expiration-date').val();
		var today = new Date();
		var expiredDate = expiredDateString != '' ? $.datepicker.parseDate('mm/dd/yy', expiredDateString) : today;
		if (expiredDate < today)
			expiredDateContainer.addClass('error');
		else
			expiredDateContainer.removeClass('error');
	}
})(jQuery);
