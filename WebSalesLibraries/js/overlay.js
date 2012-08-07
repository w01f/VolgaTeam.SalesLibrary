(function( $ ) {
    $.showOverlay = function(){
        $('#contentOverlay').css({
            'width':$(window).width()+'px'
        });
        $('#contentOverlay').css({
            'height':$(window).height()+'px'
        });            
        $('#contentOverlay').fadeIn(0);
        $('#content').fadeOut(0);
        $('<div id="fancybox-loading"><div></div></div>').appendTo('body'); 
    }  
    $.hideOverlay = function(){
        $('#fancybox-loading').remove(); 
        $('#contentOverlay').fadeOut(0);
        $('#ribbon').fadeIn(0);
        $('#content').fadeIn(0);
    }      
})( jQuery );    


