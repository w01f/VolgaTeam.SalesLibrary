<?
	/** @var $windowShortcut WindowShortcut */
	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/wallbin.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/window-loader.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
?>
<div class="shortcut-title"><? echo $windowShortcut->title; ?></div>