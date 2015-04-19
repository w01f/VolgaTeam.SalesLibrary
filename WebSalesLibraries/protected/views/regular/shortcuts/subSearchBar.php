<?
	/** @var $optionsContainer SearchShortcut|SearchBar */
	$hasTemplates = count($optionsContainer->subConditions) > 0;
	$defaultView = $optionsContainer->subSearchDefaultView;
?>
<? if ($optionsContainer->showSubSearchAll): ?>
	<img class="no-filter<? echo($defaultView == 'all' ? ' active' : '') ?>" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/list.png'; ?>" data-toggle="tooltip" title="Show All Files">
<? endif; ?>
<? if ($optionsContainer->showSubSearchSearch): ?>
	<img class="custom-filter<? echo($defaultView == 'search' ? ' active' : '') ?>" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/search.png'; ?>" data-toggle="tooltip" title="Custom Search">
<? endif; ?>
<? if ($optionsContainer->showSubSearchTemplates && $hasTemplates): ?>
	<img class="predefined-filter<? echo($defaultView == 'links' ? ' active' : '') ?>" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/sub-search-bar/link.png'; ?>" data-toggle="tooltip" title="Category Links">
<? endif; ?>