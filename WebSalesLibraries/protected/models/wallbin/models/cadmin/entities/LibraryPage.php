<?
	namespace application\models\wallbin\models\cadmin\entities;
	use application\models\wallbin\models\cadmin\settings\LibraryPageSettings as LibraryPageSettings;

	/**
	 * Class LibraryPage
	 */
	class LibraryPage extends VersionedObject
	{
		public $libraryId;
		public $name;
		public $order;
		/** @var  LibraryPageSettings */
		public $settings;
		public $lastModified;

		/**
		 * @param $pageRecord \LibraryPageRecord
		 * @return LibraryPage
		 */
		public static function fromPageRecord($pageRecord)
		{
			$pageInfo = new LibraryPage();

			$pageInfo->id = $pageRecord->id;
			$pageInfo->libraryId = $pageRecord->id_library;
			$pageInfo->name = $pageRecord->name;
			$pageInfo->order = $pageRecord->order;
			$pageInfo->lastModified = $pageRecord->date_modify;

			if (isset($pageRecord->settings))
				$pageInfo->settings = \CJSON::decode($pageRecord->settings, false);
			else
			{
				$pageSettings = new LibraryPageSettings();
				$pageSettings->enableColumnTitles = $pageRecord->has_columns == "1";
				$pageSettings->applyForAllColumnTitles = false;
				$pageInfo->settings = $pageSettings;
			}
			return $pageInfo;
		}
	}