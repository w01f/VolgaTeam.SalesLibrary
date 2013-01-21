<script type="text/javascript">
    (function($){
        var updatePosition = function(){
            $('#container').css({
                'height':$(window).height()+'px'
            });
        };
        
        $(document).ready(function() 
        {
            updatePosition();
            $(window).on('resize',updatePosition); 
        });
    })( jQuery );    
</script>        
<table id="container" style="width: 100%; height: 100%; text-align: center;">
    <tr><td><img style="width: 100%; height: auto;" src="<?php echo Yii::app()->baseUrl . '/images/unauthorized.png'; ?>"></td></tr>
</table>