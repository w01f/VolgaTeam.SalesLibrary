<?
	/**
	 * @var $itemData UrlNavigationItem
	 */
?>
<a href="<? echo $itemData->getUrl(); ?>" target="_blank">
	<img class="item-icon expanded" src="<? echo $itemData->iconUrlExpanded; ?>">
	<img class="item-icon collapsed" src="<? echo $itemData->iconUrlCollapsed; ?>">
    <div class="item-title" style="color: <? echo '#' . $itemData->textColor; ?>;"><? echo $itemData->title; ?></div>
</a>
