<?
	/** @var $shortcut WindowShortcut */

	$window = $shortcut->getWindow();

	if(!$shortcut->linksOnly)
	{
		if ($shortcut->windowViewType == 'columns')
			$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderContainer.php', array('folder' => $window, 'showHeader' => true), true);
		else
			$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/accordionFolder.php', array('folder' => $window), true);
	}
	else
		$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderLinks.php', array('folder' => $window), true);
?>
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
	<?endif; ?>
</div>
