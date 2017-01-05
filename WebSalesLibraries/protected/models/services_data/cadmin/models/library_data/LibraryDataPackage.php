<?
	namespace application\models\services_data\cadmin\models\library_data;

	use application\models\wallbin\models\cadmin\entities\Library;
	use application\models\wallbin\models\cadmin\entities\LibraryPage;

	/**
	 * Class LibraryData
	 */
	class LibraryDataPackage
	{
		/** @var  $library Library */
		public $library;
		/** @var  $pages LibraryPage */
		public $pages;
		public $columns;
		public $folders;
		public $links;
		public $previewContainers;

		/**
		 * @param $libraryId string
		 * @return LibraryDataPackage
		 */
		public static function get($libraryId)
		{
			$dataPackage = new LibraryDataPackage();

			$dataPackage->library = Library::get($libraryId);
			if (!isset($dataPackage->library))
				return null;

			$dataPackage->columns = array();
			$dataPackage->pages = array();
			$dataPackage->folders = array();
			$dataPackage->links = array();
			$dataPackage->previewContainers = array();

			/** @var $pageRecords \LibraryPageRecord[] */
			$pageRecords = \LibraryPageRecord::model()->findAll("id_library=?", array($libraryId));
			foreach ($pageRecords as $pageRecord)
			{
				$pageData = LibraryPage::fromPageRecord($pageRecord);
				$dataPackage->pages[] = $pageData;

				/** @var $folderRecords \FolderRecord[] */
				$folderRecords = \FolderRecord::model()->findAll('id_library=? and id_page=?', array($libraryId, $pageRecord->id));
				foreach ($folderRecords as $folderRecord)
				{
					//$folderData
				}
			}
			return $dataPackage;
		}
	}