(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };
	var EmailManager = function ()
	{
		this.init = function()
		{
			$('#email-existed-list').find('input[type="checkbox"]').checkboxradio();

			$('#email-to-select-button').off('click').on('click', function ()
			{
				$.mobile.changePage('#email-to-existed-list', {
					transition: "pop"
				});
			});
			$('#email-to-copy-select-button').off('click').on('click', function ()
			{
				$.mobile.changePage('#email-to-copy-existed-list', {
					transition: "pop"
				});
			});
			$('#email-to-apply-button').off('click').on('click', function ()
			{
				var selectedEmails = [];
				$.each($('#email-to-existed-list-container').find('.existed-email-to:checked'), function ()
				{
					selectedEmails.push($(this).val());
				});
				if (selectedEmails.length > 0)
					$('#email-to').val(selectedEmails.join('; '));
				else
					$('#email-to').val('');
				$("#email-to-existed-list").dialog("close");
			});
			$('#email-to-copy-apply-button').off('click').on('click', function ()
			{
				var selectedEmails = [];
				$.each($('#email-to-copy-existed-list-container').find('.existed-email-to-copy:checked'), function ()
				{
					selectedEmails.push($(this).val());
				});
				if (selectedEmails.length > 0)
					$('#email-to-copy').val(selectedEmails.join('; '));
				else
					$('#email-to-copy').val('');
				$("#email-to-copy-existed-list").dialog("close");
			});

			$('#email-summary').on('pageshow', function ()
			{
				updateSummary();
				return true;
			});
			$('#add-page-name-enabled').on('change', function ()
			{
				if ($(this).is(':checked'))
					$('#add-page-name').textinput('enable');
				else
					$('#add-page-name').val('').textinput('disable');
			});
			$('#add-page-info-disclaimer').on('click', function ()
			{
				var infoDialog = $('#info-dialog');
				infoDialog.find('.dialog-description').text('You are Sending a WEB LINK to this file over the internet. The Recipient will receive an email with the website Link. Tell your recipient to click this link to view or download this file…');
				infoDialog.find('.dialog-title').text('Important Info you should KNOW about EMAILING LINKS');
				$.mobile.changePage("#info-dialog");
			});
			$('#add-page-tracking-disclaimer').on('click', function ()
			{
				var infoDialog = $('#info-dialog');
				infoDialog.find('.dialog-description').text('Enable Link Notifications if you want to know who clicks on your shared Links. DO NOT ENABLE User Login if you are sharing a Link OUTSIDE your company!');
				infoDialog.find('.dialog-title').text('Important Info you should KNOW about EMAILING LINKS');
				$.mobile.changePage("#info-dialog");
			});
			$('#add-page-options-disclaimer').on('click', function ()
			{
				var infoDialog = $('#info-dialog');
				infoDialog.find('.dialog-description').text('If you disable Widgets & Banners AND If you enable Blue Hyperlinks, then your quickSITE will be clean and simple…');
				infoDialog.find('.dialog-title').text('Important Info you should KNOW about EMAILING LINKS');
				$.mobile.changePage("#info-dialog");
			});
			$('#add-page-pin-disclaimer').on('click', function ()
			{
				var infoDialog = $('#info-dialog');
				infoDialog.find('.dialog-description').text('Create a 4-Digit Access Pin if you want to control who is ALLOWED to view your shared Link...');
				infoDialog.find('.dialog-title').text('Important Info you should KNOW about EMAILING LINKS');
				$.mobile.changePage("#info-dialog");
			});
			$('#add-page-logo').find('.page-content').find('li').on('click', function (e)
			{
				$('#add-page-logo').find('.page-content').find('li').attr("data-theme", "c").removeClass("ui-btn-up-e").removeClass('ui-btn-hover-e').removeClass('qpage-logo-selected').addClass("ui-btn-up-c").addClass('ui-btn-hover-c');
				$(this).attr("data-theme", "e").removeClass("ui-btn-up-c").removeClass('ui-btn-hover-c').addClass("ui-btn-up-e").addClass('ui-btn-hover-e').addClass('qpage-logo-selected');
			});
			$('#add-page-access-code-enabled').on('change', function ()
			{
				var accessCode = $('#add-page-access-code');
				if ($(this).is(':checked'))
					accessCode.textinput('enable');
				else
					accessCode.val('').textinput('disable');
			});
			$('#add-page-access-code').keydown(function (event)
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
			$('#add-page-record-activity').on('change', function ()
			{
				var ccEmail = $('#add-page-activity-email-copy');
				if ($(this).is(':checked'))
					ccEmail.textinput('enable');
				else
					ccEmail.val('').textinput('disable');
			});
		};

		this.sendEmail = function (linkId)
		{
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "site/emailLinkSend",
				data: {
					linkId: linkId,
					emailTo: $('#email-to').val(),
					emailCopyTo: $('#email-to-copy').val(),
					emailFrom: $('#email-from').val(),
					emailToMe: $('#email-from-copy-me').is(':checked'),
					emailSubject: $('#email-subject').val(),
					emailBody: $('#email-body').val(),
					expiresIn: $('#expires-in').val()
				},
				beforeSend: function ()
				{
					$.mobile.loading('show', {
						textVisible: false,
						html: ""
					});
				},
				success: function ()
				{
					$.mobile.changePage('#email-success-popup', {
						transition: "pop"
					});
				},
				complete: function ()
				{
					$.mobile.loading('hide', {
						textVisible: false,
						html: ""
					});
				},
				async: true,
				dataType: 'html'
			});
		};

		this.addLitePage = function (linkId)
		{
			var subtitle = $('#add-page-name').val();
			var pinCode = $('#add-page-access-code').val();
			var now = new Date();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qbuilder/addPageLite",
				data: {
					linkId: linkId,
					createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
					subtitle: subtitle,
					logo: $('#add-page-logo').find('.page-content').find('li.qpage-logo-selected').find('img').attr('src'),
					expiresInDays: $('#add-page-expires-in').val(),
					restricted: $('#add-page-restricted').is(':checked'),
					pinCode: pinCode,
					disableWidgets: $('#add-page-disable-widgets').is(':checked'),
					disableBanners: $('#add-page-disable-banners').is(':checked'),
					showLinksAsUrl: $('#add-page-show-links-as-url').is(':checked'),
					recordActivity: $('#add-page-record-activity').is(':checked'),
					activityEmailCopy: $('#add-page-activity-email-copy').val()
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
					$.mobile.changePage('#email-success-popup', {
						transition: "pop"
					});
				},
				success: function (msg)
				{
					$.mobile.changePage("#preview", {
						transition: "slidefade"
					});
					if (subtitle != '')
						window.open('mailto: ?subject=' + subtitle + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
					else
						window.open('mailto: ?body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg + (pinCode.length > 0 ? ("%0D%0APin-code: " + pinCode) : ''), "_self");
				},
				async: true,
				dataType: 'html'
			});
		};

		var updateSummary = function ()
		{
			$('#email-to-summary').html($('#email-to').val());
			$('#email-to-copy-summary').html($('#email-to-copy').val());
			$('#email-from-summary').html($('#email-from').val());
			$('#email-subject-summary').html($('#email-subject').val());
			$('#email-body-summary').html($('#email-body').val());
		};
	};
	$.SalesPortal.EmailManager = new EmailManager();
	$(document).ready(function ()
	{
		$.SalesPortal.EmailManager.init();
	});
})(jQuery);