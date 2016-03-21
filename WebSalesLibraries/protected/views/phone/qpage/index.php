<?
	/** @var $page QPageRecord */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/phone/qpage-manager.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
	$(document).ready(function ()
	{
		$.SalesPortal.QPage.init();
	});
</script>
<? echo $this->renderPartial('pageContent', array('page' => $page)); ?>