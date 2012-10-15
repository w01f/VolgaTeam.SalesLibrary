(function( $ ) {
    $.runSearch = function(isSort){
        var selectedCondition = $('#condition-content-value').val();
        if($('#content-compare-exact').hasClass('active'))
            selectedCondition = '"' + selectedCondition + '"';
        
        var selectedFileTypes = [];
        if($('#search-file-type-powerpoint').hasClass('active'))
            selectedFileTypes.push("ppt");
        if($('#search-file-type-word').hasClass('active'))
            selectedFileTypes.push("doc");        
        if($('#search-file-type-excel').hasClass('active'))
            selectedFileTypes.push("xls");                
        if($('#search-file-type-pdf').hasClass('active'))
            selectedFileTypes.push("pdf");                        
        if($('#search-file-type-video').hasClass('active'))
            selectedFileTypes.push("video");     
        
        var dateString = $('#condition-date-range input').val().split(" - ");
        if(dateString.length == 2)
        {
            var startDate = dateString[0];
            var endDate = dateString[1];
        }
        
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
                startDate: startDate,
                endDate: endDate,
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
        if($(this).hasClass('active'))
            $(this).removeClass('active');
        else
            $(this).addClass('active');
        $.cookie("fileTypePpt", $('#search-file-type-powerpoint').hasClass('active'), {
            expires: (60 * 60 * 24 * 7)
        });
        $.cookie("fileTypeDoc", $('#search-file-type-word').hasClass('active'), {
            expires: (60 * 60 * 24 * 7)
        });        
        $.cookie("fileTypeXls", $('#search-file-type-excel').hasClass('active'), {
            expires: (60 * 60 * 24 * 7)
        });                
        $.cookie("fileTypePdf", $('#search-file-type-pdf').hasClass('active'), {
            expires: (60 * 60 * 24 * 7)
        });                
        $.cookie("fileTypeVideo", $('#search-file-type-video').hasClass('active'), {
            expires: (60 * 60 * 24 * 7)
        });                        
    }
    
    $.conditionTypeChanged = function(event, ui){
        $.cookie("search-control-panel", ui.index, {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.contentMatchButtonClick = function(){
        if(!$(this).hasClass('active'))
        {
            $('#content-compare-type .btn').removeClass('active');
            $(this).addClass('active');    
        }
        $.cookie("exactMatch", $('#content-compare-exact').hasClass('active'), {
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
        var conditionType  = 0;
        if($.cookie("search-control-panel")!=null)
            conditionType = parseInt($.cookie("search-control-panel"));
        $( "#search-control-panel" ).tabs({
            selected: conditionType,
            disabled: [2]
        });        
        $( "#search-control-panel" ).on('tabsselect',$.conditionTypeChanged)
        
        $( "#clear-content-value" ).off('click');
        $( "#clear-content-value" ).on('click',function () {
            $('#condition-content-value').val('');
        });
        
        if($.cookie("exactMatch")!=null)
        {
            if($.cookie("exactMatch")==  "true")
                $( "#content-compare-exact" ).button('toggle');
            else
                $( "#content-compare-partial" ).button('toggle');
        }
        else
            $( "#content-compare-exact" ).button('toggle');
        $( "#content-compare-exact" ).off('click');        
        $( "#content-compare-exact" ).on('click',$.contentMatchButtonClick);
        $( "#content-compare-partial" ).off('click');        
        $( "#content-compare-partial" ).on('click',$.contentMatchButtonClick);

        if($.cookie("fileTypePpt")!=null)
        {
            if($.cookie("fileTypePpt")==  "true")
                $( "#search-file-type-powerpoint" ).button('toggle');
        }
        else
            $( "#search-file-type-powerpoint" ).button('toggle');

        if($.cookie("fileTypeDoc")!=null)
        {
            if($.cookie("fileTypeDoc")==  "true")
                $( "#search-file-type-word" ).button('toggle');
        }
        else
            $( "#search-file-type-word" ).button('toggle');

        if($.cookie("fileTypeXls")!=null)
        {
            if($.cookie("fileTypeXls")==  "true")
                $( "#search-file-type-excel" ).button('toggle');
        }
        else
            $( "#search-file-type-excel" ).button('toggle');

        if($.cookie("fileTypePdf")!=null)
        {
            if($.cookie("fileTypePdf")==  "true")
                $( "#search-file-type-pdf" ).button('toggle');
        }
        else
            $( "#search-file-type-pdf" ).button('toggle');
        
        if($.cookie("fileTypeVideo")!=null)
        {
            if($.cookie("fileTypeVideo") ==  "true")
                $( "#search-file-type-video" ).button('toggle');
        }
        else
            $( "#search-file-type-video" ).button('toggle');

        $('#file-types input[type="checkbox"]').button({
            text: false
        });
        $( '#file-types .search-file-type').off('click');
        $( '#file-types .search-file-type').on('click',$.fileTypeButtonClick);
        
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
        
        $.dateFormat = 'MM/dd/yyyy';
        $('#condition-date-range').daterangepicker(
        {
            format: $.dateFormat,
            ranges: {
                'Last day': ['yesterday','today'],
                'Last 15 days': [Date.today().add({
                    days: -14
                }), 'today'],
                'Last 30 days': [Date.today().add({
                    days: -29
                }), 'today']
            }
        }, 
        function(start, end) {
            $('#condition-date-range input').val(start.toString($.dateFormat) + ' - ' + end.toString($.dateFormat));
        }
        );
        $( "#clear-date-range" ).off('click');
        $( "#clear-date-range" ).on('click',function () {
            $('#condition-date-range input').val('');
        });            
        
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