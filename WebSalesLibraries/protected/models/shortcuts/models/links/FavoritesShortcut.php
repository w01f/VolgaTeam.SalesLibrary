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
			$userId = UserIdentity::getCurrentUserId();
			if ($this->isPhone)
			{
				$viewParameters['folders'] = FavoritesFolderRecord::getChildFolders($userId, null);
				$viewParameters['links'] = FavoritesLinkRecord::getLinksByFolder($userId, null);

			}
			else
			{
				$rootFolder = FavoritesFolderRecord::getRootFolder($userId);
				$viewParameters['rootFolder'] = $rootFolder;
				$viewParameters['selectedFolderId'] = $this->getSelectedFolderId();
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
			$data['selectedFolderId'] = $this->getSelectedFolderId();
			return $data;
		}

		/**
		 * @return string
		 */
		private function getSelectedFolderId()
		{
			$selectedFolderTagName = 'favorites-selected-folder-name';
			$selectedFolderName = isset(Yii::app()->request->cookies[$selectedFolderTagName]) ?
				Yii::app()->request->cookies[$selectedFolderTagName]->value :
				null;
			if (isset($selectedFolderName))
			{
				$userId = UserIdentity::getCurrentUserId();
				$selectedFolder = FavoritesFolderRecord::getFolderByName($userId, $selectedFolderName);
				return $selectedFolder->id;
			}
			return null;
		}
	}