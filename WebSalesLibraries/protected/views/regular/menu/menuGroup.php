<?
	/** @var $menuGroup ShortcutGroup */
	$identifier = 'group-' . $menuGroup->id;
?>
<div data-groupid="<? echo $identifier; ?>" class="om-ctrlitem menu-icon-holder shortcut-menu-group-item">
	<? if ($menuGroup->useIcon == true): ?>
		<i class="<? echo $menuGroup->iconClass; ?>"></i>
	<? else: ?>
		<img src="<? echo $menuGroup->imageContent; ?>" alt=""/>
	<?endif; ?>
	<div class="service-data">
		<? echo $menuGroup->getGroupData(); ?>
	</div>
</div>