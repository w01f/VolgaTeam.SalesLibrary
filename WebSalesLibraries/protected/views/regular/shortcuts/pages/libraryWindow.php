<?
	/**
     * @var $shortcut WindowShortcut
	 * @var $screenSettings array
     */

	$window = $shortcut->getWindow();

	if (!$shortcut->linksOnly)
	{
		if ($shortcut->windowViewType == 'columns')
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
<style>
    <? if ($shortcut->searchBar->configured): ?>

    <? if (isset($shortcut->header->padding) && $shortcut->header->padding->isConfigured): ?>
    #content .wallbin-header-container {

        padding-top: <? echo $shortcut->header->padding->top; ?>px !important;
        padding-left: <? echo $shortcut->header->padding->left; ?>px !important;
        padding-bottom: <? echo $shortcut->header->padding->bottom; ?>px !important;
        padding-right: <? echo $shortcut->header->padding->right; ?>px !important;
    }

    <?endif;?>

    #content .wallbin-header .wallbin-header-cell {
        border-bottom: 1px <? echo Utils::formatColorToHex($shortcut->header->headerBorderColor); ?> solid !important;
    }

    <? endif; ?>

    <? if (isset($shortcut->contentPadding) && $shortcut->contentPadding->isConfigured): ?>
    #content .window-container {

        padding-top: <? echo $shortcut->contentPadding->top; ?>px !important;
        padding-left: <? echo $shortcut->contentPadding->left; ?>px !important;
        padding-bottom: <? echo $shortcut->contentPadding->bottom; ?>px !important;
        padding-right: <? echo $shortcut->contentPadding->right; ?>px !important;
    }

    <?endif;?>
</style>
<? if ($shortcut->searchBar->configured): ?>
    <div class="wallbin-header-container">
        <table class="wallbin-header">
            <tr>
                <td class="wallbin-header-cell shortcuts-search-bar-container">
					<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
                </td>
            </tr>
        </table>
    </div>
<? endif; ?>
<div class='window-container'>
	<? if ($shortcut->column < 0): ?>
		<? echo $content; ?>
	<? else: ?>
		<? for ($i = 0; $i < 3; $i++): ?>
            <div class="page-column column<? echo $i; ?>">
				<? if ($shortcut->column == $i): ?>
					<? echo $content; ?>
				<? else: ?>
                    <div class="mock">mock</div>
				<? endif; ?>
            </div>
		<? endfor; ?>
	<? endif; ?>
</div>
