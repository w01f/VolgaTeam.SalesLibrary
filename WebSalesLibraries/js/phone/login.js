(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var AuthManager = function ()
	{
		this.init = function ()
		{
			$('#button-login').off('click').on('click', function (event)
			{
				if ($('#disclaimer').length)
				{
					$.mobile.changePage("#disclaimer", {
						transition: "slidefade"
					});
					$('#button-login').off('click');
					event.preventDefault();
					event.stopPropagation();
				}
			});
			$('#button-accept-dislaimer').off('click').on('click', function ()
			{
				$('#button-login').click();
			});

			$('#button-switch-version').off('click').on('click', function ()
			{
				switchVersion();
			});

			$('#button-recover-password').off('click').on('click', function ()
			{
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "auth/validateUserByEmail",
					data: {
						login: $('#login').val(),
						email: $('#email').val()
					},
					beforeSend: function ()
					{
						$.mobile.loading('show', {
							textVisible: false,
							html: ""
						});
					},
					complete: function ()
					{
						$.mobile.loading('hide', {
							textVisible: false,
							html: ""
						});
					},
					success: function (msg)
					{
						if (msg != '')
							$('#recover-password').find('.error-message div').html(msg);
						else
						{
							$('#recover-password').find('.error-message div').html('');
							$.ajax({
								type: "POST",
								url: window.BaseUrl + "auth/recoverPassword",
								data: {
									login: $('#login').val()
								},
								beforeSend: function ()
								{
									$.mobile.loading('show', {
										textVisible: false,
										html: ""
									});
								},
								complete: function ()
								{
									$.mobile.loading('hide', {
										textVisible: false,
										html: ""
									});
								},
								success: function ()
								{
									$.mobile.changePage("#recover-password-success", {
										transition: "slidefade"
									});
								},
								async: true,
								dataType: 'html'
							});
						}
					},
					error: function ()
					{
						$('#recover-password').find('.error-message div').html('Error while validating user. Try again or contact to technical support');
					},
					async: true,
					dataType: 'html'
				});
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

		var switchVersion = function ()
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "site/switchVersion",
				data: {
					siteVersion: 'full'
				},
				beforeSend: function ()
				{
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
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
	};
	$.SalesPortal.Auth = new AuthManager();
	$(document).ready(function ()
	{
		$.SalesPortal.Auth.init();
	});
})(jQuery);
