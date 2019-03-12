<? /** @var $content string */ ?>
<!DOCTYPE html>
<html>
    <head>
        <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0"/>
        <title><?php echo $this->pageTitle; ?></title>
        <?
            $cs = Yii::app()->clientScript;
            $cs->registerCoreScript('jquery');
            $cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-extensions.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
        ?>
        <script type="text/javascript">
            window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/';
            $.SalesPortal.SalesLibraryExtensions.activate();
        </script>
    </head>
    <body <? if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile()): ?> class="mobile-body"<? endif; ?>>
    <div id="content-overlay"></div>
    <!--  View dialog hidden part  -->
    <div style="display: none;">
        <a id="view-dialog-link" href="#view-dialog-container">View Options</a>

        <div id="view-dialog-wrapper">
            <div id="view-dialog-container"></div>
        </div>
    </div>
    <ul id="contextMenu" class="dropdown-menu" role="menu" style="display:none" >
        <li><a tabindex="-1" href="#">Action</a></li>
        <li><a tabindex="-1" href="#">Another action</a></li>
        <li><a tabindex="-1" href="#">Something else here</a></li>
        <li class="divider"></li>
        <li><a tabindex="-1" href="#">Separated link</a></li>
    </ul>
    <!------------------------->
    <? echo $content; ?>
    </body>
</html>