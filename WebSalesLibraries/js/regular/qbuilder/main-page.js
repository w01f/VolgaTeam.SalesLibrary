(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.QBuilder = $.SalesPortal.QBuilder || { };
	var QBuilderManager = function ()
	{
		var that = this;
		this.init = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/getMainPage",
				beforeSend: function ()
				{
					$('#content').html('');
					$.SalesPortal.Overlay.show(true);
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					$('#content').html(msg);

					$.SalesPortal.QBuilder.PageList.init();
					$.SalesPortal.QBuilder.PageList.show();

					$.SalesPortal.QBuilder.LinkCart.init();
					$.SalesPortal.QBuilder.LinkCart.show();

					updateContentSize();
				},
				async: true,
				dataType: 'html'
			});
			$(window).off('resize.qbuilder').on('resize.qbuilder', updateContentSize);
		};

		var updateContentSize = function ()
		{
			$.SalesPortal.Layout.updateContentSize();
			$.SalesPortal.QBuilder.PageList.updateContentSize();
			$.SalesPortal.QBuilder.LinkCart.updateContentSize();
		}
	};
	$.SalesPortal.QBuilder.Manager = new QBuilderManager();
})(jQuery);
