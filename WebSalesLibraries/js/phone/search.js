(function( $ ) {
    $.initSearch = function(){
        $.ajax({
            type: "POST",
            url: "search/getSearchView",
            beforeSend: function(){
                $('#search .page-content').html('');
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
                $('#search .page-content').html(msg);
                loadSearchPanel();
            },
            async: true,
            dataType: 'html'                        
        });
    }
    
    var loadSearchPanel = function(){
        initMatchSelector();
        initDateSelector();
        initFileTypeSelector();
        initLibrariesSelector();
    }
    
    var initMatchSelector = function(){
        $('#search-match-selector a').removeClass('ui-btn-active');
        if($.cookie("exactMatch")!=null)
        {
            if($.cookie("exactMatch")==  "true")
                $( "#search-match-exact" ).addClass('ui-btn-active');
            else
                $( "#search-match-partial" ).addClass('ui-btn-active');
        }
        else
            $( "#search-match-exact" ).addClass('ui-btn-active');
        $( "#search-match-exact, #search-match-partial" ).off('click');        
        $( "#search-match-exact, #search-match-partial" ).on('click',function(){
            $.cookie("exactMatch", $('#search-match-partial').hasClass('ui-btn-active'), {
                expires: (60 * 60 * 24 * 7)
            });
        });
    }
    
    var initDateSelector = function(){
        $('#search-date-container').collapsible();
        $('#search-date-start, #search-date-end').scroller({
            preset: 'date',
            theme: 'jqm',
            display: 'modal',
            mode: 'clickpick',
            animate: 'fade',
            dateOrder: 'mmD ddy',
            dateFormat:'mm/dd/y',
            onSelect: function(valueText, inst)
            {
                updateDateRange();
            }
        });    
        $('#search-clear-button').off('click');
        $('#search-clear-button').on('click',function(){
            $('#search-date-start, #search-date-end').val('');
            updateDateRange();
        });
        
        var updateDateRange = function()
        {
            var startDateText = $('#search-date-start').val();
            var endDateText = $('#search-date-end').val();
            if(startDateText != null && endDateText != null && startDateText!=''&& endDateText!='')
            {
                $('#search-date-container .layout-group-title').html('Dates: ' + startDateText + '-' + endDateText);
            }
            else
            {
                $('#search-date-container .layout-group-title').html('Dates: All');
            }            
        }
        updateDateRange();
    }    
    
    var initFileTypeSelector = function(){
        $( '#search-file-type-container').collapsible();
        $( '#search-file-type-container input[type="checkbox"]').checkboxradio();
        
        if($.cookie("fileTypePpt")!=null)
        {
            if($.cookie("fileTypePpt")==  "true")
                $( "#search-file-type-powerpoint" ).attr("checked",true).checkboxradio("refresh");
        }
        else
            $( "#search-file-type-powerpoint").attr("checked",true).checkboxradio("refresh");

        if($.cookie("fileTypeDoc")!=null)
        {
            if($.cookie("fileTypeDoc")==  "true")
                $( "#search-file-type-word" ).attr("checked",true).checkboxradio("refresh");
        }
        else
            $( "#search-file-type-word" ).attr("checked",true).checkboxradio("refresh");

        if($.cookie("fileTypeXls")!=null)
        {
            if($.cookie("fileTypeXls")==  "true")
                $( "#search-file-type-excel" ).attr("checked",true).checkboxradio("refresh");
        }
        else
            $( "#search-file-type-excel" ).attr("checked",true).checkboxradio("refresh");

        if($.cookie("fileTypePdf")!=null)
        {
            if($.cookie("fileTypePdf")==  "true")
                $( "#search-file-type-pdf" ).attr("checked",true).checkboxradio("refresh");
        }
        else
            $( "#search-file-type-pdf" ).attr("checked",true).checkboxradio("refresh");
        
        if($.cookie("fileTypeVideo")!=null)
        {
            if($.cookie("fileTypeVideo") ==  "true")
                $( "#search-file-type-video" ).attr("checked",true).checkboxradio("refresh");
        }
        else
            $( "#search-file-type-video" ).attr("checked",true).checkboxradio("refresh");

        $('#file-types input[type="checkbox"]').button({
            text: false
        });
        $( '#search-file-type-container input[type="checkbox"]').off('change');
        $( '#search-file-type-container  input[type="checkbox"]').on('change',function(){
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
            updateFileTypes();
        });
        
        var updateFileTypes = function()
        {
            var fileTypesTitle = 'File Types: ';
            if($('#search-file-type-powerpoint').is(':checked') &&
                $('#search-file-type-word').is(':checked') &&
                $('#search-file-type-excel').is(':checked') &&
                $('#search-file-type-pdf').is(':checked') &&
                $('#search-file-type-video').is(':checked'))
                {
                fileTypesTitle+='All';
            }
            else
            {
                var selectedFileTypes =[];
                if($('#search-file-type-powerpoint').is(':checked'))
                    selectedFileTypes.push('ppt');
                if($('#search-file-type-word').is(':checked'))
                    selectedFileTypes.push('doc');
                if($('#search-file-type-excel').is(':checked'))
                    selectedFileTypes.push('xls');
                if($('#search-file-type-pdf').is(':checked'))
                    selectedFileTypes.push('pdf');
                if($('#search-file-type-video').is(':checked'))
                    selectedFileTypes.push('video');
                fileTypesTitle +=  selectedFileTypes.join(', ');
            }
            $('#search-file-type-container .layout-group-title').html(fileTypesTitle);
        }
        updateFileTypes();
    }        
    
    var initLibrariesSelector = function(){
        $( '#search-libraries-container').collapsible();
        $( '#search-libraries-container input[type="checkbox"]').checkboxradio();
        $( '#search-libraries-container input[type="checkbox"]').off('change');
        $( '#search-libraries-container  input[type="checkbox"]').on('change',function(){
            var selectedLibraryIds = [];
            $('#search-libraries-container :checked').each(function(){
                selectedLibraryIds.push(this.id);
            });
            $.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
                expires: (60 * 60 * 24 * 7)
            });
            updateLibraries();
        });
        
        var updateLibraries = function()
        {
            var selectedLibrariesTitle ='Libraries: '
            var selectedLibraryNames =[];
            $('#search-libraries-container :checked').each(function(){
                selectedLibraryNames.push($(this).attr('name'));
            });
            if($( '#search-libraries-container  input[type="checkbox"]').length == selectedLibraryNames.length)
                selectedLibrariesTitle += 'All';
            else if(selectedLibraryNames.length==1)
                selectedLibrariesTitle +=  selectedLibraryNames.join(', ');
            else if(selectedLibraryNames.length==0)
                selectedLibrariesTitle += 'Nothing selected';
            else
                selectedLibrariesTitle += 'Some are selected';
            $('#search-libraries-container .layout-group-title').html(selectedLibrariesTitle);
        }
        
        updateLibraries();
    }
})( jQuery );    