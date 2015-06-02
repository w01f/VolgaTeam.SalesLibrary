<?

	/**
	 * Class ExcelPreviewData
	 */
	class ExcelPreviewData extends FilePreviewData
	{
		/**
		 * @param $link LibraryLink
		 * @param $isQuickSite boolean
		 */
		public function __construct($link, $isQuickSite)
		{
			parent::__construct($link, $isQuickSite);
			$this->linkTitle = 'Excel File';
		}
	}