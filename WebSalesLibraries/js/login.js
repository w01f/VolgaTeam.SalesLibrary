(function( $ )    {
    var updateLoginBodyPosition = function(){
        var top = ($(window).height() - $('#form-login').height())/2;
        var left = ($(window).width() - $('#form-login').width())/2;
        $('#form-login').css({
            'left':left+'px'
        });
        $('#form-login').css({
            'top':top+'px'
        });
    }
        
    $(document).ready(function() 
    {
        updateLoginBodyPosition();
        $(window).on('resize',updateLoginBodyPosition); 
    });
})( jQuery );