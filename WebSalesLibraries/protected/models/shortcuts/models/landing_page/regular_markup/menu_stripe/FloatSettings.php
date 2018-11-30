<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	/**
	 * Class FloatSettings
	 */
	class FloatSettings
	{
		public $useForLargeScreen;
		public $useForMediumScreen;
		public $useForSmallScreen;
		public $useForExtraSmallScreen;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return FloatSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$floatSettings = self::createEmpty();

			$queryResult = $xpath->query('./Large', $contextNode);
			$floatSettings->useForLargeScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $floatSettings->useForLargeScreen;

			$queryResult = $xpath->query('./Medium', $contextNode);
			$floatSettings->useForMediumScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $floatSettings->useForMediumScreen;

			$queryResult = $xpath->query('./Small', $contextNode);
			$floatSettings->useForSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $floatSettings->useForSmallScreen;

			$queryResult = $xpath->query('./ExtraSmall', $contextNode);
			$floatSettings->useForExtraSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $floatSettings->useForExtraSmallScreen;

			if (!($floatSettings->useForLargeScreen ||
				$floatSettings->useForMediumScreen ||
				$floatSettings->useForSmallScreen ||
				$floatSettings->useForExtraSmallScreen))
			{
				$value = filter_var(trim($contextNode->nodeValue), FILTER_VALIDATE_BOOLEAN);
				$floatSettings->useForLargeScreen = $value;
				$floatSettings->useForMediumScreen = $value;
				$floatSettings->useForSmallScreen = $value;
				$floatSettings->useForExtraSmallScreen = $value;
			}

			return $floatSettings;
		}

		/**
		 * @return FloatSettings
		 */
		public static function createEmpty()
		{
			$floatSettings = new self();
			$floatSettings->useForLargeScreen = false;
			$floatSettings->useForMediumScreen = false;
			$floatSettings->useForSmallScreen = false;
			$floatSettings->useForExtraSmallScreen = false;
			return $floatSettings;
		}
	}