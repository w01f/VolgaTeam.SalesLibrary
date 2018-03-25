<?
	use application\models\shortcuts\models\landing_page\regular_markup\toggle_panel\TogglePanelItem;

	/**
     * @var $contentBlock TogglePanelItem
	 * @var $screenSettings array
     */

	$blockId = sprintf('toggle-panel-item-%s', $contentBlock->id);

	echo $this->renderPartial('landingPageMarkup/style/styleBorder',
		array(
			'border' => $contentBlock->border,
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleTextAppearance',
		array(
			'textAppearance' => $contentBlock->getTextAppearance(),
			'blockId' => $blockId
		)
		, true);
	echo $this->renderPartial('landingPageMarkup/style/styleBackground',
		array(
			'background' => $contentBlock->background,
			'blockId' => $blockId
		)
		, true);
?>
<div id="<? echo $blockId; ?>" data-toggle-tag="<? echo $contentBlock->tag; ?>" class="row toggle-item<? if ($contentBlock->isDefault): ?> toggle-item-active<? endif; ?>">
    <div class="col-xs-12">
	    <? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
    </div>
</div>