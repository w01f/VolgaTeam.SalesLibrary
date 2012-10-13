<?php
$version = '2.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->clientScript->getCoreScriptUrl() . '/jui/css/base/jquery-ui.css');
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/video-js/video-js.min.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/bootstrap/css/bootstrap.min.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/vendor/datepicker/css/daterangepicker.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/ribbon.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/columns.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/search.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/view-dialog.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/file-card.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/library-selector.css?'.$version);
$cs->registerCssFile(Yii::app()->baseUrl . '/css/search-grid.css?'.$version);
$cs->registerCoreScript('jquery');
$cs->registerCoreScript('jquery.ui');
$cs->registerCoreScript('cookie');
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/json/jquery.json-2.3.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/jquery.fancybox.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/lib/jquery.mousewheel-3.0.6.pack.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/fancybox/source/helpers/jquery.fancybox-thumbs.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/video-js/video.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/bootstrap/js/bootstrap.min.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/date.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/datepicker/js/daterangepicker.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/overlay.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/textSizing.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/scaling.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/linkViewing.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/columns.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/librarySelector.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/search-grid.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/search.js?'.$version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/ribbon.js?'.$version, CClientScript::POS_HEAD);
?>

