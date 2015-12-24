<? /** @var $folder LibraryFolder */ ?>
<div class="folder-links-scroll-area"
	 style="background-color: <? echo $folder->windowBackColor; ?>;color: <? echo $folder->windowForeColor; ?>;">
	<? if (count($folder->files) > 0): ?>
		<div class="folder-links-container">
			<? foreach ($folder->files as $link): ?>
				<?
				$link = $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php',
					array(
						'link' => $link,
						'disableBanner' => false,
						'disableWidget' => false,
						'authorized' => true
					), true);
				if (isset(Yii::app()->browser) && Yii::app()->browser->isMobile())
					$link = str_replace("title=", "t1=", $link);
				echo $link;
				?>
			<? endforeach; ?>
		</div>
	<? endif; ?>
</div>