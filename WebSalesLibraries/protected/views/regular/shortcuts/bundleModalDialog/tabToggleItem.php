<?

	use application\models\shortcuts\models\bundle_modal_dialog\TabToggleItem;
	use application\models\shortcuts\models\bundle_modal_dialog\UrlItem;

	/**
	 * @var $item TabToggleItem
	 */
?>
<a class="tab-toggle-link" href="<? echo $item->getUrl(); ?>" target="<? echo $item->getTarget(); ?>">
	<img class="item-image" src="<? echo $item->imageUrl; ?>">
    <div class="item-title" style="color: <? echo Utils::formatColor($item->textColor); ?>; font-size: <? echo $item->textSize; ?>px;"><? echo $item->title; ?></div>
    <div class="service-data">
        <div class="tab-id"><? echo $item->tabId; ?></div>
    </div>
</a>
