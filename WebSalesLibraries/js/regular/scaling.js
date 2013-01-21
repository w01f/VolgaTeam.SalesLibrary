(function( $ ) {
    $.updateContentAreaDimensions = function(){
        var height = $(window).height() - $('#ribbon').height()-10;
        $('body').css({
            'height':'auto'
        });        
        $('#content').css({
            'height':height+'px'
        });
        $('#content').find('>div').css({
            'height':height+'px'
        });        
        $.updateSearchAreaDimensions();
    };
    
    $.updateSearchAreaDimensions = function(){
        var height = $('#content').height();
        $('#right-navbar').find('> div').css({
            'height':height+'px'
        });        
        $('#search-result').find('> div').css({
            'height':height+'px'
        });    
        $('#categories-container').css({
            'height':(height-147)+'px'
        });                
        $('#libraries-container').css({
            'height':(height-142)+'px'
        });                        
        
        $('#file-types-container').css({
            'height':(height-47)+'px'
        });                                
        $.updateSearchGridDimensions();
    };
    
    $.updateSearchGridDimensions = function(){
        $('#search-grid-body-container').css({
            'height':($('#search-result').find('> div').height() -($('#search-grid-info').height()+12)- $('#search-grid-header').height())+'px'
        });        
        
        var linkDateWidth = 140;

        var linkNameHeaderWidth = $('#search-result').width()- $('#search-grid-header').find('td.details-button').width() - $('#search-grid-header').find('td.library-column').width() - $('#search-grid-header').find('td.link-type-column').width() -linkDateWidth;
        $('#search-grid-header').find('td.link-name-column').css({
            'width':linkNameHeaderWidth+'px'
        });
        
        var linkNameBodyWidth = $('#search-result').width() - $('#search-grid-body').find('td.details-button').width()- $('#search-grid-body').find('td.library-column').width() - $('#search-grid-body').find('td.link-type-column').width() -linkDateWidth;
        $('#search-grid-body').find('td.link-name-column').css({
            'width':linkNameBodyWidth+'px'
        });
    };
})( jQuery );    