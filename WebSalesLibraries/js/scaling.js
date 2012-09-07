(function( $ ) {
    $.updateContentAreaWidth = function(){
        var height = $(window).height() - $('#ribbon').height()-10;
        $('#content').css({
            'height':height+'px'
        });
        $('#search-grid').css({
            'height':height+'px'
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