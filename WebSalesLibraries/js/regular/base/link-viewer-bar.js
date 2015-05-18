(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ViewerBar = function ()
	{
		var that = this;
		var buttonsPanel = undefined;

		this.show = function (options)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getBar",
				data: {},
				success: function (msg)
				{
					buttonsPanel = $(msg);
					$('body').append(buttonsPanel);
					that.resize();
					buttonsPanel.find('.back').off('click').on('click', function ()
					{
						$.fancybox.close();
						options.returnCallback();
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.resize = function ()
		{
			if (buttonsPanel != undefined)
			{
				var viewDialogContainer = $('.fancybox-skin');

				var viewDialogWidth = viewDialogContainer.width();
				var viewDialogHeight = viewDialogContainer.height();
				var viewDialogTop = viewDialogContainer.offset().top;
				var viewDialogLeft = viewDialogContainer.offset().left;

				var panelWidth = 56;
				var barTop = viewDialogTop + 15;
				var barLeft = viewDialogLeft + viewDialogWidth + 40;
				if (barLeft > ($(window).width() - panelWidth))
					barLeft = $(window).width() - panelWidth;


				buttonsPanel.offset({top: barTop, left: barLeft}).css({
					'height': viewDialogHeight + 'px',
					'width': panelWidth + 'px'
				});
			}
		};

		this.close = function ()
		{
			if (buttonsPanel != undefined)
				buttonsPanel.remove();
		};
	};
})(jQuery);
