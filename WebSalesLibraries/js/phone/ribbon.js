(function( $ ) {
    $(document).ready(function() {
        $.mobile.changePage( "#libraries", {
            transition: "slidefade",
            direction: "reverse"
        });
        
        $.initLibraries();
        $.initSearch();
        
        $('.logout-button').off('click').on('click',function(){
            $.logout();
        });  

        $('#gallery-page').on('pageshow', function(e){
            var options = {
                enableMouseWheel: false, 
                enableKeyboard: false,
                captionAndToolbarAutoHideDelay: 0,
                jQueryMobile: true
            };
            $("#gallery").find("a").photoSwipe(options);
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