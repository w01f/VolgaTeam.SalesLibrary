<?
	/**
	 * @var $itemData UrlNavigationItem
	 */
?>
<a href="<? echo $itemData->url; ?>" target="_blank">
	<img class="item-icon" src="<? echo $itemData->iconUrl; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
</a>
