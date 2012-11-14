<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <?php
        $version = '5.0';
        $cs = Yii::app()->clientScript;
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?' . $version);
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?' . $version);
        $cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?' . $version);
        $cs->registerCoreScript('jquery');
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
        $cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/linkViewing.js?' . $version, CClientScript::POS_HEAD);
        ?>
        <script type="text/javascript">
            (function($){
                $(document).ready(function() 
                {
                    $('a#view-dialog-link').fancybox();
                    $('a#video-link').on('click',function(){
                        $.viewSelectedFormat($(this), false);
                    } );
                });
            })( jQuery );    
        </script>        
    </head>
    <body>
        <span>Billy Byrd Sent you this Video Link to preview.</span>
        <br>
        <br>
        <span>Click this link to play the MP4 file:</span>
        <a id="video-link" href="#">
            <span><?php echo $link->fileName; ?></span>
            <span class ="service-data" style="display: none;">
                <div class ="link-id"><?php echo $link->id; ?></div>
                <div class ="library-id"><?php echo $link->parent->parent->parent->id; ?></div>
                <div class ="file-type"><?php echo $link->originalFormat; ?></div>
                <div class ="view-type"><?php echo 'mp4'; ?></div>
                <?php $viewLinks = $link->getViewSource('mp4'); ?>
                <?php if (isset($viewLinks)): ?>
                    <div class ="links"><?php echo json_encode($viewLinks); ?></div>
                <?php endif; ?>
            </span>
        </a>
        <?php if (isset($expiresIn)): ?>
            <br>
            <br>
            <span>This link will expire in <?php echo $expiresIn; ?> days.</span>
        <?php endif; ?>        
        <!--  View dialog hidden part  -->
        <div>
            <a id="view-dialog-link" href="#view-dialog-container" style="display: none;">View Options</a>
            <div id="view-dialog-wrapper" style="display: none;">
                <div id="view-dialog-container">
                </div>
            </div>
        </div>
        <!------------------------->
    </body>
</html>




