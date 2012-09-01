(function( $ ) {
    $.updateContentAreaWidth = function(){
        $('#content').css({
            'width':$(window).width()+'px'
        });
        $('#content').css({
            'height':($(window).height() - $('#ribbon').height()-5)+'px'
        });
    }
    
    $.updateColumnsWidth = function(){
        
        var columnWidth = $('#content').width()/3;
        var rightColumnWidth = $('#content').width() - (columnWidth*2);
        $('#column0').css({
            'width':columnWidth+'px'
        });
        $('#columnHeaderContainer0').css({
            'width':columnWidth+'px'
        });        
        $('#column1').css({
            'width':columnWidth+'px'
        });
        $('#columnHeaderContainer1').css({
            'width':columnWidth+'px'
        });        
        $('#column2').css({
            'width':rightColumnWidth+'px'
        });
        $('#columnHeaderContainer2').css({
            'width':rightColumnWidth+'px'
        });        
    }
})( jQuery );    