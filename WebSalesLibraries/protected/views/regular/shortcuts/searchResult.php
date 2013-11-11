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
<div class="search-conditions" style="display: none;">
	<div class="shortcut-title"><? echo $searchContainer->title; ?></div>
	<? if (isset($searchContainer->conditions->text)): ?>
		<div class="search-text"><? echo $searchContainer->conditions->text; ?></div>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->startDate)): ?>
		<div class="start-date"><? echo $searchContainer->conditions->startDate; ?></div>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->endDate)): ?>
		<div class="end-date"><? echo $searchContainer->conditions->endDate; ?></div>
	<? endif; ?>
	<? if ($searchContainer->conditions->dateModified): ?>
		<div class="use-file-date">true</div>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->fileTypes)): ?>
		<? foreach ($searchContainer->conditions->fileTypes as $fileType): ?>
			<div class="file-type"><? echo $fileType; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->libraries)): ?>
		<? foreach ($searchContainer->conditions->libraries as $library): ?>
			<div class="library"><? echo $library; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->superFilters)): ?>
		<? foreach ($searchContainer->conditions->superFilters as $superFilter): ?>
			<div class="super-filter"><? echo $superFilter; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->categories)): ?>
		<? foreach ($searchContainer->conditions->categories as $category): ?>
			<div class="category"><? echo $category->category; ?>------<? echo $category->tag; ?></div>
		<? endforeach; ?>
	<? endif; ?>
	<? if ($searchContainer->conditions->onlyWithCategories): ?>
		<div class="only-with-categories">true</div>
	<? endif; ?>
	<? if ($searchContainer->conditions->hideDuplicated): ?>
		<div class="hide-duplicated">true</div>
	<? endif; ?>
	<? if ($searchContainer->conditions->searchByName): ?>
		<div class="search-by-name">true</div>
	<? endif; ?>
	<? if ($searchContainer->conditions->searchByContent): ?>
		<div class="search-by-content">true</div>
	<? endif; ?>
	<? if (!$searchContainer->showResultsBar && $searchContainer->samePage): ?>
		<div class="hide-results">true</div>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->sortColumn)): ?>
		<div class="sort-column"><? echo $searchContainer->conditions->sortColumn; ?></div>
	<? endif; ?>
	<? if (isset($searchContainer->conditions->sortDirection)): ?>
		<div class="sort-direction"><? echo $searchContainer->conditions->sortDirection; ?></div>
	<? endif; ?>
</div>