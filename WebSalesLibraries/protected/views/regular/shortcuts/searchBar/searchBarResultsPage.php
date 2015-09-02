<?
	/**
	 * @var $searchBar SearchBar
	 * @var $bundleId string
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-search-bar-results-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/singlePageMenu', array('headerText' => 'Quick Search')); ?>
<div id="content" oncontextmenu="return false;">
	<div class="service-data">
		<div class="object-id" style="display: none;"><? echo $bundleId; ?></div>
		<div class="search-bar-actions">
			<? $this->renderPartial('../menu/actionItems', array('actionContainer' => $searchBar), false, true); ?>
		</div>
		<? $this->renderPartial('searchConditions', array('searchContainer' => $searchBar)); ?>
	</div>
</div>
<div id="content-overlay"></div>
<!--  View dialog hidden part  -->
<div>
	<a id="view-dialog-link" href="#view-dialog-container">View Options</a>

	<div id="view-dialog-wrapper">
		<div id="view-dialog-container"></div>
	</div>
</div>