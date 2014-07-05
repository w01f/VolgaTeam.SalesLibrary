<?	/** @var $folder LibraryFolder */?>
<div class="folder-body" style="border-color: <? echo $folder->borderColor; ?>;">
	<div class="folder-header-container" id="folder<? echo $folder->id; ?>"
		 style="font-family: <? echo $folder->headerFont->name; ?>,serif;
			 font-size: <? echo $folder->headerFont->size; ?>pt;
			 font-weight: <? echo $folder->headerFont->isBold ? ' bold' : ' normal'; ?>;
			 font-style: <? echo $folder->headerFont->isItalic ? ' italic' : ' normal'; ?>;
			 text-align: <? echo $folder->headerAlignment; ?>;
			 background-color: <? echo $folder->headerBackColor; ?>;
			 color: <? echo $folder->headerForeColor; ?>;
			 border-bottom-color: <? echo $folder->borderColor; ?>;">
		<? if (isset($folder->banner) && $folder->banner->isEnabled): ?>
			<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $folder->banner, 'isLinkBanner' => false), true); ?>
		<? else: ?>
			<? $widget = $folder->getWidget(); ?>
			<? if (isset($widget)): ?>
				<img class="folder-widget" src="data:image/png;base64,<? echo $widget; ?>">
			<? endif; ?>
			<span class="folder-header"><? echo $folder->name; ?></span>
		<? endif; ?>
	</div>
	<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $folder), true); ?>
</div>