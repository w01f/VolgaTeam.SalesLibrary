window.salesDepot = window.salesDepot || { };

(function ($)
{
    $.initCalendarView = function ()
    {
        $.ajax({
            type:"POST",
            url:"calendar/getCalendarView",
            beforeSend:function ()
            {
                $('#content').html('');
                $.showOverlay();
            },
            complete:function ()
            {
                $.hideOverlay();
                $('#calendar').fullCalendar('today');
                $.updateContentAreaDimensions();
            },
            success:function (msg)
            {
                $('#content').html(msg);
                var content = $('#content');
                $.buildCalendar(content);
            },
            async:true,
            dataType:'html'
        });
    };

    $.buildCalendar = function (content)
    {
        var events = content.find('#schedule').html();
        if (events == null) return;
        events = $.parseJSON(events);
        if (events == null) return;
        $.each(events, function (index)
        {
            var date = events[index].start.split("-");
            events[index].start = new Date(date[2], date[0] - 1, date[1], 0, 0, 0, 0);
        });
        $('#calendar').fullCalendar({
            editable:false,
            ignoreTimezone:false,
            events:events,
            eventRender:function (event, element)
            {
                element.qtip({
                    content:$("<div/>").html(event.description).text(),
                    position:{
                        corner:{
                            tooltip:'bottomMiddle',
                            target:'topMiddle'
                        }
                    },
                    style:{
                        name:'green', // 'dark', 'cream', 'green', 'red', 'light', 'blue'
                        border:{
                            width:5,
                            radius:10
                        },
                        padding:10,
                        textAlign:'left',
                        tip:true
                    }
                });
            },

            eventClick:function (event)
            {
                if (event.url)
                {
                    window.open(event.url);
                    return false;
                }
            }

        });
    };

    $(document).ready(function ()
    {
        $('#calendar-pdf1').on('click', function ()
        {
            window.open('sd_cache/Calendar/pdf/1.pdf');
        });

        $('#calendar-pdf2').on('click', function ()
        {
            window.open('sd_cache/Calendar/pdf/2.pdf');
        });

        $('#calendar-pdf3').on('click', function ()
        {
            window.open('sd_cache/Calendar/pdf/3.pdf');
        });

        $('#calendar-video').on('click', function ()
        {
            VideoJS.players = {};
            $.fancybox({
                title:'Help Using This Site',
                content:$('<div style="height:480px; width:640px;"><video id="video-player" class="video-js vjs-default-skin" height = "480" width="640"></video><div>'),
                openEffect:'none',
                closeEffect:'none',
                afterClose:function ()
                {
                    $('#video-player').remove();
                }
            });
            _V_.options.flash.swf = 'vendor/video-js/video-js.swf';
            var myPlayer = _V_("video-player", {
                controls:true,
                autoplay:true,
                preload:'auto',
                width:640,
                height:480
            });
            myPlayer.src([
                {
                    src:'sd_cache/Calendar/helpvideo//help.mp4',
                    href:'sd_cache/calendar/helpvideo/help.mp4',
                    title:'Help Using This Site',
                    type:'video/mp4'
                }
            ]);
        });
    });
})(jQuery);