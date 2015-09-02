<?
	/** @var $shortcut WallbinShortcut */
	$library = $shortcut->library;
	$defaultPage = $library->pages[0];

	if (isset(Yii::app()->user))
	{
		$userId = Yii::app()->user->getId();
		$tabPageExisted = UserTabRecord::isUserTabExists($userId, $library->id);
	}
	else
		$tabPageExisted = false;

	$this->renderPartial('../wallbin/libraryContent', array('library' => $library, 'defaultPage' => $defaultPage, 'tabPageExisted' => $tabPageExisted));
?>

