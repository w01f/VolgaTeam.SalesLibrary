(function( $ ) {
    var storedTextSize = $.cookie("textSize");
    if(storedTextSize==null)
        storedTextSize = 12;
    
    var storedTextSpace = $.cookie("textSpace");
    if(storedTextSpace==null)
        storedTextSpace = 2;
    
    $.libraryChanged = function(){
        var selectedLibraryName = $("#select-library :selected").text();
        $.cookie("selectedLibraryName", selectedLibraryName, {
            expires: 60 * 60 * 24 * 7
        });
        $.ajax({
            type: "POST",
            url: "wallbin/getPageDropDownList",
            beforeSend: function(){
                $('#libraries-selector-container').css({
                    'visibility':'hidden'
                });
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
                $('#libraries-selector-container').css({
                    'visibility':'visible'
                });
            },
            success: function(msg){
                $('#select-page').html(msg);
                $.pageChanged();
                $("#page-logo").attr('src', $("#select-library").val());
                $('#libraries-selector-title').html(selectedLibraryName);                                    
            },
            async: true,
            dataType: 'html'            
        });     
    }
    
    $.pageChanged = function(){
        var selectedPageName = $("#select-page :selected").text();
        $.cookie("selectedPageName", selectedPageName, {
            expires: 60 * 60 * 24 * 7
        });
        $("#page-logo").attr('src', $("#select-page").val());
        $.loadColumns();
    }
    
    $.loadColumns = function(){
        var savedState = null;
        $.ajax({
            type: "POST",
            url: "wallbin/getColumnsView",
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
                $('.clickable').on('click',$.openViewDialogEmbedded);        
                $('.view-dialog-content .format-list .item').on('click',$.viewSelectedFormat);        
            },
            error: function(){
                $('#content').html('');
            },
            async: true,
            dataType: 'html'
        });     
    }
    
    $.initColumnsView = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryDropDownList",
            beforeSend: function(){
                $('#content').html('');
                $.showOverlay();
                $('#libraries-selector-container').css({
                    'visibility':'hidden'
                });
            },
            complete: function(){
                $('#libraries-selector-container').css({
                    'visibility':'visible'
                });
                $.hideOverlay();
            },
            success: function(msg){
                $('#select-library').html(msg);
                $.libraryChanged();
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
        $('#select-library').on('change',function(){
            $.libraryChanged();
        });
        $('#select-page').on('change',function(){
            $.pageChanged();
        });
        
        $(window).on('resize',$.updateContentAreaWidth);         
        
        $('#increase-text-space').on('click',function(){
            if(storedTextSpace < 3)
            {
                storedTextSpace++;
                $.updateTextSpace(storedTextSpace);
            }
        });
        $('#decrease-text-space').on('click',function(){
            if(storedTextSpace > 1)
            {
                storedTextSpace--;
                $.updateTextSpace(storedTextSpace);
            }
        });
        
        $('#increase-text-size').on('click',function(){
            if(storedTextSize<22)
                storedTextSize++;
            $.updateTextSize(storedTextSize);
        });
        $('#decrease-text-size').on('click',function(){
            if(storedTextSize>8)
                storedTextSize--;
            $.updateTextSize(storedTextSize);
        });                    
    });
})( jQuery );    