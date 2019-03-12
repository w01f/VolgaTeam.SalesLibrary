<? /** @var $content string */ ?>
<!DOCTYPE html>
<html id="jqm-markup">
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
        <?
            $cs = Yii::app()->clientScript;
            $cs->registerCoreScript('jquery');
            $cs->registerCoreScript('jquery.ui');
            $cs->registerCoreScript('cookie');
        ?>
        <script type="text/javascript">
            window.BaseUrl = '<? echo Yii::app()->getBaseUrl(true); ?>' + '/';
            <?if (Yii::app()->params['jqm_home_page_enabled'] == true):?>
                window.homePage = window.BaseUrl;
            <?endif;?>
        </script>
        <title><? echo $this->pageTitle; ?></title>
    </head>
    <body>
    <? echo $content; ?>
    </body>
</html>