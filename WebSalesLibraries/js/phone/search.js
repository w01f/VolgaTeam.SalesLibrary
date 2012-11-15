(function( $ ) {
    $.initSearch = function(){
        $.ajax({
            type: "POST",
            url: "search/getSearchView",
            beforeSend: function(){
                $('#search-basic .page-content').html('');
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
                $('#search-basic .page-content').html(msg);
                loadSearchPanel();
            },
            async: true,
            dataType: 'html'                        
        });
        $.ajax({
            type: "POST",
            url: "search/getTagsView",
            beforeSend: function(){
                $('#search-tags .page-content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#search-tags .page-content').html(msg);
                loadTagsPanel();
            },
            async: true,
            dataType: 'html'                        
        });                        
        $.ajax({
            type: "POST",
            url: "search/getLibrariesView",
            beforeSend: function(){
                $('#search-libraries .page-content').html('');
            },
            complete: function(){
            },
            success: function(msg){
                $('#search-libraries .page-content').html(msg);
                loadLibrariesPanel();
            },
            async: true,
            dataType: 'html'                        
        });                
    }
    
    var loadSearchPanel = function(){
        initMatchSelector();
        initDateSelector();
        initFileTypeSelector();
        initSearchSortSelectors();
        
        $('#search-basic, #search-tags, #search-libraries').on('pageshow', function(e){
            var currentPage = $(e.target);
            $('#search-result .link.back').attr('href','#'+currentPage.attr('id'));
            return true;
        });
        
        $('.search-button').on('click',function(){
            runSearch(0);
        });
    }
    
    var loadLibrariesPanel = function(){
        initLibrariesSelector();
    }    
    
    var loadTagsPanel = function(){
        initTagsSelector();
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
        var updateLibraries = function(){
            var selectedLibraryIds = [];
            $('#search-libraries-container :checked').each(function(){
                selectedLibraryIds.push(this.id);
            });
            $.cookie("selectedLibraryIds", $.toJSON(selectedLibraryIds), {
                expires: (60 * 60 * 24 * 7)
            });            
        }
        $( '#search-libraries-container input[type="checkbox"]').checkboxradio();
        updateLibraries();
        $( '#search-libraries-container input[type="checkbox"]').off('change');
        $( '#search-libraries-container  input[type="checkbox"]').on('change',function(){
            updateLibraries();
        });
        $( '#search-libraries-select-button').off('click');
        $( '#search-libraries-select-button').on('click',function(){
            $('#search-libraries-container input[type="checkbox"]').attr('checked', true).checkboxradio("refresh");
            updateLibraries();
        });
        $( '#search-libraries-clear-button').off('click');
        $( '#search-libraries-clear-button').on('click',function(){
            $('#search-libraries-container input[type="checkbox"]').attr('checked', false).checkboxradio("refresh");
            updateLibraries();
        });        
    }
    
    var initSearchSortSelectors = function(){
        $.cookie("sortColumn", 'link-name', {
            expires: (60 * 60 * 24 * 7)
        });
        $('#search-result-sort-column').on('change',function(){
            $.cookie("sortColumn", $( '#search-result-sort-column :selected').val(), {
                expires: (60 * 60 * 24 * 7)
            });
            runSearch(1);
        });
        $.cookie("sortDirection", 'asc', {
            expires: (60 * 60 * 24 * 7)
        });        
        $('#search-result-sort-order').on('change',function(){
            $.cookie("sortDirection", $( '#search-result-sort-order :selected').val(), {
                expires: (60 * 60 * 24 * 7)
            });
            runSearch(1);
        });        
    }
    
    var initTagsSelector = function(){
        $( '.search-tags-group').collapsible();
        $( '.search-tags-item').checkboxradio();
        
        $('#search-tags-clear-button').on('click',function(){
            $('.search-tags-item').attr('checked', false).checkboxradio("refresh");
        });
    }
    
    var runSearch = function(isSort){
        var selectedCondition = $('#search-keyword').val();
        if($('#search-match-exact').hasClass('ui-btn-active'))
            selectedCondition = '"' + selectedCondition + '"';
        
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
        
        
        var startDateText = $('#search-date-start').val();
        var endDateText = $('#search-date-end').val();
        if(startDateText != null && endDateText != null && startDateText!=''&& endDateText!='')
        {
            var startDate = startDateText;
            var endDate = endDateText;
        }
        
        var onlyFileCards = $('#search-only-filecards').is(':checked')?1:0;
        
        var categories = [];
        $.each($( ".search-tags-item:checked"),function(){
            var substr = $(this).attr('id').split('------');
            var category= {
                category:  substr[0],
                tag:    substr[1]
            };
            categories.push(category);
        });
            
        $.ajax({
            type: "POST",
            url: "search/searchByContent",
            data: {
                fileTypes: selectedFileTypes,
                condition: selectedCondition,
                startDate: startDate,
                endDate: endDate,
                onlyFileCards: onlyFileCards,
                categories: categories.length>0?$.toJSON(categories):null,
                categoriesExactMatch: $('#search-tags-exact-match :selected').val(),                
                isSort: isSort
            },
            beforeSend: function(){
                $('#search-result-body').html('');
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
                $('#search-result-body').html(msg);
                $.mobile.changePage( "#search-result", {
                    transition: "slidefade"
                });
                $('#search-result-body').listview('refresh');                     
                
                var itemsNumber = $('#search-result-body li').length;
                if(itemsNumber>0)
                {
                    $('#search-result-links-number').html('Results: '+itemsNumber);                        
                    $('#search-result-sort-column-container').show();
                    $('#search-result-sort-order-container').show();
                }
                else
                {
                    $('#search-result-links-number').html('Files was not found');            
                    $('#search-result-sort-column-container').hide();
                    $('#search-result-sort-order-container').hide();
                }
                
                $( ".file-link" ).on('click',function(){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLink(selectedLink,true,false);
                });
                $( ".file-link-detail" ).on('click',function(event){
                    var selectedLink = $.trim($(this).attr("href").replace('#link', ''));
                    $.loadLinkDeatils(selectedLink,true);
                    event.stopPropagation();
                });                                
            },
            error: function(){
                $('#search-result-body').html('');
            },            
            async: true,
            dataType: 'html'                        
        });                        
    }    
})( jQuery );    