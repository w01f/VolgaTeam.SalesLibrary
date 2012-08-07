(function( $ ) {
    $.openViewDialog = function(){
        var formatItems = $(this).find('.viewDialogFormatItem');
        var selectedFileType = formatItems.find('.viewDialogFormatServiceDataFileType').first().html();
        if(formatItems.length > 1 || selectedFileType == 'xls')
        {
            var viewDialogContent = $(this).find('.viewDialogContent').html();
            $.fancybox({
                content: $(this).find('.viewDialogBody'),
                title: 'How Do you want to Open this File?',
                minWidth: 300,
                helpers: {
                    overlay : {
                        css : {
                            'background-color' : '#eee'
                        }
                    }
                },
                openEffect  : 'none',
                closeEffect	: 'none'            
            });
            $(this).find('.viewDialogContent').html(viewDialogContent);
            $(this).find('.viewDialogFormatItem').on('click',$.viewSelectedFormat);        
        }
        else
        {
            $.viewSelectedFormat.call(formatItems);
        }
    }
    
    $.viewSelectedFormat = function()
    {
        var selectedFileType = $(this).find('.viewDialogFormatServiceDataFileType').html();
        var selectedViewType = $(this).find('.viewDialogFormatServiceDataViewType').html();
        var selectedLinks = $(this).find('.viewDialogFormatServiceDataLinks').html();
        var selectedThumbs = $(this).find('.viewDialogFormatServiceDataThumbs').html()

        $.fancybox.close();

        if(selectedFileType != ''&& selectedViewType != '' && selectedLinks != '')
        {
            selectedLinks = $.parseJSON(selectedLinks);
            selectedThumbs = $.parseJSON(selectedThumbs);
            var thumbLinks = [];
            for ( var item in selectedThumbs )
                thumbLinks.push(selectedThumbs[item].href);
            switch(selectedFileType)
            {
                case 'ppt':
                case 'doc':
                case 'pdf':
                    switch(selectedViewType)
                    {
                        case 'png':
                        case 'jpeg':
                            $.fancybox(selectedLinks,{
                                helpers: {
                                    overlay : {
                                        css : {
                                            'background-color' : '#eee'
                                        }
                                    },
                                    thumbs : {
                                        height: selectedThumbs[0].height,
                                        width: selectedThumbs[0].width,
                                        source: thumbLinks
                                    }
                                },
                                openEffect  : 'none',
                                closeEffect	: 'none'            
                            });                        
                            break;
                        default:
                            window.open(selectedLinks[0].href);                            
                            break;
                    }
                    break;                    
                case 'xls':                                    
                case 'url':
                    window.open(selectedLinks[0].href);                    
                    break;                
                case 'png':
                case 'jpeg':
                    $.fancybox(selectedLinks,{
                        helpers: {
                            overlay : {
                                css : {
                                    'background-color' : '#eee'
                                }
                            }                                
                        },
                        openEffect  : 'none',
                        closeEffect	: 'none'            
                    });
                    break;
                case 'mp4':                    
                case 'video':
                    switch(selectedViewType)
                    {
                        case 'video':
                        case 'tab':                            
                            window.open(selectedLinks[0].href);                            
                            break;                        
                        case 'mp4':
                            VideoJS.players = {};
                            $.fancybox({
                                content: $('<video id="videoPlayer" class="video-js vjs-default-skin" width="640" height="480"></video>'),
                                helpers: {
                                    overlay : {
                                        css : {
                                            'background-color' : '#eee'
                                        }
                                    }
                                },
                                openEffect  : 'none',
                                closeEffect	: 'none',
                                afterClose: function(){
                                    $('#videoPlayer').remove();
                                }
                            });
                            var myPlayer = _V_("videoPlayer",{
                                controls: true, 
                                autoplay: true, 
                                preload: 'auto'
                            });
                            myPlayer.src({
                                type: "video/mp4", 
                                src: selectedLinks[0].href
                            });
                            break;                    
                    }
                    break;
            }
        }
    }
})( jQuery );    

