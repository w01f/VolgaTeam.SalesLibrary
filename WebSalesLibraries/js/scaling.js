(function( $ ) {
    $.updateContentAreaWidth = function(){
        var height = $(window).height() - $('#ribbon').height()-10;
        $('#content').css({
            'height':height+'px'
        });
        $('#search-grid').css({
            'height':(height-5)+'px'
        });        
    }
})( jQuery );    