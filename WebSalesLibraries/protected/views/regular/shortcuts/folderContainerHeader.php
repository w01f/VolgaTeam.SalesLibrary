<?
	/** @var $windowShortcut WindowShortcut */
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/banner.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/folder-links.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/window-loader.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
?>
<div class="shortcut-title"><? echo $windowShortcut->title; ?></div>