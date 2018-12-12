<?

	namespace application\models\data_query\data_table;

	/**
	 * Class DataTableColumnVisibilitySettings
	 */
	class DataTableColumnVisibilitySettings
	{
		public $hideForLargeScreen;
		public $hideForMediumScreen;
		public $hideForSmallScreen;
		public $hideForExtraSmallScreen;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return DataTableColumnVisibilitySettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$visibilitySettings = self::createEmpty();

			$queryResult = $xpath->query('./Large', $contextNode);
			$visibilitySettings->hideForLargeScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $visibilitySettings->hideForLargeScreen;

			$queryResult = $xpath->query('./Medium', $contextNode);
			$visibilitySettings->hideForMediumScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $visibilitySettings->hideForMediumScreen;

			$queryResult = $xpath->query('./Small', $contextNode);
			$visibilitySettings->hideForSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $visibilitySettings->hideForSmallScreen;

			$queryResult = $xpath->query('./ExtraSmall', $contextNode);
			$visibilitySettings->hideForExtraSmallScreen = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $visibilitySettings->hideForExtraSmallScreen;

			if (!($visibilitySettings->hideForLargeScreen ||
				$visibilitySettings->hideForMediumScreen ||
				$visibilitySettings->hideForSmallScreen ||
				$visibilitySettings->hideForExtraSmallScreen))
			{
				$value = filter_var(trim($contextNode->nodeValue), FILTER_VALIDATE_BOOLEAN);
				$visibilitySettings->hideForLargeScreen = $value;
				$visibilitySettings->hideForMediumScreen = $value;
				$visibilitySettings->hideForSmallScreen = $value;
				$visibilitySettings->hideForExtraSmallScreen = $value;
			}

			return $visibilitySettings;
		}

		/**
		 * @return DataTableColumnVisibilitySettings
		 */
		public static function createEmpty()
		{
			$floatSettings = new self();
			$floatSettings->hideForLargeScreen = false;
			$floatSettings->hideForMediumScreen = false;
			$floatSettings->hideForSmallScreen = false;
			$floatSettings->hideForExtraSmallScreen = false;
			return $floatSettings;
		}
	}