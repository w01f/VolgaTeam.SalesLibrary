(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	$.SalesPortal.ModalDialog = function (userOptions)
	{
		var that = this;

		var defaultOptions = {
			title: 'Title',
			description: undefined,
			logo: undefined,
			width: 400,
			buttons: [
				{
					tag: 'ok',
					title: 'OK',
					clickHandler: function ()
					{
						that.close();
					}
				}
			]
		};

		var options = {
			title: userOptions.title != undefined ? userOptions.title : defaultOptions.title,
			description: userOptions.description != undefined ? userOptions.description : defaultOptions.description,
			logo: userOptions.logo != undefined ? userOptions.logo : defaultOptions.logo,
			width: userOptions.width != undefined ? userOptions.width : defaultOptions.width,
			buttons: userOptions.buttons != undefined ? userOptions.buttons : defaultOptions.buttons,
			closeOnOutsideClick: userOptions.closeOnOutsideClick != undefined ? userOptions.closeOnOutsideClick : false
		};

		this.show = function ()
		{
			$.fancybox.close();
			var content = buildDialogContent();
			$.fancybox({
				content: content,
				width: options.width,
				autoSize: false,
				autoHeight: true,
				openEffect: 'none',
				closeEffect: 'none',
				helpers: {
					title: false,
					overlay: {
						closeClick: options.closeOnOutsideClick
					}
				}
			});
			$.each(options.buttons, function ()
			{
				var button = this;
				$('.modal-dialog-button-' + this.tag).on('click', function ()
				{
					button.clickHandler();
				})
			});
		};

		this.close = function ()
		{
			$.fancybox.close();
		};

		var buildDialogContent = function ()
		{
			var content = '';

			var titleAndDescription = '<h3 style="margin-left: 0">' + options.title + '</h3>';
			if (options.description != undefined)
				titleAndDescription += '<p class="text-muted">' + options.description + '</p>';
			if (options.logo != undefined)
				content +=
					'<div class="row" style="margin: 0;">' +
						'<div class="col-xs-3"><img src="' + options.logo + '"></div>' +
						'<div class="col-xs-8 col-xs-offset-1">' + titleAndDescription + '</div>' +
						'</div>';
			else
				content +=
					'<div class="row" style="margin: 0;">' +
						'<div class="col-xs-12">' + titleAndDescription + '</div>' +
						'</div>';

			var buttonsCount = options.buttons.length;
			var columnIndex = 12 / buttonsCount;
			var buttonsContent = '';
			$.each(options.buttons, function ()
			{
				buttonsContent +=
					'<div class="col-xs-' + columnIndex + ' text-center">' +
						'<button type="button" class="btn btn-default modal-dialog-button-' + this.tag + '" style="width: ' + (this.width != undefined ? this.width : 80) + 'px; margin-top: 20px">' + this.title + '</button>' +
						'</div>'
			});

			content +=
				'<div class="row" style="margin: 0;">' +
					buttonsContent +
					'</div>';

			return content;
		};
	};
})(jQuery);