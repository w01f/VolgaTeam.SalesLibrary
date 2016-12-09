<?
	/** @var $searchContainer IShortcutSearchOptionsContainer */
?>
<div class="search-conditions" style="display: none;">
	<div class="encoded-object"><? echo CJSON::encode($searchContainer->getSearchOptions()) ?></div>
</div>
<div class="search-view-options" style="display: none;">
	<div class="encoded-object">
		<? echo CJSON::encode(array(
			'showCategory' => Yii::app()->params['search_options']['hide_tag'] != true,
			'categoryColumnName' => Yii::app()->params['tags']['column_name'],
			'showLibraries' => Yii::app()->params['search_options']['hide_libraries'] != true,
			'librariesColumnName' => Yii::app()->params['stations']['column_name'],
			'showType' => true,
			'showDate' => true,
			'showRate' => true,
			'showViewsCount' => true,
			'showDeleteButton' => false
		)); ?>
	</div>
</div>