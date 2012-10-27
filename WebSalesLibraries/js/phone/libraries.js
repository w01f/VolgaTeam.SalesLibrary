(function( $ ) {
   
    $.initLibraries = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryThumbnailList",
            beforeSend: function(){
                $('#content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#content').html(msg);
                $('#content div').collapsibleset();
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
        $.initLibraries();
    });
})( jQuery );    