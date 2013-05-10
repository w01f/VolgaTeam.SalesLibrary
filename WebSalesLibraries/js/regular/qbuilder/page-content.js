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
				.datetimepicker({
					pickTime: false
				})
				.on('changeDate', function ()
				{
					$('.dropdown-menu').hide();
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

			var logoSelector = $('#page-content-tab-logo');
			logoSelector.find('ul a').on('click', function ()
			{
				logoSelector.find('ul a').removeClass('opened');
				$(this).addClass('opened');
			});

			$('#page-content-links-container').droppable({
				greedy: true,
				accept: ".draggable-link",
				hoverClass: "droppable-hover",
				drop: function (event, ui)
				{
					var selectedPageId = $('#page-list').find('a.selected').parent().attr('id').replace('page', '');
					var linkInCartId = ui.helper.attr('id');
					$.ajax({
						type: "POST",
						url: "qbuilder/addLinkToPage",
						data: {
							pageId: selectedPageId,
							linkInCartId: linkInCartId
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
			});

			$.pageContent.afterLinksLoad();

			$.updateContentAreaDimensions();
		},
		loadLinks: function ()
		{
			var pageLinksContainer = $('#page-content-links-container');
			var selectedPageId = $('#page-list').find('a.selected').parent().attr('id').replace('page', '');
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
			$('#page-content-links-number').html(pageLinks.find('.link-delete').length);
		},
		addLink: function (linkInCartId)
		{
			var selectedPageId = $('#page-list').find('a.selected').parent().attr('id').replace('page', '');

			$.ajax({
				type: "POST",
				url: "qbuilder/addLinkToPage",
				data: {
					pageId: selectedPageId,
					linkInCartId: linkInCartId
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
		clear: function ()
		{
			$('#page-content').html('');
		},
		updateEditors: function ()
		{
			var content = $('#content');
			var containerHeight = content.height() - $('#page-content .page-title').height() - $('#page-content .page-url').height() - $('#page-content .ui-tabs-nav').height();

			var descriptionHeight = containerHeight - $('#page-content-tab-title .checkbox').height() - 45;
			if ($.pageContent.editorDescription != undefined)
			{
				$.pageContent.editorDescription.$main.height(descriptionHeight + "px");
				$.pageContent.editorDescription.refresh();
			}

			var headerHeight = containerHeight - $('#page-content-tab-header .checkbox').height() - 45;
			if ($.pageContent.editorHeader != undefined)
			{
				$.pageContent.editorHeader.$main.height(headerHeight + "px");
				$.pageContent.editorHeader.refresh();
			}

			var footerHeight = containerHeight - $('#page-content-tab-footer .checkbox').height() - 45;
			if ($.pageContent.editorFooter != undefined)
			{
				$.pageContent.editorFooter.$main.height(footerHeight + "px");
				$.pageContent.editorFooter.refresh();
			}
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
		var expiredDate = expiredDateString != '' ? Date.parse(expiredDateString) : today;
		if (expiredDate < today)
		{
			expiredDateContainer.addClass('error');
			expiredDateContainer.find('.control-label').show();
		}
		else
		{
			expiredDateContainer.removeClass('error');
			expiredDateContainer.find('.control-label').hide();
		}
	}
})(jQuery);
