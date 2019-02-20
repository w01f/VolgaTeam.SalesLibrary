(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.ShortcutsResetPassword = function () {
		var content = undefined;
		this.init = function () {
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "auth/getChangePasswordDialog",
				beforeSend: function () {
					$.SalesPortal.Overlay.show();
				},
				complete: function () {
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg) {
					content = $(msg);

					content.find('.accept-button').off('click.search-app').on('click.search-app', function () {
						let password = $('#change-password-dialog-password').val();
						let passwordRepeat = $('#change-password-dialog-password-repeat').val();
						let errorInfo = $('#change-password-dialog-error-info');
						if (password == '')
						{
							errorInfo.find('.error-text').html('Please set your new password before save');
							errorInfo.show();
						}
						else if (password != passwordRepeat)
						{
							errorInfo.find('.error-text').html('Password should be the same in the both fields');
							errorInfo.show();
						}
						else
						{
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "auth/changePasswordQuick",
								data: {
									password: password
								},
								async: true,
								dataType: 'json'
							});
							$.fancybox.close();
						}
					});

					content.find('.cancel-button').off('click.search-app').on('click.search-app', function () {
						$.fancybox.close();
					});

					$.fancybox({
						content: content,
						width: 600,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						helpers: {
							title: false
						},
						afterShow: function () {
						}
					});

					updateWarningVisibility();
				},
				error: function () {
				},
				async: true,
				dataType: 'html'
			});
		};
	};
})(jQuery);
