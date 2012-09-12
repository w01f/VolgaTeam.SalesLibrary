(function( $ ) {
    $.selectLibraries = function(){
        $.ajax({
            type: "POST",
            url: "search/getLibraryCheckboxList",
            beforeSend: function(){
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
            },
            success: function(msg){
                $.fancybox({
                    content: msg,
                    title: 'Select Stations',
                    helpers: {
                        overlay : {
                            css : {
                                'background-color' : '#eee'
                            }
                        }
                    },
                    openEffect  : 'none',
                    closeEffect	: 'none'            
                });                
                $.initLibrarySelector();
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });  
    }    

    $.selectAllLibraries = function(){
        $('#library-checkbox-list input[type="checkbox"]').attr('checked', true);
    }    

    $.clearAllLibraries = function(){
        $('#library-checkbox-list input[type="checkbox"]').attr('checked', false);
    }    
    
    $.saveSelectedLibraries = function(){
        var selectedLibraryIds = [];
        $('#library-checkbox-list input:checked').each(function(){
            selectedLibraryIds.push(this.id);
        });
        $.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
            expires: (60 * 60 * 24 * 7)
        });
        $.getSelectedLibraries();
        $.fancybox.close();
    }    
    
    $.getSelectedLibraries = function(){
        $.ajax({
            type: "POST",
            url: "search/getSelectedLibraries",
            success: function(msg){
                $('#library-selected').html(msg);
            },
            async: true,
            dataType: 'html'                        
        });  
    }    
    
    $.initLibrarySelector = function(){
        $("#library-select-all, #library-clear-all, #library-select-save, #library-select-cancel").button();                
        $("#library-select-all").on('click',$.selectAllLibraries);
        $("#library-clear-all").on('click',$.clearAllLibraries);
        $("#library-select-save").on('click',$.saveSelectedLibraries);
        $("#library-select-cancel").on('click',$.fancybox.close);
    }    
    
    $(document).ready(function() 
    {
        });
})( jQuery );    