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
				data: {format: options.format},
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
					buttonsPanel.find('.download-all').off('click').on('click', function ()
					{
						$.SalesPortal.LinkManager.downloadFile({
							name: options.originalFileName,
							path: options.originalFilePath
						});
					});
					buttonsPanel.find('.download-pdf').off('click').on('click', function ()
					{
						$.SalesPortal.LinkManager.downloadFile({
							name: options.pdfFileName,
							path: options.pdfFilePath
						});
					});
					buttonsPanel.find('.download').off('click').on('click', function ()
					{
						var partId = parseInt($('.fancybox-image').attr('id').split('---')[1]);
						var page = options.pages[partId];
						$.SalesPortal.LinkManager.downloadFile({
							name: page.fileName,
							path: page.path
						});
					});
					buttonsPanel.find('.email-all').off('click').on('click', function ()
					{
						$.SalesPortal.QBuilder.PageList.addLitePage(options.linkId, options.linkName, options.originalFileName, options.format);
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
