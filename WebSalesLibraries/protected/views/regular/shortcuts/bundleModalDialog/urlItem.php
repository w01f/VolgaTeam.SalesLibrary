<?
	use application\models\shortcuts\models\bundle_modal_dialog\UrlItem;

	/**
	 * @var $item UrlItem
	 */
?>
<a href="<? echo $item->getUrl(); ?>" target="<? echo $item->getTarget(); ?>">
	<img class="item-image" src="<? echo $item->imageUrl; ?>">
    <div class="item-title" style="color: <? echo Utils::formatColor($item->textColor); ?>; font-size: <? echo $item->textSize; ?>px;"><? echo $item->title; ?></div>
</a>
