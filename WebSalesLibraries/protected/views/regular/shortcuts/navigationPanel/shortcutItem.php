<?
	/**
	 * @var $itemData ShortcutNavigationItem
	 */
?>
<? if (isset($itemData->shortcut)): ?>
	<a class="shortcuts-link" href="<? echo $itemData->shortcut->getSourceLink(); ?>" target="_blank">
		<img class="item-icon" src="<? echo $itemData->iconUrl; ?>">
		<div class="item-title"><? echo $itemData->title; ?></div>
		<div class="service-data">
			<? echo $itemData->shortcut->getMenuItemData(); ?>
		</div>
	</a>
<? endif; ?>
