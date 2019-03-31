<?

	use application\models\shortcuts\models\bundle_modal_dialog\TabItemContainer;

	/**
	 * Class BundleModalShortcut
	 */
	class BundleModalDialogShortcut extends BaseShortcut
	{
		public $textColor;
		public $textSize;

		/** @var \application\models\shortcuts\models\bundle_modal_dialog\LeftPanelContainer */
		public $leftPanel;

		/** @var \application\models\shortcuts\models\bundle_modal_dialog\FavoritesPageContainer */
		public $favoritesPage;

		/** @var TabItemContainer[] */
		public $tabPages;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
		}

		public function loadConfig()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/TextSize');
			$this->textSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 10;

			$queryResult = $xpath->query('//Config/TextColor');
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'ffffff';

			$queryResult = $xpath->query('//Config/LeftPanel');
			if ($queryResult->length > 0)
				$this->leftPanel = new \application\models\shortcuts\models\bundle_modal_dialog\LeftPanelContainer($xpath, $queryResult->item(0), $this);

			$queryResult = $xpath->query('//Config/FavoritesPage');
			if ($queryResult->length > 0)
				$this->favoritesPage = new \application\models\shortcuts\models\bundle_modal_dialog\FavoritesPageContainer($xpath, $queryResult->item(0), $this);

			$this->tabPages = array();
			$queryResult = $xpath->query('//Config/TabControl/TabPage');
			foreach ($queryResult as $tabPageNode)
				$this->tabPages[] = new TabItemContainer($xpath, $tabPageNode, $this);
		}

		public function loadFavoritesConfig()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/FavoritesPage');
			if ($queryResult->length > 0)
				$this->favoritesPage = new \application\models\shortcuts\models\bundle_modal_dialog\FavoritesPageContainer($xpath, $queryResult->item(0), $this);
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Bundle Modal';
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return '#';
		}

		/**
		 * @return array
		 */
		public function getOptions()
		{
			$options = array();
			return $options;
		}
	}