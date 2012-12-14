<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0" />
        <?php
        $version = '3.0';
        $cs = Yii::app()->clientScript;
        $cs->registerCoreScript('jquery');
        ?>
    </head>
    <body>
        <?php echo $content; ?>
    </body>
</html>