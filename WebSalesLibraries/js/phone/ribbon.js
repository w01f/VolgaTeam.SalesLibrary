(function( $ ) {
    $(document).ready(function() {
        $.initLibraries();
        $.initSearch();
        
        $('.logout-button').off('click'); 
        $('.logout-button').on('click',function(){
            $.logout();
        });                    
        
        $('#button-cllapse-all').off('click'); 
        $('#button-cllapse-all').on('click',function(){
            $.collapseAllLibraries();
        });      
        
//        $('#libraries').on('pageshow', function(e){
//            if($.storedLibrariesScrollPosition!= null)
//            {
//                $(window.document).scrollTop($.storedLibrariesScrollPosition-50)
//                $.storedLibrariesScrollPosition = null;
//            }
//        });
//        
//        $('#links').on('pageshow', function(e){
//            if($.storedLinksScrollPosition!= null)
//            {
//                $(window.document).scrollTop($.storedLinksScrollPosition-50)
//                $.storedLinksScrollPosition = null;
//            }
//        });
        
        $('#gallery-page').on('pageshow', function(e){
            var options = {
                enableMouseWheel: false, 
                enableKeyboard: false,
                captionAndToolbarAutoHideDelay: 0,
                jQueryMobile: true
            };
            $("#gallery a").photoSwipe(options);
            return true;
        }).on('pagehide', function(e){
            var currentPage = $(e.target),
            photoSwipeInstance = window.Code.PhotoSwipe.getInstance(currentPage.attr('id'));
            if (typeof photoSwipeInstance != "undefined" && photoSwipeInstance != null) {
                window.Code.PhotoSwipe.detatch(photoSwipeInstance);
            }
            return true;
        });
    });
})( jQuery );