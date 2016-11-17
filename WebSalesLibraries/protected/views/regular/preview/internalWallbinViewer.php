<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;

	/**
	 * @var $data InternalLinkPreviewData
	 */

	/** @var InternalWallbinPreviewInfo $previewInfo */
	$previewInfo = $data->previewInfo;

	$libraryManager = new LibraryManager();
	$library = $libraryManager->getLibraryById($previewInfo->libraryId);

	if(isset($previewInfo->pageId))
	{
		$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $library->id);
		$cookie = new CHttpCookie($savedSelectedPageIdTag, $previewInfo->pageId);
		$cookie->expire = time() + (60 * 60 * 24 * 7);
		Yii::app()->request->cookies[$savedSelectedPageIdTag] = $cookie;
	}

	$this->renderPartial('../wallbin/library', array(
		'library' => $library,
		'pageSelectorMode' => $previewInfo->pageSelectorType,
		'pageViewType' => $previewInfo->pageViewType,
		'showLogo' => $previewInfo->showLogo,
		'headerBorderColor' => 999
	));
?>