<?
	/**
	 * @var $defaultShortcut PageContentShortcut
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
?>
<script type="text/javascript">
	window.BaseUrl = '<?php echo Yii::app()->getBaseUrl(true); ?>';
</script>
<? $this->renderPartial('../menu/singlePageMenu'); ?>
<div id="content" oncontextmenu="return false;">
	<div class="service-data default-shortcut-data">
		<? echo $defaultShortcut->getMenuItemData(); ?>
	</div>
</div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>

	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>