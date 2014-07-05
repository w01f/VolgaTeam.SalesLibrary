(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || { };
	var LinkCartManager = function ()
	{
		var that = this;

		this.load = function ()
		{
			var linkCartGrid = $('#link-cart-grid');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/getLinkCart",
				data: {    },
				beforeSend: function ()
				{
					linkCartGrid.html('');
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					linkCartGrid.html(msg);
					that.init();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.init = function ()
		{
			$('#link-cart-refresh').off('click').on('click', function ()
			{
				that.load();
			});
			$('#link-cart-clear').off('click').on('click', function ()
			{
				clear();
			});
			$('#link-cart-add-all').off('click').on('click', function ()
			{
				addAllLinksToPage();
			});
			$('#link-cart-button').off('click').on('click', function ()
			{
				if ($(this).hasClass('sel'))
					$(this).removeClass('sel');
				else
					$(this).addClass('sel');
				$.cookie("showLinkCart", $(this).hasClass('sel'), {
					expires: (60 * 60 * 24 * 7)
				});
				that.show();
				that.updateContentSize();
			});

			var linkCart = $('#link-cart');
			linkCart.find('.link-delete').off('click').on('click', deleteLink);
			linkCart.find('.draggable-link').draggable({
					revert: "invalid",
					distance: 70,
					delay: 500,
					helper: function ()
					{
						var ids = $(this).find('.link-id-column').html().split('---');
						var linkInCartId = ids[0].replace('cart', '');
						return  $('<span id="' + linkInCartId + '" class="glyphicon glyphicon-file"></span>');
					},
					appendTo: "body",
					cursorAt: { left: -10, top: 0 }
				}
			);
		};

		this.show = function ()
		{
			var linkCart = $('#link-cart');
			var showLinkCart = $.cookie("showLinkCart") != undefined ?
				$.cookie("showLinkCart") == "true" :
				$('#link-cart-button').hasClass('sel');
			if (showLinkCart)
				linkCart.show();
			else
				linkCart.hide();
		};

		this.addLinks = function (linkIds)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addLinksToCart",
				data: {
					linkIds: linkIds
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
					if (linkIds.length > 1)
						$('body').append('<div id="add-link-info" title="Link Cart">Links was added to Link Cart</div>');
					else
						$('body').append('<div id="add-link-info" title="Link Cart">Link was added to Link Cart</div>');
					$("#add-link-info").dialog({
						resizable: false,
						modal: true,
						buttons: {
							"OK": function ()
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
							$("#add-link-info").remove();
						}
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.addFolder = function (folderId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addLinksToCart",
				data: {
					folderId: folderId
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
					$('body').append('<div id="add-link-info" title="Link Cart">Links were added to Link Cart</div>');
					$("#add-link-info").dialog({
						resizable: false,
						modal: true,
						buttons: {
							"OK": function ()
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
							$("#add-link-info").remove();
						}
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.updateContentSize = function ()
		{
			var height = $('#content').height() - $('#link-cart-buttons').height();
			$('#link-cart-grid').css({
				'height': height + 'px'
			});
		};

		var clear = function ()
		{
			var linkCartGrid = $('#link-cart-grid');
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/clearLinkCart",
				data: {    },
				beforeSend: function ()
				{
					linkCartGrid.html('');
					$.SalesPortal.Overlay.show(false);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var deleteLink = function ()
		{
			var ids = $(this).parent().find('.link-id-column').html().split('---');
			var linkInCartId = ids[0].replace('cart', '');
			if (linkInCartId != null)
			{
				$('body').append('<div id="delete-link-warning" title="Delete Link">Are you SURE you want to delete selected link from Cart?</div>');
				$("#delete-link-warning").dialog({
					resizable: false,
					modal: true,
					buttons: {
						"Yes": function ()
						{
							$(this).dialog("close");
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "qbuilder/deleteLinkFromCart",
								data: {
									linkInCartId: linkInCartId
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

		var addAllLinksToPage = function ()
		{
			var selectedPageId = $.SalesPortal.QBuilder.PageList.selectedPage.pageId;
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addAllLinksToPage",
				data: {
					pageId: selectedPageId
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
					that.load();
					$.SalesPortal.QBuilder.PageList.selectedPage.loadLinks();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		}
	};
	$.SalesPortal.QBuilder.LinkCart = new LinkCartManager();
})(jQuery);
