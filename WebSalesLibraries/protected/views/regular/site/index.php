<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $defaultShortcut PageContentShortcut
	 */

	$this->renderPartial('scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/main-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);


?>
<? $this->renderPartial('../menu/mainMenu', array('menuGroups' => $menuGroups)); ?>
<div id="content">
	<? if (isset($defaultShortcut)): ?>
		<div class="service-data default-shortcut-data">
			<? echo $defaultShortcut->getMenuItemData(); ?>
		</div>
	<? endif; ?>
</div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>
	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>
<!------------------------->