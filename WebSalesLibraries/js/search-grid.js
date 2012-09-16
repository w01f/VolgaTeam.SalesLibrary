(function( $ ) {
    $.updateSearchGridDimensions = function(){
        $('#search-grid-body-container').css({
            'height':($('#search-result > div').height() - $('#search-grid-header').height())+'px'
        });        
        
        var linkDateWidth = 140;

        var linkNameHeaderWidth = $('#search-result').width() - $('#search-grid-header td.library-column').width() - $('#search-grid-header td.link-type-column').width() -linkDateWidth;
        $('#search-grid-header td.link-name-column').css({
            'width':linkNameHeaderWidth+'px'
        });
        
        var linkNameBodyWidth = $('#search-result').width() - $('#search-grid-body td.library-column').width() - $('#search-grid-body td.link-type-column').width() -linkDateWidth;
        $('#search-grid-body td.link-name-column').css({
            'width':linkNameBodyWidth+'px'
        });
    }
    
    $.updateSortingColumns = function(){
        $('#search-grid-header td span').removeClass('asc');
        $('#search-grid-header td span').removeClass('desc');
        
        var selector = null;
        if($.cookie("sortColumn")!=null)
        {
            switch($.cookie("sortColumn"))
            {
                case "library":
                    selector = '#search-grid-header td.library-column span';
                    break;
                case "link-type":
                    selector = '#search-grid-header td.link-type-column span';
                    break;                 
                case "link-name":
                    selector = '#search-grid-header td.link-name-column span';
                    break;                                  
                case "link-date":
                    selector = '#search-grid-header td.link-date-column span';
                    break;                                                   
            }
        }
        if(selector != null && $.cookie("sortDirection")!=null)
            $(selector).addClass($.cookie("sortDirection"));
    }
    
    $.sortByColumn = function(columnName){
        var sortDirection = 'asc';
        if($.cookie("sortColumn")!=null && $.cookie("sortDirection")!=null)
        {
            if($.cookie("sortColumn") == columnName)
            {
                if($.cookie("sortDirection") == 'asc')
                    sortDirection = 'desc';
            }
        }
        
        $.cookie("sortColumn", columnName, {
            expires: (60 * 60 * 24 * 7)
        });
        $.cookie("sortDirection", sortDirection, {
            expires: (60 * 60 * 24 * 7)
        });        
        
        var scrollPosition = $('#search-grid-body-container').scrollTop();
        $.cookie("searchGridScrollPosition", scrollPosition, {
            expires: (60 * 60 * 24 * 7)
        });        
        $.runSearch(1);
    }
    
    $.searchGridViewDetails = function(){
        var linkId =  $(this).parent().find('.link-id-column').html();
        $.openViewDialogSearchGrid(linkId);
    }
    
    $.initSearchGrid = function(){
        if($('#search-grid-body tr').length>0)
        {
            $.updateSortingColumns();
            if($.cookie("searchGridScrollPosition")!= null)
                $('#search-grid-body-container').scrollTop(parseInt($.cookie("searchGridScrollPosition")));
        }
        else
            $.cookie("searchGridScrollPosition", 0, {
                expires: (60 * 60 * 24 * 7)
            });        
        
        $( "#search-grid-header td.library-column" ).off('click');
        $( "#search-grid-header td.library-column" ).on('click',function(){
            $.sortByColumn('library');
        });
        
        $( "#search-grid-header td.link-type-column" ).off('click');
        $( "#search-grid-header td.link-type-column" ).on('click',function(){
            $.sortByColumn('link-type');
        });        
        
        $( "#search-grid-header td.link-name-column" ).off('click');
        $( "#search-grid-header td.link-name-column" ).on('click',function(){
            $.sortByColumn('link-name');
        });                
        
        $( "#search-grid-body td.click-no-mobile" ).off('click');
        $( "#search-grid-body td.click-no-mobile" ).on('click',function(){
            $.searchGridViewDetails.call($(this));
        });
        $( "#search-grid-body td.click-mobile" ).off('touchstart').off('touchmove').off('touchend');
        $( "#search-grid-body td.click-mobile" ).on('touchstart',function(){
            isScrolling = false;
        }).on('touchmove',function(){
            isScrolling = true;
        }).on('touchend',function(){
            if(!isScrolling)
                $.searchGridViewDetails.call($(this));
        });
    }
    
})( jQuery );    