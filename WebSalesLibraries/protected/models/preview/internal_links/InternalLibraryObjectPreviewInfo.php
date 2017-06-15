<?

	/**
	 * Class InternalLibraryObjectPreviewInfo
	 */
	class InternalLibraryObjectPreviewInfo
	{
		public $internalLinkType;
		public $libraryLinkId;

		/**
		 * @param $linkSettings InternalLibraryObjectLinkSettings
		 */
		public function __construct($linkSettings)
		{
			$this->internalLinkType = $linkSettings->internalLinkType;

			$libraryName = str_replace("'", "''", $linkSettings->libraryName);
			$pageName = str_replace("'", "''", $linkSettings->pageName);
			$windowName = str_replace("'", "''", $linkSettings->windowName);
			$linkName = str_replace("'", "''", $linkSettings->linkName);

			if (isset($libraryName) && isset($pageName) && isset($linkName) && isset($windowName))
			{
				$linkRecord = Yii::app()->db->createCommand()
					->select("l.*")
					->from('tbl_link l')
					->join('tbl_folder f', 'f.id = l.id_folder')
					->join('tbl_page p', 'p.id = f.id_page')
					->join('tbl_library lb', 'lb.id = p.id_library')
					->where("(l.file_name='" . $linkName . "' or l.name='" . $linkName . "') and f.name='" . $windowName . "' and p.name='" . $pageName . "' and lb.name='" . $libraryName . "'")
					->queryRow();
				if ($linkRecord != false)
				{
					$linkRecord = (object)$linkRecord;
					$this->libraryLinkId = $linkRecord->id;
				}
			}

		}
	}