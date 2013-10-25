<?php
	class WindowShortcut
	{
		public $type;
		public $title;
		public $imagePath;
		public $sourceLink;
		public $samePage;

		private $linkRecord;

		public function __construct($linkRecord)
		{
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getWindowShortcut', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
		}

		public function getWindow()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			$windowName = trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue);
			if (!(isset($libraryName) && isset($pageName) && isset($windowName))) return null;
			$windowRecord = (object)Yii::app()->db->createCommand()
				->select("f.*")
				->from('tbl_folder f')
				->join('tbl_page p', 'p.id = f.id_page')
				->join('tbl_library l', 'l.id = p.id_library')
				->where("f.name='" . $windowName . "' and p.name='" . $pageName . "' and l.name='" . $libraryName . "'")
				->queryRow();
			if (!isset($windowRecord)) return null;
			$libraryManager = new LibraryManager();
			$library = $libraryManager->getLibraryById($windowRecord->id_library);
			$folder = new LibraryFolder(new LibraryPage($library));
			$folder->load($windowRecord);
			$isAdmin = false;
			$userId = null;
			if (isset(Yii::app()->user))
			{
				$userId = Yii::app()->user->getId();
				if (isset(Yii::app()->user->role))
					$isAdmin = Yii::app()->user->role == 2;
				else
					$isAdmin = true;
			}
			$folder->loadFiles($isAdmin, $userId);
			return $folder;
		}
	}