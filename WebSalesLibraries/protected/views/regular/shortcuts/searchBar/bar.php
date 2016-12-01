<?
	/**
	 * @var $searchBar SearchBar
	 */
?>
<? if ($searchBar->configured): ?>
	<?
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	?>
	<table class="shortcuts-search-bar logger-form open" data-log-group="Shortcut Tile" data-log-action="Search Bar" style="text-align: <? echo $searchBar->alignment; ?>;">
		<tr>
			<td>
				<? $this->renderPartial('searchConditions', array('searchContainer' => $searchBar)); ?>
				<div class="tag-condition-selector-wrapper">
					<? $this->renderPartial('categorySelector', array('categoryManager' => $searchBar->categoryManager), false, true); ?>
				</div>
				<div class="search-bar-actions">
					<? $this->renderPartial('../menu/actionItems', array('actionContainer' => $searchBar), false, true); ?>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div class="input-group search-input-container">
					<span class="input-group-addon">Search:</span>
					<input class="form-control log-action search-bar-text" type="text" placeholder="<? echo $searchBar->defaultLabel; ?>">
					<span class="input-group-btn">
						<? if ($searchBar->showTagsSelector): ?>
							<button class="btn btn-default log-action tags-filter-panel-switcher" type="button"><? echo $tagsName; ?></button>
						<? endIf; ?>
						<button class="btn btn-default log-action search-bar-options" type="button">Search Options</button>
						<button class="btn btn-default search-bar-run" type="button">
							<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search-shortcuts.png'; ?>">
						</button>
				  	</span>
				</div>
				<p class="tag-condition-selected text-muted">
					<small>Fusce dapibus, tellus ac cursus commodo, tortor mauris nibh.</small>
				</p>
			</td>
		</tr>
	</table>
<? endif; ?>