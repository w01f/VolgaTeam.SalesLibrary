<?
	/**
	 * @var $conditionTag string
	 */
	$title = '';
	$viewPath = '';
	$logoPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'search' . DIRECTORY_SEPARATOR . 'ribbon' . DIRECTORY_SEPARATOR;
	switch ($conditionTag)
	{
		case 'keyword':
			$title = 'Keyword Search';
			$viewPath = 'keyword';
			$logoPath .= 'keyword.png';
			break;
		case 'dateRange':
			$title = 'Search By Date';
			$viewPath = 'dateRange';
			$logoPath .= 'date-range.png';
			break;
		case 'settings':
			$title = 'Filters';
			$viewPath = 'settings';
			$logoPath .= 'filter.png';
			break;
		case 'fileTypes':
			$title = 'File Types';
			$viewPath = 'fileTypes';
			$logoPath .= 'file-types.png';
			break;
		case 'categories':
			$title = Yii::app()->params['tags']['tab_name'];
			$viewPath = 'categories';
			$logoPath .= 'tags.png';
			break;
		case 'superFilters':
			$title = 'Super Tags';
			$viewPath = 'superFilters';
			$logoPath .= 'super-filters.png';
			break;
		case 'libraries':
			$title = Yii::app()->params['stations']['tab_name'];
			$viewPath = 'libraries';
			$logoPath .= 'libraries.png';
			break;
	};
?>
<div class="search-filters">
	<div class="row">
		<div class="col-xs-12">
			<h3 class="header">
				<img class="search-filters-logo" src="<? echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoPath)); ?>"/>
				<? echo $title; ?>
			</h3>
		</div>
	</div>
	<div class="row">
		<div class="col-xs-12 search-filter-editors">
			<? echo $this->renderPartial('conditions/' . $viewPath, array(), true); ?>
		</div>
	</div>
	<div class="row buttons-area">
		<div class="col-xs-3">
			<button class="btn btn-default accept-button" type="button">OK</button>
		</div>
		<div class="col-xs-3">
			<button class="btn btn-default cancel-button" type="button">Cancel</button>
		</div>
		<div class="col-xs-3 col-xs-offset-3">
			<button class="btn btn-default search-button" type="button">Search Now</button>
		</div>
	</div>
</div>