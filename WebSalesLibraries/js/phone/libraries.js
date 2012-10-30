(function( $ ) {
    var selectedLibrary = '';
    $.initLibraries = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryCollapsibleList",
            beforeSend: function(){
                $('#libraries .page-content').html('');
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
                $('#libraries .page-content').html(msg);
                $('.library-item-container > ul').listview();                
                $('#libraries .page-content > div').collapsibleset();
                $( ".library-item-container" ).on('expand',function(){
                    selectedLibrary = $.trim($(this).find('.library-title').text());
                });
                $( ".folder-link" ).on('click',function(){
                    var substr = $(this).attr("href").split('-folder-');
                    
                    var selectedPage = $.trim(substr[0].replace('#', ''));
                    var selectedFolder = $.trim(substr[1]);
                    $.loadFolder(selectedLibrary,selectedPage,selectedFolder);
                });
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.loadFolder = function(library, page, folderId){
        $.ajax({
            type: "POST",
            url: "wallbin/getFolderLinksList",
            data:{
                selectedLibrary: library,
                selectedPage: page,
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
                $('#links .library-title').html(library);
                $.mobile.changePage( "#links", {
                    transition: "slidefade"
                });
                $('#links .page-content').children('ul').listview();                     
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.collapseAllLibraries = function(){
        $( "#libraries .page-content > div" ).children().trigger("collapse");
    }
    
    $(document).ready(function() 
    {
        });
})( jQuery );    