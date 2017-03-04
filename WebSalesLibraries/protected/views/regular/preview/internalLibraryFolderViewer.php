<?
	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var InternalLibraryFolderPreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;

	$window = $previewInfo->getWindow();

	if(!$previewInfo->linksOnly)
	{
		if ($previewInfo->windowViewType == 'columns')
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
?>
<? if ($previewInfo->searchBar->configured): ?>
    <style>
        #content .shortcuts-search-bar-container
        {
            width: 100%;
            padding: 20px 20px 20px;
        }
    </style>
    <div class="shortcuts-search-bar-container">
		<? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $previewInfo->searchBar), true); ?>
    </div>
<? endif; ?>
<div class='padding'>
	<? if ($previewInfo->column < 0): ?>
		<? echo $content; ?>
	<? else: ?>
		<? for ($i = 0; $i < 3; $i++): ?>
            <div class="page-column column<? echo $i; ?>">
				<? if ($previewInfo->column == $i): ?>
					<? echo $content; ?>
				<? else: ?>
                    <div class="mock">mock</div>
				<? endif; ?>
            </div>
		<? endfor; ?>
	<?endif; ?>
</div>
