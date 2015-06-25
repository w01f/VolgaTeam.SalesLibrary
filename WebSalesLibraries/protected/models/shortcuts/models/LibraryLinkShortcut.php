<?php

	/**
	 * Class LibraryLinkShortcut
	 */
	class LibraryLinkShortcut extends BaseShortcut
	{
		public $linkId;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = 'libraryFileLink';

			$libraryName = trim($linkConfig->getElementsByTagName("Library")->item(0)->nodeValue);
			$pageName = trim($linkConfig->getElementsByTagName("Page")->item(0)->nodeValue);
			$windowName = trim($linkConfig->getElementsByTagName("Window")->item(0)->nodeValue);
			$fileName = trim($linkConfig->getElementsByTagName("File")->item(0)->nodeValue);
			if (isset($libraryName) && isset($pageName) && isset($windowName) && isset($fileName))
			{
				$linkRecord = Yii::app()->db->createCommand()
					->select("l.*")
					->from('tbl_link l')
					->join('tbl_folder f', 'f.id = l.id_folder')
					->join('tbl_page p', 'p.id = f.id_page')
					->join('tbl_library lb', 'lb.id = p.id_library')
					->where("l.file_name='" . $fileName . "' and f.name='" . $windowName . "' and p.name='" . $pageName . "' and lb.name='" . $libraryName . "'")
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
		public function getServiceData()
		{
			$result = '';
			$result .= '<div class="link-id">' . $this->linkId . '</div>';
			$result .= '<div class="mobile-header">' . $this->mobileHeader . '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => 'Shortcut File Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			return $result;
		}
	}