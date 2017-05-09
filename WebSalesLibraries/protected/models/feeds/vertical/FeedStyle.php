<?

	namespace application\models\feeds\vertical;

	/**
	 * Class FeedStyle
	 */
	class FeedStyle
	{
		public $headerColor;
		public $footerColor;
		public $headerTextColor;
		public $headerIconColor;
		public $outsideBorderColor;
		public $dividerColor;
		public $buttonBackColor;
		public $buttonBorderColor;
		public $buttonIconColor;

		/** @var  \Padding */
		public $headerPadding;
		/** @var  \Padding */
		public $footerPadding;

		public function __construct()
		{
			$this->headerPadding = new \Padding(0);
			$this->headerPadding->top = 10;
			$this->headerPadding->right = 15;
			$this->headerPadding->bottom = 10;
			$this->headerPadding->left = 15;

			$this->footerPadding = new \Padding(0);
			$this->footerPadding->top = 10;
			$this->footerPadding->right = 15;
			$this->footerPadding->bottom = 10;
			$this->footerPadding->left = 15;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./HeaderColor', $contextNode);
			$this->headerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./FooterColor', $contextNode);
			$this->footerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HeaderTextColor', $contextNode);
			$this->headerTextColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HeaderIconColor', $contextNode);
			$this->headerIconColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./OutsideBorderColor', $contextNode);
			$this->outsideBorderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./DividerColor', $contextNode);
			$this->dividerColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonBackColor', $contextNode);
			$this->buttonBackColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonBorderColor', $contextNode);
			$this->buttonBorderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ButtonIconColor', $contextNode);
			$this->buttonIconColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./HeaderPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->headerPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./FooterPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->footerPadding = \Padding::fromXml($xpath, $queryResult->item(0));
		}
	}