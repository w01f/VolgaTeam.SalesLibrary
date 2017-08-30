<?
	/**
	 * @var $itemData UrlNavigationItem
	 */

	/** @var RegularNavigationItemSettings $settings */
	$settings = $itemData->settings;
?>
<a href="<? echo $itemData->getUrl(); ?>" target="_blank">
	<img class="item-icon expanded" src="<? echo $settings->iconUrlExpanded; ?>">
	<img class="item-icon collapsed" src="<? echo $settings->iconUrlCollapsed; ?>">
    <div class="item-title" style="color: <? echo Utils::formatColor($settings->textColor); ?>;"><? echo $settings->title; ?></div>
</a>
