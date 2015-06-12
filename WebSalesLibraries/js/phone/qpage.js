(function ($)
{
	window.BaseUrl = window.BaseUrl || '';
	$.SalesPortal = $.SalesPortal || { };

	var recordActivity = function (linkId)
	{
		var emailControl = $('#user-email');
		if (emailControl.length > 0)
		{
			var pageId = $('#page-id').html();
			$.ajax({
				type: "POST",
				url: window.BaseUrl + "qpage/recordActivity",
				data: {
					pageId: pageId,
					userEmail: emailControl.val(),
					linkId: linkId
				},
				async: true,
				dataType: 'html'
			});
		}
	};

	function checkEmail()
	{
		var emailControl = $('#user-email');
		if (emailControl.length > 0)
		{
			var emailValue = emailControl.val();
			var regex = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
			if (!regex.test(emailValue))
			{
				$("#email-warning-dialog").popup( "open");
				return false;
			}
		}
		return true;
	}

	$(document).ready(function ()
	{
		var mainPage = $('#quicksite');
		mainPage.find(".file-link").on('click', function ()
		{
			if (checkEmail())
			{
				var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
				recordActivity(selectedLink);
				$.SalesPortal.LinkManager.requestViewDialog(
					$(this).find('.link-id').text(),
					{
						id: '#quicksite',
						name: $('#quicksite').find('.content-header .title').text()
					},
					true
				);
			}
		});

		$('.logout-button').off('click').on('click', function (e)
		{
			e.stopPropagation();
			e.preventDefault();
			$.SalesPortal.Auth.logout();
		})
	});
})(jQuery);