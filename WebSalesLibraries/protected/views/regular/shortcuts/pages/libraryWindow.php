<?
	/** @var $shortcut WindowShortcut */

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
<? if ($shortcut->searchBar->configured): ?>
    <style>
        <? if (isset($shortcut->header->padding) && $shortcut->header->padding->isConfigured): ?>
        #content .wallbin-header-container {

            padding-top: <? echo $shortcut->header->padding->top; ?>px !important;
            padding-left: <? echo $shortcut->header->padding->left; ?>px !important;
            padding-bottom: <? echo $shortcut->header->padding->bottom; ?>px !important;
            padding-right: <? echo $shortcut->header->padding->right; ?>px !important;
        }
        <?endif;?>

        #content .wallbin-header .wallbin-header-cell {
            border-bottom: 1px <? echo Utils::formatColor($shortcut->header->headerBorderColor); ?> solid !important;
        }
    </style>
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
<div class='padding'>
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
