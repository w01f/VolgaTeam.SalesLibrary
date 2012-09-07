(function( $ ) {
    $.openViewDialog = function(){
        var formatItems = $(this).find('.view-dialog-body .format-list .item');
        var selectedFileType = formatItems.find('.service-data .file-type').first().html();
        if(formatItems.length > 1 || selectedFileType == 'xls')
        {
            var viewDialogContent = $(this).find('.view-dialog-content').html();
            $.fancybox({
                content: $(this).find('.view-dialog-body'),
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
            $(this).find('.view-dialog-content').html(viewDialogContent);
            $(this).find('.view-dialog-body .format-list .item').on('click',$.viewSelectedFormat);        
        }
        else
        {
            $.viewSelectedFormat.call(formatItems);
        }
    }
    
    $.viewSelectedFormat = function()
    {
        var selectedFileType = $(this).find('.service-data .file-type').html();
        var selectedViewType = $(this).find('.service-data .view-type').html();
        var selectedLinks = $(this).find('.service-data .links').html();
        var selectedThumbs = $(this).find('.service-data .thumbs').html()

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
                        case 'ogv':
                            window.open(selectedLinks[0].src);                            
                            break;                        
                        case 'mp4':
                            VideoJS.players = {};
                            $.fancybox({
                                content: $('<div style="height:480px; width:640px;"><video id="video-player" class="video-js vjs-default-skin" height = "480" width="640"></video><div>'),
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
                                    $('#video-player').remove();
                                }
                            });
                            _V_.options.flash.swf = selectedLinks[0].swf;
                            var myPlayer = _V_("video-player",{
                                controls: true, 
                                autoplay: true, 
                                preload: 'auto',
                                width: 640,
                                height:480
                            });
                            myPlayer.src(selectedLinks);
                            break;                    
                    }
                    break;
            }
        }
    }
})( jQuery );    

