(function( $ ) {
    
    $.downloadFile = function(url)
    {
        window.open(url);
    }    
    
    $.viewSelectedFormat = function(itemContent, resolution)
    {
        var selectedFileId = itemContent.find('.link-id').html();
        var selectedFileType = itemContent.find('.file-type').html();
        var selectedViewType = itemContent.find('.view-type').html();
        var selectedLinks = itemContent.find('.links');

        if(selectedFileType != ''&& selectedViewType != '' && selectedLinks != '')
        {
            if(!(selectedViewType == 'png' || selectedViewType == 'jpeg'))
                selectedLinks = $.parseJSON(selectedLinks.html());
            switch(selectedFileType)
            {
                case 'ppt':
                case 'doc':
                case 'pdf':
                    switch(selectedViewType)
                    {
                        case 'png':
                        case 'jpeg':
                            var imageItems = '';
                            var selector = 'li.low-res';
                            if(resolution == 'hi')
                                selector = 'li.hi-res';
                            selectedLinks.find(selector).each(function() {
                                imageItems+='<li>'+$(this).html()+'</li>';
                            });
                            $('#gallery').html(imageItems);
                            $.mobile.changePage( "#gallery-page", {
                                transition: "slidefade"
                            });                            
                            break;
                        case 'email':
                            $('.email-tab .link-container .name').html(selectedLinks[0].title);
                            $( '#email-send').off('click');
                            $( '#email-send').on('click',function(){
                                $.sendEmail(selectedFileId);
                            });                             
                            $.mobile.changePage( "#email-address", {
                                transition: "slidefade"
                            });                                                        
                            break;
                        default:
                            $.downloadFile(selectedLinks[0].href);
                            break;
                    }
                    break;                    
                case 'xls':
                    switch(selectedViewType)
                    {
                        case 'email':
                            break;                        
                        default:
                            $.downloadFile(selectedLinks[0].href);
                            break;
                    }                    
                    break;
                case 'url':
                case 'other':
                    $.downloadFile(selectedLinks[0].href);
                    break;                
                case 'png':
                case 'jpeg':
                    switch(selectedViewType)
                    {
                        case 'email':
                            break;                        
                        default:
                            $.downloadFile(selectedLinks[0].href);
                            break;                
                    }                    
                    break;
                case 'mp4':                    
                case 'video':
                    switch(selectedViewType)
                    {
                        case 'video':
                        case 'tab':                            
                        case 'ogv':
                            $.downloadFile(selectedLinks[0].src);
                            break;                        
                        case 'email':
                            break;                                                    
                    }
                    break;
            }
        }
    }
    
    $.viewFileCard = function(linkId){
        $.ajax({
            type: "POST",
            url: "wallbin/getFileCard",
            data:{
                linkId: linkId
            },
            beforeSend: function(){
                $('#preview .page-content').html('');
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
                $('#preview .page-content').html(msg);
                $('#preview .library-title').html('Important Info');
                $('#preview .link.back').attr('href','#link-details');
                $.mobile.changePage( "#preview", {
                    transition: "slidefade"
                });
                $('#preview .page-content').children('ul').listview();                     
            },
            async: true,
            dataType: 'html'                        
        });
    }
})( jQuery );    

