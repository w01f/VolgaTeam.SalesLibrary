<?
	/** @var $optionsContainer SearchShortcut|SearchBar */
	$hasTemplates = count($optionsContainer->subConditions) > 0;
	$defaultView = $optionsContainer->subSearchDefaultView;
?>
<? if ($optionsContainer->showSubSearchAll): ?>
	<img class="no-filter" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/list-' . ($defaultView == 'all' ? 'on' : 'off') . '.png'; ?>" data-toggle="tooltip" title="Show All Files">
<? endif; ?>
<? if ($optionsContainer->showSubSearchSearch): ?>
	<img class="custom-filter" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/search-' . ($defaultView == 'search' ? 'on' : 'off') . '.png'; ?>" data-toggle="tooltip" title="Custom Search">
<? endif; ?>
<? if ($optionsContainer->showSubSearchTemplates && $hasTemplates): ?>
	<img class="predefined-filter" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/link-' . ($defaultView == 'links' ? 'on' : 'off') . '.png'; ?>" data-toggle="tooltip" title="Category Links">
<? endif; ?>