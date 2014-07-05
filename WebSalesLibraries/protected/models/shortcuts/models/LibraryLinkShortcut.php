<?php

	/**
	 * Class LibraryLinkShortcut
	 */
	class LibraryLinkShortcut
	{
		public $type;
		public $name;
		public $tooltip;
		public $imagePath;
		public $linkId;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;

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
	}