(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var EmailManager = function ()
	{
		var emailPage = undefined;
		this.init = function ()
		{
			emailPage = $('#email-page');

			var logoTab = $('#email-tab-logo');
			logoTab.find('li').on('click', function (e)
			{
				var currentItem = $(this);
				if (!currentItem.hasClass('selected'))
				{
					logoTab.find('li.selected').removeClass('selected').attr("data-theme", "a").find('a').removeClass("ui-btn-e").addClass("ui-btn-a");
					$(this).addClass('selected').attr("data-theme", "e").find('a').removeClass("ui-btn-a").addClass("ui-btn-e");
				}
			});

			$('#email-tab-security-email-send').on('change', function ()
			{
				if ($(this).is(':checked'))
					$('#email-tab-security-email-address').prop('disabled', false);
				else
					$('#email-tab-security-email-address').val('').prop('disabled', true);
			});
			$('#email-tab-security-access-code-enable').on('change', function ()
			{
				var accessCode = $('#email-tab-security-access-code');
				if ($(this).is(':checked'))
					accessCode.prop('disabled', false);
				else
					accessCode.val('').prop('disabled', true);
			});
			$('#email-tab-security-access-code').keydown(function (event)
			{
				if (event.keyCode == 46 || event.keyCode == 8)
				{
				}
				else
				{
					if (event.keyCode < 48 || event.keyCode > 57)
						event.preventDefault();
				}
			});

			emailPage.find('.page-footer .buttons .accept').off('click').on('click', function ()
			{
				var subtitle = $('#email-tab-page-name').val();
				var pinCode = $('#email-tab-security-access-code').val();
				var logo = $('#email-tab-logo').find('li.selected img').prop('src');
				var now = new Date();
				$.ajax({
					type: "POST",
					url: window.BaseUrl + "qBuilder/addPageLite",
					data: {
						linkId: emailPage.find('.service-data .link-id').text(),
						createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
						subtitle: subtitle,
						logo: logo,
						expiresInDays: emailPage.find('.email-tab-expire-toggle:checked').val(),
						restricted: $('#email-tab-security-require-login').is(':checked'),
						pinCode: pinCode,
						disableWidgets: $('#email-tab-options-disable-widgets').is(':checked'),
						disableBanners: $('#email-tab-options-disable-banners').is(':checked'),
						showLinksAsUrl: $('#email-tab-options-enable-blue-links').is(':checked'),
						recordActivity: $('#email-tab-security-email-send').is(':checked'),
						activityEmailCopy: $('#email-tab-security-email-address').val()
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
					success: function (msg)
					{
						$.mobile.pageContainer.pagecontainer("change", "#link-viewer",
							{
								transition: "slidefade",
								direction: "reverse"
							});
						if (subtitle != '')
							window.open('mailto: ?subject=' + subtitle + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
						else
							window.open('mailto: ?body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
					},
					async: true,
					dataType: 'html'
				});
			});
		};

		this.show = function ()
		{
			$.mobile.pageContainer.pagecontainer("change", "#email-page", {
				transition: "slidefade"
			});
		};
	};
	$.SalesPortal.EmailManager = new EmailManager();
})(jQuery);