<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\MarkupSettings;

	/**
	 * @var $markup MarkupSettings
	 * @var $height int
	 * @var $isTop boolean
	 * @var $screenSettings array
	 */
?>
<div class="container-fluid landing-page-markup fixed-panel fixed-panel-<? if ($isTop): ?>top<? else: ?>bottom<? endif; ?>"
    <? if ($isTop): ?>
        style="width: 100%;"
    <? else: ?>
        style="width: 100%; position: fixed; height: <? echo $height ?>px; bottom: 0;"
    <? endif; ?>>
    <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $markup->contentBlocks, 'screenSettings' => $screenSettings), true); ?>
</div>
