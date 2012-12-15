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
    
    var openFile = function(url)
    {
        window.open(url.replace(/&amp;/g,'%26'));
    }    
    
    var downloadFile = function(linkId, title)
    {
        $.ajax({
            type: "POST",
            url: "site/downloadDialog",
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
                var content = $(msg);
                content.find( ".download-type button" ).on('click',function(){
                    if(!$(this).hasClass('active'))
                    {
                        $('.download-type button').removeClass('active');
                        $(this).addClass('active');    
                    }
                });                
                content.find('#download-cancel').on('click',function(){
                    $.fancybox.close();
                });                    
                content.find('#download-accept').on('click',function(){
                    window.open("site/downloadFile?linkId="+linkId+"&format="+$('.download-type button.active img').attr('alt'));
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
    
    $.emailFile = function(linkId, title)
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

                content.find('.dropdown .dropdown-toggle').dropdown();
                content.find('.dropdown .dropdown-toggle').on('click',function(event){
                    var selector = $(this).parent();
                    var textFiledSelector = '#' + selector.attr('id').replace("-select", "");

                    var applyEmails = function(event){
                        var selectedEmails = [];
                        $.each(selector.find(':checked'),function(){
                            selectedEmails.push($(this).val());
                        });
                        if(selectedEmails.length>0)
                            $(textFiledSelector).val(selectedEmails.join('; '));
                        else
                            $(textFiledSelector).val('');
                        
                        $(this).parent.dropdown('toggle');
                        event.stopPropagation();
                    };

                    selector.find('.apply-selection').off('click');
                    selector.find('.apply-selection').on('click',applyEmails);                
                    
                    $(this).parent.dropdown('toggle');
                    event.stopPropagation();
                });
                
                content.find('.dropdown .dropdown-menu li').on('touchstart',function(event){
                    event.stopPropagation();
                });                
                
                content.find('.dropdown .dropdown-menu li').on('click',function(event){
                    event.stopPropagation();
                });                
                
                content.find('#email-accept').on('click',function(){
                    $.ajax({
                        type: "POST",
                        url: "site/emailLinkSend",
                        data: {
                            linkId: linkId,
                            emailTo: content.find('#email-to').val(),
                            emailCopyTo: content.find('#email-to-copy').val(),
                            emailFrom: content.find('#email-from').val(),
                            emailToMe: content.find('#email-to-me').is(':checked'),
                            emailSubject: content.find('#email-subject').val(),
                            emailBody: content.find('#email-body').val(),
                            expiresIn: content.find('#expires-in').val()
                        },
                        success: function(){
                            $.fancybox.close();
                            $.ajax({
                                type: "POST",
                                url: "site/emailLinkSuccess",
                                data: {},
                                beforeSend: function(){
                                    $.showOverlayLight();
                                },
                                complete: function(){
                                    $.hideOverlayLight();
                                },
                                success: function(msg){
                                    var content = $(msg);
                                    content.find('#accept').off('click');
                                    content.find('#accept').on('click',function(){
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
                        },
                        complete: function(){
                            $.fancybox.close();
                        },
                        async: true,
                        dataType: 'html'                        
                    });
                });
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
                            $.emailFile(selectedFileId,selectedLinks[0].title);
                            break;
                        default:
                            openFile(selectedLinks[0].href);
                            break;
                    }
                    break;                    
                case 'xls':
                    switch(selectedViewType)
                    {
                        case 'email':
                            $.emailFile(selectedFileId,selectedLinks[0].title);
                            break;
                        default:
                            openFile(selectedLinks[0].href);
                            break;
                    }                    
                    break;
                case 'url':
                case 'other':
                    openFile(selectedLinks[0].href);
                    break;                
                case 'png':
                case 'jpeg':
                    switch(selectedViewType)
                    {
                        case 'email':
                            $.emailFile(selectedFileId,selectedLinks[0].title);
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
                            openFile(selectedLinks[0].href);
                            break;                        
                        case 'email':
                            $.emailFile(selectedFileId,selectedLinks[0].title);
                            break;                            
                        case 'download':
                            downloadFile(selectedFileId,selectedLinks[0].title);
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

