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
        updateLoginBodyPosition();
        $(window).on('resize',updateLoginBodyPosition); 
    });
})( jQuery );