<?
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeItem;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeShortcut;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeUrl;

	/** @var $contentBlock ScrollStripeBlock */

	/** @var ScrollStripeItem[] $scrollStripeItems */
	$scrollStripeItems = $contentBlock->items;

	$scrollStripeId = sprintf("scroll-stripe-%s", $contentBlock->id);

	$stripeConfigurationClass = '';
	if ($contentBlock->iconPosition == ScrollStripeBlock::IconPositionTop)
		$stripeConfigurationClass .= ' center-stack';

	if ($contentBlock->stripeSize == ScrollStripeBlock::StripeSizeMedium)
		$stripeConfigurationClass .= ' scrolltab-medium';
	else if ($contentBlock->stripeSize == ScrollStripeBlock::StripeSizeLarge)
		$stripeConfigurationClass .= ' scrolltab-large';
?>
<style>
    <? echo '#'.$scrollStripeId; ?> div.scroll_tab_inner > span.scroll_tab_over {
        background-color: #ffffff;
    }

    <? echo '#'.$scrollStripeId; ?> .scroll_tab_left_button,
    <? echo '#'.$scrollStripeId; ?> .scroll_tab_right_button,
    <? echo '#'.$scrollStripeId; ?> span.scroll_tab_left_finisher,
    <? echo '#'.$scrollStripeId; ?> span.scroll_tab_right_finisher{
        border-color: <? echo Utils::formatColorToHex($contentBlock->border->color); ?> !important;
    }

    <? echo '#'.$scrollStripeId; ?> .scroll_tab_left_button i{
        color: <? echo Utils::formatColorToHex($contentBlock->leftButtonColor); ?> !important;
    }

    <? echo '#'.$scrollStripeId; ?> .scroll_tab_left_button.scroll_arrow_disabled i{
        color: <? echo Utils::formatColorToHex($contentBlock->leftButtonDisabledColor); ?> !important;
    }

    <? echo '#'.$scrollStripeId; ?> .scroll_tab_right_button i{
        color: <? echo Utils::formatColorToHex($contentBlock->rightButtonColor); ?> !important;
    }

    <? echo '#'.$scrollStripeId; ?> .scroll_tab_right_button.scroll_arrow_disabled i{
        color: <? echo Utils::formatColorToHex($contentBlock->rightButtonDisabledColor); ?> !important;
    }
</style>
<div id="<? echo $scrollStripeId; ?>" class="scroll_tabs_theme_light<? echo $stripeConfigurationClass; ?> scroll-stripe">
	<? foreach ($scrollStripeItems as $stripeItem): ?>
        <?
		$stripeItemId = sprintf("stripe-item-%s", $stripeItem->id);
        ?>
        <style>
            <? echo '#'.$stripeItemId; ?>
            {
                border-color: <? echo Utils::formatColorToHex($contentBlock->border->color); ?> !important;
                background-color: <? echo Utils::formatColorToHex($stripeItem->backgroundColor); ?> !important;
            }
            <? echo '#'.$stripeItemId; ?>:hover {
                background-color: <? echo Utils::formatColorToHex($stripeItem->backgroundHoverColor); ?> !important;
            }

            <? echo '#'.$stripeItemId; ?> a i {
                color: <? echo Utils::formatColorToHex($stripeItem->iconColor); ?>
            }

            <? echo '#'.$stripeItemId; ?>:hover a i {
                color: <? echo Utils::formatColorToHex($stripeItem->iconHoverColor); ?>
            }

            <?if(!empty($stripeItem->textAppearance->color)):?>
                <? echo '#'.$stripeItemId; ?> a .item-text {
                    color: <? echo Utils::formatColorToHex($stripeItem->textAppearance->color); ?>
                }
            <?endif;?>

            <?if(!empty($stripeItem->textAppearance->hoverColor)):?>
                <? echo '#'.$stripeItemId; ?>:hover a .item-text {
                    color: <? echo Utils::formatColorToHex($stripeItem->textAppearance->hoverColor); ?>
                }
            <?endif;?>
        </style>
        <span id="<? echo $stripeItemId; ?>">
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
                        <i class="icomoon<? if (!empty($stripeItem->iconSize)): ?> icomoon-<? echo $stripeItem->iconSize; ?><? endif; ?> <? echo $stripeItem->icon; ?>"></i>
                        <? if ($contentBlock->iconPosition == ScrollStripeBlock::IconPositionTop): ?>
                            </div>
                        <? endif; ?>
                    <? endif; ?>
	                <?
                        $stripeTextId = sprintf("stripe-item-text-%s", $stripeItem->id);
                        $textAppearance = $stripeItem->getTextAppearance();
                        $textAppearance->color = null;
		                $textAppearance->hoverColor = null;
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
