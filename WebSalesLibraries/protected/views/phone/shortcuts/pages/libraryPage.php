<?
	/** @var $shortcut LibraryPageShortcut */

	$libraryPage = $shortcut->getLibraryPage();
	$this->renderPartial('../wallbin/pageContent', array('page' => $libraryPage));
?>