(function( $ ) {
    var storedTextSize = $.cookie("textSize");
    if(storedTextSize==null)
        storedTextSize = 12;
    
    var storedTextSpace = $.cookie("textSpace");
    if(storedTextSpace==null)
        storedTextSpace = 2;
    
    $.libraryChanged = function(){
        var selectedLibraryName = $("#select-library .btn .list-item-name").text();
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

                $("#page-logo").attr('src', $("#select-library .btn .list-item-info .list-item-image-path").text());
                $('#libraries-selector-title').html(selectedLibraryName);                                    
                
                $('#select-page .dropdown-menu li').on('click',function(){
                    var a = $(this).find('a');
                    $('#select-page a.btn').html($(this).html());
                    $('#select-page a.btn').append($('<span class="caret"></span>')).find('a').replaceWith($('<span class="list-item-name"/>').html(a.html()));
                    $.pageChanged();
                });
                
                $.pageChanged();
            },
            async: true,
            dataType: 'html'            
        });     
    }
    
    $.pageChanged = function(){
        var selectedPageName = $("#select-page .btn .list-item-name").text();
        $.cookie("selectedPageName", selectedPageName, {
            expires: 60 * 60 * 24 * 7
        });
        $("#page-logo").attr('src', $("#select-page .btn .list-item-info .list-item-image-path").text());
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
                $('#content').html('<div>'+msg+'</div>');
                $.updateTextSize(storedTextSize);
                $.updateTextSpace(storedTextSpace);
                $.updateContentAreaDimensions();            
                $('.clickable').off('click');        
                $('.clickable').on('click',$.openViewDialogEmbedded);        
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
                
                $('#select-library .dropdown-menu li').on('click',function(){
                    var a = $(this).find('a');
                    $('#select-library a.btn').html($(this).html());
                    $('#select-library a.btn').append($('<span class="caret"></span>')).find('a').replaceWith($('<span class="list-item-name"/>').html(a.html()));
                    $.libraryChanged();
                });
                
                $.libraryChanged();
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
        $(window).on('resize',$.updateContentAreaDimensions);         
        
        $('#increase-text-space').off('click'); 
        $('#increase-text-space').on('click',function(){
            if(storedTextSpace < 3)
            {
                storedTextSpace++;
                $.updateTextSpace(storedTextSpace);
            }
        });
        $('#decrease-text-space').off('click'); 
        $('#decrease-text-space').on('click',function(){
            if(storedTextSpace > 1)
            {
                storedTextSpace--;
                $.updateTextSpace(storedTextSpace);
            }
        });
        
        $('#increase-text-size').off('click'); 
        $('#increase-text-size').on('click',function(){
            if(storedTextSize<22)
                storedTextSize++;
            $.updateTextSize(storedTextSize);
        });
        
        $('#decrease-text-size').off('click'); 
        $('#decrease-text-size').on('click',function(){
            if(storedTextSize>8)
                storedTextSize--;
            $.updateTextSize(storedTextSize);
        });                    
    });
})( jQuery );    