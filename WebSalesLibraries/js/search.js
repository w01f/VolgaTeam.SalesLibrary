(function( $ ) {
    $.runSearch = function(isSort){
        var selectedFileTypes = [];
        if($('#search-file-type-powerpoint').is(':checked'))
            selectedFileTypes.push("ppt");
        if($('#search-file-type-word').is(':checked'))
            selectedFileTypes.push("doc");        
        if($('#search-file-type-excel').is(':checked'))
            selectedFileTypes.push("xls");                
        if($('#search-file-type-pdf').is(':checked'))
            selectedFileTypes.push("pdf");                        
        if($('#search-file-type-video').is(':checked'))
            selectedFileTypes.push("video");     
        
        var selectedCondition = $('#condition-content-value').val();
        if($('#content-compare-exact').is(':checked'))
            selectedCondition = '"' + selectedCondition + '"';
        
        var selectedTabId = $.cookie("selectedRibbonTabId");
        var onlyFileCards = 0;
        if(selectedTabId == 'search-file-card-tab')
            onlyFileCards = 1;
        else if($.cookie("onlyFileCards")!= null)
            onlyFileCards = parseInt($.cookie("onlyFileCards"));
            
        $.ajax({
            type: "POST",
            url: "search/searchByContent",
            data: {
                fileTypes: selectedFileTypes,
                condition: selectedCondition,
                onlyFileCards: onlyFileCards,
                isSort: isSort
            },
            beforeSend: function(){
                $.showOverlayLight();
            },
            complete: function(){
                $.hideOverlayLight();
                $.updateContentAreaDimensions();
                $.initSearchGrid();
            },
            success: function(msg){
                $('#search-result>div').html('');
                $('#search-result>div').append(msg);
            },
            error: function(){
                $('#search-result>div').html('');
            },            
            async: true,
            dataType: 'html'                        
        });                        
    }
    
    $.fileTypeButtonClick = function(){
        $.cookie("fileTypePpt", $('#search-file-type-powerpoint').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });
        $.cookie("fileTypeDoc", $('#search-file-type-word').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });        
        $.cookie("fileTypeXls", $('#search-file-type-excel').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });                
        $.cookie("fileTypePdf", $('#search-file-type-pdf').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });                
        $.cookie("fileTypeVideo", $('#search-file-type-video').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });                        
    }
    
    $.conditionTypeChanged = function(event, ui){
        $.cookie("search-control-panel", ui.index, {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.contentMatchButtonClick = function(){
        $.cookie("exactMatch", $('#content-compare-exact').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.updateSearchAreaDimensions = function(){
        var height = $('#content').height();
        $('#right-navbar > div').css({
            'height':height+'px'
        });        
        $('#search-result > div').css({
            'height':height+'px'
        });        
        
        $.updateSearchGridDimensions();
    }
    
    $.toggleSearchFileCard = function(toggleState){
        if(toggleState == 1)
            $('#search-file-card-button').addClass('sel');        
        else
            $('#search-file-card-button').removeClass('sel');        
        
        $.cookie("onlyFileCards", toggleState, {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.initControlPanel = function(){
        if($.cookie("fileTypePpt")!=null)
        {
            if($.cookie("fileTypePpt")==  "true")
                $( "#search-file-type-powerpoint" ).prop('checked', true);
        }
        else
            $( "#search-file-type-powerpoint" ).prop('checked',true);

        if($.cookie("fileTypeDoc")!=null)
        {
            if($.cookie("fileTypeDoc")==  "true")
                $( "#search-file-type-word" ).prop('checked', true);
        }
        else
            $( "#search-file-type-word" ).prop('checked',true);

        if($.cookie("fileTypeXls")!=null)
        {
            if($.cookie("fileTypeXls")==  "true")
                $( "#search-file-type-excel" ).prop('checked', true);
        }
        else
            $( "#search-file-type-excel" ).prop('checked',true);

        if($.cookie("fileTypePdf")!=null)
        {
            if($.cookie("fileTypePdf")==  "true")
                $( "#search-file-type-pdf" ).prop('checked', true);
        }
        else
            $( "#search-file-type-pdf" ).prop('checked', true);
        
        if($.cookie("fileTypeVideo")!=null)
        {
            if($.cookie("fileTypeVideo") ==  "true")
                $( "#search-file-type-video" ).prop('checked', true);
        }
        else
            $( "#search-file-type-video" ).prop('checked',true);

        $('#file-types input[type="checkbox"]').button({
            text: false
        });
        $( '#file-types input[type="checkbox"]').off('click');
        $( '#file-types input[type="checkbox"]').on('click',$.fileTypeButtonClick);
        
        $( "#search-file-type-powerpoint" ).button("option", "icons",{
            primary: "button-search-powerpoint"
        });        
        $( "#search-file-type-word" ).button("option", "icons",{
            primary: "button-search-word"
        });        
        $( "#search-file-type-excel" ).button("option", "icons",{
            primary: "button-search-excel"
        });        
        $( "#search-file-type-pdf" ).button("option", "icons",{
            primary: "button-search-pdf"
        });                
        $( "#search-file-type-video" ).button("option", "icons",{
            primary: "button-search-video"
        });

        var conditionType  = 0;
        if($.cookie("search-control-panel")!=null)
            conditionType = parseInt($.cookie("search-control-panel"));
        $( "#search-control-panel" ).tabs({
            selected: conditionType,
            disabled: [2]
        });        
        $( "#search-control-panel" ).on('tabsselect',$.conditionTypeChanged)

        if($.cookie("exactMatch")!=null)
        {
            if($.cookie("exactMatch")==  "true")
                $( "#content-compare-exact" ).prop('checked', true);
            else
                $( "#content-compare-partial" ).prop('checked', true);
        }
        else
            $( "#content-compare-exact" ).prop('checked', true);
        $( "#content-compare-type").buttonset();
        $( "#content-compare-exact" ).off('click');        
        $( "#content-compare-exact" ).on('click',$.contentMatchButtonClick);
        $( "#content-compare-partial" ).off('click');        
        $( "#content-compare-partial" ).on('click',$.contentMatchButtonClick);
        
        $( "#run-search-full" ).off('click');
        $( "#run-search-full" ).on('click',function () {
            $.runSearch(0);
        });        
        $( "#run-search-file-card" ).off('click');
        $( "#run-search-file-card" ).on('click',function () {
            $.runSearch(0);
        });                
        $("#right-navbar input").keypress(function (e) {
            if (e.which == 13) {
                return false;
            }
        });        
        $('#condition-content-value').keypress(function (e) {
            if (e.which == 13) {
                $.runSearch(0);
            }
        });  
    
        var selectedTabId = $.cookie("selectedRibbonTabId");
        if(selectedTabId == 'search-full-tab')
        {
            var onlyFileCards = 0;
            if($.cookie("onlyFileCards")!= null)
                onlyFileCards = parseInt($.cookie("onlyFileCards"));
            $.toggleSearchFileCard(onlyFileCards);
            $( "#search-file-card-button" ).off('click');
            $( "#search-file-card-button" ).on('click',function () {
                var onlyFileCards = 0;
                if($.cookie("onlyFileCards")!= null)
                    onlyFileCards = parseInt($.cookie("onlyFileCards"));
                if(onlyFileCards == 0)
                    onlyFileCards = 1;
                else
                    onlyFileCards = 0;
                $.toggleSearchFileCard(onlyFileCards);
            });  
        }
        else
            $.toggleSearchFileCard(0);

        $.initLibrarySelector();
    }
    
    $.initSearchView = function(){
        $.ajax({
            type: "POST",
            url: "search/getSearchView",
            beforeSend: function(){
                $('#content').html('');
                $.showOverlay();
            },
            complete: function(){
                $.hideOverlay();
                $.initControlPanel();
                $.updateContentAreaDimensions();
                $.initSearchGrid();
            },
            success: function(msg){
                $('#content').html(msg);
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
        });
})( jQuery );    