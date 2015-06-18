<?
	/** @var $pageShortcut SinglePageShortcut */

	$libraryPage = $pageShortcut->getLibraryPage();
	$this->renderPartial('../wallbin/pageContent', array('page' => $libraryPage));
?>