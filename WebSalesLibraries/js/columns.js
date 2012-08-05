(function( $ ) {
    
    var showOverlay = function(){
        $('#contentOverlay').css({
            'width':$(window).width()+'px'
        });
        $('#contentOverlay').css({
            'height':$(window).height()+'px'
        });            
        $('#contentOverlay').fadeIn(0);
        $('#content').fadeOut(0);
        $('<div id="fancybox-loading"><div></div></div>').appendTo('body'); 
    }  
    var hideOverlay = function(){
        $('#fancybox-loading').remove(); 
        $('#contentOverlay').fadeOut(0);
        $('#ribbon').fadeIn(0);
        $('#content').fadeIn(0);
    }      
    
    var storedTextSize = $.cookie("textSize");
    if(storedTextSize==null)
        storedTextSize = 12;
    
    var storedTextSpace = $.cookie("textSpace");
    if(storedTextSpace==null)
        storedTextSpace = 2;
    
    var updateTextSpace = function(textSpace){
        if(textSpace==1){
            $('.linkContainer').css('margin-bottom','5px');
        }
        else if(textSpace==2){
            $('.linkContainer').css('margin-bottom','9px');
        }
        else if(textSpace==3){
            $('.linkContainer').css('margin-bottom','14px');
        }
        
        $.cookie("textSpace", textSpace, {
            path: "/", 
            expires: "60 * 60 * 24 * 7"
        });
    }
    
    var updateTextSize = function(textSize){
        $('.linkText').css('font-size',textSize+'pt');
        
        $.cookie("textSize", textSize, {
            path: "/", 
            expires: "60 * 60 * 24 * 7"
        });
    }  
    
    var libraryChanged = function(){
        var selectedLibraryName = $("#selectLibrary :selected").text();
        $.cookie("selectedLibraryName", selectedLibraryName, {
            path: "/", 
            expires: "60 * 60 * 24 * 7"
        });
        $.cookie("selectedPageName", null);        
        $.ajax({
            type: "POST",
            url: "wallbin/getPageDropDownList",
            beforeSend: function(){
                $('#librariesSelectorContainer').css({
                    'visibility':'hidden'
                });
                showOverlay();
            },
            complete: function(){
                hideOverlay();
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
            path: "/", 
            expires: "60 * 60 * 24 * 7"
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
                showOverlay();
                $('#content').html('');
            },
            complete: function(){
                hideOverlay(); 
            },
            success: function(msg){
                $('#content').html(msg);
                updateTextSize(storedTextSize);
                updateTextSpace(storedTextSpace);
                updateContentAreaWidth();            
                updateColumnsWidth(); 
                $('.clickable').on('click',openViewDialog);        
                $('.viewDialogFormatItem').on('click',viewSelectedFormat);        
            },
            error: function(){
                $('#content').html('');
            },
            async: true,
            dataType: 'html'
        });     
    }
    
    var updateContentAreaWidth = function(){
        $('#content').css({
            'width':$(window).width()+'px'
        });
        $('#content').css({
            'height':($(window).height() - $('#ribbon').height())+'px'
        });
    }
    
    var updateColumnsWidth = function(){
        
        var columnWidth = $('#content').width()/3;
        var rightColumnWidth = $('#content').width() - (columnWidth*2);
        $('#column0').css({
            'width':columnWidth+'px'
        });
        $('#columnHeaderContainer0').css({
            'width':columnWidth+'px'
        });        
        $('#column1').css({
            'width':columnWidth+'px'
        });
        $('#columnHeaderContainer1').css({
            'width':columnWidth+'px'
        });        
        $('#column2').css({
            'width':rightColumnWidth+'px'
        });
        $('#columnHeaderContainer2').css({
            'width':rightColumnWidth+'px'
        });        
    }
    
    var openViewDialog = function(){
        var formatItems = $(this).find('.viewDialogFormatItem');
        var selectedFileType = formatItems.find('.viewDialogFormatServiceDataFileType').first().html();
        if(formatItems.length > 1 || selectedFileType == 'xls')
        {
            var viewDialogContent = $(this).find('.viewDialogContent').html();
            $.fancybox({
                content: $(this).find('.viewDialogBody'),
                title: 'How Do you want to Open this File?',
                minWidth: 300,
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
            $(this).find('.viewDialogContent').html(viewDialogContent);
            $(this).find('.viewDialogFormatItem').on('click',viewSelectedFormat);        
        }
        else
            viewSelectedFormat.call(formatItems);
    }
    
    var viewSelectedFormat = function()
    {
        $.fancybox.close();
        
        var selectedFileType = $(this).find('.viewDialogFormatServiceDataFileType').html()
        var selectedViewType = $(this).find('.viewDialogFormatServiceDataViewType').html()
        var selectedLinks = $(this).find('.viewDialogFormatServiceDataLinks').html()

        if(selectedFileType != ''&& selectedViewType != '' && selectedLinks != '')
        {
            selectedLinks = $.parseJSON(selectedLinks);
            switch(selectedFileType)
            {
                case 'ppt':
                case 'doc':
                case 'pdf':
                    switch(selectedViewType)
                    {
                        case 'png':
                        case 'jpeg':
                            $.fancybox(selectedLinks,{
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
                            break;
                        default:
                            window.open(selectedLinks[0].href);                            
                            break;
                    }
                    break;                    
                case 'xls':                                    
                case 'url':
                    window.open(selectedLinks[0].href);                    
                    break;                
                case 'png':
                case 'jpeg':
                    $.fancybox(selectedLinks,{
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
                    break;
            }
        }
    }
    
    $(document).ready(function() 
    {
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryDropDownList",
            beforeSend: function(){
                showOverlay();
                $('#librariesSelectorContainer').css({
                    'visibility':'hidden'
                });
            },
            complete: function(){
                $('#librariesSelectorContainer').css({
                    'visibility':'visible'
                });
                hideOverlay();
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
        
        $(window).on('resize',updateContentAreaWidth);         
        $(window).on('resize',updateColumnsWidth);
        
        $('#increaseTextSpace').on('click',function(){
            if(storedTextSpace < 3)
            {
                storedTextSpace++;
                updateTextSpace(storedTextSpace);
            }
        });
        $('#decreaseTextSpace').on('click',function(){
            if(storedTextSpace > 1)
            {
                storedTextSpace--;
                updateTextSpace(storedTextSpace);
            }
        });
        
        $('#increaseTextSize').on('click',function(){
            if(storedTextSize<22)
                storedTextSize++;
            updateTextSize(storedTextSize);
        });
        $('#decreaseTextSize').on('click',function(){
            if(storedTextSize>8)
                storedTextSize--;
            updateTextSize(storedTextSize);
        });            
    });
})( jQuery );    