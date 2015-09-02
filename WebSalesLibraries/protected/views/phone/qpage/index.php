<?
	/** @var $page QPageRecord */

	$this->renderPartial('../site/scripts');
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>' + '/qpage/';
	$(document).ready(function ()
	{
		$.SalesPortal.QPage.init();
	});
</script>
<? echo $this->renderPartial('pageContent', array('page' => $page)); ?>