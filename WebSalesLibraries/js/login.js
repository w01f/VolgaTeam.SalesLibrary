(function( $ )    {
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
        
    $(document).ready(function() 
    {
        updateLoginBodyPosition();
        
        $(window).on('resize',updateLoginBodyPosition); 
            
        $('#field-login').on('click',function(){
            if(this.value == 'Username')
                this.value='';
        }); 
        $('#field-login').on('blur',function(){
            if(this.value == '')
                this.value='Username';
        });         
        $('#field-password').on('click',function(){
            if(this.value == 'Password')
                this.value='';
        }); 
        $('#field-password').on('blur',function(){
            if(this.value == '')
                this.value='Password';
        });                 
    });
})( jQuery );