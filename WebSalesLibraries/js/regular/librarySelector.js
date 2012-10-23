(function( $ ) {
    $.selectAllLibraries = function(){
        $('#library-checkbox-list .btn').addClass('active');
        $.saveSelectedLibraries();
    }    

    $.clearAllLibraries = function(){
        $('#library-checkbox-list .btn').removeClass('active');
        $.saveSelectedLibraries();
    }    
    
    $.saveSelectedLibraries = function(){
        var selectedLibraryIds = [];
        $('#library-checkbox-list .btn.active').each(function(){
            selectedLibraryIds.push(this.id);
        });
        $.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
            expires: (60 * 60 * 24 * 7)
        });
    }    
    
    $.initLibrarySelector = function(){
        $('#library-checkbox-list .btn').off('click');
        $('#library-checkbox-list .btn').on('click',function(){
            if($(this).hasClass('active'))
                $(this).removeClass('active');
            else
                $(this).addClass('active');            
            $.saveSelectedLibraries();
        });
        $("#library-select-all").off('click'); 
        $("#library-select-all").on('click',$.selectAllLibraries);
        $("#library-clear-all").off('click'); 
        $("#library-clear-all").on('click',$.clearAllLibraries);
    }    
    
    $(document).ready(function() 
    {
        });
})( jQuery );    