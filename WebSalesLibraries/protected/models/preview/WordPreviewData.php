<?

	/**
	 * Class WordPreviewData
	 */
	class WordPreviewData extends DocumentPreviewData
	{
		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->linkTitle = 'Word';
		}
	}