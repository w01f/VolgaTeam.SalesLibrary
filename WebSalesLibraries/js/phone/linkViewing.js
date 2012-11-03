(function( $ ) {
    
    $.downloadFile = function(url)
    {
        window.open(url);
    }    
    
    $.viewSelectedFormat = function(itemContent, resolution)
    {
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
})( jQuery );    

