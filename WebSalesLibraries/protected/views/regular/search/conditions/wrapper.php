<?
	/**
	 * @var $conditionTag string
	 */
	$title = '';
	$viewPath = '';
	$logoPath = realpath(Yii::app()->basePath . DIRECTORY_SEPARATOR . '..') . DIRECTORY_SEPARATOR . 'images' . DIRECTORY_SEPARATOR . 'search' . DIRECTORY_SEPARATOR . 'ribbon' . DIRECTORY_SEPARATOR;
	switch ($conditionTag)
	{
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
<table class="tool-dialog">
	<tr>
		<td colspan="2">
			<h3 class="search-filter-header">
				<img class="search-filters-logo" src="<? echo 'data:image/png;base64,' . base64_encode(file_get_contents($logoPath)); ?>"/>
				<? echo $title; ?>
			</h3>
			<div class="search-filter-editors">
				<? echo $this->renderPartial('conditions/' . $viewPath, array(), true); ?>
			</div>
		</td>
	</tr>
	<tr>
		<td colspan="2" class="buttons-area">
			<button class="btn btn-default accept-button" type="button">OK</button>
			<button class="btn btn-default cancel-button" type="button">Cancel</button>
		</td>
	</tr>
</table>