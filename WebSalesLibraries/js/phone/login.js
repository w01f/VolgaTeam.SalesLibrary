(function ($)
{
	$.logout = function ()
	{
		$.ajax({
			type:"POST",
			url:"site/logout",
			data:{
			},
			beforeSend:function ()
			{
			},
			complete:function ()
			{
			},
			success:function ()
			{
				location.reload();
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	};

	var switchVersion = function ()
	{
		$.ajax({
			type:"POST",
			url:"switchVersion",
			data:{
				siteVersion:'full'
			},
			beforeSend:function ()
			{
				$.mobile.loading('show', {
					textVisible:false,
					html:""
				});
			},
			complete:function ()
			{
			},
			success:function ()
			{
				location.reload();
			},
			error:function ()
			{
			},
			async:true,
			dataType:'html'
		});
	};

	$(document).ready(function ()
	{
		$('#button-login').off('click').on('click', function (event)
		{
			if ($('#disclaimer').length)
			{
				$.mobile.changePage("#disclaimer", {
					transition:"slidefade"
				});
				$('#button-login').off('click');
				event.preventDefault();
				event.stopPropagation();
			}
		});
		$('#button-accept-dislaimer').off('click').on('click', function (event)
		{
			$('#button-login').click();
		});

		$('#button-switch-version').off('click').on('click', function (e)
		{
			switchVersion();
		});

		$('#button-recover-password').off('click').on('click', function (event)
		{
			$.ajax({
				type:"POST",
				url:"validateUserByEmail",
				data:{
					login:$('#login').val(),
					email:$('#email').val()
				},
				beforeSend:function ()
				{
					$.mobile.loading('show', {
						textVisible:false,
						html:""
					});
				},
				complete:function ()
				{
					$.mobile.loading('hide', {
						textVisible:false,
						html:""
					});
				},
				success:function (msg)
				{
					if (msg != '')
						$('#recover-password').find('.error-message div').html(msg);
					else
					{
						$('#recover-password').find('.error-message div').html('');
						$.ajax({
							type:"POST",
							url:"recoverPassword",
							data:{
								login:$('#login').val()
							},
							beforeSend:function ()
							{
								$.mobile.loading('show', {
									textVisible:false,
									html:""
								});
							},
							complete:function ()
							{
								$.mobile.loading('hide', {
									textVisible:false,
									html:""
								});
							},
							success:function ()
							{
								$.mobile.changePage("#recover-password-success", {
									transition:"slidefade"
								});
							},
							async:true,
							dataType:'html'
						});
					}
				},
				error:function ()
				{
					$('#recover-password').find('.error-message div').html('Error while validating user. Try again or contact to technical support');
				},
				async:true,
				dataType:'html'
			});
		});
	});
})(jQuery);