<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeSubMenu;

	/**
	 * @var $menuItem MenuStripeSubMenu
	 * @var $topLevel boolean
	 */

	$blockId = sprintf('menu-stripe-submenu-%s', $menuItem->id);
	echo $this->renderPartial('landingPageMarkup/menu_stripe/itemStyle', array('menuItem' => $menuItem, 'blockId' => $blockId), true);
	$textAppearance = $menuItem->textAppearance;
?>
<style>
    <?if(!$topLevel&& $menuItem->enable && $menuItem->showArrow):?>
        <? echo '#'.$blockId; ?> > a:after
        {
            display: block;
            content: " ";
            float: right;
            width: 0;
            height: 0;
            border-color: transparent;
            border-style: solid;
            border-width: 5px 0 5px 5px;
            <? if (isset($textAppearance->color)): ?>
                border-left-color: <? echo Utils::formatColor($textAppearance->color); ?>;
            <?else:?>
                border-left-color: #ccc;
            <? endif; ?>
            margin-top: 5px;
            margin-right: -10px;
        }

        <? echo '#'.$blockId; ?>:hover a:after
        {
            <? if (isset($textAppearance->hoverColor)): ?>
                border-left-color: <? echo Utils::formatColor($textAppearance->hoverColor); ?>;
            <?else:?>
                border-left-color: #ccc;
            <? endif; ?>
        }
    <?endif;?>


    <? echo '#'.$blockId; ?> .menu-stripe-submenu > li > a
    {
        padding-bottom: <? echo $menuItem->itemSpacing->extraSmall;?>px;
    }

    @media (min-width: 768px)
    {
        <? echo '#'.$blockId; ?> .menu-stripe-submenu > li > a
        {
            padding-bottom: <? echo $menuItem->itemSpacing->small;?>px;
        }
    }

    @media (min-width: 992px)
    {
        <? echo '#'.$blockId; ?> .menu-stripe-submenu > li > a
        {
            padding-bottom: <? echo $menuItem->itemSpacing->medium;?>px;
        }
    }

    @media (min-width: 1200px)
    {
        <? echo '#'.$blockId; ?> .menu-stripe-submenu > li > a
        {
            padding-bottom: <? echo $menuItem->itemSpacing->large;?>px;
        }
    }
</style>

<li id="<? echo $blockId; ?>" class="<? echo $topLevel ? 'dropdown menu-stripe-top-item' : 'dropdown-submenu menu-stripe-item-submenu'; ?><? if (!$menuItem->enable): ?> disabled<? endif; ?><? if ($menuItem->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($menuItem->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($menuItem->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($menuItem->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
    <a href="#" class="dropdown-toggle disabled" data-toggle="dropdown">
		<? echo $menuItem->title; ?><? if ($topLevel && $menuItem->showArrow): ?><span class="caret"></span><? endif; ?>
    </a>
	<? if ($menuItem->enable): ?>
        <ul class="dropdown-menu menu-stripe-submenu" aria-labelledby="<? echo $blockId; ?>">
			<? foreach ($menuItem->items as $menuItem): ?>
				<?
				switch ($menuItem->type)
				{
					case 'menu':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/subMenu', array('menuItem' => $menuItem, 'topLevel' => false), true);
						break;
					case 'url':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/url', array('menuItem' => $menuItem), true);
						break;
					case 'shortcut':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/shortcut', array('menuItem' => $menuItem), true);
						break;
					case 'divider':
						echo $this->renderPartial('landingPageMarkup/menu_stripe/divider', array('menuItem' => $menuItem), true);
						break;
				}
				?>
			<? endforeach; ?>
        </ul>
	<? endif; ?>
</li>
