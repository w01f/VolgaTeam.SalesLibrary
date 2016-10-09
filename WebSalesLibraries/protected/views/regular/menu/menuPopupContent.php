<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 */
?>
<div class="om-ctrlbar menu-back-colored">
	<div class="om-controlitems">
		<div class="om-controlitem om-closenav menu-icon-holder">
			<i class="icon-close2"></i>
		</div>
		<div class="om-ctrlitems">
			<div class="om-centerblock">
				<? foreach ($menuGroups as $menuGroup): ?>
					<? $this->renderPartial('../menu/menuGroup', array('menuGroup' => $menuGroup)); ?>
				<? endforeach; ?>
			</div>
		</div>
		<div class="om-movenext om-controlitem">
			<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/menu/right-arrow.png' ?>" alt=""/>
		</div>
	</div>
</div>
<div class="om-itemholder">
	<div class="om-itemlist">
		<? foreach ($menuGroups as $menuGroup): ?>
			<? foreach ($menuGroup->menuItems as $menuItem): ?>
				<? $this->renderPartial('../menu/menuItem', array('menuItem' => $menuItem)); ?>
			<? endforeach; ?>
		<? endforeach; ?>
	</div>
</div>