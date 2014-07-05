<?
	/** @var $windowShortcut WindowShortcut */
	$version = '1.0';
	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/wallbin.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/window-loader.js?' . $version, CClientScript::POS_HEAD);
?>
<div class="shortcut-title"><? echo $windowShortcut->title; ?></div>