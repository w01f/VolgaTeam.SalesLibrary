(function ($)
{
	$.linkCart = {
		load: function ()
		{
			var linkCartGrid = $('#link-cart-grid');
			$.ajax({
				type: "POST",
				url: "qbuilder/getLinkCart",
				data: {    },
				beforeSend: function ()
				{
					linkCartGrid.html('');
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
				},
				success: function (msg)
				{
					linkCartGrid.html(msg);
					$.linkCart.afterLoad();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		clear: function ()
		{
			var linkCartGrid = $('#link-cart-grid');
			$.ajax({
				type: "POST",
				url: "qbuilder/clearLinkCart",
				data: {    },
				beforeSend: function ()
				{
					linkCartGrid.html('');
					$.showOverlayLight();
				},
				complete: function ()
				{
					$.hideOverlayLight();
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
			var linkCart = $('#link-cart');
			linkCart.find('.link-delete').off('click').on('click', $.linkCart.deleteLink);
			linkCart.find('.draggable-link').draggable({
					delay: 100,
					revert: "invalid",
					helper: function (event)
					{
						var ids = $(this).find('.link-id-column').html().split('---');
						var linkInCartId = ids[0].replace('cart', '');
						return  $('<i id="' + linkInCartId + '" class="icon-file"></i>');
					},
					appendTo: "body",
					cursorAt: { left: -10, top: 0 }
				}
			);
		},
		init: function ()
		{
			var linkCart = $('#link-cart');
			var showLinkCart = $.cookie("showLinkCart") != undefined ?
				$.cookie("showLinkCart") == "true" :
				$('#link-cart-button').hasClass('sel');
			if (showLinkCart)
				linkCart.show();
			else
				linkCart.hide();

			$('#link-cart-refresh').off('click').on('click', function ()
			{
				$.linkCart.load();
			});
			$('#link-cart-clear').off('click').on('click', function ()
			{
				$.linkCart.clear();
			});
		},
		addLink: function (linkId)
		{
			$.ajax({
				type: "POST",
				url: "qbuilder/addLinkToCart",
				data: {
					linkId: linkId
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
					$('body').append('<div id="add-link-info" title="Link Cart">Link was added to Link Cart</div>');
					$("#add-link-info").dialog({
						resizable: false,
						modal: true,
						buttons: {
							"quickSITES": function ()
							{
								$('.qbuilder-button').trigger('click');
								$(this).dialog("close");
							},
							"OK": function ()
							{
								$(this).dialog("close");
							}
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
		},
		addFolder: function (folderId)
		{
			$.ajax({
				type: "POST",
				url: "qbuilder/addLinkToCart",
				data: {
					folderId: folderId
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
					$('body').append('<div id="add-link-info" title="Link Cart">Links were added to Link Cart</div>');
					$("#add-link-info").dialog({
						resizable: false,
						modal: true,
						buttons: {
							"quickSITES": function ()
							{
								$('.qbuilder-button').trigger('click');
								$(this).dialog("close");
							},
							"OK": function ()
							{
								$(this).dialog("close");
							}
						},
						close: function (event, ui)
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
		},
		deleteLink: function ()
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
								url: "qbuilder/deleteLinkFromCart",
								data: {
									linkInCartId: linkInCartId
								},
								beforeSend: function ()
								{
									$.showOverlayLight();
								},
								complete: function ()
								{
									$.hideOverlayLight();
									$.linkCart.load();
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
		}
	};

	$(document).ready(function ()
	{
		$('#link-cart-button').off('click').on('click', function ()
		{
			if ($(this).hasClass('sel'))
				$(this).removeClass('sel');
			else
				$(this).addClass('sel');
			$.cookie("showLinkCart", $(this).hasClass('sel'), {
				expires: (60 * 60 * 24 * 7)
			});
			$.linkCart.init();
			$.updateContentAreaDimensions();
		});
	});
})(jQuery);
