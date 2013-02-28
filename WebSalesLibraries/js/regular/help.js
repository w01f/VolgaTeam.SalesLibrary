(function ($)
{
    var loadHelpPage = function (tabId)
    {
        var pageIdSelector = '#' + tabId + ' .sel.help-page';
        var pageId = $(pageIdSelector).attr('id');
        $.ajax({
            type:"POST",
            url:"help/getPage",
            data:{
                pageId:pageId
            },
            beforeSend:function ()
            {
                $('#content').html('');
                $.showOverlay();
            },
            complete:function ()
            {
                $.hideOverlay();
                $.updateContentAreaDimensions();
                $('#calendar').fullCalendar('today');
            },
            success:function (msg)
            {
                $('#content').html('<div>' + msg + '</div>');
                var content = $('#content');
                $('.help-link').off('click').on('click', function ()
                {
                    $.viewSelectedFormat($(this), false, true);
                });
                $.buildCalendar(content);
            },
            async:true,
            dataType:'html'
        });
    };

    $.initHelpView = function (tabId)
    {
        var pageSelector = '#' + tabId + ' .enabled.help-page';
        $('.help-page').off('click');
        $(pageSelector).on('click', function ()
        {
            $(pageSelector).removeClass('sel');
            $(this).addClass('sel');
            loadHelpPage(tabId);
        });

        loadHelpPage(tabId);
    };

    $(document).ready(function ()
    {
    });
})(jQuery);