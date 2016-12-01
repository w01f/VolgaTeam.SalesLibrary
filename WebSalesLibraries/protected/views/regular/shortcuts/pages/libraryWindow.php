<?
	/** @var $shortcut WindowShortcut */

	$window = $shortcut->getWindow();

	if(!$shortcut->linksOnly)
	{
		if ($shortcut->windowViewType == 'columns')
			$content = $this->renderFile(
				Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderContainer.php',
				array(
					'folder' => $window,
					'style' => \application\models\wallbin\models\web\style\FolderStyle::createDefault()
				), true);
		else
			$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/accordionFolder.php', array('folder' => $window), true);
	}
	else
		$content = $this->renderFile(Yii::getPathOfAlias($this->pathPrefix . 'wallbin') . '/folderLinks.php', array('folder' => $window), true);
?>
<? if ($shortcut->searchBar->configured): ?>
	<style>
		#content .shortcuts-search-bar-container
		{
			width: 100%;
			padding: 20px 20px 20px;
		}
	</style>
	<div class="shortcuts-search-bar-container">
		<? echo $this->renderPartial('searchBar/bar', array('searchBar' => $shortcut->searchBar), true); ?>
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
	<?endif; ?>
</div>
