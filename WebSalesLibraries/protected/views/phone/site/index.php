<?php
$version = '1.0';
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/css/phone/libraries.css?' . $version);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/phone/libraries.js?' . $version, CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/vendor/mobile/jquery.mobile-1.2.0.js?' . $version, CClientScript::POS_HEAD);
?>