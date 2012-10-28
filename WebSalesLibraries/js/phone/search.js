(function( $ ) {
   
    $.initSearch = function(){
        $.ajax({
            type: "POST",
            url: "search/getSearchView",
            beforeSend: function(){
                $('#content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#content').html(msg);
                $('#ribbon-title').html('Search');                
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
    });
})( jQuery );    