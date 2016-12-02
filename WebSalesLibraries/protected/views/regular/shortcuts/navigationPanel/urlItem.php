<?
	/**
	 * @var $itemData UrlNavigationItem
	 */
?>
<a href="<? echo $itemData->url; ?>" target="_blank">
	<img class="item-icon expanded" src="<? echo $itemData->iconUrlExpanded; ?>">
	<img class="item-icon collapsed" src="<? echo $itemData->iconUrlCollapsed; ?>">
	<div class="item-title"><? echo $itemData->title; ?></div>
</a>
