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
	});
})(jQuery);