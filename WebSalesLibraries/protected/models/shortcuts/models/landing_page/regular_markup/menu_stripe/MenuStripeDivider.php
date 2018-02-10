<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class MenuStripeDivider
	 */
	class MenuStripeDivider extends MenuStripeItem
	{
		/**
		 * @param $parentMenu IParentMenu
		 */
		public function __construct($parentMenu)
		{
			parent::__construct($parentMenu);
			$this->type = 'divider';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
			else
				$this->textAppearance = TextAppearance::createEmpty();
		}
	}