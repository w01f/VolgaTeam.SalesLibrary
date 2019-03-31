<?
	use application\models\shortcuts\models\bundle_modal_dialog\UrlItem;

	/**
	 * @var $item UrlItem
	 */

	$bundleLinkId = sprintf("bundle-modal-link-%s", $item->id);
?>
<a id="<? echo $bundleLinkId; ?>" class="bundle-modal-link" href="<? echo $item->getUrl(); ?>" target="<? echo $item->getTarget(); ?>">
	<img class="item-image" src="<? echo $item->imageUrl; ?>">
    <div class="item-title" style="color: <? echo Utils::formatColor($item->textColor); ?>; font-size: <? echo $item->textSize; ?>px;"><? echo $item->title; ?></div>
    <div class="service-data">
        <div class="bundle-item-id"><? echo $item->id; ?></div>
        <div class="bundle-item-type"><? echo $item->type; ?></div>
        <div class="bundle-encoded-item"><? echo CJSON::encode($item); ?></div>
    </div>
</a>
