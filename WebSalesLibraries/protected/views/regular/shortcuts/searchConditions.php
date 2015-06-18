<?
	/** @var $searchContainer IShortcutSearchOptionsContainer */
	$cs = Yii::app()->clientScript;
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/css/dataTables.bootstrap.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/data-table.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts.css?' . Yii::app()->params['version']);
	$cs->registerCssFile(Yii::app()->getBaseUrl(true) . '/css/regular/base/shortcuts-search.css?' . Yii::app()->params['version']);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/js/jquery.dataTables.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/vendor/data-table/js/dataTables.bootstrap.min.js', CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/common/search-processor.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/data-table.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/base/shortcuts-search.js?' . Yii::app()->params['version'], CClientScript::POS_HEAD);
?>
<div class="search-conditions" style="display: none;">
	<div class="encoded-object"><? echo CJSON::encode($searchContainer->getSearchOptions()) ?></div>
</div>
<div class="search-view-options" style="display: none;">
	<div class="encoded-object">
		<? echo CJSON::encode(array(
			'showCategory' => Yii::app()->params['search_options']['hide_tag'] != true,
			'categoryColumnName' => Yii::app()->params['tags']['column_name'],
			'showLibraries' => Yii::app()->params['search_options']['hide_libraries'] != true,
			'librariesColumnName' => Yii::app()->params['stations']['column_name']
		)) ?>
	</div>
</div>