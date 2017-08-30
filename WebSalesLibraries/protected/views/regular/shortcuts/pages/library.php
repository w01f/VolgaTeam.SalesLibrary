<?
	/** @var $shortcut WallbinShortcut */
	$library = $shortcut->library;
	$this->renderPartial('../wallbin/library', array(
		'library' => $library,
		'pageSelectorMode' => $shortcut->pageSelectorMode,
		'pageViewType' => $shortcut->pageViewType,
		'isEmbedded' => false,
		'containerId' => 'content',
		'styleContainer' => $shortcut,
		'searchBar'=> $shortcut->getSearchBar()
	));
?>