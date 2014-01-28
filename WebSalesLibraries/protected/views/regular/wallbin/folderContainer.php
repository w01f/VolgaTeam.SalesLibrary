<div class="folder-body" style="border-color: <?php echo $folder->borderColor; ?>;">
	<div class="folder-header-container" id="folder<?php echo $folder->id; ?>"
		 style="font-family: <?php echo $folder->headerFont->name; ?>,serif;
			 font-size: <?php echo $folder->headerFont->size; ?>pt;
			 font-weight: <?php echo $folder->headerFont->isBold ? ' bold' : ' normal'; ?>;
			 font-style: <?php echo $folder->headerFont->isItalic ? ' italic' : ' normal'; ?>;
			 text-align: <?php echo $folder->headerAlignment; ?>;
			 background-color: <?php echo $folder->headerBackColor; ?>;
			 color: <?php echo $folder->headerForeColor; ?>;
			 border-bottom-color: <?php echo $folder->borderColor; ?>;">
		<?php if (isset($folder->banner) && $folder->banner->isEnabled): ?>
			<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $folder->banner, 'isLinkBanner' => false), true); ?>
		<?php else: ?>
			<?php $widget = $folder->getWidget(); ?>
			<?php if (isset($widget)): ?>
				<img class="folder-widget" src="data:image/png;base64,<?php echo $widget; ?>">
			<?php endif; ?>
			<span class="folder-header"><?php echo $folder->name; ?></span>
		<?php endif; ?>
	</div>
	<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $folder), true); ?>
</div>