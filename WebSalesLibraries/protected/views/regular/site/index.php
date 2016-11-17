<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $defaultShortcut PageContentShortcut
	 */

	$this->renderPartial('scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/main-page-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/mainMenu', array('menuGroups' => $menuGroups)); ?>
<table id="content">
	<tr>
		<td class="navigation-panel">
		</td>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<? if (isset($defaultShortcut)): ?>
					<div class="service-data default-shortcut-data">
						<? echo $defaultShortcut->getMenuItemData(); ?>
					</div>
				<? endif; ?>
			</div>
		</td>
	</tr>
</table>