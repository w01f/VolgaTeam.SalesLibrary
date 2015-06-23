<?

	/**
	 * Class LibraryTab
	 */
	class LibraryTab extends UserTabModel
	{
		/**
		 * @param $userTabRecord UserTabRecord
		 */
		public function __construct($userTabRecord)
		{
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($userTabRecord->id_object);

			$this->name = $library->alias;
			$this->order = $userTabRecord->order;

			$this->iconUrl = Yii::app()->getBaseUrl(true) . '/images/jqm-icons/stations.png';
			$this->contentUrl = Yii::app()->createAbsoluteUrl('wallbin/getLibraryPage', array('libraryId' => $library->id));
		}
	}