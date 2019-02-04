(function ($) {
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	let ShortcutsPublicAuthManager = function () {
		let loginDataModel = undefined;

		this.init = function (loginData) {
			loginDataModel = loginData;

			$('[data-img-src]').each(function () {
				let imgValue = $(this).data('img-src');
				$(this).css('background-image', 'url(' + imgValue + ')');
			});

			$('#login-submit').off('click').on('click', function () {
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "shortcuts/processPublicShortcutLoginData",
					data: {
						loginData: {
							shortcutId: loginDataModel.shortcutId,
							password: $('#login-shortcut-password').val(),
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
								let errorContainer = $('#login-error-info');
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

			$('#contact-submit').off('click').on('click', function () {
				if (!$(this).hasClass('mdl-button--disabled'))
				{
					$.ajax({
						type: "POST",
						url: window.BaseUrl + "shortcuts/sendPublicLoginHelpRequest",
						data: {
							shortcutId: loginDataModel.shortcutId,
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
				let nameText = $('#contact-full-name').val();
				let stationText = $('#contact-station').val();
				let sendButton = $('#contact-submit');

				if (nameText !== '' && stationText !== '')
				{
					sendButton.removeClass('mdl-button--disabled');
				}
				else if (!sendButton.hasClass('mdl-button--disabled'))
					sendButton.addClass('mdl-button--disabled');
			});
		};

		let processLoginSubmit = function (returnUrl) {
			let form = document.getElementById('form-login-submit');
			if (form === null)
			{
				form = document.createElement("form");
				form.setAttribute("id", "form-login-submit");
				form.setAttribute("method", "post");
				form.setAttribute("action", window.BaseUrl + 'shortcuts/processSuccessfulUniversalLogin');
				form._submit_function_ = form.submit;

				let hiddenField = document.createElement("input");
				hiddenField.setAttribute("id", "input-return-url");
				hiddenField.setAttribute("type", "hidden");
				hiddenField.setAttribute("name", 'returnUrl');
				form.appendChild(hiddenField);

				document.body.appendChild(form);
			}
			document.getElementById('input-return-url').setAttribute("value", returnUrl);
			form._submit_function_();
		};
	};
	$.SalesPortal.ShortcutsPublicAuth = new ShortcutsPublicAuthManager();
})(jQuery);