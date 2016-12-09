<?
	/** @var $shortcut SearchAppShortcut */
?>
<div id="shortcuts-search-app" class="data-table-content-container">
	<table id="data-table-content" class="table table-striped table-bordered"></table>
</div>
<div class="search-app-footer">
	<div class="search-app-footer-block applied-filters-block">
		<em>
			<span class="search-filter keyword"><span class="title">Keyword: </span><span class="value"></span>; </span>
			<span class="search-filter date-range"><span class="title">Date Range: </span><span class="value"></span>; </span>
			<span class="search-filter settings"><span class="title">Filters: </span><span class="value"></span>; </span>
			<span class="search-filter file-types"><span class="title">File Types: </span><span class="value"></span>; </span>
			<span class="search-filter categories"><span class="title"><? echo Yii::app()->params['tags']['tab_name']; ?>: </span><span class="value"></span>; </span>
			<span class="search-filter super-filters"><span class="title">Super Tags: </span><span class="value"></span>; </span>
			<span class="search-filter libraries"><span class="title"><? echo Yii::app()->params['stations']['tab_name']; ?>: </span><span class="value"></span>; </span>
		</em>
		<p class="search-filter file-types-warning text-danger">ATTENTION: You must select at least ONE File Type…</p>
	</div>
</div>