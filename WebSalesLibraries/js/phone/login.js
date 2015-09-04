(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	window.homePage = window.homePage || '';
	$.SalesPortal = $.SalesPortal || { };
	var AuthManager = function ()
	{
		this.init = function ()
		{
			$('#button-login').off('click').on('click', function (event)
			{
				if ($('#disclaimer').length > 0)
				{
					$("#disclaimer").popup("open");
					$('#button-login').off('click');
					event.preventDefault();
					event.stopPropagation();
				}
			});
			$('#button-accept-dislaimer').off('click').on('click', function ()
			{
				$("#disclaimer").popup("close");
				$('#button-login').click();
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
							$('#recover-password').find('.error-message').html(msg);
						else
						{
							$('#recover-password').find('.error-message').html('');
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
									$.mobile.pageContainer.pagecontainer("change", "#recover-password-success", {
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
						$('#recover-password').find('.error-message').html('Error while validating user. Try again or contact to technical support');
					},
					async: true,
					dataType: 'html'
				});
			});

			$('#site-help-request-link').off('click').on('click', function ()
			{
				$('#site-help-menu').popup('close');
			});

			$('#button-switch-version').on('change', function ()
			{
				switchVersion();
			});

			var tabletLoginContent = $('.login-content.tablet');
			if (tabletLoginContent.length > 0)
			{
				var windowWidth = $(window).width();
				var margin = '0 ' + (windowWidth / 4) + 'px';
				tabletLoginContent.css({
					'margin': margin
				});

				$('#disclaimer').css({
					'margin': margin
				});
			}
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
					if (window.homePage != '')
						window.open(window.homePage, "_self");
					else
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
					siteVersion: 'desktop'
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
})
	(jQuery);
