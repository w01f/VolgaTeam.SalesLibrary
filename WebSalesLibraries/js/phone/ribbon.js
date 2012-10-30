(function( $ ) {
    $(document).ready(function() {
        $.initLibraries();
        $.initSearch();
        
        $('.logout-button').off('click'); 
        $('.logout-button').on('click',function(){
            $.logout();
        });                    
        
        $('#button-cllapse-all').off('click'); 
        $('#button-cllapse-all').on('click',function(){
            $.collapseAllLibraries();
        });                    
    });
})( jQuery );