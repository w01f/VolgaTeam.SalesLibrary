<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeDivider;

	/** @var $menuItem MenuStripeDivider */

	$blockId = sprintf('menu-stripe-divider-%s', $menuItem->id);
	$textAppearance = $menuItem->textAppearance;
?>
<style>
    <? echo '#'.$blockId; ?>
    {
        <? if (isset($textAppearance->color)): ?>
            background-color: <? echo Utils::formatColorToHex($textAppearance->color); ?> !important;
        <? endif; ?>
    }
</style>
<li id="<? echo $blockId; ?>" role="separator" class="divider menu-stripe-item-divider<? if ($menuItem->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($menuItem->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($menuItem->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($menuItem->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
</li>
