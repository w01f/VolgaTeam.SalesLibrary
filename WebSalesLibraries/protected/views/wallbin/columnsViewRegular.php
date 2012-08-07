<?php
$cs = Yii::app()->clientScript;
$cs->registerCssFile(Yii::app()->baseUrl . '/css/columns.css');
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/overlay.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/textSizing.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/scaling.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/linkViewing.js', CClientScript::POS_HEAD);
$cs->registerScriptFile(Yii::app()->baseUrl . '/js/columns.js', CClientScript::POS_HEAD);
?>