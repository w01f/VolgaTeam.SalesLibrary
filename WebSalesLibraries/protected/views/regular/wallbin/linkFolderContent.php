<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/** @var $link LibraryLink */

	foreach ($link->folderContent as $contentLink)
		echo $this->renderFile(Yii::getPathOfAlias('application.views.regular.wallbin') . '/link.php',
			array(
				'link' => $contentLink,
				'disableBanner' => false,
				'disableWidget' => false,
				'authorized' => true
			), true);
