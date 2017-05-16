<?
	/**
	 * @var $menuGroups ShortcutGroup[]
	 * @var $linkName string
	 * @var $linkId string
	 */

	$this->renderPartial('../site/scripts');
	if (UserIdentity::isUserAuthorized())
		$this->renderPartial('../site/chat');

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
			<div class="content-scrollable-area" style="padding: 40px">
                <a href="#" class="single-link">
                    <?echo $linkName?>
                    <div class="service-data link-viewer-data">
                        <div class="link-id"><? echo $linkId; ?></div>
                    </div>
                </a>
			</div>
		</td>
	</tr>
</table>