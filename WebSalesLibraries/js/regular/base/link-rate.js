(function ($)
{
	$.linkRate = {
		container: $('<div id="link-rate-bar">Like It!</div>'),
		relatedObject: undefined,
		show: function (linkId, relatedObject)
		{
			$.ajax({
				type: "POST",
				url: "rate/getRate",
				data: {linkId: linkId},
				success: function (msg)
				{
					if (msg != '')
					{
						$.linkRate.relatedObject = relatedObject;
						$.linkRate.container.html(msg);
						$.linkRate.container.find('.rate-value').tooltip({
							animation: false,
							delay: { show: 500, hide: 100 },
							container: 'body'
						})
						$.linkRate.container.find('.rate-value').off('click').on('click', function ()
						{
							$.linkRate.processRate(linkId);
						});
						$.linkRate.container.appendTo('body');
						$.linkRate.resizeContainer();
					}
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		},
		close: function ()
		{
			$.linkRate.container.remove();
			$.linkRate.relatedObject = undefined;
		},
		resizeContainer: function (relatedObject)
		{
			if (relatedObject == undefined)
				relatedObject = $.linkRate.relatedObject;
			else
				$.linkRate.relatedObject = relatedObject;

			var container = $.linkRate.container;
			if (relatedObject != undefined)
			{
				var relatedObjectBottom = relatedObject.offset().top + relatedObject.height();
				var relatedObjectRight = relatedObject.offset().left + relatedObject.width();
				var containerTop = relatedObjectBottom - container.height();
				var containerLeft = relatedObjectRight + 10;
				var containerWidth = container.width();
				if (containerLeft > ($(window).width() - containerWidth))
					containerLeft = $(window).width() - containerWidth;
				container.offset({top: containerTop, left: containerLeft});
			}
		},
		processRate: function (linkId)
		{
			$.linkRate.container.find('.rate-value').tooltip('hide');
			if ($.linkRate.container.find('.rated').length == 0)
			{
				$.ajax({
					type: "POST",
					url: "rate/addRate",
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
						var relatedObject = $.linkRate.relatedObject;
						$.linkRate.close();
						$.linkRate.show(linkId, relatedObject);
					},
					error: function ()
					{
					},
					async: true,
					dataType: 'html'
				});
			}
			else
			{
				$.ajax({
					type: "POST",
					url: "rate/deleteRate",
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
						var relatedObject = $.linkRate.relatedObject;
						$.linkRate.close();
						$.linkRate.show(linkId, relatedObject);
					},
					error: function ()
					{
					},
					async: true,
					dataType: 'html'
				});
			}
		}
	};

	$(document).ready(function ()
	{
	});
})(jQuery);