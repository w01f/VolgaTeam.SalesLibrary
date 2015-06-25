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

			$version = Yii::app()->cacheDB->get('siteVersion');
			$this->viewPath = Yii::app()->browser->isMobile() && isset($version) && $version == 'mobile' ? 'pageLink' : 'libraryPageLink';
		}
	}
