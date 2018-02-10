<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeBlock;

	/** @var $contentBlock MenuStripeBlock */

	$blockId = sprintf('menu-stripe-%s', $contentBlock->id);
	$textAppearance = $contentBlock->textAppearance;
?>
<style>
    <? echo '#'.$blockId; ?> > li > a
    {
        padding-right: <? echo $contentBlock->itemSpacing->extraSmall;?>px;
    }

    <? if($contentBlock->expandOnHover): ?>
        <? echo '#'.$blockId; ?> .menu-stripe-top-item:hover > .menu-stripe-submenu,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu:hover > .menu-stripe-submenu
        {
            visibility: visible !important;
            opacity: 1 !important;
        }
    <?endif;?>

    <? if($contentBlock->animationSpeed > 0): ?>
        <? echo '#'.$blockId; ?> .menu-stripe-top-item > .menu-stripe-submenu,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu > .menu-stripe-submenu
        {
            -webkit-transition: opacity <? echo $contentBlock->animationSpeed;?>ms, visibility <? echo $contentBlock->animationSpeed;?>ms !important;
            transition: opacity <? echo $contentBlock->animationSpeed;?>ms, visibility <? echo $contentBlock->animationSpeed;?>ms !important;
        }
    <?endif;?>

    <? echo '#'.$blockId; ?>  li  a
    {
        <? if (isset($textAppearance->color)): ?>
            color: <? echo Utils::formatColor($textAppearance->color); ?>;
        <? endif; ?>
        <? if ($textAppearance->lineHeight > 0): ?>
            line-height: <? echo $textAppearance->lineHeight; ?>px;
        <? endif; ?>
        <? if (isset($textAppearance->alignment)): ?>
            text-align: <? echo $textAppearance->alignment; ?>;
        <? endif; ?>
        <? if (isset($textAppearance->font)): ?>
            font-family: <? echo FontReplacementHelper::replaceFont($textAppearance->font->name); ?>;
            font-size: <? echo $textAppearance->font->size->extraSmall; ?>pt;
            font-weight: <? echo $textAppearance->font->isBold ? 'bold' : 'normal'; ?>;
            font-style: <? echo $textAppearance->font->isItalic ? 'italic' : 'normal'; ?>;
            text-decoration: <? echo $textAppearance->font->isUnderlined ? 'underline' : 'inherit'; ?>;
        <? endif; ?>
    }
    <? if (isset($textAppearance->hoverColor)): ?>
        <? echo '#'.$blockId; ?>  li a:hover
        {
            color: <? echo Utils::formatColor($textAppearance->hoverColor); ?>;
        }
    <? endif; ?>

    @media (min-width: 768px)
    {
        <? echo '#'.$blockId; ?> > li > a
        {
            padding-right: <? echo $contentBlock->itemSpacing->small;?>px;
        }

        <? echo '#'.$blockId; ?>  li  a
        {
            <? if (isset($textAppearance->font)): ?>
                font-size: <? echo $textAppearance->font->size->small; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 992px)
    {
        <? echo '#'.$blockId; ?> > li > a
        {
            padding-right: <? echo $contentBlock->itemSpacing->medium;?>px;
        }
        <? echo '#'.$blockId; ?>  li  a
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->medium; ?>pt !important;
            <? endif; ?>
        }
    }

    @media (min-width: 1200px)
    {
        <? echo '#'.$blockId; ?> > li > a
        {
            padding-right: <? echo $contentBlock->itemSpacing->large;?>px;
        }
        <? echo '#'.$blockId; ?>  li  a
        {
            <? if (isset($textAppearance->font)): ?>
              font-size: <? echo $textAppearance->font->size->large; ?>pt !important;
            <? endif; ?>
        }
    }
</style>

<div class="navbar navbar-default menu-stripe-container<? if ($contentBlock->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($contentBlock->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($contentBlock->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($contentBlock->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
    <div class="container-fluid">
        <ul id="<? echo $blockId; ?>" class="nav navbar-nav menu-stripe">
			<? foreach ($contentBlock->items as $menuItem): ?>
				<?
				switch ($menuItem->type)
				{
					case 'menu':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/subMenu', array('menuItem' => $menuItem, 'topLevel' => true), true);
						break;
					case 'url':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/url', array('menuItem' => $menuItem), true);
						break;
					case 'shortcut':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/shortcut', array('menuItem' => $menuItem), true);
						break;
				}
				?>
			<? endforeach; ?>
        </ul>
    </div>
</div>