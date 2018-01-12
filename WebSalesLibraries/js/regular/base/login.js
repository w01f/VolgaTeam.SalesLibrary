(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var AuthManager = function ()
	{
		var that = this;
		this.init = function ()
		{
			var passwordRequirementsTag = $('#password-requirements');
			var passwordField = $('#edit-field-password');

			$('#recover-password-link').fancybox().off('click').on('click', function (e)
			{
				e.preventDefault();
				e.stopPropagation();
				resetPassword();
			});

			var modeSwitchButton = $('#button-switch-version');
			if (typeof(modeSwitchButton.bootstrapSwitch) === "function")
				modeSwitchButton.bootstrapSwitch().on('switchChange.bootstrapSwitch', function ()
				{
					switchVersion();
				});

			passwordRequirementsTag.off('click').on('click', function (e)
			{
				e.preventDefault();
				e.stopPropagation();
				$.fancybox({
					content: $('<div style="text-align: center">' +
						'Password MUST Be AT LEAST 8 characters<br><br>' +
						'Must contain at least 3 of these 4 conditions below:<br><br>' +
						'<b>1 CAPITAL LETTER</b><br>' +
						'<b>1 lower case letter</b><br>' +
						'<b>Number (1, 2, 3…)</b><br>' +
						'<b>Symbols (!, @, #...)</b><br><br>' +
						'<button type="button" class="btn btn-default" onclick="$.fancybox.close()">OK</button>' +
						'</div>'),
					title: 'Password Requirements',
					width: 350,
					autoSize: false,
					autoHeight: true,
					openEffect: 'none',
					closeEffect: 'none'
				});
			});

			$('#button-change-password').off('click').on('click', function (event)
			{
				if (passwordRequirementsTag.length)
				{
					if (!checkComplexPassword(passwordField.val()))
					{
						event.preventDefault();
						event.stopPropagation();
						$('#edit-field-password').val('');
						$('#edit-field-password-confirm').val('');
						$('#password-requirements').trigger('click');
					}
				}
			});

			$('#form-login-data').off('submit').on('submit', function ()
			{
				var disclaimer = $('.disclaimer-text');
				if (disclaimer.length > 0)
				{
					$.fancybox({
						content: $('<div style="text-align: center">' +
							'<i>Before you log in:</i><br><br>' +
							disclaimer.text() +
							'<br><br>' +
							'<button type="button" id="confirm-disclaimer" class="btn btn-default" onclick="continueLogin = true;$.fancybox.close();">I Agree</button>' +
							'</div>'),
						width: 450,
						autoSize: false,
						autoHeight: true,
						openEffect: 'none',
						closeEffect: 'none',
						closeBtn: false,
						helpers: {
							title: false
						},
						afterClose: function ()
						{
							document.forms[0].submit();
						}
					});
					return false;
				}
				return true;
			});

			updateContentSize();
			$(window).off('resize').on('resize', updateContentSize);
		};

		this.logout = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "auth/logout",
				data: {
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
				},
				success: function ()
				{
					location.reload();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		this.requestRefreshDueToInactivity = function ()
		{
			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Site Notification',
				description: 'Your current session is expired. Click the button below to refresh your page',
				closeBtn: false,
				buttons: [
					{
						tag: 'ok',
						title: 'Refresh this page',
						width: 160,
						clickHandler: function ()
						{
							modalDialog.close();
							location.reload();
						}
					}
				]
			});
			modalDialog.show();
		};

		this.requestLogoutDueToInactivity = function ()
		{
			var modalDialog = new $.SalesPortal.ModalDialog({
				title: 'Site Notification',
				description: 'Your current session is expired. Click the button below to refresh your page',
				closeBtn: false,
				buttons: [
					{
						tag: 'ok',
						title: 'Refresh this page',
						width: 160,
						clickHandler: function ()
						{
							modalDialog.close();
							that.logout();
						}
					}
				]
			});
			modalDialog.show();
		};

		var resetPassword = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "auth/recoverPasswordDialog",
				data: {},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function (msg)
				{
					var content = $(msg);
					content.find('#accept-button').off('click').on('click', function ()
					{
						$.ajax({
							type: "POST",
							url: window.BaseUrl + "auth/validateUserByEmail",
							data: {
								login: content.find('#login').val(),
								email: content.find('#email').val()
							},
							beforeSend: function ()
							{
								$.SalesPortal.Overlay.show();
							},
							complete: function ()
							{
								$.SalesPortal.Overlay.hide();
							},
							success: function (msg)
							{
								if (msg !== '')
									content.find('.error-message').html(msg);
								else
								{
									$.ajax({
										type: "POST",
										url: window.BaseUrl + "auth/recoverPassword",
										data: {
											login: content.find('#login').val()
										},
										success: function ()
										{
											$.fancybox.close();
											$.ajax({
												type: "POST",
												url: window.BaseUrl + "auth/recoverPasswordDialogSuccess",
												data: {},
												beforeSend: function ()
												{
													$.SalesPortal.Overlay.show();
												},
												complete: function ()
												{
													$.SalesPortal.Overlay.hide();
												},
												success: function (msg)
												{
													var content = $(msg);
													content.find('#accept-button').off('click');
													content.find('#accept-button').on('click', function ()
													{
														$.fancybox.close();
													});
													$.fancybox({
														content: content,
														title: 'Password recovery',
														openEffect: 'none',
														closeEffect: 'none'
													});
												},
												error: function ()
												{
												},
												async: true,
												dataType: 'html'
											});
										},
										async: true,
										dataType: 'html'
									});
								}
							},
							error: function ()
							{
								content.find('#error-message').html('Error while validating user. Try again or contact to technical support');
							},
							async: true,
							dataType: 'html'
						});
					});
					content.find('#cancel-button').off('click');
					content.find('#cancel-button').on('click', function ()
					{
						$.fancybox.close();
					});
					$.fancybox({
						content: content,
						title: 'Site Help',
						openEffect: 'none',
						closeEffect: 'none'
					});
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var switchVersion = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "site/switchVersion",
				data: {
					siteVersion: 'mobile'
				},
				beforeSend: function ()
				{
					$.SalesPortal.Overlay.show();
				},
				complete: function ()
				{
					$.SalesPortal.Overlay.hide();
				},
				success: function ()
				{
					location.reload();
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateContentSize = function ()
		{
			var formLogin = $('#form-login');
			var top = ($(window).height() - formLogin.height()) / 2;
			var left = ($(window).width() - formLogin.width()) / 2;
			formLogin.css({
				'left': left + 'px'
			});
			formLogin.css({
				'top': top + 'px'
			});
		};

		var checkComplexPassword = function (password)
		{
			if (password.length < 8)
				return false;
			var conditionPass = 0;
			if (password.match(/.*[0-9]+/))
				conditionPass++;
			if (password.match(/.*[a-z]+/))
				conditionPass++;
			if (password.match(/.*[A-Z]+/))
				conditionPass++;
			if (password.match(/.*\W+/))
				conditionPass++;
			return conditionPass >= 3;
		};
	};
	$.SalesPortal.Auth = new AuthManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Auth.init();
	});
})(jQuery);