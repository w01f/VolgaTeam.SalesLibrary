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
<table id="content">
	<tr>
		<td class="navigation-panel">
		</td>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<div class="service-data default-shortcut-data">
					<? echo $shortcut->getMenuItemData(); ?>
				</div>
			</div>
		</td>
	</tr>
</table>