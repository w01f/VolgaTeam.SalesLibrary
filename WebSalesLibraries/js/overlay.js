(function( $ ) {
    $.showOverlay = function(){
        $('#content-overlay').css({
            'width':$(window).width()+'px'
        });
        $('#content-overlay').css({
            'height':$(window).height()+'px'
        });            
        $('#content-overlay').fadeIn(0);
        $('#content').fadeOut(0);
        $('<div id="fancybox-loading"><div></div></div>').appendTo('body'); 
    }  
    $.hideOverlay = function(){
        $('#fancybox-loading').remove(); 
        $('#content-overlay').fadeOut(0);
        $('#ribbon').fadeIn(0);
        $('#content').fadeIn(0);
    }      
})( jQuery );    


