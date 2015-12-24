<?

	/**
	 * Class FavoritesShortcut
	 */
	class FavoritesShortcut extends PageContentShortcut
	{
		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
		}

		/**
		 * @return array
		 */
		public function getViewParameters()
		{
			$viewParameters = parent::getViewParameters();
			$userId = Yii::app()->user->getId();
			if ($this->isPhone)
			{
				$viewParameters['folders'] = FavoritesFolderRecord::getChildFolders($userId, null);
				$viewParameters['links'] = FavoritesLinkRecord::getLinksByFolder($userId, null, false, 'name', 'asc');

			}
			else
			{
				$rootFolder = FavoritesFolderRecord::getRootFolder($userId);
				$viewParameters['rootFolder'] = $rootFolder;
			}
			return $viewParameters;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Favorites';
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}
	}