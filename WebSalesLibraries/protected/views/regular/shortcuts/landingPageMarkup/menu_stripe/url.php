<?

	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeUrl;

	/** @var $menuItem MenuStripeUrl */

	$blockId = sprintf('menu-stripe-url-%s', $menuItem->id);
	echo $this->renderPartial('landingPageMarkup/menu_stripe/itemStyle', array('menuItem' => $menuItem, 'blockId' => $blockId), true);
?>
<li id="<? echo $blockId; ?>" class="menu-stripe-item-url<? if (!$menuItem->enable): ?> disabled<? endif; ?><? if ($menuItem->hideCondition->large): ?> hidden-lg<? endif; ?>
    <? if ($menuItem->hideCondition->medium): ?> hidden-md<? endif; ?>
    <? if ($menuItem->hideCondition->small): ?> hidden-sm<? endif; ?>
    <? if ($menuItem->hideCondition->extraSmall): ?> hidden-xs<? endif; ?>">
    <a href="<? echo $menuItem->url; ?>" title="<? echo $menuItem->title; ?>" target="_blank"><? echo $menuItem->title; ?></a>
</li>
