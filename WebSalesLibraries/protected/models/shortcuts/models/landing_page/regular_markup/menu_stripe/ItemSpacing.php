<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	/**
	 * Class ItemSpacing
	 */
	class ItemSpacing
	{
		public $extraSmall;
		public $small;
		public $medium;
		public $large;

		/** @param int $size */
		public function __construct($size = 0)
		{
			$this->extraSmall = $size;
			$this->small = $size;
			$this->medium = $size;
			$this->large = $size;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ItemSpacing
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$size = new ItemSpacing();

			$queryResult = $xpath->query('./*', $contextNode);
			if ($queryResult->length > 0)
			{
				$queryResult = $xpath->query('./ExtraSmall', $contextNode);
				$size->extraSmall = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $size->extraSmall;

				$queryResult = $xpath->query('./Small', $contextNode);
				$size->small = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $size->small;

				$queryResult = $xpath->query('./Medium', $contextNode);
				$size->medium = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $size->medium;

				$queryResult = $xpath->query('./Large', $contextNode);
				$size->large = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $size->large;
			}
			else
			{
				$size->extraSmall =
				$size->small =
				$size->medium =
				$size->large = intval(trim($contextNode->nodeValue));
			}

			return $size;
		}
	}