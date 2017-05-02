<?
	namespace application\models\feeds\horizontal;

	/**
	 * Class SimpleFeedStyle
	 */
	class SimpleFeedStyle
	{
		public $controlButtonTop;
		public $controlButtonWidth;
		public $controlButtonHeight;

		/** @var  \Padding */
		public $feedPadding;

		/** @var  \Padding */
		public $itemPadding;

		public function __construct()
		{
			$this->controlButtonTop = 26;
			$this->controlButtonWidth = 30;
			$this->controlButtonHeight = 65;

			$this->feedPadding = new \Padding(0);
			$this->feedPadding->top = 15;
			$this->feedPadding->right = 50;
			$this->feedPadding->bottom = 15;
			$this->feedPadding->left = 50;

			$this->itemPadding = new \Padding(0);
			$this->itemPadding->top = 10;
			$this->itemPadding->right = 15;
			$this->itemPadding->bottom = 0;
			$this->itemPadding->left = 15;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./ButtonTop', $contextNode);
			$this->controlButtonTop = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->controlButtonTop;

			$queryResult = $xpath->query('./ButtonWidth', $contextNode);
			$this->controlButtonWidth = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->controlButtonWidth;

			$queryResult = $xpath->query('./ButtonHeight', $contextNode);
			$this->controlButtonHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->controlButtonHeight;

			$queryResult = $xpath->query('./SliderPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->feedPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ItemPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->itemPadding = \Padding::fromXml($xpath, $queryResult->item(0));
		}
	}