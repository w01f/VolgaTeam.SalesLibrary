<?php

	/**
	 * Class LibraryPage
	 */
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
		 * @var GroupModel[]
		 * @soap
		 */
		public $groups;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allGroups;
		/**
		 * @var UserModel[]
		 * @soap
		 */
		public $users;
		/**
		 * @var boolean selected
		 * @soap
		 */
		public $allUsers;
		public $cachedColumnsView;
		public $logoPath;
		public $logoLink;

		/**
		 * @param $library
		 */
		public function __construct($library)
		{
			$this->parent = $library;
		}

		/**
		 * @param $pageRecord
		 */
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
			foreach (FolderRecord::model()->findAll('id_page=?', array($this->id)) as $folderRecord)
			{
				$folder = new LibraryFolder($this);
				$folder->load($folderRecord);
				$this->folders[] = $folder;
			}
			if (isset($this->folders))
				usort($this->folders, "LibraryFolder::libraryFolderComparer");

			unset($this->columns);
			foreach (ColumnRecord::model()->findAll('id_page=?', array($this->id)) as $columnRecord)
			{
				$column = new Column($this);
				$column->load($columnRecord);
				$this->columns[] = $column;
			}

			if (isset($this->columns))
				usort($this->columns, "Column::columnComparer");
		}

		/**
		 * @param $allLinks
		 * @param $userId
		 */
		public function loadFolders($allLinks, $userId)
		{
			if (isset($this->folders))
				foreach ($this->folders as $folder)
					$folder->loadFiles($allLinks, $userId);
		}

		/**
		 * @param $controller
		 */
		public function buildCache($controller)
		{
			$this->buildCacheForUser($controller, null, false);

			$adminIds = UserRecord::getAdminUserIds();
			if (isset($adminIds))
				foreach ($adminIds as $userId)
				{
					UserPageCacheRecord::updateData($userId, $this->id, $this->libraryId);
					$this->buildCacheForUser($controller, $userId, true);
				}

			$restrictedUserIds = UserLinkRecord::getRestrictedUsersIds($this->libraryId);
			if (isset($restrictedUserIds))
				foreach ($restrictedUserIds as $userId)
				{
					if (!isset($adminIds) || (isset($adminIds) && !in_array($userId, $adminIds)))
					{
						UserPageCacheRecord::updateData($userId, $this->id, $this->libraryId);
						$this->buildCacheForUser($controller, $userId, false);
					}
				}
		}

		/**
		 * @param $controller CController
		 * @param $userId
		 * @param $isAdmin
		 * @return bool
		 */
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
						$cacheRecord = UserPageCacheRecord::getPageCache($userId, $this->id);
					}
					else
					{
						$cacheRecord = LibraryPageRecord::model()->findByPk($this->id);
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

		/**
		 * @return mixed|null
		 */
		public function getCache()
		{
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset($userId))
					$cacheRecord = UserPageCacheRecord::getPageCache($userId, $this->id);
			}
			if (!isset($cacheRecord))
				$cacheRecord = LibraryPageRecord::model()->findByPk($this->id);
			if (isset($cacheRecord))
				return $cacheRecord->cached_col_view;
			return null;
		}

		/**
		 * @param $columnOrder
		 * @return LibraryFolder[]
		 */
		public function getFoldersByColumn($columnOrder)
		{
			$columnFolders = array();
			if (isset($this->folders))
				foreach ($this->folders as $folder)
				{
					if ($folder->columnOrder == $columnOrder)
						$columnFolders[] = $folder;
				}
			return $columnFolders;
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryPageComparer($x, $y)
		{
			if ($x->order == $y->order)
				return 0;
			else
				return ($x->order < $y->order) ? -1 : 1;
		}
	}
