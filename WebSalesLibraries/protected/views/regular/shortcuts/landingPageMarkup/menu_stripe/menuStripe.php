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
        <? echo '#'.$blockId; ?> .menu-stripe-top-item > a:hover + .menu-stripe-submenu,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu > a:hover + .menu-stripe-submenu,
        <? echo '#'.$blockId; ?> .menu-stripe-top-item > .menu-stripe-submenu:hover,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu > .menu-stripe-submenu:hover
        {
            visibility: visible !important;
            opacity: 1 !important;
            height: auto;
            width: auto;
            padding: 5px 0;
            border: 1px solid rgba(0,0,0,.15);
        }

        <? echo '#'.$blockId; ?> .menu-stripe-top-item > a:hover + .menu-stripe-submenu > li,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu > a:hover + .menu-stripe-submenu > li,
        <? echo '#'.$blockId; ?> .menu-stripe-top-item > .menu-stripe-submenu:hover > li,
        <? echo '#'.$blockId; ?> .menu-stripe-item-submenu > .menu-stripe-submenu:hover > li
        {
             display: list-item;
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

    <? echo '#'.$blockId; ?> > li
    {
        float: left;
    }

    <? echo '#'.$blockId; ?>.navbar-right > li
     {
         float: right !important;
     }

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
        <?
	        if ($contentBlock->floatRight->useForLargeScreen ||
                $contentBlock->floatRight->useForMediumScreen ||
		        $contentBlock->floatRight->useForSmallScreen ||
		        $contentBlock->floatRight->useForExtraSmallScreen)
            {
                echo $this->renderPartial('landingPageMarkup/menu_stripe/menuStripeList', array(
                    'blockId'=>$blockId,
	                'items' => $contentBlock->floatRight->useForLargeScreen ? $contentBlock->itemsReversed : $contentBlock->items,
                    'expandOnHover' => $contentBlock->expandOnHover,
	                'floatRight' => $contentBlock->floatRight->useForLargeScreen,
                    'hideLarge' => false,
                    'hideMedium' => true,
                    'hideSmall' => true,
                    'hideExtraSmall' => true,
                ), true);
	            echo $this->renderPartial('landingPageMarkup/menu_stripe/menuStripeList', array(
		            'blockId'=>$blockId,
		            'items' => $contentBlock->floatRight->useForMediumScreen ? $contentBlock->itemsReversed : $contentBlock->items,
		            'expandOnHover' => $contentBlock->expandOnHover,
		            'floatRight' => $contentBlock->floatRight->useForMediumScreen,
		            'hideLarge' => true,
		            'hideMedium' => false,
		            'hideSmall' => true,
		            'hideExtraSmall' => true,
	            ), true);
	            echo $this->renderPartial('landingPageMarkup/menu_stripe/menuStripeList', array(
		            'blockId'=>$blockId,
		            'items' => $contentBlock->floatRight->useForSmallScreen ? $contentBlock->itemsReversed : $contentBlock->items,
		            'expandOnHover' => $contentBlock->expandOnHover,
		            'floatRight' => $contentBlock->floatRight->useForSmallScreen,
		            'hideLarge' => true,
		            'hideMedium' => true,
		            'hideSmall' => false,
		            'hideExtraSmall' => true,
	            ), true);
	            echo $this->renderPartial('landingPageMarkup/menu_stripe/menuStripeList', array(
		            'blockId'=>$blockId,
		            'items' => $contentBlock->floatRight->useForExtraSmallScreen ? $contentBlock->itemsReversed : $contentBlock->items,
		            'expandOnHover' => $contentBlock->expandOnHover,
		            'floatRight' => $contentBlock->floatRight->useForExtraSmallScreen,
		            'hideLarge' => true,
		            'hideMedium' => true,
		            'hideSmall' => true,
		            'hideExtraSmall' => false,
	            ), true);
            }
            else
	            echo $this->renderPartial('landingPageMarkup/menu_stripe/menuStripeList', array(
		                'blockId'=>$blockId,
	                    'items' => $contentBlock->items,
		                'expandOnHover' => $contentBlock->expandOnHover,
		                'floatRight' => false,
		                'hideLarge' => false,
		                'hideMedium' => false,
		                'hideSmall' => false,
		                'hideExtraSmall' => false,
                ), true);
        ?>
    </div>
</div>