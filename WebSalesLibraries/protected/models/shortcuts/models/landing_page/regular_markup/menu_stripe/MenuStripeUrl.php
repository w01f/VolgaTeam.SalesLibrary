<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	/**
	 * Class MenuStripeUrl
	 */
	class MenuStripeUrl extends MenuStripeItem
	{
		public $url;

		/**
		 * @param $parentMenu IParentMenu
		 */
		public function __construct($parentMenu)
		{
			parent::__construct($parentMenu);
			$this->type = 'url';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			if ($this->isAccessGranted)
			{
				$queryResult = $xpath->query('./Url', $contextNode);
				$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			}
		}
	}