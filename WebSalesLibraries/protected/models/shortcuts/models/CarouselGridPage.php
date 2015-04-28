<?

	/**
	 * Class GridPage
	 */
	class CarouselGridPage extends GridPage
	{
		public $allowSwitchView;

		/**
		 * @param $pageRecord ShortcutsPageRecord
		 */
		public function __construct($pageRecord)
		{
			parent::__construct($pageRecord);

			$config = new DOMDocument();
			$config->loadXML($pageRecord->config);
			$xpath = new DomXPath($config);

			$allowSwitchViewTags = $xpath->query('//Config/TileToggle');
			$this->allowSwitchView = $allowSwitchViewTags->length > 0 ? filter_var(trim($allowSwitchViewTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}

		/**
		 * @param $pageRecord ShortcutsPageRecord
		 * @return ShortcutsLinkRecord[]
		 */
		public function getPageLinkRecords($pageRecord)
		{
			$links = array();
			$parentLinks = parent::getPageLinkRecords($pageRecord);
			foreach ($parentLinks as $parentLink)
				$links = array_merge($links, $parentLink->getSubLinks());
			return $links;
		}
	}