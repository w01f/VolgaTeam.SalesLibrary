(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	var AuthManager = function () {
		var savedLoginModel = undefined;
		var that = this;

		this.init = function (savedLoginModelInput) {
			savedLoginModel = savedLoginModelInput;

			$('[data-img-src]').each(function () {
				var imgValue = $(this).data('img-src');
				$(this).css('background-image', 'url(' + imgValue + ')');
			});

			$('#login-submit').off('click').on('click', function () {
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "auth/processUniversalLoginData",
					data: {
						actionType: 'login',
						actionData: {
							login: $('#login-user-name').val(),
							password: $('#login-password').val(),
							rememberMe: $('#login-remember-me').is(':checked')
						}
					},
					beforeSend: function () {
					},
					complete: function () {
					},
					success: function (data) {
						switch (data.nextAction)
						{
							case "login":
								processLoginSubmit(data.returnUrl);
								break;
							case "change-password":
								savedLoginModel = data.loginModel;
								$('#login-modal').modal('toggle');
								$('#new-password-modal').modal('toggle');
								break;
							case "fix-errors":
								let errorText = "";
								$.each(data.errors, function (index, value) {
									errorText += (value + '<br>');
								});
								var errorContainer = $('#login-error-info');
								errorContainer.find('p').html(errorText);
								errorContainer.show();
								break;
						}
					},
					error: function () {
					},
					async: true,
					dataType: 'json'
				});
			});

			var passwordRequirementsContaner = $('#new-password-requirements');
			$('#new-password-submit').off('click').on('click', function () {
				var passwordField = $('#new-password-password');
				var passwordConfirmField = $('#new-password-password-confirm');
				if (passwordRequirementsContaner.length > 0)
				{
					if (!checkComplexPassword(passwordField.val()))
					{
						passwordField.val('');
						passwordConfirmField.val('');
						$('#new-password-requirements-action').trigger('click');
						return;
					}
				}

				$.ajax({
					type: "POST",
					url: window.BaseUrl + "auth/processUniversalLoginData",
					data: {
						actionType: 'change-password',
						actionData: {
							login: savedLoginModel.login,
							oldPassword: savedLoginModel.password,
							rememberMe: savedLoginModel.rememberMe,
							newInitialPassword: passwordField.val(),
							newRepeatPassword: passwordConfirmField.val()
						}
					},
					beforeSend: function () {
					},
					complete: function () {
					},
					success: function (data) {
						switch (data.nextAction)
						{
							case "login":
								processLoginSubmit(data.returnUrl);
								break;
							case "fix-errors":
								let errorText = "";
								$.each(data.errors, function (index, value) {
									errorText += (value + '<br>');
								});
								var errorContainer = $('#new-password-error-info');
								errorContainer.find('p').html(errorText);
								errorContainer.show();
								break;
						}
					},
					error: function () {
					},
					async: true,
					dataType: 'json'
				});
			});

			$('#new-password-requirements-action').off('click').on('click', function () {
				passwordRequirementsContaner.show();
			});

			$('#recover-password-submit').off('click').on('click', function () {
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "auth/validateUserByEmail",
					data: {
						email: $('#recover-password-email-address').val()
					},
					beforeSend: function () {
					},
					complete: function () {
					},
					success: function (msg) {
						if (msg !== '')
						{
							var errorContainer = $('#recover-password-error-info');
							errorContainer.find('p').html(msg);
							errorContainer.show();
						}
						else
						{
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "auth/recoverPassword",
								data: {
									email: $('#recover-password-email-address').val()
								},
								success: function () {
									$('#recover-password-success-info-area').show();
									$('#recover-password-main-area').hide();
								},
								async: true,
								dataType: 'html'
							});
						}
					},
					error: function () {
						var errorContainer = $('#recover-password-error-info');
						errorContainer.find('p').html('Error while validating user. Try again or contact to technical support');
						errorContainer.show();
					},
					async: true,
					dataType: 'html'
				});
			});

			$('#recover-password-success-confirm').off('click').on('click', function () {
				$('#recover-password-modal').modal('toggle');
			});

			$('#contact-submit').off('click').on('click', function () {
				if (!$(this).hasClass('mdl-button--disabled'))
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "auth/sendHelpRequest",
						data: {
							email: $('#contact-email-address').val(),
							name: $('#contact-full-name').val(),
							station: $('#contact-station').val(),
							text: $('#contact-text').val()
						},
						beforeSend: function () {
						},
						complete: function () {
						},
						success: function () {
							$('#contact-modal').modal('toggle');
						},
						async: true,
						dataType: 'html'
					});
				}
			});

			$('#contact-full-name, #contact-station').off('input').on('input', function () {
				var nameText = $('#contact-full-name').val();
				var stationText = $('#contact-station').val();
				var sendButton = $('#contact-submit');

				if (nameText !== '' && stationText !== '')
				{
					sendButton.removeClass('mdl-button--disabled');
				}
				else if (!sendButton.hasClass('mdl-button--disabled'))
					sendButton.addClass('mdl-button--disabled');
			});
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

		var processLoginSubmit = function (returnUrl) {
			var form = document.getElementById('form-login-submit');
			if (form === null)
			{
				form = document.createElement("form");
				form.setAttribute("id", "form-login-submit");
				form.setAttribute("method", "post");
				form.setAttribute("action", window.BaseUrl + 'auth/processSuccessfulUniversalLogin');
				form._submit_function_ = form.submit;

				var hiddenField = document.createElement("input");
				hiddenField.setAttribute("id", "input-return-url");
				hiddenField.setAttribute("type", "hidden");
				hiddenField.setAttribute("name", 'returnUrl');
				form.appendChild(hiddenField);

				document.body.appendChild(form);
			}
			document.getElementById('input-return-url').setAttribute("value", returnUrl);
			form._submit_function_();
		};

		var checkComplexPassword = function (password) {
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
})(jQuery);