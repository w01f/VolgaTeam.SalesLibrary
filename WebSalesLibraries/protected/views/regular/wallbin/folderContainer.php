<?
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;
	use application\models\wallbin\models\web\style\FolderStyle as FolderStyle;

	/**
	 * @var $folder LibraryFolder
	 * @var $style FolderStyle
	 */
?>
<div class="folder-body" style="border-color: <? echo $folder->borderColor; ?>;">
	<? if ($style->showRegularHeader): ?>
        <div class="folder-header-container regular" id="folder<? echo $folder->id; ?>"
             style="font-family: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->name : $folder->headerFont->name; ?>,serif;
                     font-size: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->size : $folder->headerFont->size; ?>pt;
                     font-weight: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isBold : $folder->headerFont->isBold) ? ' bold' : ' normal'; ?>;
                     font-style: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isItalic : $folder->headerFont->isItalic) ? ' italic' : ' normal'; ?>;
                     text-decoration: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isUnderlined : $folder->headerFont->isUnderlined) ? ' underline' : ' inherit'; ?>;
                     text-align: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->imageAlignment : $folder->headerAlignment; ?>;
                     background-color: <? echo $folder->headerBackColor; ?>;
                     color: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->foreColor : $folder->headerForeColor; ?>;
                     border-bottom-color: <? echo $folder->borderColor; ?>;
                     min-height: <? echo $folder->headerHeight; ?>px;">
			<? if (isset($folder->banner) && $folder->banner->isEnabled): ?>
                <table style="height: 100%; display: inline;">
                    <tr>
                        <td><img src="data:image/png;base64,<? echo $folder->banner->image; ?>"></td>
						<? if ($folder->banner->showText): ?>
                            <td>
								<span style="text-align: <? echo $folder->banner->imageAlignment; ?>;">
									<? echo $folder->banner->text; ?>
								</span>
                            </td>
						<? endif; ?>
                    </tr>
                </table>
			<? else: ?>
				<? $widget = $folder->getWidget(); ?>
				<? if (isset($widget)): ?>
                    <img class="folder-widget" src="data:image/png;base64,<? echo $widget; ?>">
				<? endif; ?>
                <span class="folder-header"
                      style="line-height: <? echo($folder->headerHeight - 1); ?>px;"><? echo $folder->name; ?></span>
			<? endif; ?>
        </div>
	<? elseif ($style->showCustomTitle && (!$style->hideTopFoldersCustomTitle || ($style->hideTopFoldersCustomTitle && $folder->rowOrder > 0))): ?>
        <style>
            #folder<? echo $folder->id; ?> {
                font-family: <? echo $style->font->name; ?>, serif;
                font-size: <? echo $style->font->size; ?>pt;
                font-weight: <? echo $style->font->isBold ? 'bold' : 'normal'; ?>;
                font-style: <? echo $style->font->isItalic ? 'italic' : 'normal'; ?>;
                text-decoration: <? echo $style->font->isUnderlined ? 'underline' : 'inherit'; ?>;
                text-align: <? echo $style->textAlign; ?>;
                color: <? echo (isset($style->textColor)?('#'.$style->textColor):(isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->foreColor : $folder->headerForeColor)); ?>;
                background-color: <? echo (isset($style->backColor)?('#'.$style->backColor):$folder->headerBackColor); ?>;
            }

            .folder-header-container.custom-title img.folder-widget {
                height: 32px;
            }

            .folder-header-container.custom-title span.folder-header {
                line-height: 32px;
            }
        </style>
        <div class="folder-header-container custom-title" id="folder<? echo $folder->id; ?>">
			<? if ($style->showWidget): ?>
				<? $widget = $folder->getWidget(); ?>
				<? if (isset($widget)): ?>
                    <img class="folder-widget" src="data:image/png;base64,<? echo $widget; ?>">
                    <span class="folder-header"><? echo $folder->name; ?></span>
				<? else: ?>
                    <span class="folder-header"><? echo $folder->name; ?></span>
				<? endif; ?>
			<? else: ?>
				<? echo $folder->name; ?>
			<? endif; ?>
        </div>
	<? endif; ?>
	<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $folder), true); ?>
</div>