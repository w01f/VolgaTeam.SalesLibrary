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
    };
    
    $.libraryChanged = function(){
        var selectedLibraryName = $("#libraries-selector").find(":selected").text();
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
                $('#page-selector').html(msg).selectmenu('refresh', true);
            },
            async: true,
            dataType: 'html'            
        });     
    };
    
    $.pageChanged = function(){
        var selectedPageName = $("#page-selector").find(":selected").text();
        $.cookie("selectedPageName", selectedPageName, {
            expires: 60 * 60 * 24 * 7
        });
    };
    
    $.loadPage = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getFoldersList",
            data:{},
            beforeSend: function(){
                $('#folders').find('.page-content').html('');
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
                $('#folders').find('.page-content').html(msg);
                $('#folders').find('.library-title').html($.cookie("selectedLibraryName"));
                $('#links').find('.library-title').html($.cookie("selectedLibraryName"));
                $('#link-details').find('.library-title').html($.cookie("selectedLibraryName"));
                $('#gallery-page').find('.library-title').html($.cookie("selectedLibraryName"));
                $.mobile.changePage( "#folders", {
                    transition: "slidefade"
                });
                $('#folders').find('.page-content').children('ul').listview();
                $( ".folder-link" ).on('click',function(){
                    var folderId = $.trim($(this).attr("href").replace('#folder', ''));
                    $.loadFolder(folderId);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    };
    
    $.loadFolder = function(folderId){
        $.ajax({
            type: "POST",
            url: "wallbin/getFolderLinksList",
            data:{
                folderId: folderId
            },
            beforeSend: function(){
                $('#links').find('.page-content').html('');
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
                $('#links').find('.page-content').html(msg);
                $.mobile.changePage( "#links", {
                    transition: "slidefade"
                });
                $('#links').find('.page-content').children('ul').listview();
                $( ".file-link" ).on('click',function(){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLink(selectedLink,false,false);
                });
                $( ".file-link-detail" ).on('click',function(event){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLinkDeatils(selectedLink,false);
                    event.stopPropagation();
                });                                
            },
            async: true,
            dataType: 'html'                        
        });
    };
    
    $.loadLink = function(linkId, isSearch, isAttachment){
        $.ajax({
            type: "POST",
            url: isAttachment?"wallbin/getAttachmentPreviewList":"wallbin/getLinkPreviewList",
            data:{
                linkId: linkId
            },
            beforeSend: function(){
                $('#preview').find('.page-content').html('');
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
                $('#preview').find('.page-content').html(msg);
                $('#preview').find('.library-title').html(isSearch?'Search':$.cookie("selectedLibraryName"));
                $('.email-tab .library-title').html(isSearch?'Search':$.cookie("selectedLibraryName"));
                $('#preview').find('.link.back').attr('href',isAttachment?'#link-details':(isSearch?'#search-result':'#links'));
                $.mobile.changePage( "#preview", {
                    transition: "slidefade"
                });
                $('#preview').find('.page-content').children('ul').listview();
                $('#preview').find('.res-selector').navbar();
                
                $('.file-size.regular').hide();
                $('.file-size.phone').show();
                $('.res-selector a').on('click',function(){
                    if($('a.low-res-button').hasClass('ui-btn-active'))
                    {
                        $('.file-size.regular').hide();
                        $('.file-size.phone').show();                        

                    }
                    else
                    {
                        $('.file-size.regular').show();
                        $('.file-size.phone').hide();                        
                    }
                });
                
                $( ".preview-link" ).on('click',function(){
                    var itemContent = $(this).find('.item-content');
                    var viewFormat = itemContent.find('.view-type').html().toUpperCase();
                    
                    var resolution = 'hi';
                    if($('.res-selector .low-res-button').hasClass('ui-btn-active'))
                        var resolution = 'low';
                    else if($('.res-selector .hi-res-button').hasClass('ui-btn-active'))
                        var resolution = 'hi';                    
                    
                    if(viewFormat == 'PNG' || viewFormat == 'JPEG')
                    {
                        var galleryHeader = $('#preview').find('.link-container').first().clone();
                        var previewInfo = '';
                        if(resolution == 'hi')
                            previewInfo += 'High Resolution - ';
                        else if(resolution == 'low')
                            previewInfo += 'Low Resolution - ';
                        previewInfo += viewFormat + ' Images';
                        galleryHeader.find('.file').html(previewInfo);
                    
                        $('#gallery-title').html('').append(galleryHeader);
                    }
                    
                    $.viewSelectedFormat(itemContent,resolution);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    };
    
    $.loadLinkDeatils = function(linkId,isSearch){
        $.ajax({
            type: "POST",
            url: "wallbin/getLinkDetails",
            data:{
                linkId: linkId
            },
            beforeSend: function(){
                $('#preview').find('.page-content').html('');
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
                $('#link-details').find('.page-content').html(msg);
                $('#link-details').find('.link.back').attr('href',isSearch?'#search-result':'#links');
                $.mobile.changePage( "#link-details", {
                    transition: "slidefade"
                });
                $('#link-details').find('.page-content').children('ul').listview();
                $( ".file-card-link" ).on('click',function(){
                    var linkId = $.trim($(this).attr("href").replace('#file-card', ''));
                    $.viewFileCard(linkId);
                });
                $( ".attachment-link" ).on('click',function(){
                    var attachmentId = $.trim($(this).attr("href").replace('#attachment', ''));
                    $.loadLink(attachmentId,isSearch,true);
                });                    
            },
            async: true,
            dataType: 'html'                        
        });
    };
    
    $(document).ready(function() 
    {
        $('#libraries-selector').on('change',function(){
            $.libraryChanged();
        });
        $('#page-selector').on('change',function(){
            $.pageChanged();
        });        
        $('#load-page-button').off('click').on('click',function(){
            $.loadPage();
        });                
    });
})( jQuery );    