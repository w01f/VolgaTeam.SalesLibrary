<?php
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/js/fancybox/source/jquery.fancybox.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/js/fancybox/source/helpers/jquery.fancybox-thumbs.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/js/video-js/video-js.min.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/css/ribbon.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/css/columns.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/css/search.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/css/viewDialog.css');
$cs->registerCoreScript('jquery');
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/video-js/video.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/overlay.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/textSizing.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/scaling.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/linkViewing.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/columns.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/search.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/ribbon.js', CClientScript::POS_HEAD);
?>

