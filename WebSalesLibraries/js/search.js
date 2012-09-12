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
            selectedFileTypes.push("video");     
        
        var selectedConditionType = parseInt($( "#condition-type" ).tabs( "option", "selected" ));
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
                        $('#search-result').append(msg);
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
        $.cookie("conditionType", ui.index, {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.contentMatchButtonClick = function(){
        $.cookie("exactMatch", $('#content-compare-exact').is(':checked'), {
            expires: (60 * 60 * 24 * 7)
        });
    }
    
    $.initControlPanel = function(){
        $( "#run-search" ).on('click',$.runSearch);        
        $('#condition-content-value').keypress(function (e) {
            if (e.which == 13) {
                $.runSearch();
            }
        });        

        if($.cookie("fileTypePpt")!=null)
        {
            if($.cookie("fileTypePpt")==  "true")
                $( "#search-file-type-powerpoint" ).prop('checked', true);
        }
        else
            $( "#search-file-type-powerpoint" ).prop('checked',true);
        $( "#search-file-type-powerpoint" ).button({
            text: false,
            icons: {
                primary: "button-search-powerpoint"
            }
        });
        $( "#search-file-type-powerpoint" ).on('click',$.fileTypeButtonClick);
        
        
        if($.cookie("fileTypeDoc")!=null)
        {
            if($.cookie("fileTypeDoc")==  "true")
                $( "#search-file-type-word" ).prop('checked', true);
        }
        else
            $( "#search-file-type-word" ).prop('checked',true);
        $( "#search-file-type-word" ).button({
            text: false,
            icons: {
                primary: "button-search-word"
            }
        });
        $( "#search-file-type-word" ).on('click',$.fileTypeButtonClick);
        
        
        if($.cookie("fileTypeXls")!=null)
        {
            if($.cookie("fileTypeXls")==  "true")
                $( "#search-file-type-excel" ).prop('checked', true);
        }
        else
            $( "#search-file-type-excel" ).prop('checked',true);
        $( "#search-file-type-excel" ).button({
            text: false,
            icons: {
                primary: "button-search-excel"
            }
        });        
        $( "#search-file-type-excel" ).on('click',$.fileTypeButtonClick);
        
        
        if($.cookie("fileTypePdf")!=null)
        {
            if($.cookie("fileTypePdf")==  "true")
                $( "#search-file-type-pdf" ).prop('checked', true);
        }
        else
            $( "#search-file-type-pdf" ).prop('checked', true);
        $( "#search-file-type-pdf" ).button({
            text: false,
            icons: {
                primary: "button-search-pdf"
            }
        });                
        $( "#search-file-type-pdf" ).on('click',$.fileTypeButtonClick);
        
        
        if($.cookie("fileTypeVideo")!=null)
        {
            if($.cookie("fileTypeVideo") ==  "true")
                $( "#search-file-type-video" ).prop('checked', true);
        }
        else
            $( "#search-file-type-video" ).prop('checked',true);
        $( "#search-file-type-video" ).button({
            text: false,
            icons: {
                primary: "button-search-video"
            }
        });                        
        $( "#search-file-type-video" ).on('click',$.fileTypeButtonClick);
        
        $.getSelectedLibraries();
        $("#library-select").button();
        $("#library-select").on('click',$.selectLibraries);

        var conditionType  = 0;
        if($.cookie("conditionType")!=null)
            conditionType = parseInt($.cookie("conditionType"));
        $( "#condition-type" ).tabs({
            selected: conditionType
        });        
        $( "#condition-type" ).on('tabsselect',$.conditionTypeChanged)

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
        $( "#content-compare-exact" ).on('click',$.contentMatchButtonClick);
        $( "#content-compare-partial" ).on('click',$.contentMatchButtonClick);
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