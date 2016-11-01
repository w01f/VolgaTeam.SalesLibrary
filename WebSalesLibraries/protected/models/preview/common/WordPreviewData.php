<?
	use application\models\wallbin\models\web\LibraryLink as LibraryLink;

	/**
	 * Class WordPreviewData
	 */
	class WordPreviewData extends DocumentPreviewData
	{
		/**
		 * @param $link LibraryLink
		 */
		public function __construct($link)
		{
			parent::__construct($link);
			$this->linkTitle = 'Word';
		}
	}