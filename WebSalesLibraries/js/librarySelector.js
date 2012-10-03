(function( $ ) {
    $.selectAllLibraries = function(){
        $('#library-checkbox-list input[type="checkbox"]').attr('checked', true);
        $('#library-checkbox-list input[type="checkbox"]').button('refresh');
        $.saveSelectedLibraries();
    }    

    $.clearAllLibraries = function(){
        $('#library-checkbox-list input[type="checkbox"]').attr('checked', false);
        $('#library-checkbox-list input[type="checkbox"]').button('refresh');
        $.saveSelectedLibraries();
    }    
    
    $.saveSelectedLibraries = function(){
        var selectedLibraryIds = [];
        $('#library-checkbox-list input:checked').each(function(){
            selectedLibraryIds.push(this.id);
        });
        $.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
            expires: (60 * 60 * 24 * 7)
        });
    }    
    
    $.initLibrarySelector = function(){
        $("#library-select-all, #library-clear-all, #library-select-save, #library-select-cancel").button();                
        $('#library-checkbox-list input[type="checkbox"]').button();
        $('#library-checkbox-list input[type="checkbox"]').off('click');
        $('#library-checkbox-list input[type="checkbox"]').on('click',$.saveSelectedLibraries);
        $("#library-select-all").off('click'); 
        $("#library-select-all").on('click',$.selectAllLibraries);
        $("#library-clear-all").off('click'); 
        $("#library-clear-all").on('click',$.clearAllLibraries);
    }    
    
    $(document).ready(function() 
    {
        });
})( jQuery );    