<?
	use application\models\shortcuts\models\landing_page\regular_markup\common\UrlBlock;

	/**
     * @var $contentBlock UrlBlock
	 * @var $screenSettings array
     */

	$blockId = sprintf('url-%s', $contentBlock->id);

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
<a id="<? echo $blockId; ?>" class="landing-url"
   href="<? echo $contentBlock->url; ?>" <? if ($contentBlock->isMailTo!==false): ?>target="_self"<?else:?>target="_blank"<? endif; ?>
   style="<? echo $this->renderPartial('landingPageMarkup/style/stylePadding', array('padding' => $contentBlock->padding), true); ?>
    <? echo $this->renderPartial('landingPageMarkup/style/styleMargin', array('margin' => $contentBlock->margin), true); ?>"
	<? if (!empty($contentBlock->hoverText)): ?> title="<? echo $contentBlock->hoverText; ?>"<? endif; ?>>
	<? echo $this->renderPartial('landingPageMarkup/common/blockContainer', array('contentBlocks' => $contentBlock->items, 'screenSettings' => $screenSettings), true); ?>
</a>