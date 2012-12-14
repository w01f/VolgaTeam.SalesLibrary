(function( $ )    {
    $.logout = function(){
        $.ajax({
            type: "POST",
            url: "site/logout",
            data: {
            },
            beforeSend: function(){
            },
            complete: function(){
            },
            success: function(){
                location.reload();
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });  
    }    
    
    var switchVersion = function(){
        $.ajax({
            type: "POST",
            url: "switchVersion",
            data: {
                siteVersion: 'full'
            },
            beforeSend: function(){
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
            },
            success: function(){
                location.reload();
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });  
    }
    
    $(document).ready(function() 
    {
        $(document).on('mobileinit', function(){
            $.mobile.ajaxLinksEnabled = false;
            $.mobile.ajaxFormsEnabled = false;
        });
        
        $('#button-switch-version').off('click');
        $('#button-switch-version').on('click',function(e){
            switchVersion();
        });        
    });
})( jQuery );