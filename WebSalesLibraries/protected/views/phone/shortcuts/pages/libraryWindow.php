<?
	/** @var $shortcut WindowShortcut */

	$folder = $shortcut->getWindow();
	$this->renderPartial('../wallbin/folderLinks', array('folder' => $folder));
?>
