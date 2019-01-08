<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\style;

	/**
	 * Class BorderStyle
	 */
	class BorderStyle
	{
		public $color;
		public $style;
		/** @var  \Padding */
		public $size;

		/** @var  \HideCondition */
		public $hideCondition;

		public function __construct()
		{
			$this->size = new \Padding(0);
			$this->hideCondition = new \HideCondition();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return BorderStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$borderStyle = new BorderStyle();

			$queryResult = $xpath->query('./Color', $contextNode);
			$borderStyle->color = $queryResult->length > 0 ? str_replace("#", "", strtolower(trim($queryResult->item(0)->nodeValue))) : null;

			$queryResult = $xpath->query('./Style', $contextNode);
			$borderStyle->style = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : "solid";

			$queryResult = $xpath->query('./Size', $contextNode);
			if ($queryResult->length > 0)
				$borderStyle->size = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$borderStyle->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));

			return $borderStyle;
		}
	}