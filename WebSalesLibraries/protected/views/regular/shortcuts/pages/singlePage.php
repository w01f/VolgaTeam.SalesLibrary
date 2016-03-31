<?
	/**
	 * @var $shortcut PageContentShortcut
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/singlePageMenu', array('showMainSiteUrl' => $shortcut->showMainSiteUrl)); ?>
<div id="content">
	<div class="service-data default-shortcut-data">
		<? echo $shortcut->getMenuItemData(); ?>
	</div>
</div>