<?
	/** @var $shortcut QPageShortcut */
	$page = QPageRecord::model()->findByPk($shortcut->pageId);
	$this->renderPartial('../qpage/pageContent', array('page' => $page, 'isShortcut' => true));
?>