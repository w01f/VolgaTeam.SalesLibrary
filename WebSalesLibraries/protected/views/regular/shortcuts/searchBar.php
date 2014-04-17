<? if ($searchBar->configured): ?>
	<?
	$lastCharFromTagsName = substr(Yii::app()->params['tags']['column_name'], -1);
	$tagsName = $lastCharFromTagsName == "y" ? substr_replace(Yii::app()->params['tags']['column_name'], "ies", -1) : (Yii::app()->params['tags']['column_name'] . "s");
	?>
	<table class="shortcuts-search-bar open" style="text-align: <? echo $searchBar->alignment; ?>;">
		<tr>
			<td>
				<? if (!Yii::app()->browser->isMobile()): ?>
					<img style="margin-top: 20px; margin-bottom: 20px;" src="<?php echo Yii::app()->getBaseUrl(true) . '/images/shortcuts/search-bar-logo.png?' . $pageId ?>" alt=""/>
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
					<? if (isset($searchBar->conditions->sortColumn)): ?>
						<div class="sort-column"><? echo $searchBar->conditions->sortColumn; ?></div>
					<? endif; ?>
					<? if (isset($searchBar->conditions->sortDirection)): ?>
						<div class="sort-direction"><? echo $searchBar->conditions->sortDirection; ?></div>
					<? endif; ?>
					<div class="tag-condition-selector">
						<div class="tool-dialog">
							<div class="group-panel">
								<button type="button" class="btn btn-block tags-clear-all">Clear All <? echo $tagsName; ?></button>
								<br>
								<div class="group-title">What Are You Looking For?</div>
								<?php if (isset($searchBar->categoryManager->superFilters)): ?>
									<div class="btn-group centered super-filter-list">
										<?php foreach ($searchBar->categoryManager->superFilters as $superFilter): ?>
											<button type="button" class="btn"><? echo $superFilter->value; ?></button>
										<?php endforeach; ?>
									</div>
								<?php endif; ?>
							</div>
							<? if (isset($searchBar->categoryManager->groups)): ?>
								<div class="tag-list-container" style="height: 400px">
									<div class="accordion tag-list">
										<?php foreach ($searchBar->categoryManager->groups as $group): ?>
											<h3><span><?php echo $group; ?></span></h3>
											<div>
												<label class="checkbox group-selector-title"><input class="group-selector" type="checkbox"><?php echo $group; ?>
												</label>
												<?php foreach ($searchBar->categoryManager->getTagsByGroup($group) as $tag): ?>
													<label class="checkbox">
														<input class="item-selector" type="checkbox" value="<?php echo $group . '------' . $tag['tag']; ?>">
														<?php echo $tag['tag']; ?>
													</label>
												<?php endforeach; ?>
											</div>
										<?php endforeach; ?>
									</div>
								</div>
							<? endif ?>
							<div class="buttons-area">
								<button class="btn accept-button" type="button">OK</button>
								<button class="btn cancel-button" type="button">Cancel</button>
							</div>
						</div>
					</div>
				</div>
			</td>
		</tr>
		<tr>
			<td>
				<div class="input-append input-prepend">
					<span class="add-on">Search:</span>
					<input class="input-xxlarge search-bar-text" type="text" placeholder="<? echo $searchBar->defaultLabel; ?>">
					<button class="btn search-bar-run" type="submit">
						<img src="<? echo Yii::app()->getBaseUrl(true) . '/images/search/search-shortcuts.png'; ?>">
					</button>
					<button class="btn tags-filter-panel-switcher" type="submit"><? echo $tagsName; ?></button>
				</div>
				<p class="muted">
					<small class="tag-condition-selected">Fusce dapibus, tellus ac cursus commodo, tortor mauris nibh.</small>
				</p>
			</td>
		</tr>
		<tr class="file-filter-panel">
			<td>
				<form class="form-inline">
					<label class="checkbox file-selector"><input id="search-file-type-video" type="checkbox" checked>video</label>
					<label class="checkbox file-selector"><input id="search-file-type-powerpoint" type="checkbox" checked>presentations</label>
					<label class="checkbox file-selector"><input id="search-file-type-other" type="checkbox">all other files</label>
				</form>
			</td>
		</tr>
	</table>
<?php endif; ?>