<?
	/**
	 * @var $itemData ShortcutNavigationItem
	 */

	/** @var RegularNavigationItemSettings $settings */
	$settings = $itemData->settings;
?>
<? if (isset($itemData->shortcut)): ?>
    <a class="<? if ($settings->enabled): ?>shortcuts-link<? endif; ?>"
       href="<? echo $itemData->getUrl(); ?>" target="_blank">
        <img class="item-icon expanded" src="<? echo $settings->iconUrlExpanded; ?>">
        <img class="item-icon collapsed" src="<? echo $settings->iconUrlCollapsed; ?>">
        <div class="item-title"
             style="color: <? echo Utils::formatColorToHex($settings->textColor); ?>;"><? echo $settings->title; ?></div>
        <div class="service-data">
			<? echo $itemData->shortcut->getMenuItemData(); ?>
        </div>
    </a>
<? endif; ?>
