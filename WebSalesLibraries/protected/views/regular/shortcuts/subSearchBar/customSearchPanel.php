<?
	/**
	 * @var $searchBar SearchBar
	 */
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
?>
<div class="sub-search-conditions-pane">
	<div class="search-conditions" style="display: none;">
		<div class="encoded-object"><? echo CJSON::encode($searchBar->getSearchOptions()) ?></div>
		<? $this->renderPartial('categorySelector', array('categoryManager' => $searchBar->categoryManager), false, true); ?>
	</div>
	<div class="row">
		<div class="col-xs-12">
			<button class="btn btn-default btn-block search-bar-run" type="button">
				<span class="glyphicon glyphicon-search" aria-hidden="true"></span>Search
			</button>
		</div>
	</div>
	<div class="row">
		<div class="col-xs-12">
			<input class="form-control search-bar-text" type="text" placeholder="<? echo $searchBar->defaultLabel; ?>">
		</div>
	</div>
	<div class="row">
		<div class="col-xs-12">
			<button class="btn btn-default btn-block tags-filter-panel-switcher" type="button"><? echo $tagsName; ?></button>
			<p class="text-muted">
				<small class="tag-condition-selected"></small>
			</p>
		</div>
	</div>
	<div class="row">
		<div class="col-xs-12 file-filter-panel">
			<div class="search-option checkbox">
				<label class="file-selector"><input id="search-file-type-video" type="checkbox" checked>video</label>
			</div>
			<div class="search-option checkbox">
				<label class="file-selector"><input id="search-file-type-powerpoint" type="checkbox" checked>presentations</label>
			</div>
			<div class="search-option checkbox">
				<label class="file-selector"><input id="search-file-type-other" type="checkbox" checked>all other files</label>
			</div>
			<div class="search-option checkbox">
				<label class="file-selector"><input id="search-file-names-only" type="checkbox">file names only</label>
			</div>
			<div class="search-option checkbox">
				<label class="file-selector"><input id="search-exact-match" type="checkbox" checked>exact search</label>
			</div>
		</div>
	</div>
</div>