<?
	/** @var $shortcut QPageShortcut */

	$page = QPageRecord::model()->findByPk($this->pageId);
	$parentId = isset($shortcut->bundleId) ? ('shortcut-link-page-' . $shortcut->bundleId) : 'shortcut-group';

	$this->renderPartial('../qpage/pageContent', array('page' => $page, 'parentId' => $parentId));
?>