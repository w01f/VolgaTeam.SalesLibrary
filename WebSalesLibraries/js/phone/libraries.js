(function( $ ) {
    var selectedLibrary = '';
    $.initLibraries = function(){
        $.ajax({
            type: "POST",
            url: "wallbin/getLibraryThumbnailList",
            beforeSend: function(){
                $('#content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#content').html(msg);
                $('#content div').collapsibleset();
                $('#ribbon-title').html('Sales Libraries');
                $( ".library-item-container" ).on('expand',function(){
                    selectedLibrary = $.trim($(this).find('.library-name').text());
                });
                $( ".page-name" ).on('click',function(){
                    $.loadPage(selectedLibrary,$.trim($(this).html()));
                });
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    $.loadPage = function(library, page){
        $.ajax({
            type: "POST",
            url: "wallbin/getColumnsView",
            data:{
                selectedLibrary: library,
                selectedPage: page
            },
            beforeSend: function(){
                $('#content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#content').html(msg);
                loadContent('#content > .item-content');
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    var loadContent = function(containerSelector){
        var container = $(containerSelector);
        var listItems = container.children('.back-button').html();
        $.each(container.find('> .item-content > .title'), function() { 
            listItems += $(this).html();
        });
        $('.page-content-list').remove();
        $('#content').append($('<ul data-role="listview" class="page-content-list"></ul>'));
        $('.page-content-list').html(listItems);
        $('.page-content-list').listview();
        $('.back').button();
        
        $( ".page-content-list li" ).off('click');
        $( ".page-content-list li" ).on('click',function(){
            var parentId = $(this).find('a.link').attr("href");
            if(parentId != '#libraries')
                loadContent(parentId);
            else
                $.initLibraries();
        });
    }
    
    $(document).ready(function() 
    {
        $.initLibraries();
    });
})( jQuery );    