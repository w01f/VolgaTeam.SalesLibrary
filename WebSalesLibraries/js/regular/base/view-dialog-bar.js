(function ($)
{
	$.viewDialogBar = [];
	$.viewDialogBar.active = false;

	$.viewDialogBar.show = function (format, back)
	{
		$.ajax({
			type: "POST",
			url: "preview/getViewDialogBar",
			data: {format: format},
			success: function (msg)
			{
				$.viewDialogBar.active = true;

				$.viewDialogBar.buttonsPanel = $(msg);
				$('body').append($.viewDialogBar.buttonsPanel);
				$.viewDialogBar.resize();
				initFormatButtons();
				$.viewDialogBar.buttonsPanel.find('.back').off('click').on('click', function ()
				{
					$.showViewDialog($.viewDialogBar.backToConent);
				});
				$.viewDialogBar.buttonsPanel.find('.download-all').off('click').on('click', function ()
				{
					window.open("site/downloadFile?linkId=" + $.viewDialogBar.linkId + "&format=" + format);
				});
				$.viewDialogBar.buttonsPanel.find('.download-pdf').off('click').on('click', function ()
				{
					window.open("site/downloadFile?linkId=" + $.viewDialogBar.linkId + "&format=pdf");
				});
				$.viewDialogBar.buttonsPanel.find('.download').off('click').on('click', function ()
				{
					var currentSlideIdValues = $('.fancybox-image').attr('id').split('---');
					var partId = currentSlideIdValues[1];
					window.open("site/downloadFile?linkId=" + $.viewDialogBar.linkId + "&format=" + format + "&partId=" + partId + "&partFormat=" + $.cookie("singleFileFormat").replace(' ', '%20'));
				});
				$.viewDialogBar.buttonsPanel.find('.email-all').off('click').on('click', function ()
				{
					$.pageList.addLitePage($.viewDialogBar.linkId, $.viewDialogBar.linkName, $.viewDialogBar.fileName, $.viewDialogBar.fileType);
				});
			},
			error: function ()
			{
			},
			async: true,
			dataType: 'html'
		});
	};

	$.viewDialogBar.close = function (videDialogContainer)
	{
		$.viewDialogBar.buttonsPanel.remove();
		$.viewDialogBar.backToConent = null;
		$.viewDialogBar.linkId = null;
		$.viewDialogBar.linkName = null;
		$.viewDialogBar.fileName = null;
		$.viewDialogBar.fileType = null;
		$.viewDialogBar.active = false;
	};

	$.viewDialogBar.resize = function ()
	{
		if ($.viewDialogBar.active)
		{
			var videDialogContainer = $('.fancybox-skin');

			var videDialogWidth = videDialogContainer.width();
			var videDialogHeight = videDialogContainer.height();
			var videDialogTop = videDialogContainer.offset().top;
			var videDialogLeft = videDialogContainer.offset().left;

			var panelWidth = 56;
			var panelHeight = videDialogHeight;
			var barTop = videDialogTop + 15;
			var barLeft = videDialogLeft + videDialogWidth + 40;
			if (barLeft > ($(window).width() - panelWidth))
				barLeft = $(window).width() - panelWidth;

			$.viewDialogBar.buttonsPanel.offset({top: barTop, left: barLeft}).css({
				'height': panelHeight + 'px',
				'width': panelWidth + 'px'
			});
		}
	};

	var initFormatButtons = function ()
	{
		if ($.cookie("singleFileFormat") != null)
		{
			if ($.cookie("singleFileFormat") == "new office")
				$.viewDialogBar.buttonsPanel.find('.format.new').show();
			else
				$.viewDialogBar.buttonsPanel.find('.format.old').show();
		}
		else
		{
			$.viewDialogBar.buttonsPanel.find('.format.new').show();
			$.cookie("singleFileFormat", 'new office', {
				expires: (60 * 60 * 24 * 7)
			});
		}

		$.viewDialogBar.buttonsPanel.find('.format').off('click').on('click', function ()
		{
			if ($(this).hasClass('new'))
			{
				$.viewDialogBar.buttonsPanel.find('.format.new').hide();
				$.viewDialogBar.buttonsPanel.find('.format.old').show();
				$.cookie("singleFileFormat", 'old office', {
					expires: (60 * 60 * 24 * 7)
				});
			}
			else
			{
				$.viewDialogBar.buttonsPanel.find('.format.new').show();
				$.viewDialogBar.buttonsPanel.find('.format.old').hide();
				$.cookie("singleFileFormat", 'new office', {
					expires: (60 * 60 * 24 * 7)
				});
			}
		});
	};
})(jQuery);
