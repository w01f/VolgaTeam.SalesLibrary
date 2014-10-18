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
					<img style="margin-top: 20px; margin-bottom: 20px;" src="<? echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar/search-bar-logo.png?' . $pageId ?>" alt=""/>
				<? endif; ?>
				<div class="search-conditions" style="display: none;">
					<div class="shortcut-title"><? echo $searchBar->title; ?></div>
					<? if (isset($searchBar->conditions->startDate)): ?>
						<div class="start-date"><? echo $searchBar->conditions->startDate; ?></div>
					<? endif; ?>
					<? if (isset($searchBar->conditions->endDate)): ?>
						<div class="end-date"><? echo $searchBar->conditions->endDate; ?></div>
					<? endif; ?>
					<? if ($searchBar->conditions->dateModified): ?>
						<div class="use-file-date">true</div>
					<? endif; ?>
					<? if (isset($searchBar->conditions->libraries)): ?>
						<? foreach ($searchBar->conditions->libraries as $library): ?>
							<div class="library"><? echo $library; ?></div>
						<? endforeach; ?>
					<? endif; ?>
					<? if (isset($searchBar->conditions->superFilters)): ?>
						<? foreach ($searchBar->conditions->superFilters as $superFilter): ?>
							<div class="super-filter"><? echo $superFilter; ?></div>
						<? endforeach; ?>
					<? endif; ?>
					<? if (isset($searchBar->conditions->categories)): ?>
						<? foreach ($searchBar->conditions->categories as $category): ?>
							<? $category = (object)$category; ?>
							<div class="category"><? echo $category->category; ?>------<? echo $category->tag; ?></div>
						<? endforeach; ?>
					<? endif; ?>
					<? if ($searchBar->conditions->onlyWithCategories): ?>
						<div class="only-with-categories">true</div>
					<? endif; ?>
					<? if ($searchBar->conditions->hideDuplicated): ?>
						<div class="hide-duplicated">true</div>
					<? endif; ?>
					<? if ($searchBar->conditions->searchByName): ?>
						<div class="search-by-name">true</div>
					<? endif; ?>
					<? if ($searchBar->conditions->searchByContent): ?>
						<div class="search-by-content">true</div>
					<? endif; ?>
					<? if (!$searchBar->showResultsBar && $searchBar->samePage): ?>
						<div class="hide-results">true</div>
					<? endif; ?>
					<? if ($searchBar->samePage === true): ?>
						<div class="same-page">true</div>
					<? endif; ?>
					<? if ($searchBar->enableSubSearch): ?>
						<div class="enable-sub-search">true</div>
					<? endif; ?>
					<? if ($searchBar->showSubSearchAll): ?>
						<div class="show-sub-search-all">true</div>
					<? endif; ?>
					<? if ($searchBar->showSubSearchSearch): ?>
						<div class="show-sub-search-search">true</div>
					<? endif; ?>
					<? if ($searchBar->showSubSearchTemplates): ?>
						<div class="show-sub-search-templates">true</div>
					<? endif; ?>
					<? if ($searchBar->subSearchDefaultView): ?>
						<div class="sub-search-default-view"><? echo $searchBar->subSearchDefaultView; ?></div>
					<? endif; ?>
					<? if (isset($searchBar->conditions->sortColumn)): ?>
						<div class="sort-column"><? echo $searchBar->conditions->sortColumn; ?></div>
					<? endif; ?>
					<? if (isset($searchBar->conditions->sortDirection)): ?>
						<div class="sort-direction"><? echo $searchBar->conditions->sortDirection; ?></div>
					<? endif; ?>
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
							<button class="btn btn-default search-bar-run" type="button">
								<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search-shortcuts.png'; ?>">
							</button>
							<button class="btn btn-default tags-filter-panel-switcher" type="button"><? echo $tagsName; ?></button>
				  	</span>
				</div>
				<p class="text-muted">
					<small class="tag-condition-selected">Fusce dapibus, tellus ac cursus commodo, tortor mauris nibh.</small>
				</p>
			</td>
		</tr>
		<tr class="file-filter-panel">
			<td>
				<form class="form-inline">
					<div class="search-option checkbox">
						<label class="file-selector"><input id="search-file-type-video" type="checkbox" checked>video</label>
					</div>
					<div class="search-option checkbox">
						<label class="file-selector"><input id="search-file-type-powerpoint" type="checkbox" checked>presentations</label>
					</div>
					<div class="search-option checkbox">
						<label class="file-selector"><input id="search-file-type-other" type="checkbox">all other files</label>
					</div>
					<div class="search-option checkbox">
						<label class="file-selector"><input id="search-file-names-only" type="checkbox" checked>file names only</label>
					</div>
					<div class="search-option checkbox">
						<label class="file-selector"><input id="search-exact-match" type="checkbox" checked>exact search</label>
					</div>
				</form>
			</td>
		</tr>
	</table>
<? endif; ?>