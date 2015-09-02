<?
	/** @var $shortcut SearchLinkShortcut */
	$parentId = isset($shortcut->bundleId) ? ('shortcut-link-page-' . $shortcut->bundleId) : 'shortcut-group';
	$this->renderPartial('../search/searchResultPage', array('parentId' => $parentId));
?>