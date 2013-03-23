<div class="folder-links-scroll-area"
	 style="background-color: <?php echo $folder->windowBackColor; ?>;color: <?php echo $folder->borderColor; ?>;">
	<?php if (isset($folder->files)): ?>
	<div class="folder-links-container">
		<?php foreach ($folder->files as $link): ?>
		<?php
		$link = $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $link), true);
		if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile())
			$link = str_replace("title=", "t1=", $link);
		echo $link;
		?>
		<?php endforeach; ?>
	</div>
	<?php endif; ?>
</div>