<?
	/** @var $contentBlock \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock */

	/** @var \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeItem[] $scrollStripeItem */
	$scrollStripeItems = $contentBlock->items;

	$stripeConfigurationClass = '';
	if ($contentBlock->iconPosition == \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock::IconPositionTop)
		$stripeConfigurationClass .= ' center-stack';

	if ($contentBlock->stripeSize == \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock::StripeSizeMedium)
		$stripeConfigurationClass .= ' scrolltab-medium';
	else if ($contentBlock->stripeSize == \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock::StripeSizeLarge)
		$stripeConfigurationClass .= ' scrolltab-large';
?>
<div id="scroll-stripe-<? echo $contentBlock->id; ?>"
     class="scroll_tabs_theme_light<? echo $stripeConfigurationClass; ?> scroll-stripe">
	<? foreach ($scrollStripeItems as $stripeItem): ?>
        <span>
            <? if ($stripeItem->type === 'url'): ?>
                <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeUrl $stripeItem */ ?>
                <a href="<? echo $stripeItem->url; ?>" target="_blank"
                   class="item"<? if (!empty($stripeItem->hoverText)): ?> title="<? echo $stripeItem->hoverText; ?>"<? endif; ?>>
            <? elseif ($stripeItem->type === 'shortcut'): ?>
                <? /** @var \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeShortcut $stripeItem */ ?>
                <a href="<? echo isset($stripeItem->shortcut) ? $stripeItem->shortcut->getSourceLink() : '#'; ?>"
                   class="item shortcuts-link<? if (!isset($stripeItem->shortcut)): ?> disabled<? endif; ?>"<? if (!empty($stripeItem->hoverText)): ?> title="<? echo $stripeItem->hoverText; ?>"<? endif; ?>>
                    <div class="service-data">
                        <? echo isset($stripeItem->shortcut) ? $stripeItem->shortcut->getMenuItemData() : '<div class="same-page"></div><div class="has-custom-handler"></div>'; ?>
                    </div>
            <? endif; ?>
                    <? if (!empty($stripeItem->icon)): ?>
                        <? if ($contentBlock->iconPosition == \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock::IconPositionTop): ?>
                            <div class="icomoon-wrap">
                        <? endif; ?>
                        <i class="icomoon<? if (!empty($stripeItem->iconSize)): ?>icomoon-<? echo $stripeItem->iconSize; ?><? endif; ?> <? echo $stripeItem->icon; ?>"
                           style="color: <? echo '#'.$stripeItem->iconColor; ?>"></i>
                        <? if ($contentBlock->iconPosition == \application\models\shortcuts\models\landing_page\regular_markup\ScrollStripeBlock::IconPositionTop): ?>
                            </div>
                        <? endif; ?>
                    <? endif; ?>
                    <span style="<? echo $this->renderPartial('landingPageMarkup/styleTextAppearance', array('textAppearance' => $stripeItem->getTextAppearance()), true); ?>"><? echo $stripeItem->text;?></span>
                </a>
          </span>
	<? endforeach; ?>
</div>
