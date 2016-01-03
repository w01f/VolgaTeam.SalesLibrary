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
		public $logoContent;

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
				$this->logoPath = $logoPath;
			else
				$this->logoPath = $this->parent->logoPath;
			if (isset($this->logoPath))
				$this->logoContent = 'data:image/png;base64,' . base64_encode(file_get_contents($this->logoPath));
			else
				$this->logoContent = '//:0';
		}

		public function loadData()
		{
			unset($this->folders);
			$this->folders = array();
			$minFolderHeaderHeight = 0;
			foreach (FolderRecord::model()->findAll('id_page=?', array($this->id)) as $folderRecord)
			{
				$folder = new LibraryFolder($this);
				$folder->load($folderRecord);
				if ($folder->headerHeight > $minFolderHeaderHeight)
					$minFolderHeaderHeight = $folder->headerHeight;
				$this->folders[] = $folder;
			}
			foreach ($this->folders as $folder)
				$folder->headerHeight = $minFolderHeaderHeight;
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

		public function loadFolders()
		{
			if (isset($this->folders))
				foreach ($this->folders as $folder)
					$folder->loadFiles(false);
		}

		/**
		 * @param $controller IsdController
		 */
		public function buildCache($controller)
		{
			$this->loadData();
			$this->loadFolders();
			$path = Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsView.php';
			$content = $controller->renderFile($path, array('libraryPage' => $this), true);
			if (isset($content) && $content != '')
			{
				/** @var $cacheRecord LibraryPageRecord */
				$cacheRecord = LibraryPageRecord::model()->findByPk($this->id);
				if (isset($cacheRecord))
				{
					$cacheRecord->cached_col_view = $content;
					$cacheRecord->save();
					unset($content);
				}
			}
		}

		/**
		 * @return string
		 */
		public function getCache()
		{
			/** @var  $cacheRecord LibraryPageRecord */
			$cacheRecord = LibraryPageRecord::model()->findByPk($this->id);
			$cachedPageContent = $cacheRecord->cached_col_view;
			$cachedPage = phpQuery::newDocument($cachedPageContent);
			$linkTags = $cachedPage['.link-container.restricted'];

			$isAdmin =UserIdentity::isUserAdmin();
			$userId = UserIdentity::getCurrentUserId();
			if (!$isAdmin)
			{
				foreach ($linkTags as $linkTag)
				{
					$linkId = str_replace('link', '', pq($linkTag)->attr('id'));
					$allowLink = false;
					$isWhiteListEnabled = count(LinkWhiteListRecord::getUserIds($linkId)) > 0;
					if ($isWhiteListEnabled)
					{
						$availableLinkIds = LinkWhiteListRecord::getAvailableLinks($userId);
						$allowLink = in_array($linkId, $availableLinkIds);
					}
					$isBlackListEnabled = count(LinkBlackListRecord::getUserIds($linkId)) > 0;
					if ($isBlackListEnabled)
					{
						$availableLinkIds = LinkBlackListRecord::getDeniedLinks($userId);
						$allowLink = !in_array($linkId, $availableLinkIds);
					}
					if ($allowLink)
						pq($linkTag)->removeClass('restricted');
				}
			}
			else
				pq($linkTags)->removeClass('restricted');
			$filteredContent = $cachedPage->htmlOuter();
			return $filteredContent;
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
