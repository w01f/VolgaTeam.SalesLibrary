(function( $ ) {
    $.initSearchView = function(){
    $.ajax({
            type: "POST",
            url: "search/getSearchView",
            beforeSend: function(){
                $('#content').html('');
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
            },
            success: function(msg){
                $('#content').html(msg);
            },
            error: function(){
                $('#content').html('');
            },            
            async: true,
            dataType: 'html'                        
        });        
    }
    
    $(document).ready(function() 
    {
    });
})( jQuery );    