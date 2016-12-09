<?
	namespace application\models\wallbin\models\web;
	use application\models\wallbin\models\web\style\WallbinPageStyle;

	/**
	 * Class LibraryPage
	 */
	class LibraryPage
	{
		public $parent;
		public $id;
		public $libraryId;
		public $name;
		public $libraryName;
		public $order;

		/** @var  LibraryPageSettings */
		public $settings;
		/**
		 * @var LibraryFolder[]
		 */
		public $folders;
		public $enableColumns;
		/**
		 * @var Column[]
		 */
		public $columns;
		public $dateModify;
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
		 * @param $pageRecord \LibraryPageRecord
		 */
		public function load($pageRecord)
		{
			$this->id = $pageRecord->id;
			$this->libraryId = $pageRecord->id_library;
			$this->name = $pageRecord->name;
			$this->order = $pageRecord->order;
			$this->settings = isset($pageRecord->settings) ? \CJSON::decode($pageRecord->settings, false) : new LibraryPageSettings();
			$this->enableColumns = $pageRecord->has_columns;
			$logoPath = \Yii::app()->params['librariesRoot'] . "/Graphics/" . $this->parent->name . "/page" . strval($this->order + 1) . ".png";
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

			$folderRecords = \FolderRecord::model()->model()->findAll(array('order' => 'column_order asc, row_order asc, name asc', 'condition' => 'id_page=:id_page', 'params' => array(':id_page' => $this->id)));
			$maxColumn0Row = 0;
			$maxColumn1Row = 0;
			$maxColumn2Row = 0;
			foreach ($folderRecords as $folderRecord)
			{
				$folder = new LibraryFolder($this);
				$folder->load($folderRecord);
				switch ($folderRecord->column_order)
				{
					case 0:
						$folder->rowOrder = $maxColumn0Row;
						$maxColumn0Row++;
						break;
					case 1:
						$folder->rowOrder = $maxColumn1Row;
						$maxColumn1Row++;
						break;
					case 2:
						$folder->rowOrder = $maxColumn2Row;
						$maxColumn2Row++;
						break;
				}
				if ($folder->headerHeight > $minFolderHeaderHeight)
					$minFolderHeaderHeight = $folder->headerHeight;
				$this->folders[] = $folder;
			}
			foreach ($this->folders as $folder)
				$folder->headerHeight = $minFolderHeaderHeight;
			usort($this->folders, "application\\models\\wallbin\\models\\web\\LibraryFolder::libraryFolderComparer");

			unset($this->columns);
			foreach (\ColumnRecord::model()->findAll('id_page=?', array($this->id)) as $columnRecord)
			{
				$column = new Column($this);
				$column->load($columnRecord);
				$this->columns[] = $column;
			}

			if (isset($this->columns))
				usort($this->columns, "application\\models\\wallbin\\models\\web\\Column::columnComparer");
		}

		/**
		 * @param $usePermissionsFilter boolean
		 */
		public function loadFolders($usePermissionsFilter)
		{
			if (isset($this->folders))
				foreach ($this->folders as $folder)
					$folder->loadFiles($usePermissionsFilter);
		}

		/**
		 * @param $controller \CController
		 */
		public function buildCache($controller)
		{
			$this->loadData();
			$this->loadFolders(false);
			$path = \Yii::getPathOfAlias('application.views.regular.wallbin') . '/columnsView.php';
			$style = WallbinPageStyle::createDefault();
			$content = $controller->renderFile($path, array('libraryPage' => $this, 'style' => $style), true);

			if (isset($content) && $content != '')
			{
				/** @var $cacheRecord \LibraryPageRecord */
				$cacheRecord = \LibraryPageRecord::model()->findByPk($this->id);
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
			/** @var  $cacheRecord \LibraryPageRecord */
			$cacheRecord = \LibraryPageRecord::model()->findByPk($this->id);
			$cachedPageContent = $cacheRecord->cached_col_view;
			$cachedPage = \phpQuery::newDocument($cachedPageContent);
			$linkTags = $cachedPage['.link-container.restricted'];

			$useFilterByUser = \UserIdentity::isUserAuthorized() && !\UserIdentity::isUserAdmin();
			if ($useFilterByUser)
			{
				$userId = \UserIdentity::getCurrentUserId();
				foreach ($linkTags as $linkTag)
				{
					$linkId = str_replace('link', '', pq($linkTag)->attr('id'));
					$allowLink = false;
					$isWhiteListEnabled = count(\LinkWhiteListRecord::getUserIds($linkId)) > 0;
					if ($isWhiteListEnabled)
					{
						$availableLinkIds = \LinkWhiteListRecord::getAvailableLinks($userId);
						$allowLink = in_array($linkId, $availableLinkIds);
					}
					$isBlackListEnabled = count(\LinkBlackListRecord::getUserIds($linkId)) > 0;
					if ($isBlackListEnabled)
					{
						$availableLinkIds = \LinkBlackListRecord::getDeniedLinks($userId);
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
