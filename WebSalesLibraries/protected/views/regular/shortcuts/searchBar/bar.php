<?
	/**
	 * @var $searchBar SearchBar
	 * @var $pageId string
	 */
?>
<? if ($searchBar->configured): ?>
	<?
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	?>
	<table class="shortcuts-search-bar open" style="text-align: <? echo $searchBar->alignment; ?>;">
		<tr>
			<td>
				<? if (!Yii::app()->browser->isMobile()): ?>
					<? $logoUrl = Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search-bar-logo.png'; ?>
					<div style="width:100%;min-height: 20px;">
						<img style="margin-top: 20px; margin-bottom: 20px;" src="<? echo $logoUrl . '?' . $pageId; ?>" alt="" onerror="this.style.display = 'none'"/>
					</div>
				<? endif; ?>
				<div class="search-conditions" style="display: none;">
					<div class="encoded-object"><? echo CJSON::encode($searchBar->getSearchOptions()) ?></div>
					<? $this->renderPartial('categorySelector', array('categoryManager' => $searchBar->categoryManager), false, true); ?>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div class="input-group search-input-container">
					<span class="input-group-addon">Search:</span>
					<input class="form-control search-bar-text" type="text" placeholder="<? echo $searchBar->defaultLabel; ?>">
					<span class="input-group-btn">
							<button class="btn btn-default tags-filter-panel-switcher" type="button"><? echo $tagsName; ?></button>
							<button class="btn btn-default search-bar-options" type="button">Search Options</button>
							<button class="btn btn-default search-bar-run" type="button">
								<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search-shortcuts.png'; ?>">
							</button>
				  	</span>
				</div>
				<p class="text-muted">
					<small class="tag-condition-selected">Fusce dapibus, tellus ac cursus commodo, tortor mauris nibh.</small>
				</p>
			</td>
		</tr>
	</table>
<? endif; ?>