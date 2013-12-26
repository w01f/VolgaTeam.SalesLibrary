<div class="folder-body" style="border-color: <?php echo $quizItem->borderColor; ?>;">
	<div class="folder-header-container" id="folder<?php echo $quizItem->id; ?>"
		 style="font-family: <?php echo $quizItem->headerFont->name; ?>,serif;
			 font-size: <?php echo $quizItem->headerFont->size; ?>pt;
			 font-weight: <?php echo $quizItem->headerFont->isBold ? ' bold' : ' normal'; ?>;
			 font-style: <?php echo $quizItem->headerFont->isItalic ? ' italic' : ' normal'; ?>;
			 text-align: <?php echo $quizItem->headerAlignment; ?>;
			 background-color: <?php echo $quizItem->headerBackColor; ?>;
			 color: <?php echo $quizItem->headerForeColor; ?>;
			 border-bottom-color: <?php echo $quizItem->borderColor; ?>;">
		<?php if (isset($quizItem->banner) && $quizItem->banner->isEnabled): ?>
			<?php echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/banner.php', array('banner' => $quizItem->banner, 'isLinkBanner' => false), true); ?>
		<?php else: ?>
			<?php $widget = $quizItem->getWidget(); ?>
			<?php if (isset($widget)): ?>
				<img class="folder-widget" src="data:image/png;base64,<?php echo $widget; ?>">
			<?php endif; ?>
			<span class="folder-header"><?php echo $quizItem->name; ?></span>
		<?php endif; ?>
	</div>
	<? echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/folderLinks.php', array('folder' => $quizItem), true); ?>
</div>