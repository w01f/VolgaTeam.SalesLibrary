<div class="folder-links-scroll-area"
	 style="background-color: <?php echo $quizItem->windowBackColor; ?>;color: <?php echo $quizItem->windowForeColor; ?>;">
	<?php if (isset($quizItem->files)): ?>
		<div class="folder-links-container">
			<?php foreach ($quizItem->files as $link): ?>
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