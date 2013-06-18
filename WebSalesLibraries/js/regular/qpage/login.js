(function( $ )    {
    var updateLoginBodyPosition = function(){
		var formLogin = $('#form-login');
        var top = ($(window).height() - formLogin.height())/2;
        var left = ($(window).width() - formLogin.width())/2;
		formLogin.css({
            'left':left+'px'
        });
		formLogin.css({
            'top':top+'px'
        });
    };
    
    $(document).ready(function()
    {
		$("#pin-code").keydown(function (event)
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
        updateLoginBodyPosition();
        $(window).on('resize',updateLoginBodyPosition); 
    });
})( jQuery );