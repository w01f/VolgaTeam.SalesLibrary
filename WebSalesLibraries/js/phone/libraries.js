(function( $ ) {
    $.initLibraries = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryDropDownList",
            beforeSend: function(){
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#libraries-selector').html(msg);
                $('#libraries-selector').selectmenu('refresh', true);
                $.libraryChanged();
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.libraryChanged = function(){
        var selectedLibraryName = $("#libraries-selector :selected").text();
        $.cookie("selectedLibraryName", selectedLibraryName, {
            expires: 60 * 60 * 24 * 7
        });
        $.ajax({
            type: "POST",
            url: "wallbin/getPageDropDownList",
            beforeSend: function(){
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#page-selector').html(msg);
                $('#page-selector').selectmenu('refresh', true);
            },
            async: true,
            dataType: 'html'            
        });     
    }    
    
    $.pageChanged = function(){
        var selectedPageName = $("#page-selector :selected").text();
        $.cookie("selectedPageName", selectedPageName, {
            expires: 60 * 60 * 24 * 7
        });
    }    
    
    $.loadPage = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getFoldersList",
            data:{},
            beforeSend: function(){
                $('#folders .page-content').html('');
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#folders .page-content').html(msg);
                $('#folders .library-title').html($.cookie("selectedLibraryName"));                
                $('#links .library-title').html($.cookie("selectedLibraryName"));
                $('#link-details .library-title').html($.cookie("selectedLibraryName"));
                $('#preview .library-title').html($.cookie("selectedLibraryName"));
                $('#gallery-page .library-title').html($.cookie("selectedLibraryName"));
                $.mobile.changePage( "#folders", {
                    transition: "slidefade"
                });
                $('#folders .page-content').children('ul').listview();                     
                $( ".folder-link" ).on('click',function(){
                    var folderId = $.trim($(this).attr("href").replace('#folder', ''));
                    $.loadFolder(folderId);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    }    
    
    $.loadFolder = function(folderId){
        $.ajax({
            type: "POST",
            url: "wallbin/getFolderLinksList",
            data:{
                folderId: folderId
            },
            beforeSend: function(){
                $('#links .page-content').html('');
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#links .page-content').html(msg);
                $.mobile.changePage( "#links", {
                    transition: "slidefade"
                });
                $('#links .page-content').children('ul').listview();                     
                $( ".file-link" ).on('click',function(){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLink(selectedLink,false);
                });
                $( ".file-link-detail" ).on('click',function(event){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLinkDeatils(selectedLink);
                    event.stopPropagation();
                });                                
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.loadLink = function(linkId, isAttachment){
        $.ajax({
            type: "POST",
            url: isAttachment?"wallbin/getAttachmentPreviewList":"wallbin/getLinkPreviewList",
            data:{
                linkId: linkId
            },
            beforeSend: function(){
                $('#preview .page-content').html('');
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#preview .page-content').html(msg);
                $('#preview .link.back').attr('href',isAttachment?'#link-details':'#links');
                $.mobile.changePage( "#preview", {
                    transition: "slidefade"
                });
                $('#preview .page-content').children('ul').listview();                     
                $('#preview .res-selector').navbar();                     
                $( ".preview-link" ).on('click',function(){
                    var itemContent = $(this).find('.item-content');
                    var viewFormat = itemContent.find('.view-type').html().toUpperCase();
                    
                    var resolution = 'low';
                    if($('.res-selector .hi-res-button').hasClass('ui-btn-active'))
                        var resolution = 'hi';
                    
                    if(viewFormat == 'PNG' || viewFormat == 'JPEG')
                    {
                        var galleryHeader = $('#preview .link-container').first().clone();
                        var previewInfo = '';
                        if(resolution == 'hi')
                            previewInfo += 'High Resolution - ';
                        else if(resolution == 'low')
                            previewInfo += 'Low Resolution - ';
                        previewInfo += viewFormat + ' Images';
                        galleryHeader.find('.file').html(previewInfo);
                    
                        $('#gallery-title').html('');
                        $('#gallery-title').append(galleryHeader);
                    }
                    
                    $.viewSelectedFormat(itemContent,resolution);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.loadLinkDeatils = function(linkId){
        $.ajax({
            type: "POST",
            url: "wallbin/getLinkDetails",
            data:{
                linkId: linkId
            },
            beforeSend: function(){
                $('#preview .page-content').html('');
                $.mobile.loading( 'show', {
                    textVisible: false,
                    html: ""
                });
            },
            complete: function(){
                $.mobile.loading( 'hide', {
                    textVisible: false,
                    html: ""
                });
            },
            success: function(msg){
                $('#link-details .page-content').html(msg);
                $.mobile.changePage( "#link-details", {
                    transition: "slidefade"
                });
                $('#link-details .page-content').children('ul').listview();                     
                $( ".file-card-link" ).on('click',function(){
                    });
                $( ".attachment-link" ).on('click',function(){
                    var attachmentId = $.trim($(this).attr("href").replace('#attachment', ''));
                    $.loadLink(attachmentId,true);
                });                    
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $(document).ready(function() 
    {
        $('#libraries-selector').on('change',function(){
            $.libraryChanged();
        });
        $('#page-selector').on('change',function(){
            $.pageChanged();
        });        
        $('#load-page-button').off('click');        
        $('#load-page-button').on('click',function(){
            $.loadPage();
        });                
    });
})( jQuery );    