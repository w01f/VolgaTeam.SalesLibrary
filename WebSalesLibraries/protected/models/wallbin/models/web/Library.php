<?
	namespace application\models\wallbin\models\web;
	/**
	 * Class Library
	 */
	class Library
	{
		public $id;
		public $name;
		public $alias;
		public $groupId;
		public $order;
		public $storagePath;
		public $storageLink;
		public $logoPath;
		/** @var  @var LibraryPage pages */
		public $pages;
		public $autoWidgets;
		public $lastUpdate;

		public function load()
		{
			$isAdmin = \UserIdentity::isUserAdmin();
			if (!$isAdmin)
			{
				$userId = \UserIdentity::getCurrentUserId();
				$availablePageIds = \UserLibraryRecord::getPageIdsByUserAngHisGroups($userId);
			}
			else
				$availablePageIds = array();
			foreach (\LibraryPageRecord::model()->findAll('id_library=?', array($this->id)) as $pageRecord)
			{
				$page = new LibraryPage($this);
				$page->load($pageRecord);
				if (in_array($page->id, $availablePageIds) || $isAdmin)
					$this->pages[] = $page;
			}
			if (isset($this->pages))
				usort($this->pages, "application\\models\\wallbin\\models\\web\\LibraryPage::libraryPageComparer");

			foreach (\AutoWidgetRecord::model()->findAll('id_library=?', array($this->id)) as $autoWidgetRecord)
			{
				$autoWidget = new AutoWidget();
				$autoWidget->load($autoWidgetRecord);
				$this->autoWidgets[] = $autoWidget;
			}
		}

		/**
		 * @param $controller \CController
		 */
		public function buildCache($controller)
		{
			foreach ($this->pages as $page)
			{
				/** @var $page LibraryPage */
				$page->buildCache($controller);
			}
		}

		/**
		 * @param $extension
		 * @return string
		 */
		public function getAutoWidget($extension)
		{
			if (isset($this->autoWidgets))
				foreach ($this->autoWidgets as $autoWidget)
					if (str_replace('.', '', $autoWidget->extension) == $extension)
						return $autoWidget->widget;
			return null;
		}

		/**
		 * @param $pageId
		 * @return LibraryPage
		 */
		public function getPageById($pageId)
		{
			$pages = $this->pages;
			if (isset($pages))
				foreach ($pages as $page)
					if ($page->id == $pageId)
						return $page;
			return null;
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryComparerByName($x, $y)
		{
			if ($x->name == $y->name)
				return 0;
			else
				return ($x->name < $y->name) ? -1 : 1;
		}

		/**
		 * @param $x
		 * @param $y
		 * @return int
		 */
		public static function libraryComparerByGroup($x, $y)
		{
			if ($x->order == $y->order)
				return 0;
			else
				return ($x->order < $y->order) ? -1 : 1;
		}
	}