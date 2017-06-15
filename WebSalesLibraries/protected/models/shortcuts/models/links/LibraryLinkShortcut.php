<?php

	/**
	 * Class LibraryLinkShortcut
	 */
	class LibraryLinkShortcut extends CustomHandledShortcut
	{
		public $linkId;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$libraryName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue));
			$pageName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue));
			$windowName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue));
			$fileName = str_replace("'", "''", trim($linkConfig->getElementsByTagName("File")->item(0)->nodeValue));
			if (isset($libraryName) && isset($pageName) && isset($windowName) && isset($fileName))
			{
				$linkRecord = Yii::app()->db->createCommand()
					->select("l.*")
					->from('tbl_link l')
					->join('tbl_folder f', 'f.id = l.id_folder')
					->join('tbl_page p', 'p.id = f.id_page')
					->join('tbl_library lb', 'lb.id = p.id_library')
					->where("(trim(l.file_name)='" . $fileName . "' or trim(l.name)='" . $fileName . "') and trim(f.name)='" . $windowName . "' and trim(p.name)='" . $pageName . "' and trim(lb.name)='" . $libraryName . "'")
					->queryRow();
				if ($linkRecord != false)
				{
					$linkRecord = (object)$linkRecord;
					$this->linkId = $linkRecord->id;
				}
			}
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			$result = parent::getMenuItemData();
			$result .= '<div class="library-link-id">' . $this->linkId . '</div>';
			$result .= '<div class="link-header">' . ($this->headerTitle != '' ? $this->headerTitle : $this->title) . '</div>';
			return $result;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Libraryfile';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}
	}