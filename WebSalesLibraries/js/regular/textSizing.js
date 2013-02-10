(function( $ ) {
    $.updateTextSpace = function(textSpace){
        if(textSpace==1){
            $('.link-container').css('margin-bottom','5px');
        }
        else if(textSpace==2){
            $('.link-container').css('margin-bottom','9px');
        }
        else if(textSpace==3){
            $('.link-container').css('margin-bottom','14px');
        }
        
        $.cookie("textSpace", textSpace, {
            expires: (60 * 60 * 24 * 7)
        });
    };
    
    $.updateTextSize = function(textSize){
        $('.link-text, .link-note').css('font-size',textSize+'pt');
        
        $.cookie("textSize", textSize, {
            expires: (60 * 60 * 24 * 7)
        });
    };
})( jQuery );    