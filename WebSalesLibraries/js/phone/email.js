(function ($)
{
	var updateSummary = function ()
	{
		$('#email-to-summary').html($('#email-to').val());
		$('#email-to-copy-summary').html($('#email-to-copy').val());
		$('#email-from-summary').html($('#email-from').val());
		$('#email-subject-summary').html($('#email-subject').val());
		$('#email-body-summary').html($('#email-body').val());
	};

	$.sendEmail = function (linkId)
	{
		$.ajax({
			type: "POST",
			url: "site/emailLinkSend",
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

	$.addLitePage = function (linkId)
	{
		var subtitle = $('#add-page-name').val();
		var now = new Date();
		$.ajax({
			type: "POST",
			url: "qbuilder/addPageLite",
			data: {
				linkId: linkId,
				createDate: now.toLocaleDateString() + ' ' + now.toLocaleTimeString(),
				subtitle: subtitle,
				logo: $('#add-page-logo').find('.page-content').find('li.qpage-logo-selected').find('img').attr('src'),
				expiresInDays: $('#add-page-expires-in').val(),
				restricted: $('#add-page-restricted').is(':checked'),
				showLinkToMainSite: $('#add-page-show-link-to-main-site').is(':checked')
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
			success: function (msg)
			{
				$.mobile.changePage("#preview", {
					transition: "slidefade"
				});
				window.open('mailto: ?subject=' + subtitle + '&body=' + '%0D%0A%0D%0A%0D%0A%0D%0A%0D%0A' + msg, "_self");
			},
			error: function ()
			{
			},
			async: true,
			dataType: 'html'
		});
	};

	$(document).ready(function ()
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

		$('#email-summary').on('pageshow', function (e)
		{
			updateSummary();
			return true;
		});

		$('#add-page-logo').find('.page-content').find('li').on('click', function (e)
		{
			$('#add-page-logo').find('.page-content').find('li').attr("data-theme", "c").removeClass("ui-btn-up-e").removeClass('ui-btn-hover-e').removeClass('qpage-logo-selected').addClass("ui-btn-up-c").addClass('ui-btn-hover-c');
			$(this).attr("data-theme", "e").removeClass("ui-btn-up-c").removeClass('ui-btn-hover-c').addClass("ui-btn-up-e").addClass('ui-btn-hover-e').addClass('qpage-logo-selected');
		});
	});
})(jQuery);