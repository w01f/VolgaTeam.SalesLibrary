(function( $ ) {
    $.updateContentAreaWidth = function(){
        $('#content').css({
            'height':($(window).height() - $('#ribbon').height()-8)+'px'
        });
    }
    
    $.updateColumnsWidth = function(){
        var columnWidth = $('#content').width()/3;
        var rightColumnWidth = $('#content').width() - (columnWidth*2);
        $('#column0').css({
            'width':columnWidth+'px'
        });
        $('#column-header-container-0').css({
            'width':columnWidth+'px'
        });        
        $('#column1').css({
            'width':columnWidth+'px'
        });
        $('#column-header-container-1').css({
            'width':columnWidth+'px'
        });        
        $('#column2').css({
            'width':rightColumnWidth+'px'
        });
        $('#column-header-container-2').css({
            'width':rightColumnWidth+'px'
        });        
    }
})( jQuery );    