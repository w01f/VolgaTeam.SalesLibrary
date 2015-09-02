<?
	/** @var $menuGroup ShortcutGroup */
	$identifier = 'group-' . $menuGroup->id;
?>
<div data-groupid="<? echo $identifier; ?>" class="om-ctrlitem menu-icon-holder">
	<? if ($menuGroup->useIcon == true): ?>
		<i class="<? echo $menuGroup->iconClass; ?>"></i>
	<? else: ?>
		<img src="<? echo $menuGroup->imageContent; ?>" alt=""/>
	<?endif; ?>
</div>