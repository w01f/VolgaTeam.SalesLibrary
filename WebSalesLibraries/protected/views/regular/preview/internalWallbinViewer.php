<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;

	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var InternalWallbinPreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;

	$libraryManager = new LibraryManager();
	$library = $libraryManager->getLibraryById($previewInfo->libraryId);

	if(!empty($previewInfo->pageId))
	{
		$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $library->id);
		$cookie = new CHttpCookie($savedSelectedPageIdTag, $previewInfo->pageId);
		$cookie->expire = time() + (60 * 60 * 24 * 7);
		Yii::app()->request->cookies[$savedSelectedPageIdTag] = $cookie;
	}

	$this->renderPartial('../wallbin/library', array(
		'library' => $library,
		'pageSelectorMode' => $previewInfo->pageSelectorMode,
		'pageViewType' => $previewInfo->pageViewType,
		'isEmbedded' => false,
		'containerId' => 'content',
		'styleContainer' => $previewInfo,
		'searchBar'=> $previewInfo->searchBar
	));
?>