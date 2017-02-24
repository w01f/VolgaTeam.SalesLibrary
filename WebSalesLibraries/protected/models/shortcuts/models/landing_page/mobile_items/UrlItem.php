<?
	namespace application\models\shortcuts\models\landing_page\mobile_items;

	/** Class UrlItem */
	class UrlItem extends BaseMobileItem
	{
		public $url;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 */
		public function __construct($parentShortcut)
		{
			parent::__construct($parentShortcut);
			$this->type = 'url';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Url', $contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		public function getSourceLink()
		{
			return $this->url;
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			return null;
		}
	}