<?php
	class LibraryPage
	{
		public $parent;
		/**
		 * @var string name
		 * @soap
		 */
		public $id;
		/**
		 * @var string name
		 * @soap
		 */
		public $libraryId;
		/**
		 * @var string name
		 * @soap
		 */
		public $name;
		/**
		 * @var string libraryName
		 * @soap
		 */
		public $libraryName;
		/**
		 * @var int order
		 * @soap
		 */
		public $order;
		public $logoPath;
		public $logoLink;
		/**
		 * @var LibraryFolder[]
		 * @soap
		 */
		public $folders;
		/**
		 * @var boolean enableColumns
		 * @soap
		 */
		public $enableColumns;
		/**
		 * @var Column[]
		 * @soap
		 */
		public $columns;
		/**
		 * @var string
		 * @soap
		 */
		public $dateModify;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $selected;
		/**
		 * @var GroupRecord[]
		 * @soap
		 */
		public $groups;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allGroups;
		/**
		 * @var UserRecord[]
		 * @soap
		 */
		public $users;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allUsers;

		public $cachedColumnsView;

		public function __construct($library)
		{
			$this->parent = $library;
		}

		public function load($pageRecord)
		{
			$this->id = $pageRecord->id;
			$this->libraryId = $pageRecord->id_library;
			$this->name = $pageRecord->name;
			$this->order = $pageRecord->order;
			$this->enableColumns = $pageRecord->has_columns;

			$logoPath = Yii::app()->params['librariesRoot'] . "/Graphics/" . $this->parent->name . "/page" . strval($this->order + 1) . ".png";
			if (file_exists($logoPath))
			{
				$this->logoPath = $logoPath;
				$this->logoLink = str_replace(' ', '%20', htmlspecialchars($logoPath));
			}
			else
			{
				$this->logoPath = $this->parent->logoPath;
				$this->logoLink = str_replace(' ', '%20', htmlspecialchars($this->parent->logoPath));
			}
		}

		public function loadData()
		{
			unset($this->folders);
			foreach (FolderStorage::model()->findAll('id_page=?', array($this->id)) as $folderRecord)
			{
				$folder = new LibraryFolder($this);
				$folder->load($folderRecord);
				$this->folders[] = $folder;
			}
			if (isset($this->folders))
				usort($this->folders, "LibraryFolder::libraryFolderComparer");

			unset($this->columns);
			foreach (ColumnStorage::model()->findAll('id_page=?', array($this->id)) as $columnRecord)
			{
				$column = new Column($this);
				$column->load($columnRecord);
				$this->columns[] = $column;
			}

			if (isset($this->columns))
				usort($this->columns, "Column::columnComparer");
		}

		public function loadFolders($allLinks, $userId)
		{
			if (isset($this->folders))
				foreach ($this->folders as $folder)
					$folder->loadFiles($allLinks, $userId);
		}

		public function buildCache($controller)
		{
			$this->buildCacheForUser($controller, null, false);

			$adminIds = UserStorage::getAdminUserIds();
			if (isset($adminIds))
				foreach ($adminIds as $userId)
				{
					UserPageCacheStorage::updateData($userId, $this->id, $this->libraryId);
					$this->buildCacheForUser($controller, $userId, true);
				}

			$restrictedUserIds = UserLinkStorage::getRestrictedUsersIds($this->libraryId);
			if (isset($restrictedUserIds))
				foreach ($restrictedUserIds as $userId)
				{
					if (!isset($adminIds) || (isset($adminIds) && !in_array($userId, $adminIds)))
					{
						UserPageCacheStorage::updateData($userId, $this->id, $this->libraryId);
						$this->buildCacheForUser($controller, $userId, false);
					}
				}
		}

		private function buildCacheForUser($controller, $userId, $isAdmin)
		{
			$this->loadData();
			$this->loadFolders($isAdmin, $userId);

			$path = Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsPage.php';
			$content = $controller->renderFile($path, array('libraryPage' => $this), true);

			if (isset($content))
			{
				if ($content != '')
				{
					if (isset($userId))
					{
						$cacheRecord = UserPageCacheStorage::getPageCache($userId, $this->id);
					}
					else
					{
						$cacheRecord = LibraryPageStorage::model()->findByPk($this->id);
					}
					if (isset($cacheRecord))
					{
						$cacheRecord->cached_col_view = $content;
						$cacheRecord->save();
						unset($content);
						return true;
					}
				}
			}
			return false;
		}

		public function getCache()
		{
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset($userId))
					$cacheRecord = UserPageCacheStorage::getPageCache($userId, $this->id);
			}
			if (!isset($cacheRecord))
				$cacheRecord = LibraryPageStorage::model()->findByPk($this->id);
			if (isset($cacheRecord))
				return $cacheRecord->cached_col_view;
			return null;
		}

		public function getFoldersByColumn($columnOrder)
		{
			if (isset($this->folders))
				foreach ($this->folders as $folder)
				{
					if ($folder->columnOrder == $columnOrder)
						$columnFolders[] = $folder;
				}
			if (isset($columnFolders))
				return $columnFolders;
			else
				return null;
		}

		public static function libraryPageComparer($x, $y)
		{
			if ($x->order == $y->order)
				return 0;
			else
				return ($x->order < $y->order) ? -1 : 1;
		}
	}
