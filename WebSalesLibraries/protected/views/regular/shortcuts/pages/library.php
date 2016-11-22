<?
	/** @var $shortcut WallbinShortcut */
	$library = $shortcut->library;
	$this->renderPartial('../wallbin/library', array(
		'library' => $library,
		'pageSelectorMode' => $shortcut->pageSelectorMode,
		'pageViewType' => $shortcut->pageViewType,
		'style' => $shortcut->style
	));
?>