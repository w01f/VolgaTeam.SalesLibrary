<?

	/**
	 * Class NavigationPanelShortcut
	 */
	class NavigationPanelShortcut extends CustomHandledShortcut
	{
		public $navigationPanelId;
		public $expandPanelId;
		public $position;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);

			$this->enabled = $this->isPhone;

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/LeftPanelID');
			$this->navigationPanelId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//Config/ExpandPosition');
			$this->position = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : "left";

			$this->expandPanelId = sprintf('shortcut-group-popup-panel-%s', $this->id);
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return sprintf('#%s', $this->expandPanelId);
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Panel Menu Shortcut';
		}

		/**
		 * @return string
		 */
		public function getTitleForActivityTracker()
		{
			return 'Panel Menu ' . (!empty($this->title) ?
					$this->title :
					(!empty($this->headerTitle) ? $this->headerTitle :
						(!empty($this->description) ? $this->description : "Empty Title")));
		}

		/** @return NavigationPanel */
		public function getNavigationPanel()
		{
			return ShortcutsManager::getNavigationPanel($this->navigationPanelId, $this->isPhone);
		}
	}