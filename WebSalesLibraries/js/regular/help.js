(function( $ ) {
    var loadHelpPage = function(tabId){
        var pageIdSelector = '#'+tabId+ ' .sel.help-page';
        var pageId = $(pageIdSelector).attr('id');
        $.ajax({
            type: "POST",
            url: "help/getPage",
            data:{
                pageId: pageId
            },
            beforeSend: function(){
                $('#content').html('');
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
            },
            success: function(msg){
                $('#content').html(msg);
                
                $('.help-link').off('click');
                $('.help-link').on('click', function(){
                    $.viewSelectedFormat($(this), false);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.initHelpView = function(tabId){
        var pageSelector = '#'+tabId+ ' .enabled.help-page';
        $('.help-page').off('click');
        $(pageSelector).on('click', function(){
            $(pageSelector).removeClass('sel');
            $(this).addClass('sel');
            loadHelpPage(tabId);
        });        
        
        loadHelpPage(tabId);
    }
    
    $(document).ready(function() 
    {
        });
})( jQuery );    