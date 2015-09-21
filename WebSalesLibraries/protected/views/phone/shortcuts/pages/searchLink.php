<?
	/** @var $shortcut SearchLinkShortcut */
	if ($shortcut->samePage)
		$parentId = isset($shortcut->bundleId) ? ('shortcut-link-page-' . $shortcut->bundleId) : 'shortcut-group';
	else
		$parentId = null;
	$this->renderPartial('../search/searchResultPage', array('parentId' => $parentId));
?>