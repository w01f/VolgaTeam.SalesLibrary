<?
	/** @var $link LibraryLink */
	foreach ($link->folderContent as $contentLink)
		echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php', array('link' => $contentLink), true);
