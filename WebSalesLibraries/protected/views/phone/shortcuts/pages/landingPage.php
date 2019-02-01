<?
    /** @var $shortcut LandingPageShortcut */

	use application\models\shortcuts\models\landing_page\mobile_items\ShortcutItem;

	/** @var NavigationPanelShortcut[] $navigationPanelShortcuts */
	$navigationPanelShortcuts = array();
	foreach ($shortcut->mobileSettings->items as $item)
		if ($item instanceof ShortcutItem)
		{
			$childShortcut = $item->shortcut;
			if ($childShortcut instanceof NavigationPanelShortcut)
			    $navigationPanelShortcuts[] = $childShortcut;
		}
?>
<div class="cbp-l-grid-masonry">
	<? foreach ($shortcut->mobileSettings->items as $item): ?>
        <a class="cbp-item shortcuts-link" href="<? echo $item->getSourceLink(); ?>" data-ajax="false" target="_blank">
            <div class="cbp-caption">
				<? if ($item->useIcon == true): ?>
                    <i class="logo <? echo $item->iconClass; ?>" <? if (!empty($item->iconColor)): ?>style="color: <? echo Utils::formatColor($item->iconColor); ?>;" <? endif; ?>></i>
				<? elseif ($item->useIcon != true && isset($item->imageUrl)): ?>
                    <img class="logo" src="<? echo $item->imageUrl; ?>" alt=""/>
				<? endif; ?>
                <p class="title" <? if (!empty($item->titleColor)): ?>style="color: <? echo Utils::formatColor($item->titleColor); ?>;" <? endif; ?>><? echo $item->title; ?></p>
            </div>
            <div class="service-data">
				<? echo $item->getMenuItemData(); ?>
            </div>
        </a>
	<? endforeach; ?>
</div>
<? if (count($navigationPanelShortcuts) > 0): ?>
    <div class="navigation-panels-dynamic">
		<? foreach ($navigationPanelShortcuts as $navigationPanelShortcut): ?>
			<? $shortcutNavigationPanel = $navigationPanelShortcut->getNavigationPanel(); ?>
			<? if (isset($shortcutNavigationPanel)): ?>
                <div data-role="panel" data-display="overlay"
                     data-position="<? echo $navigationPanelShortcut->position; ?>"
                     id="<? echo $navigationPanelShortcut->expandPanelId; ?>">
                    <ul class="navigation-items-container navigation-items-container-dynamic" data-role="listview">
						<? echo $this->renderPartial('../shortcuts/navigationPanel/itemsList', array('navigationPanel' => $shortcutNavigationPanel)); ?>
                    </ul>
                </div>
			<? endif; ?>
		<? endforeach; ?>
    </div>
<? endif; ?>
