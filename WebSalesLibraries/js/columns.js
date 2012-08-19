(function( $ ) {
    var storedTextSize = $.cookie("textSize");
    if(storedTextSize==null)
        storedTextSize = 12;
    
    var storedTextSpace = $.cookie("textSpace");
    if(storedTextSpace==null)
        storedTextSpace = 2;
    
    var libraryChanged = function(){
        var selectedLibraryName = $("#selectLibrary :selected").text();
        $.cookie("selectedLibraryName", selectedLibraryName, {
            expires: 60 * 60 * 24 * 7
        });
        $.ajax({
            type: "POST",
            url: "wallbin/getPageDropDownList",
            beforeSend: function(){
                $('#librariesSelectorContainer').css({
                    'visibility':'hidden'
                });
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
                $('#librariesSelectorContainer').css({
                    'visibility':'visible'
                });
            },
            success: function(msg){
                $('#selectPage').html(msg);
                pageChanged();
                $("#pagelogo").attr('src', $("#selectLibrary").val());
                $('#librariesSelectorTitle').html(selectedLibraryName);                                    
            },
            async: true,
            dataType: 'html'            
        });     
    }
    
    var pageChanged = function(){
        var selectedPageName = $("#selectPage :selected").text();
        $.cookie("selectedPageName", selectedPageName, {
            expires: 60 * 60 * 24 * 7
        });
        $("#pagelogo").attr('src', $("#selectPage").val());
        loadColumns();
    }
    
    var loadColumns = function(){
        var savedState = null;
        $.ajax({
            type: "POST",
            url: "wallbin/getColumnsViewAjax",
            data:savedState,
            beforeSend: function(){
                $.showOverlay();
                $('#content').html('');
            },
            complete: function(){
                $.hideOverlay(); 
            },
            success: function(msg){
                $('#content').html(msg);
                $.updateTextSize(storedTextSize);
                $.updateTextSpace(storedTextSpace);
                $.updateContentAreaWidth();            
                $.updateColumnsWidth(); 
                $('.clickable').on('click',$.openViewDialog);        
                $('.viewDialogFormatItem').on('click',$.viewSelectedFormat);        
            },
            error: function(){
                $('#content').html('');
            },
            async: true,
            dataType: 'html'
        });     
    }
    
    $(document).ready(function() 
    {
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryDropDownList",
            beforeSend: function(){
                $.showOverlay();
                $('#librariesSelectorContainer').css({
                    'visibility':'hidden'
                });
            },
            complete: function(){
                $('#librariesSelectorContainer').css({
                    'visibility':'visible'
                });
                $.hideOverlay();
            },
            success: function(msg){
                $('#selectLibrary').html(msg);
                libraryChanged();
            },
            async: true,
            dataType: 'html'                        
        });
        
        
        $('#selectLibrary').on('change',function(){
            libraryChanged();
        });
        $('#selectPage').on('change',function(){
            pageChanged();
        });
        
        $(window).on('resize',$.updateContentAreaWidth);         
        $(window).on('resize',$.updateColumnsWidth);
        
        $('#increaseTextSpace').on('click',function(){
            if(storedTextSpace < 3)
            {
                storedTextSpace++;
                $.updateTextSpace(storedTextSpace);
            }
        });
        $('#decreaseTextSpace').on('click',function(){
            if(storedTextSpace > 1)
            {
                storedTextSpace--;
                $.updateTextSpace(storedTextSpace);
            }
        });
        
        $('#increaseTextSize').on('click',function(){
            if(storedTextSize<22)
                storedTextSize++;
            $.updateTextSize(storedTextSize);
        });
        $('#decreaseTextSize').on('click',function(){
            if(storedTextSize>8)
                storedTextSize--;
            $.updateTextSize(storedTextSize);
        });            
    });
})( jQuery );    