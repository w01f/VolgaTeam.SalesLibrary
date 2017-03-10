<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $searchBar SearchBar
	 * @var $linkId string
	 */

	$this->renderPartial('../site/scripts');
	if (UserIdentity::isUserAuthorized())
		$this->renderPartial('../site/chat');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/shortcuts/shortcuts-search-bar-results-controller.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<? $this->renderPartial('../menu/mainMenu', array('menuGroups' => $menuGroups, 'headerText' => 'Quick Search', 'showMainSiteUrl' => false)); ?>
<table id="content">
	<tr>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<div class="service-data">
					<div class="object-id" style="display: none;"><? echo $linkId; ?></div>
					<div class="search-bar-actions">
						<? $this->renderPartial('../menu/actionItems', array('actionContainer' => $searchBar), false, true); ?>
					</div>
					<? $this->renderPartial('searchConditions', array('searchContainer' => $searchBar)); ?>
				</div>
			</div>
		</td>
	</tr>
</table>