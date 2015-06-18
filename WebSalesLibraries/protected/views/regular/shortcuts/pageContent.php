<?
	/** @var $pageShortcut SinglePageShortcut */

	$libraryPage = $pageShortcut->getLibraryPage();

	if ($pageShortcut->pageViewType == 'accordion')
		$this->renderPartial('../wallbin/accordionView', array('libraryPage' => $libraryPage));
	else
		$this->renderPartial('../wallbin/columnsView', array('selectedPage' => $libraryPage));
?>