<!DOCTYPE html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no">

    <meta name="description" content="">
    <meta name="author" content="">
    <title><?php echo $this->pageTitle; ?></title>
    <?
        $cs = Yii::app()->clientScript;
        $cs->registerCoreScript('jquery');
    ?>
    <script type="text/javascript">
        window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/';
    </script>
</head>
<body>
    <? echo $content; ?>
</body>
