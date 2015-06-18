<?
	/**
	 * Class LibraryPageShortcut
	 */
	class LibraryPageShortcut extends PageShortcut
	{
		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$this->viewPath = Yii::app()->browser->isMobile() ? 'pageLink' : 'libraryPageLink';
		}
	}
