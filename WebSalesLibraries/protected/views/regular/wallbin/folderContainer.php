<?
	use application\models\wallbin\models\web\LibraryFolder as LibraryFolder;

	/** @var $folder LibraryFolder */
?>
<div class="folder-body" style="border-color: <? echo $folder->borderColor; ?>;">
	<div class="folder-header-container" id="folder<? echo $folder->id; ?>"
		 style="font-family: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->name: $folder->headerFont->name; ?>,serif;
			 font-size: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->size: $folder->headerFont->size; ?>pt;
			 font-weight: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isBold: $folder->headerFont->isBold) ? ' bold' : ' normal'; ?>;
			 font-style: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isItalic: $folder->headerFont->isItalic) ? ' italic' : ' normal'; ?>;
			 text-decoration: <? echo (isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->font->isUnderlined: $folder->headerFont->isUnderlined) ? ' underline' : ' inherit'; ?>;
			 text-align: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->imageAlignment : $folder->headerAlignment; ?>;
			 background-color: <? echo $folder->headerBackColor; ?>;
			 color: <? echo isset($folder->banner) && $folder->banner->isEnabled ? $folder->banner->foreColor :$folder->headerForeColor; ?>;
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
			<span class="folder-header" style="line-height: <? echo ($folder->headerHeight-1); ?>px;"><? echo $folder->name; ?></span>
		<? endif; ?>
	</div>
	<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $folder), true); ?>
</div>