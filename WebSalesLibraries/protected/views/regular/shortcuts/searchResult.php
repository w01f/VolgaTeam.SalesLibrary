<?php
	$version = '3.0';
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/search.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/file-card.css?' . $version);
	$cs->registerCssFile(Yii::app()->baseUrl . '/css/regular/base/links-grid.css?' . $version);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/links-grid.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/search-processor.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/base/shortcuts.js?' . $version, CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->baseUrl . '/js/regular/shortcuts/search-loader.js?' . $version, CClientScript::POS_HEAD);
?>
<div id="search-conditions" style="display: none;">
	<div class="shortcut-title"><? echo $searchShortcut->title; ?></div>
	<? if (isset($searchShortcut->text)): ?>
		<div class="search-text"><? echo $searchShortcut->text; ?></div>
	<? endif; ?>
	<? if (isset($searchShortcut->startDate)): ?>
		<div class="start-date"><? echo $searchShortcut->startDate; ?></div>
	<? endif; ?>
	<? if (isset($searchShortcut->endDate)): ?>
		<div class="end-date"><? echo $searchShortcut->endDate; ?></div>
	<? endif; ?>
	<? if ($searchShortcut->dateModified): ?>
		<div class="use-file-date">true</div>
	<? endif; ?>
	<? if (isset($searchShortcut->fileTypes)): ?>
		<? foreach ($searchShortcut->fileTypes as $fileType): ?>
			<div class="file-type"><? echo $fileType; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchShortcut->libraries)): ?>
		<? foreach ($searchShortcut->libraries as $library): ?>
			<div class="library"><? echo $library; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchShortcut->superFilters)): ?>
		<? foreach ($searchShortcut->superFilters as $superFilter): ?>
			<div class="super-filter"><? echo $superFilter; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchShortcut->categories)): ?>
		<? foreach ($searchShortcut->categories as $category): ?>
			<div class="category"><? echo $category->category; ?>------<? echo $category->tag; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if ($searchShortcut->onlyWithCategories): ?>
		<div class="only-with-categories">true</div>
	<? endif; ?>
	<? if ($searchShortcut->hideDuplicated): ?>
		<div class="hide-duplicated">true</div>
	<? endif; ?>
	<? if ($searchShortcut->searchByName): ?>
		<div class="search-by-name">true</div>
	<? endif; ?>
	<? if ($searchShortcut->searchByContent): ?>
		<div class="search-by-content">true</div>
	<? endif; ?>
	<? if (!$searchShortcut->showResultsBar && $searchShortcut->samePage): ?>
		<div class="hide-results">true</div>
	<? endif; ?>
	<? if (isset($searchShortcut->sortColumn)): ?>
		<div class="sort-column"><? echo $searchShortcut->sortColumn; ?></div>
	<? endif; ?>
	<? if (isset($searchShortcut->sortDirection)): ?>
		<div class="sort-direction"><? echo $searchShortcut->sortDirection; ?></div>
	<? endif; ?>
</div>