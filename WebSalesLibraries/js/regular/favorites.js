(function ($)
{
    $.initFavoritesView = function ()
    {
        $.ajax({
            type:"POST",
            url:"favorites/getFavoritesView",
            beforeSend:function ()
            {
                $('#content').html('');
                $.showOverlay();
            },
            complete:function ()
            {
                $.hideOverlay();
            },
            success:function (msg)
            {
                $('#content').html('<div>' + msg + '</div>');
                $.updateContentAreaDimensions();
            },
            async:true,
            dataType:'html'
        });
    };

    $(document).ready(function ()
    {
    });
})(jQuery);