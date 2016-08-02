<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $shortcut PageContentShortcut
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/main-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/mainMenu', array('menuGroups' => $menuGroups, 'showMainSiteUrl' => $shortcut->showMainSiteUrl)); ?>
<div id="content">
	<div class="service-data default-shortcut-data">
		<? echo $shortcut->getMenuItemData(); ?>
	</div>
</div>