<?
	/**
	 * @var $itemData ShortcutNavigationItem
	 */
?>
<? if (isset($itemData->shortcut)): ?>
    <a class="<? if ($itemData->enabled): ?>shortcuts-link<? endif; ?>"
       href="<? echo $itemData->getUrl(); ?>" target="_blank">
        <img class="item-icon expanded" src="<? echo $itemData->iconUrlExpanded; ?>">
        <img class="item-icon collapsed" src="<? echo $itemData->iconUrlCollapsed; ?>">
        <div class="item-title"
             style="color: <? echo Utils::formatColor($itemData->textColor); ?>;"><? echo $itemData->title; ?></div>
        <div class="service-data">
			<? echo $itemData->shortcut->getMenuItemData(); ?>
        </div>
    </a>
<? endif; ?>
