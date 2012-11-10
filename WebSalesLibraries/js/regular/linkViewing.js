(function( $ ) {
    $.openViewDialogEmbedded = function(){
        var formatItems = $(this).find('td');
        var fullScreenSelector = $(this).find('.use-fullscreen');
        if(formatItems.length > 1)
        {
            $(this).find('.view-dialog-body .format-list td').off('click');        
            $(this).find('.view-dialog-body .format-list td').on('click',function(){
                $.viewSelectedFormat($(this), fullScreenSelector.is(':checked'));
            } );        
            var viewDialogContent = $(this).find('.view-dialog-content').html();
            $.fancybox({
                content: $(this).find('.view-dialog-body'),
                title: 'How Do you want to Open this File?',
                minWidth: 400,
                openEffect  : 'none',
                closeEffect	: 'none',
                helpers : {
                    overlay : {
                        css : {
                            'background' : 'rgba(224, 224, 224, 0.8)'
                        }
                    }
                }
            });
            $(this).find('.view-dialog-content').html(viewDialogContent);
        }
        else
        {
            $.viewSelectedFormat.call(formatItems[0],formatItems[0],false);
        }
    }
    
    $.openViewDialogSearchGrid = function(linkId){
        $.ajax({
            type: "POST",
            url: "search/viewLink",
            data: {
                linkId: linkId
            },
            beforeSend: function(){
                $.showOverlayLight();
            },
            complete: function(){
                $.hideOverlayLight();
            },
            success: function(msg){
                $.viewDilogContent = $('<div>'+msg+'<div>');
                $.openViewDialogEmbedded.call($.viewDilogContent);
            },
            error: function(){
                $('#search-result').html('');
            },            
            async: true,
            dataType: 'html'                        
        });  
    }
    
    $.openFileCard = function(){
        var fileCardContent = $(this).find('.file-card-content').html();
        $.fancybox({
            content: $(this).find('.file-card-body'),
            title: 'File Card',
            minWidth: 400,
            openEffect  : 'none',
            closeEffect	: 'none',
            helpers : {
                overlay : {
                    css : {
                        'background' : 'rgba(224, 224, 224, 0.8)'
                    }
                }
            }
        });
        $(this).find('.file-card-content').html(fileCardContent);
    }
    
    $.downloadFile = function(url)
    {
        window.open(url);
    }    
    
    $.emailFile = function(linkId, libraryId,title)
    {
        $.ajax({
            type: "POST",
            url: "site/emailLinkDialog",
            data: {
            },
            beforeSend: function(){
                $.showOverlayLight();
            },
            complete: function(){
                $.hideOverlayLight();
            },
            success: function(msg){
                var content = $(msg);
                content.find('#email-accept').off('click');
                content.find('#email-accept').on('click',function(){
                    $.ajax({
                        type: "POST",
                        url: "site/emailLinkSend",
                        data: {
                            linkId: linkId,
                            libraryId: libraryId,
                            emailTo: content.find('#email-to').val(),
                            emailFrom: content.find('#email-from').val(),
                            emailSubject: content.find('#email-subject').val(),
                            emailBody: content.find('#email-body').val(),
                            expiresIn: content.find('#expires-in').val()
                        },
                        complete: function(){
                            $.fancybox.close();
                        },
                        async: true,
                        dataType: 'html'                        
                    });
                });
                content.find('#email-cancel').off('click');
                content.find('#email-cancel').on('click',function(){
                    $.fancybox.close();
                });                    
                $.fancybox({
                    content: content,
                    title: title,
                    openEffect  : 'none',
                    closeEffect	: 'none',
                    helpers : {
                        overlay : {
                            css : {
                                'background' : 'rgba(224, 224, 224, 0.8)'
                            }
                        }
                    }
                });                
            },
            error: function(){
            },            
            async: true,
            dataType: 'html'                        
        });          
    }    
    
    $.viewSelectedFormat = function(target, fullScreen)
    {
        var selectedFileId = $(target).find('.service-data .link-id').html();
        var selectedLibraryId = $(target).find('.service-data .library-id').html();
        var selectedFileType = $(target).find('.service-data .file-type').html();
        var selectedViewType = $(target).find('.service-data .view-type').html();
        var selectedLinks = $(target).find('.service-data .links').html();
        var selectedThumbs = $(target).find('.service-data .thumbs').html()
        
        $.fancybox.close();
        
        if(selectedFileType != ''&& selectedViewType != '' && selectedLinks != '')
        {
            selectedLinks = $.parseJSON(selectedLinks);
            selectedThumbs = $.parseJSON(selectedThumbs);
            if(selectedThumbs!= null)
            {
                $.each(selectedLinks,function(index) {
                    selectedLinks[index].thumb = selectedThumbs[index].href;
                    selectedLinks[index].image = selectedLinks[index].href;
                });
            }
            switch(selectedFileType)
            {
                case 'ppt':
                case 'doc':
                case 'pdf':
                    switch(selectedViewType)
                    {
                        case 'png':
                        case 'jpeg':
                            if(fullScreen)
                            {
                                $.ajax({
                                    type: "POST",
                                    url: "wallbin/runFullscreenGallery",
                                    data: {
                                        selectedLinks:$.toJSON(selectedLinks)
                                    },
                                    beforeSend: function(){
                                        $.showOverlayLight();
                                    },
                                    complete: function(){
                                        $.hideOverlayLight();
                                    },
                                    success: function(msg){
                                        var galleryWindow = window.open();
                                        galleryWindow.document.write(msg);
                                    },
                                    error: function(){
                                    },            
                                    async: true,
                                    dataType: 'html'                        
                                });                                  
                            }
                            else
                            {
                                $.fancybox(selectedLinks,{
                                    openEffect  : 'none',
                                    closeEffect	: 'none',
                                    helpers : {
                                        overlay : {
                                            css : {
                                                'background' : 'rgba(224, 224, 224, 0.8)'
                                            }
                                        },
                                        thumbs : {
                                            height: selectedThumbs[0].height,
                                            width: selectedThumbs[0].width,
                                            source: this.thumb
                                        }                                    
                                    }
                                });      
                            }
                            break;
                        case 'email':
                            $.emailFile(selectedFileId,selectedLibraryId,selectedLinks[0].title);
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
                            $.emailFile(selectedFileId,selectedLibraryId,selectedLinks[0].title);
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
                            $.emailFile(selectedFileId, selectedLibraryId, selectedLinks[0].title);
                            break;
                        default:
                            $.fancybox(selectedLinks,{
                                openEffect  : 'none',
                                closeEffect	: 'none',
                                helpers : {
                                    overlay : {
                                        css : {
                                            'background' : 'rgba(224, 224, 224, 0.8)'
                                        }
                                    }
                                }
                            });
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
                            $.downloadFile(selectedLinks[0].href);
                            break;                        
                        case 'email':
                            $.emailFile(selectedFileId,selectedLibraryId,selectedLinks[0].title);
                            break;                            
                        case 'mp4':
                            playVideo(selectedLinks);
                            break;                    
                    }
                    break;
            }
        }
    }
    
    var playVideo = function(links)
    {
        VideoJS.players = {};
        $.fancybox({
            title: links[0].title,
            content: $('<div style="height:480px; width:640px;"><video id="video-player" class="video-js vjs-default-skin" height = "480" width="640"></video><div>'),
            openEffect  : 'none',
            closeEffect	: 'none',
            helpers : {
                overlay : {
                    css : {
                        'background' : 'rgba(224, 224, 224, 0.8)'
                    }
                }
            },
            afterClose: function(){
                $('#video-player').remove();
            }
        });
        _V_.options.flash.swf = links[0].swf;
        var myPlayer = _V_("video-player",{
            controls: true, 
            autoplay: true, 
            preload: 'auto',
            width: 640,
            height:480
        });
        myPlayer.src(links);        
    }
})( jQuery );    

