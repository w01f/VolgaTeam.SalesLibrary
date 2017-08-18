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
        <? if (isset($previewInfo->header->padding) && $previewInfo->header->padding->isConfigured): ?>
        #content .wallbin-header-container {

            padding-top: <? echo $previewInfo->header->padding->top; ?>px !important;
            padding-left: <? echo $previewInfo->header->padding->left; ?>px !important;
            padding-bottom: <? echo $previewInfo->header->padding->bottom; ?>px !important;
            padding-right: <? echo $previewInfo->header->padding->right; ?>px !important;
        }
        <?endif;?>

        #content .wallbin-header .wallbin-header-cell {
            border-bottom: 1px <? echo Utils::formatColor($previewInfo->header->headerBorderColor); ?> solid !important;
        }
    </style>
    <div class="wallbin-header-container">
        <table class="wallbin-header">
            <tr>
                <td class="wallbin-header-cell shortcuts-search-bar-container">
	                <? echo $this->renderPartial('../shortcuts/searchBar/bar', array('searchBar' => $previewInfo->searchBar), true); ?>
                </td>
            </tr>
        </table>
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
