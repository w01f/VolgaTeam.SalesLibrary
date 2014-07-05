(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var ViewDialogBar = function ()
	{
		var that = this;
		var active = false;
		var buttonsPanel = null;

		this.backToConent = null;

		this.show = function (options)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "preview/getViewDialogBar",
				data: {format: options.format},
				success: function (msg)
				{
					active = true;
					buttonsPanel = $(msg);
					$('body').append(buttonsPanel);
					that.resize();
					initFormatButtons();
					buttonsPanel.find('.back').off('click').on('click', function ()
					{
						$.SalesPortal.LinkManager.showViewDialog(that.backToConent);
					});
					buttonsPanel.find('.download-all').off('click').on('click', function ()
					{
						window.open("site/downloadFile?linkId=" + options.linkId + "&format=" + options.format);
					});
					buttonsPanel.find('.download-pdf').off('click').on('click', function ()
					{
						window.open("site/downloadFile?linkId=" + options.linkId + "&format=pdf");
					});
					buttonsPanel.find('.download').off('click').on('click', function ()
					{
						var currentSlideIdValues = $('.fancybox-image').attr('id').split('---');
						var partId = currentSlideIdValues[1];
						window.open("site/downloadFile?linkId=" + options.linkId + "&format=" + options.format + "&partId=" + partId + "&partFormat=" + $.cookie("singleFileFormat").replace(' ', '%20'));
					});
					buttonsPanel.find('.email-all').off('click').on('click', function ()
					{
						$.SalesPortal.QBuilder.PageList.addLitePage(options.linkId, options.linkName, options.fileName, options.fileType);
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
			if (active)
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
			buttonsPanel.remove();
			that.backToConent = null;
			active = false;
		};

		var initFormatButtons = function ()
		{
			if ($.cookie("singleFileFormat") != null)
			{
				if ($.cookie("singleFileFormat") == "new office")
					buttonsPanel.find('.format.new').show();
				else
					buttonsPanel.find('.format.old').show();
			}
			else
			{
				buttonsPanel.find('.format.new').show();
				$.cookie("singleFileFormat", 'new office', {
					expires: (60 * 60 * 24 * 7)
				});
			}

			buttonsPanel.find('.format').off('click').on('click', function ()
			{
				if ($(this).hasClass('new'))
				{
					buttonsPanel.find('.format.new').hide();
					buttonsPanel.find('.format.old').show();
					$.cookie("singleFileFormat", 'old office', {
						expires: (60 * 60 * 24 * 7)
					});
				}
				else
				{
					buttonsPanel.find('.format.new').show();
					buttonsPanel.find('.format.old').hide();
					$.cookie("singleFileFormat", 'new office', {
						expires: (60 * 60 * 24 * 7)
					});
				}
			});
		};
	};
	$.SalesPortal.ViewDialogBar = new ViewDialogBar();
})(jQuery);
