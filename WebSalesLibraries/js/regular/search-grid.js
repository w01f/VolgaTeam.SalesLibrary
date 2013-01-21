(function( $ ) {
    $.updateSortingColumns = function(){
        $('#search-grid-header').find('td span').removeClass('asc').removeClass('desc');
        
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
    };
    
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
    };
    
    $.searchGridViewPreviewLink = function(){
        var linkId =  $(this).parent().find('.link-id-column').html();
        $.openViewDialogSearchGrid(linkId);
    };
    
    $.searchGridViewFileCard = function(){
        var fileCardContainer =  $(this).parent().find('td.hidden-content');
        $.openFileCard.call(fileCardContainer);
    };
    
    $.searchGridViewAttachment = function(){
        var viewDialogContainer =  $(this).parent().find('td.hidden-content');
        $.openViewDialogEmbedded.call(viewDialogContainer);
    };
    
    $.searchGridViewLinkDetails = function(){
        if($(this).hasClass('collapsed'))
        {
            var currentCell = $(this);
            var currentRow = $(this).parent();
            var linkId =  currentRow.find('.link-id-column').html();
            $.ajax({
                type: "POST",
                url: "wallbin/getLinkDetails",
                data: {
                    linkId: linkId
                },
                beforeSend: function(){
                    $.showOverlayLight();
                },
                complete: function(){
                    $.hideOverlayLight();
                },
                success: function(msg){
                    if(msg != '')
                    {
                        $(msg).insertAfter(currentRow);
                        
                        $( "#search-grid-body").find("tr.link-details-container tr.file-card td" ).off('click');
                        $( "#search-grid-body").find("tr.link-details-container tr.file-card>td.click-no-mobile" ).on('click',function(e){
                            e.stopPropagation();
                            $.searchGridViewFileCard.call($(this));
                        });                        
                        
                        $( "#search-grid-body").find("tr.link-details-container tr.attachment td" ).off('click');
                        $( "#search-grid-body").find("tr.link-details-container tr.attachment>td.click-no-mobile" ).on('click',function(e){
                            e.stopPropagation();
                            $.searchGridViewAttachment.call($(this));
                        });                        

                        currentCell.removeClass('collapsed');
                        currentCell.addClass('expanded');
                    }
                },
                async: true,
                dataType: 'html'                        
            });                                    
        }
        else if($(this).hasClass('expanded'))
        {
            $(this).parent().next('.link-details-container').remove();
            $(this).removeClass('expanded');
            $(this).addClass('collapsed');
        }
    };
    
    $.initSearchGrid = function(){
        if($('#search-grid-body').find('tr').length>0)
        {
            $.updateSortingColumns();
            if($.cookie("searchGridScrollPosition")!= null)
                $('#search-grid-body-container').scrollTop(parseInt($.cookie("searchGridScrollPosition")));
        }
        else
            $.cookie("searchGridScrollPosition", 0, {
                expires: (60 * 60 * 24 * 7)
            });        
        
        $( "#search-grid-header").find("td.library-column" ).off('click').on('click',function(){
            $.sortByColumn('library');
        });
        
        $( "#search-grid-header").find("td.link-type-column" ).off('click').on('click',function(){
            $.sortByColumn('link-type');
        });        
        
        $( "#search-grid-header").find("td.link-name-column" ).off('click').on('click',function(){
            $.sortByColumn('link-name');
        });                
        
        $( "#search-grid-header").find("td.link-date-column" ).off('click').on('click',function(){
            $.sortByColumn('link-date');
        });                        
        
        $( "#search-grid-body").find("td.click-no-mobile" ).off('click').on('click',function(){
            $.searchGridViewPreviewLink.call($(this));
        });
        $( "#search-grid-body").find("td.click-mobile" ).off('touchstart').off('touchmove').off('touchend').on('touchstart',function(){
            isScrolling = false;
        }).on('touchmove',function(){
            isScrolling = true;
        }).on('touchend',function(e){
            if(!isScrolling)
                $.searchGridViewPreviewLink.call($(this));
            e.stopPropagation();
            e.preventDefault();
            return false;
        });
        
        $( "#search-grid-body").find("td.details-button" ).off('click');
        $( "#search-grid-body").find("td.details-button.click-no-mobile" ).on('click',function(){
            $.searchGridViewLinkDetails.call($(this));
        });
        $( "#search-grid-body").find("td.details-button" ).off('touchstart').off('touchmove').off('touchend');
        $( "#search-grid-body").find("td.details-button.click-mobile" ).on('touchstart',function(){
            isScrolling = false;
        }).on('touchmove',function(){
            isScrolling = true;
        }).on('touchend',function(){
            if(!isScrolling)
                $.searchGridViewLinkDetails.call($(this));
        });        
    };
    
})( jQuery );    