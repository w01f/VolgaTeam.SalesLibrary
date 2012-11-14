(function( $ ) {
    $.updateContentAreaDimensions = function(){
        var height = $(window).height() - $('#ribbon').height()-10;
        $('#content').css({
            'height':height+'px'
        });
        $('#content>div').css({
            'height':height+'px'
        });        
        $.updateSearchAreaDimensions();
    }
    
    $.updateSearchAreaDimensions = function(){
        var height = $('#content').height();
        $('#right-navbar > div').css({
            'height':height+'px'
        });        
        $('#search-result > div').css({
            'height':height+'px'
        });    
        $('#categories-container').css({
            'height':(height-109)+'px'
        });                
        
        $.updateSearchGridDimensions();
    }    
    
    $.updateSearchGridDimensions = function(){
        $('#search-grid-body-container').css({
            'height':($('#search-result > div').height() - $('#search-grid-header').height())+'px'
        });        
        
        var linkDateWidth = 140;

        var linkNameHeaderWidth = $('#search-result').width()- $('#search-grid-header td.details-button').width() - $('#search-grid-header td.library-column').width() - $('#search-grid-header td.link-type-column').width() -linkDateWidth;
        $('#search-grid-header td.link-name-column').css({
            'width':linkNameHeaderWidth+'px'
        });
        
        var linkNameBodyWidth = $('#search-result').width() - $('#search-grid-body td.details-button').width()- $('#search-grid-body td.library-column').width() - $('#search-grid-body td.link-type-column').width() -linkDateWidth;
        $('#search-grid-body td.link-name-column').css({
            'width':linkNameBodyWidth+'px'
        });
    }    
})( jQuery );    