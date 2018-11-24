<?

	namespace application\models\wallbin\models\web\style;

	/**
	 * ResponsiveColumnStyle
	 */
	class ResponsiveColumnsStyle
	{
		public $enabled;

		public $largeScreenColumnsCount;
		public $mediumScreenColumnsCount;
		public $smallScreenColumnsCount;
		public $extraSmallScreenColumnsCount;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ResponsiveColumnsStyle
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$responsiveColumnStyle = self::createDefault();

			$queryResult = $xpath->query('./Enabled', $contextNode);
			$responsiveColumnStyle->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('./Large', $contextNode);
			$responsiveColumnStyle->largeScreenColumnsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Medium', $contextNode);
			$responsiveColumnStyle->mediumScreenColumnsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./Small', $contextNode);
			$responsiveColumnStyle->smallScreenColumnsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;

			$queryResult = $xpath->query('./ExtraSmall', $contextNode);
			$responsiveColumnStyle->extraSmallScreenColumnsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;

			if (!isset($responsiveColumnStyle->enabled) &&
				!isset($responsiveColumnStyle->largeScreenColumnsCount) &&
				!isset($responsiveColumnStyle->mediumScreenColumnsCount) &&
				!isset($responsiveColumnStyle->smallScreenColumnsCount) &&
				!isset($responsiveColumnStyle->extraSmallScreenColumnsCount))
			{
				$responsiveColumnStyle->enabled = filter_var(trim($contextNode->nodeValue), FILTER_VALIDATE_BOOLEAN);
				$responsiveColumnStyle->largeScreenColumnsCount = 3;
				$responsiveColumnStyle->mediumScreenColumnsCount = 3;
				$responsiveColumnStyle->smallScreenColumnsCount = 2;
				$responsiveColumnStyle->extraSmallScreenColumnsCount = 1;
			}
			else
			{
				if (!isset($responsiveColumnStyle->enabled))
					$responsiveColumnStyle->enabled = false;
				if (!isset($responsiveColumnStyle->largeScreenColumnsCount))
					$responsiveColumnStyle->largeScreenColumnsCount = 3;
				if (!isset($responsiveColumnStyle->mediumScreenColumnsCount))
					$responsiveColumnStyle->mediumScreenColumnsCount = 3;
				if (!isset($responsiveColumnStyle->smallScreenColumnsCount))
					$responsiveColumnStyle->smallScreenColumnsCount = 2;
				if (!isset($responsiveColumnStyle->extraSmallScreenColumnsCount))
					$responsiveColumnStyle->extraSmallScreenColumnsCount = 1;
			}

			return $responsiveColumnStyle;
		}

		/**
		 * @return ResponsiveColumnsStyle
		 */
		public static function createDefault()
		{
			$responsiveColumnStyle = new ResponsiveColumnsStyle();

			$responsiveColumnStyle->enabled = false;
			$responsiveColumnStyle->largeScreenColumnsCount = 3;
			$responsiveColumnStyle->mediumScreenColumnsCount = 3;
			$responsiveColumnStyle->smallScreenColumnsCount = 2;
			$responsiveColumnStyle->extraSmallScreenColumnsCount = 1;

			return $responsiveColumnStyle;
		}
	}