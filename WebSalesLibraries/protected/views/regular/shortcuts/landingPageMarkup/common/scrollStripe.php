<?
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeItem;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeShortcut;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeUrl;

	/** @var $contentBlock ScrollStripeBlock */

	/** @var ScrollStripeItem[] $scrollStripeItem */
	$scrollStripeItems = $contentBlock->items;

	$stripeConfigurationClass = '';
	if ($contentBlock->iconPosition == ScrollStripeBlock::IconPositionTop)
		$stripeConfigurationClass .= ' center-stack';

	if ($contentBlock->stripeSize == ScrollStripeBlock::StripeSizeMedium)
		$stripeConfigurationClass .= ' scrolltab-medium';
	else if ($contentBlock->stripeSize == ScrollStripeBlock::StripeSizeLarge)
		$stripeConfigurationClass .= ' scrolltab-large';
?>
<div id="scroll-stripe-<? echo $contentBlock->id; ?>"
     class="scroll_tabs_theme_light<? echo $stripeConfigurationClass; ?> scroll-stripe">
	<? foreach ($scrollStripeItems as $stripeItem): ?>
        <span>
            <? if ($stripeItem->type === 'url'): ?>
                <? /** @var ScrollStripeUrl $stripeItem */ ?>
                <a href="<? echo $stripeItem->url; ?>" target="_blank"
                   class="item"<? if (!empty($stripeItem->hoverText)): ?> title="<? echo $stripeItem->hoverText; ?>"<? endif; ?>>
            <? elseif ($stripeItem->type === 'shortcut'): ?>
                <? /** @var ScrollStripeShortcut $stripeItem */ ?>
                <a href="<? echo isset($stripeItem->shortcut) ? $stripeItem->shortcut->getSourceLink() : '#'; ?>"
                   class="item shortcuts-link<? if (!isset($stripeItem->shortcut)): ?> disabled<? endif; ?>"<? if (!empty($stripeItem->hoverText)): ?> title="<? echo $stripeItem->hoverText; ?>"<? endif; ?>>
                    <div class="service-data">
                        <? echo isset($stripeItem->shortcut) ? $stripeItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                    </div>
            <? endif; ?>
                    <? if (!empty($stripeItem->icon)): ?>
                        <? if ($contentBlock->iconPosition == ScrollStripeBlock::IconPositionTop): ?>
                            <div class="icomoon-wrap">
                        <? endif; ?>
                        <i class="icomoon<? if (!empty($stripeItem->iconSize)): ?> icomoon-<? echo $stripeItem->iconSize; ?><? endif; ?> <? echo $stripeItem->icon; ?>"
                           style="color: <? echo Utils::formatColor($stripeItem->iconColor); ?>"></i>
                        <? if ($contentBlock->iconPosition == ScrollStripeBlock::IconPositionTop): ?>
                            </div>
                        <? endif; ?>
                    <? endif; ?>
	                <?
                        $stripeTextId = sprintf("stripe-item-text-%s", $stripeItem->id);
                        echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
	                        array(
		                        'textAppearance' => $stripeItem->getTextAppearance(),
		                        'blockId' => $stripeTextId
	                        )
	                        , true);
                    ?>
                    <span id="<? echo $stripeTextId; ?>" class="item-text"><? echo $stripeItem->text;?></span>
                </a>
          </span>
	<? endforeach; ?>
</div>
