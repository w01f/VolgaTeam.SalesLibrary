<?
	/**
	 * @var $defaultShortcut PageContentShortcut
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/singlePageMenu'); ?>
<div id="content" oncontextmenu="return false;">
	<div class="service-data default-shortcut-data">
		<? echo $defaultShortcut->getMenuItemData(); ?>
	</div>
</div>