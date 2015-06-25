<?

	/**
	 * Class SinglePageShortcut
	 */
	class SinglePageShortcut extends PageShortcut
	{
		public $pageViewType;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);

			$version = Yii::app()->cacheDB->get('siteVersion');
			$this->viewPath = Yii::app()->browser->isMobile() && isset($version) && $version == 'mobile' ? 'pageLink' : 'singlePageLink';

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$pageTypeTags = $linkConfig->getElementsByTagName("PageViewType");
			$this->pageViewType = $pageTypeTags->length > 0 ? trim($pageTypeTags->item(0)->nodeValue) : 'columns';
		}
	}
