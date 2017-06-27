<?
	use application\models\wallbin\models\web\LibraryFolder;
	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager;
	use application\models\wallbin\models\web\LibraryPage;

	/**
	 * Class ParentLinkBundleInfo
	 */
	class ParentLinkBundleInfo
	{
		/** @var  FileDownloadInfo[] */
		public $downloadInfo;

		/**
		 * @param $linkBundleId string
		 * @return ParentLinkBundleInfo
		 */
		public static function fromLinkId($linkBundleId)
		{
			$instance = new self();

			$linkRecord = LinkRecord::getLinkById($linkBundleId);
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($linkRecord->id_library);
			$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
			$link->load($linkRecord);
			if ($link->isLinkBundle)
			{
				$instance->downloadInfo = FileDownloadInfo::getLinkBundleDownloadInfo($link);
			}

			return $instance;
		}
	}