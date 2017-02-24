<?
	/** @var $shortcut WallbinShortcut */
	$library = $shortcut->library;
	/** @var \application\models\wallbin\models\web\LibraryPage $defaultPage */
	$defaultPage = $library->pages[0];
	$defaultPage->loadData();

	$this->renderPartial('../wallbin/libraryContent', array('library' => $library, 'defaultPage' => $defaultPage, 'openOnSamePage' => $shortcut->samePage));
?>

