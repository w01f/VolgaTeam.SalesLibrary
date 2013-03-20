(function ($)
{
	$.viewDialogBar = [];
	$.viewDialogBar.active = false;

	$.viewDialogBar.show = function (videDialogContainer)
	{
		$.viewDialogBar.videDialogContainer = videDialogContainer;
		$.ajax({
			type:"POST",
			url:"preview/getViewDialogBar",
			data:{},
			success:function (msg)
			{
				$.viewDialogBar.active = true;

				$.viewDialogBar.buttonsPanel = $(msg);
				$('body').append($.viewDialogBar.buttonsPanel);
				$.viewDialogBar.resize();
			},
			error:function ()
			{
				$('#search-result').html('');
			},
			async:true,
			dataType:'html'
		});
	}

	$.viewDialogBar.close = function (videDialogContainer)
	{
		$.viewDialogBar.buttonsPanel.remove();
		$.viewDialogBar.active = false;
	}

	$.viewDialogBar.resize = function ()
	{
		if ($.viewDialogBar.active)
		{
			var panelHeight = $.viewDialogBar.buttonsPanel.height();
			var panelWidth = $.viewDialogBar.buttonsPanel.width();

			var videDialogTop = $($.viewDialogBar.videDialogContainer).offset().top;
			var barTop = videDialogTop - panelHeight - 5;
			if (barTop <= 0)
				barTop = 0;

			var videDialogLeft = $($.viewDialogBar.videDialogContainer).offset().left;
			var videDialogWidth = $($.viewDialogBar.videDialogContainer).width();
			var barLeft = videDialogLeft + ((videDialogWidth - panelWidth) / 2);

			$.viewDialogBar.buttonsPanel.offset({top:barTop, left:barLeft})
		}
	}
})(jQuery);
