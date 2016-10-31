<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;

	/**
	 * @var $data InternalLinkPreviewData
	 */

	$libraryManager = new LibraryManager();
	$library = $libraryManager->getLibraryById($data->libraryId);
	$this->renderPartial('../wallbin/library', array(
		'library' => $library,
		'pageSelectorMode' => 'tabs',
		'pageViewType' => 'columns',
		'showLogo' => true
	));
?>