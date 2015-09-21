<?
	/**
	 * @var $conditionTag string
	 */
	$title = '';
	$viewPath = '';
	switch ($conditionTag)
	{
		case 'keyword':
			$title = 'Keyword Search';
			$viewPath = 'keyword';
			break;
		case 'dateRange':
			$title = 'Search By Date';
			$viewPath = 'dateRange';
			break;
		case 'settings':
			$title = 'Filters';
			$viewPath = 'settings';
			break;
		case 'fileTypes':
			$title = 'File Types';
			$viewPath = 'fileTypes';
			break;
		case 'categories':
			$title = Yii::app()->params['tags']['tab_name'];
			$viewPath = 'categories';
			break;
		case 'superFilters':
			$title = 'Super Tags';
			$viewPath = 'superFilters';
			break;
		case 'libraries':
			$title = Yii::app()->params['stations']['tab_name'];
			$viewPath = 'libraries';
			break;
	};
?>
<div class="search-filters">
	<div class="row">
		<div class="col-xs-12">
			<h3 class="header">
				<i class="icon"></i>
				<span class="title"><? echo $title; ?></span>
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