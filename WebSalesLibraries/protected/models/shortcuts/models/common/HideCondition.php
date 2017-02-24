<?

	/**
	 * Class HideCondition
	 */
	class HideCondition
	{
		public $extraSmall;
		public $small;
		public $medium;
		public $large;

		public function __construct()
		{
			$this->extraSmall = false;
			$this->small = false;
			$this->medium = false;
			$this->large = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return HideCondition
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$hideCondition = new HideCondition();

			$queryResult = $xpath->query('./ExtraSmall', $contextNode);
			$hideCondition->extraSmall = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $hideCondition->extraSmall;

			$queryResult = $xpath->query('./Small', $contextNode);
			$hideCondition->small = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $hideCondition->small;

			$queryResult = $xpath->query('./Medium', $contextNode);
			$hideCondition->medium = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $hideCondition->medium;

			$queryResult = $xpath->query('./Large', $contextNode);
			$hideCondition->large = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $hideCondition->large;

			return $hideCondition;
		}
	}