<?

	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryWindowBlock;

	/**
     * @var $contentBlock LibraryWindowBlock
	 * @var $screenSettings array
     */

	$blockId = sprintf('library-window-block-%s', $contentBlock->id);

	$window = $contentBlock->shortcut->getWindow();

	if (!$contentBlock->shortcut->linksOnly)
	{
		if ($contentBlock->shortcut->windowViewType == 'columns')
			$content = $this->renderFile(
				Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderContainer.php',
				array(
					'folder' => $window,
					'style' => \application\models\wallbin\models\web\style\FolderStyle::createDefault()
				), true);
		else
			$content = $this->renderFile(
				Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/accordionFolder.php',
				array(
					'folder' => $window
				), true);
	}
	else
		$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderLinks.php', array('folder' => $window), true);

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
<style>
    <? if (isset($contentBlock->shortcut->contentPadding) && $contentBlock->shortcut->contentPadding->isConfigured): ?>
    <? echo '#'.$blockId; ?> {

        padding-top: <? echo $contentBlock->shortcut->contentPadding->top; ?>px !important;
        padding-left: <? echo $contentBlock->shortcut->contentPadding->left; ?>px !important;
        padding-bottom: <? echo $contentBlock->shortcut->contentPadding->bottom; ?>px !important;
        padding-right: <? echo $contentBlock->shortcut->contentPadding->right; ?>px !important;
    }

    <?endif;?>
</style>
<div id="<? echo $blockId; ?>" class="library-window-block">
    <div class="service-data wallbin-settings">
        <div class="encoded-data">
			<? echo base64_encode(CJSON::encode(array(
				'shortcutId' => $contentBlock->shortcut->id,
				'pageViewType' => $contentBlock->shortcut->windowViewType,
				'processResponsiveColumns' => false
			))
			); ?>
        </div>
    </div>
	<? if ($contentBlock->shortcut->column < 0): ?>
		<? echo $content; ?>
	<? else: ?>
		<? for ($i = 0; $i < 3; $i++): ?>
            <div class="page-column column<? echo $i; ?>">
				<? if ($contentBlock->shortcut->column == $i): ?>
					<? echo $content; ?>
				<? else: ?>
                    <div class="mock">mock</div>
				<? endif; ?>
            </div>
		<? endfor; ?>
	<? endif; ?>
</div>
