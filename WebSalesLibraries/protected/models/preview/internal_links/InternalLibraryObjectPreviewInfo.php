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
				$linkRecord = LinkRecord::getLinkByName($libraryName, $pageName, $windowName, $linkName);
				if (isset($linkRecord))
					$this->libraryLinkId = $linkRecord->id;
			}

		}
	}