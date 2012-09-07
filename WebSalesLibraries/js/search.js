(function( $ ) {
    $.runSearch = function(){
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
            selectedFileTypes.push("pdf");     
        
        var selectedConditionType = $( "#condition-type" ).tabs( "option", "selected" );
        switch(selectedConditionType)
        {
            case 0:
                var selectedCondition = $('#condition-content-value').val();
                if($('#content-compare-exact').is(':checked'))
                    selectedCondition = '"' + selectedCondition + '"';
                
                $.ajax({
                    type: "POST",
                    url: "search/searchByContent",
                    data: {
                        fileTypes: selectedFileTypes,
                        condition: selectedCondition
                    },
                    beforeSend: function(){
                        $('#search-result').html('');
                        $.showOverlay();
                    },
                    complete: function(){
                        $.hideOverlay();
                    },
                    success: function(msg){
                        $('#search-result').html(msg);
                        $.updateContentAreaWidth();
                    },
                    error: function(){
                        $('#search-result').html('');
                    },            
                    async: true,
                    dataType: 'html'                        
                });                        
                break;
        }
    }
    $.initControlPanel = function(){
        $( "#run-search" ).on('click',$.runSearch);        
        
        $( "#search-file-type-powerpoint" ).button({
            text: false,
            icons: {
                primary: "button-search-powerpoint"
            }
        });
        $( "#search-file-type-word" ).button({
            text: false,
            icons: {
                primary: "button-search-word"
            }
        });
        $( "#search-file-type-excel" ).button({
            text: false,
            icons: {
                primary: "button-search-excel"
            }
        });        
        $( "#search-file-type-pdf" ).button({
            text: false,
            icons: {
                primary: "button-search-pdf"
            }
        });                
        $( "#search-file-type-video" ).button({
            text: false,
            icons: {
                primary: "button-search-video"
            }
        });                        
        
        $( "#condition-type" ).tabs();
        
        $( "#content-compare-type").buttonset();
        
        $('#condition-content-value').keypress(function (e) {
            if (e.which == 13) {
                $.runSearch();
            }
        });
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
                $.initControlPanel();
                $.hideOverlay();
            },
            success: function(msg){
                $('#content').html(msg);
                $.updateContentAreaWidth();
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