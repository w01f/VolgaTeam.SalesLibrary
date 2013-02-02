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
    };
    
    var updateLoginBodyPosition = function(){
		var formLogin = $('#form-login');
        var top = ($(window).height() - formLogin.height())/2;
        var left = ($(window).width() - formLogin.width())/2;
		formLogin.css({
            'left':left+'px'
        });
		formLogin.css({
            'top':top+'px'
        });
    };
    
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
                content.find('#accept-button').off('click');
                content.find('#accept-button').on('click',function(){
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
                                                content.find('#accept-button').off('click');
                                                content.find('#accept-button').on('click',function(){
                                                    $.fancybox.close();
                                                });                    
                                                $.fancybox({
                                                    content: content,
                                                    title: 'Password recovery',
                                                    openEffect  : 'none',
                                                    closeEffect	: 'none'
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
                content.find('#cancel-button').off('click');
                content.find('#cancel-button').on('click',function(){
                    $.fancybox.close();
                });                    
                $.fancybox({
                    content: content,
                    title: 'Password recovery',
                    openEffect  : 'none',
                    closeEffect	: 'none'
                });
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });          
    };
    
    var switchVersion = function(){
        $.ajax({
            type: "POST",
            url: "switchVersion",
            data: {
                siteVersion: 'mobile'
            },
            beforeSend: function(){
                $.showOverlay();
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
    };
    
    $(document).ready(function() 
    {
        $('#recover-password-link').fancybox().off('click').on('click',function(){
            recoverPassword();
        });
        
        $('#button-switch-version').off('click').on('click',function(){
            switchVersion();
        });        
        $('#button-login').off('click').on('click',function(event){
            if($('#disclaimer').length && !$("#disclaimer").attr('checked'))
            {
				event.preventDefault();
				event.stopPropagation();
				$.ajax({
					type: "POST",
					url: "disclaimerWarning",
					data: {},
					beforeSend: function(){
						$.showOverlay();
					},
					complete: function(){
						$.hideOverlay();
					},
					success: function(msg){
						var content = $(msg);
						content.find('#accept-button').off('click');
						content.find('#accept-button').on('click',function(){
							$.fancybox.close();
						});
						$.fancybox({
							content: content,
							title: 'Confirmation Needed!',
							width:450,
							autoSize:false,
							autoHeight:true,
							openEffect  : 'none',
							closeEffect	: 'none'
						});
					},
					error: function(){
					},
					async: true,
					dataType: 'html'
				});
            }
        });                
        updateLoginBodyPosition();
        $(window).on('resize',updateLoginBodyPosition); 
    });
})( jQuery );