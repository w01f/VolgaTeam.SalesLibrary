(function( $ )    {
    $.logout = function(){
        $.ajax({
            type: "POST",
            url: "site/logout",
            data: {
            },
            beforeSend: function(){
                $.showOverlayLight();
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
    
    var updateLoginBodyPosition = function(){
        var top = ($(window).height() - $('#form-login').height())/2;
        var left = ($(window).width() - $('#form-login').width())/2;
        $('#form-login').css({
            'left':left+'px'
        });
        $('#form-login').css({
            'top':top+'px'
        });
    }
    
    var recoverPassword = function(){
        $.ajax({
            type: "POST",
            url: "recoverPasswordDialog",
            data: {},
            beforeSend: function(){
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
            },
            success: function(msg){
                var content = $(msg);
                content.find('#accept').off('click');
                content.find('#accept').on('click',function(){
                    $.ajax({
                        type: "POST",
                        url: "validateUserByEmail",
                        data: {
                            login: content.find('#login').val(),
                            email: content.find('#email').val()
                        },
                        beforeSend: function(){
                            $.showOverlay();
                        },
                        complete: function(){
                            $.hideOverlay();
                        },
                        success: function(msg){
                            if(msg!='')
                                content.find('.error-message').html(msg);
                            else
                            {
                                $.ajax({
                                    type: "POST",
                                    url: "recoverPassword",
                                    data: {
                                        login: content.find('#login').val()
                                    },
                                    success: function(){
                                        $.fancybox.close();
                                        $.ajax({
                                            type: "POST",
                                            url: "recoverPasswordDialogSuccess",
                                            data: {},
                                            beforeSend: function(){
                                                $.showOverlay();
                                            },
                                            complete: function(){
                                                $.hideOverlay();
                                            },
                                            success: function(msg){
                                                var content = $(msg);
                                                content.find('#accept').off('click');
                                                content.find('#accept').on('click',function(){
                                                    $.fancybox.close();
                                                });                    
                                                $.fancybox({
                                                    content: content,
                                                    title: 'Password recovery',
                                                    openEffect  : 'none',
                                                    closeEffect	: 'none',
                                                    helpers: {
                                                        overlay : {
                                                            css : {
                                                                'background-color' : '#eee'
                                                            }
                                                        }
                                                    }
                                                });                
                                            },
                                            error: function(){
                                            },            
                                            async: true,
                                            dataType: 'html'                        
                                        });
                                    },
                                    async: true,
                                    dataType: 'html'                        
                                });                                
                            }
                        },
                        error: function(){
                            content.find('#error-message').html('Error while validating user. Try again or contact to technical support');
                        },            
                        async: true,
                        dataType: 'html'                        
                    });          
                });
                content.find('#cancel').off('click');
                content.find('#cancel').on('click',function(){
                    $.fancybox.close();
                });                    
                $.fancybox({
                    content: content,
                    title: 'Password recovery',
                    openEffect  : 'none',
                    closeEffect	: 'none',
                    helpers : {
                        overlay : {
                            css : {
                                'background' : 'rgba(224, 224, 224, 0.8)'
                            }
                        }
                    }
                });                
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });          
    }    
    
    $(document).ready(function() 
    {
        $('#recover-password-link').fancybox();
        $('#recover-password-link').off('click');
        $('#recover-password-link').on('click',function(){
            recoverPassword();
        });
        
        updateLoginBodyPosition();
        $(window).on('resize',updateLoginBodyPosition); 
    });
})( jQuery );