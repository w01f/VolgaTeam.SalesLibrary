<?
	use application\models\shortcuts\models\landing_page\regular_markup\menu_stripe\MenuStripeItem;

	/**
	 * @var $blockId string
	 * @var $items MenuStripeItem[]
	 * @var $expandOnHover boolean
	 * @var $floatRight boolean
	 * @var $hideLarge boolean
	 * @var $hideMedium boolean
	 * @var $hideSmall boolean
	 * @var $hideExtraSmall boolean
	 */
?>
<ul id="<? echo $blockId; ?>" class="nav navbar-nav menu-stripe<?if($floatRight):?> navbar-right<?endif;?><?if(!$expandOnHover):?> expand-on-click<?endif;?><?if($hideLarge):?> hidden-lg<?endif;?><?if($hideMedium):?> hidden-md<?endif;?><?if($hideSmall):?> hidden-sm<?endif;?><?if($hideExtraSmall):?> hidden-xs<?endif;?>">
	<? foreach ($items as $menuItem): ?>
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
