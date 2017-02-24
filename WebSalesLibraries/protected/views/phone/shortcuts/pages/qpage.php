<?
	/** @var $shortcut QPageShortcut */
	$page = QPageRecord::model()->findByPk($this->pageId);
	$this->renderPartial('../qpage/pageContent', array('page' => $page, 'isShortcut' => true));
?>