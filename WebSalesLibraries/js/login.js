(function( $ )    {
    var updateLoginBodyPosition = function(){
        var top = ($(window).height() - $('#formLogin').height())/2;
        var left = ($(window).width() - $('#formLogin').width())/2;
        $('#formLogin').css({
            'left':left+'px'
        });
        $('#formLogin').css({
            'top':top+'px'
        });
    }
        
    $(document).ready(function() 
    {
        updateLoginBodyPosition();
        
        $(window).on('resize',updateLoginBodyPosition); 
            
        $('#fieldLogin').on('click',function(){
            if(this.value == 'Username')
                this.value='';
        }); 
        $('#fieldLogin').on('blur',function(){
            if(this.value == '')
                this.value='Username';
        });         
        $('#fieldPassword').on('click',function(){
            if(this.value == 'Password')
                this.value='';
        }); 
        $('#fieldPassword').on('blur',function(){
            if(this.value == '')
                this.value='Password';
        });                 
    });
})( jQuery );