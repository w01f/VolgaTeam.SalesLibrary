<?

	/**
	 * Class Padding
	 */
	class Padding
	{
		public $isConfigured;

		public $top;
		public $left;
		public $bottom;
		public $right;

		/** @param $defaultSize int */
		public function __construct($defaultSize)
		{
			$this->top = $defaultSize;
			$this->left = $defaultSize;
			$this->bottom = $defaultSize;
			$this->right = $defaultSize;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return Padding
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$padding = new Padding(0);

			if (!$contextNode->hasChildNodes())
			{
				$padding->top =
				$padding->left =
				$padding->bottom =
				$padding->right = intval(trim($contextNode->nodeValue));
			}
			else
			{
				$queryResult = $xpath->query('./Top', $contextNode);
				$padding->top = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $padding->top;

				$queryResult = $xpath->query('./Left', $contextNode);
				$padding->left = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $padding->left;

				$queryResult = $xpath->query('./Bottom', $contextNode);
				$padding->bottom = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $padding->bottom;

				$queryResult = $xpath->query('./Right', $contextNode);
				$padding->right = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $padding->right;
			}
			$padding->isConfigured = true;
			return $padding;
		}
	}