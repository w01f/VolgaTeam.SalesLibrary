(function( $ ) {
    $.updateTextSpace = function(textSpace){
        if(textSpace==1){
            $('.linkContainer').css('margin-bottom','5px');
        }
        else if(textSpace==2){
            $('.linkContainer').css('margin-bottom','9px');
        }
        else if(textSpace==3){
            $('.linkContainer').css('margin-bottom','14px');
        }
        
        $.cookie("textSpace", textSpace, {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.updateTextSize = function(textSize){
        $('.linkText').css('font-size',textSize+'pt');
        
        $.cookie("textSize", textSize, {
            expires: (60 * 60 * 24 * 7)
        });
    }  
})( jQuery );    