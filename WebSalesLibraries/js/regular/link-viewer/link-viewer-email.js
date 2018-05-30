(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || {};
	$.SalesPortal.PreviewEmailer = function (viewerData)
	{
		var dialogContent = $('#email-content');

		if (viewerData.config.enableLogging)
		{
			var formLogger = new $.SalesPortal.FormLogger();
			formLogger.init({
				logObject: {
					name: viewerData.name,
					fileName: viewerData.fileName,
					format: viewerData.format,
					linkId: viewerData.linkId
				},
				formContent: dialogContent
			});
		}

		dialogContent.find('#add-page-expires-in').find('.btn').on('click', function ()
		{
			dialogContent.find('#add-page-expires-in').find('.btn').removeClass('active').blur();
			$(this).addClass('active');
		});

		var logoSelector = dialogContent.find('.logo-list');
		dialogContent.find('#add-page-show-logo').off('change.email').on('change.email', function ()
		{
			if ($(this).is(':checked'))
			{
				logoSelector.removeClass('disabled');
				logoSelector.find('ul a').first().addClass('opened');
			}
			else
			{
				logoSelector.addClass('disabled');
				logoSelector.find('ul a').removeClass('opened');
			}
		});
		logoSelector.find('ul a').on('click', function ()
		{
			logoSelector.find('ul a').removeClass('opened');
			if (!logoSelector.hasClass('disabled'))
			{
				$(this).addClass('opened');
			}
		});
		dialogContent.find('#add-page-name-enabled').on('change', function ()
		{
			if (!dialogContent.find('#add-page-name-enabled').is(':checked'))
				dialogContent.find('#add-page-name').val('').attr('disabled', 'disabled');
			else
				dialogContent.find('#add-page-name').removeAttr('disabled');
		});
		dialogContent.find('#add-page-access-code-enabled').off('change.email').on('change.email', function ()
		{
			var accessCode = dialogContent.find('#add-page-access-code');
			if ($(this).is(':checked'))
				accessCode.prop("disabled", false);
			else
			{
				accessCode.prop("disabled", "disabled");
				accessCode.val('');
			}
		});
		dialogContent.find("#add-page-access-code").keydown(function (event)
		{
			if (event.keyCode === 46 || event.keyCode === 8)
			{
			}
			else
			{
				if (event.keyCode < 48 || event.keyCode > 57)
					event.preventDefault();
			}
		});
		dialogContent.find('#add-page-record-activity').off('change.email').on('change.email', function ()
		{
			var ccEmail = dialogContent.find('#add-page-activity-email-copy');
			if ($(this).is(':checked'))
				ccEmail.removeAttr('disabled');
			else
			{
				ccEmail.attr('disabled', 'disabled');
				ccEmail.val('');
			}
		});
		dialogContent.find('.send-email').on('click', function ()
		{
			var subtitle = dialogContent.find('#add-page-name').val();
			var pinCode = dialogContent.find('#add-page-access-code').val();
			var now = new Date();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qBuilder/addPageLite",
				data: {
					linkId: viewerData.linkId,
					createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
					subtitle: subtitle,
					logo: dialogContent.find('.logo-list a.opened').find('img').attr('src'),
					expiresInDays: dialogContent.find('#add-page-expires-in').find('.active').val(),
					restricted: dialogContent.find('#add-page-require-credentials').is(':checked'),
					pinCode: pinCode,
					disableWidgets: dialogContent.find('#add-page-disable-widgets').is(':checked'),
					disableBanners: dialogContent.find('#add-page-disable-banners').is(':checked'),
					showLinksAsUrl: dialogContent.find('#add-page-show-links-as-url').is(':checked'),
					recordActivity: dialogContent.find('#add-page-record-activity').is(':checked'),
					activityEmailCopy: dialogContent.find('#add-page-activity-email-copy').val()
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
					if (subtitle !== '')
						window.open('mailto:?subject=' + subtitle.replace(/&/g, '%26').replace(' ', '%20') + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode !== undefined && pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
					else
						window.open('mailto:?body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode !== undefined && pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
				},
				error: function ()
				{
				},
				async: true,
				dataType: 'html'
			});
			$.fancybox.close();
		});
	};
})(jQuery);
