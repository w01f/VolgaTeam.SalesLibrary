<?
	/** @var $searchContainer IShortcutSearchOptionsContainer */
?>
<div class="search-conditions" style="display: none;">
    <div class="encoded-object"><? echo CJSON::encode($searchContainer->getSearchOptions()) ?></div>
</div>
<div class="search-view-options" style="display: none;">
    <div class="encoded-object">
		<?
			/** @var ShortcutsSearchOptions $searchOptions */
			$searchOptions = $searchContainer->getSearchOptions();
			$columnSettings = $searchOptions->conditions->columnSettings;
			echo CJSON::encode(array(
				'columnSettings' => $columnSettings,
				'showDeleteButton' => false,
				'defaultPageLength' => $searchOptions->defaultPageLength
			)); ?>
    </div>
</div>