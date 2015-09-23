(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || { };
	var LinkCartManager = function ()
	{
		var that = this;

		this.init = function ()
		{
//			if ($.cookie("showLinkCart") == "true")
//				that.show();
//			else
//				that.hide();

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

			initLinks();
		};

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
					initLinks();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.show = function ()
		{
			$.cookie("showLinkCart", true, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#link-cart').show();
		};

		this.hide = function ()
		{
			$.cookie("showLinkCart", false, {
				expires: (60 * 60 * 24 * 7)
			});
			$('#link-cart').hide();
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
					var succesDescription = '';
					if (linkIds.length > 1)
						succesDescription = 'Links was added to Link Cart';
					else
						succesDescription = 'Link was added to Link Cart';
					$.fancybox({
						content: $('<div class="row" style="margin: 0;">' +
							'<div class="col-xs-3"><img src="' +
							window.BaseUrl +
							'images/preview/actions/quicksite.png">' +
							'</div>' +
							'<div class="col-xs-8 col-xs-offset-1">' +
							'<h3 style="margin-left: 0">Success!</h3>' +
							'<p class="text-muted">' +
							succesDescription +
							'</p>' +
							'</div>' +
							'</div>' +
							'<div class="row" style="margin: 0;"><div class="col-xs-12 text-center"><button type="button" class="btn btn-default" style="width: 80px; margin-top: 20px" onclick="$.fancybox.close()">OK</button></div></div>'),
						title: 'Link Cart',
						width: 400,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
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
					$.fancybox({
						content: $('<div class="row" style="margin: 0;">' +
							'<div class="col-xs-3"><img src="' +
							window.BaseUrl +
							'images/preview/actions/quicksite.png">' +
							'</div>' +
							'<div class="col-xs-8 col-xs-offset-1">' +
							'<h3 style="margin-left: 0">Success!</h3>' +
							'<p class="text-muted">' +
							'Links were added to Link Cart' +
							'</p>' +
							'</div>' +
							'</div>' +
							'<div class="row" style="margin: 0;"><div class="col-xs-12 text-center"><button type="button" class="btn btn-default" style="width: 80px; margin-top: 20px" onclick="$.fancybox.close()">OK</button></div></div>'),
						title: 'Link Cart',
						width: 400,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
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
			var height = $('#content').height() - $('#service-panel').find('.headers').outerHeight(true) - $('#link-cart-buttons').outerHeight(true) - 5;
			$('#link-cart-grid').css({
				'height': height + 'px'
			});
		};

		var initLinks = function ()
		{
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
				var modalDialog = new $.SalesPortal.ModalDialog({
					title: 'Delete Link',
					description: 'Are you SURE you want to delete selected link from Cart?',
					buttons: [
						{
							tag: 'yes',
							title: 'Yes',
							clickHandler: function ()
							{
								modalDialog.close();
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
