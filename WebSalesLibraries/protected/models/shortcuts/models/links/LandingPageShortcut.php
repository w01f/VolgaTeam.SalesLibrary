<?
	/**
	 * Class LandingPageShortcut
	 */
	class LandingPageShortcut extends ContainerShortcut implements ISearchBarContainer
	{
		/** @var  \application\models\shortcuts\models\landing_page\regular_markup\MarkupSettings */
		public $markupSettings;

		/** @var  \application\models\shortcuts\models\landing_page\mobile_items\MobileSettings */
		public $mobileSettings;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			if ($this->isPhone)
			{
				$queryResult = $xpath->query('//Config/MobileSettings');
				if ($queryResult->length > 0)
					$this->mobileSettings = \application\models\shortcuts\models\landing_page\mobile_items\MobileSettings::fromXml($this, $xpath, $queryResult->item(0));
			}
			else
			{
				$queryResult = $xpath->query('//Config/MarkupSettings');
				if ($queryResult->length > 0)
					$this->markupSettings = \application\models\shortcuts\models\landing_page\regular_markup\MarkupSettings::fromXml($this, $xpath, $queryResult->item(0));
			}
		}

		/** @return string */
		public function getTypeForActivityTracker()
		{
			return 'Landing page';
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('show-search', $actionsByKey))
				$actionsByKey['show-search']->enabled = $actionsByKey['show-search']->enabled && $this->searchBar->configured;
			if (array_key_exists('hide-search', $actionsByKey))
				$actionsByKey['hide-search']->enabled = $actionsByKey['hide-search']->enabled && $this->searchBar->configured;
		}
	}