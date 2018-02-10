<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeShortcut;

	/** @var $menuItem MenuStripeShortcut */

	$blockId = sprintf('menu-stripe-shortcut-%s', $menuItem->id);
	echo $this->renderPartial('landingPageMarkup/menu_stripe/itemStyle', array('menuItem' => $menuItem, 'blockId' => $blockId), true);
?>
<li id="<? echo $blockId; ?>" class="menu-stripe-item-shortcut<? if (!$menuItem->enable): ?> disabled<? endif; ?><? if ($menuItem->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($menuItem->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($menuItem->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($menuItem->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
    <a class="shortcuts-link" href="<? echo isset($menuItem->shortcut) ? $menuItem->shortcut->getSourceLink() : '#'; ?>"
       title="<? echo $menuItem->title; ?>">
		<? echo $menuItem->title; ?>
        <div class="service-data">
			<? echo isset($menuItem->shortcut) ? $menuItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
        </div>
    </a>
</li>
