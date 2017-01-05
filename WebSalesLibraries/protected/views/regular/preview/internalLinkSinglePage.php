<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $linkId string
	 */

	$this->renderPartial('../site/scripts');

	$cs = Yii::app()->clientScript;
	$cs->registerScriptFile(Yii::app()->getBaseUrl(true) . '/js/regular/link-viewer/link-viewer-internal-link-single.js?' . Yii::app()->params['version'], CClientScript::POS_END);
?>
<?
	if (count($menuGroups) > 0)
		$this->renderPartial('../menu/mainMenu',
			array(
				'menuGroups' => $menuGroups,
				'showMainSiteUrl' => false
			)
		);
	else
		$this->renderPartial('../menu/singlePageMenu',
			array(
				'iconClass' => '',
				'headerText' => '',
				'showMainSiteUrl' => false
			)
		);
?>
<table id="content">
	<tr>
		<td class="navigation-panel">
		</td>
		<td class="content-inner">
			<div class="content-scrollable-area">
				<div class="service-data link-viewer-data">
					<div class="link-id"><? echo $linkId; ?></div>
				</div>
			</div>
		</td>
	</tr>
</table>