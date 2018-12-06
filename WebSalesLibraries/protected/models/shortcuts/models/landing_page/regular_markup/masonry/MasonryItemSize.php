<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	/**
	 * Class MasonryItemSize
	 */
	class MasonryItemSize
	{
		public $sizeForLargeScreen;
		public $sizeForMediumScreen;
		public $sizeForSmallScreen;
		public $sizeForExtraSmallScreen;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return MasonryItemSize
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$size = self::createEmpty();

			$queryResult = $xpath->query('./Large', $contextNode);
			$size->sizeForLargeScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue)) : $size->sizeForLargeScreen;

			$queryResult = $xpath->query('./Medium', $contextNode);
			$size->sizeForMediumScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue)) : $size->sizeForMediumScreen;

			$queryResult = $xpath->query('./Small', $contextNode);
			$size->sizeForSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue)) : $size->sizeForSmallScreen;

			$queryResult = $xpath->query('./ExtraSmall', $contextNode);
			$size->sizeForExtraSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue)) : $size->sizeForExtraSmallScreen;

			if ($size->sizeForLargeScreen == 0 ||
				$size->sizeForMediumScreen == 0 ||
				$size->sizeForSmallScreen == 0 ||
				$size->sizeForExtraSmallScreen == 0)
			{
				$value = intval(trim($contextNode->nodeValue));
				$size->sizeForLargeScreen = $value;
				$size->sizeForMediumScreen = $value;
				$size->sizeForSmallScreen = $value;
				$size->sizeForExtraSmallScreen = $value;
			}

			return $size;
		}

		/**
		 * @return MasonryItemSize
		 */
		public static function createEmpty()
		{
			$floatSettings = new self();
			$floatSettings->sizeForLargeScreen = 0;
			$floatSettings->sizeForMediumScreen = 0;
			$floatSettings->sizeForSmallScreen = 0;
			$floatSettings->sizeForExtraSmallScreen = 0;
			return $floatSettings;
		}
	}