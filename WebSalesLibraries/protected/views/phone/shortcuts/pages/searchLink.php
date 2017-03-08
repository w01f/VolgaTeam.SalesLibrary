<?
	/** @var $shortcut SearchLinkShortcut */
	if ($shortcut->samePage)
	{
		$parentShortcutId = $shortcut->id;
		$parentPageId = isset($shortcut->bundleId) ? ('shortcut-link-page-' . $shortcut->bundleId) : 'shortcut-group';
	}
	else
	{
		$parentShortcutId = '';
		$parentPageId = null;
	}

	$this->renderPartial('../search/searchResultPage', array('parentShortcutId' => $parentShortcutId, 'backPageId' => $parentPageId));
?>