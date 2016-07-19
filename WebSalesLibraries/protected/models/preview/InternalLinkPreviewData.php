<?
	use application\models\wallbin\models\web\LibraryManager as LibraryManager;
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class InternalLinkPreviewData
	 */
	class InternalLinkPreviewData extends PreviewData
	{
		public $libraryId;
		public $pageId;
		public $libraryLinkId;
		public $forcePreview;
		public $runLinkPreview;

		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);

			$this->viewerFormat = 'internal';
			$this->contentView = 'internalLinkViewer';

			/** @var  $linkSettings InternalLinkSettings */
			$linkSettings = $link->extendedProperties;

			$libraryName = $linkSettings->libraryName;
			$pageName = $linkSettings->pageName;
			$windowName = $linkSettings->windowName;
			$linkName = $linkSettings->linkName;

			$this->forcePreview = $linkSettings->forcePreview;
			$this->runLinkPreview = false;

			if (isset($libraryName) && isset($pageName))
			{
				$libraryManager = new LibraryManager();
				$library = $libraryManager->getLibraryByName($libraryName);
				$this->libraryId = $library->id;

				$libraryPageRecord = Yii::app()->db->createCommand()
					->select("p.*")
					->from('tbl_page p')
					->join('tbl_library l', 'l.id = p.id_library')
					->where("p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
					->queryRow();
				if (is_array($libraryPageRecord))
				{
					$libraryPageRecord = (object)$libraryPageRecord;
					$this->pageId = $libraryPageRecord->id;

					if(!$this->forcePreview)
					{
						$savedSelectedPageIdTag = sprintf('SelectedLibraryPageId-%s', $library->id);
						$cookie = new CHttpCookie($savedSelectedPageIdTag, $this->pageId);
						$cookie->expire = time() + (60 * 60 * 24 * 7);
						Yii::app()->request->cookies[$savedSelectedPageIdTag] = $cookie;
					}
				}

				if (isset($linkName) && isset($windowName))
				{
					$this->runLinkPreview = true;
					$linkRecord = Yii::app()->db->createCommand()
						->select("l.*")
						->from('tbl_link l')
						->join('tbl_folder f', 'f.id = l.id_folder')
						->join('tbl_page p', 'p.id = f.id_page')
						->join('tbl_library lb', 'lb.id = p.id_library')
						->where("l.name='" . $linkName . "' and f.name='" . $windowName . "' and p.name='" . $pageName . "' and lb.name='" . $libraryName . "'")
						->queryRow();
					if ($linkRecord != false)
					{
						$linkRecord = (object)$linkRecord;
						$this->libraryLinkId = $linkRecord->id;
					}
				}
			}
		}

		protected function initActions()
		{
			$this->actions = array();
		}
	}