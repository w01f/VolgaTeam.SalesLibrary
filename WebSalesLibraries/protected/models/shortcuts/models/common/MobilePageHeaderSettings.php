<?

	class MobilePageHeaderSettings
	{
		public $showTitle;
		public $showBackButton;
		public $showRightButton;
		public $showTopLogoDivider;
		public $topLogo;
		public $topDividerColor;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentShortcut PageContentShortcut
		 * @return MobilePageHeaderSettings
		 */
		public static function fromXml($xpath, $contextNode, $parentShortcut)
		{
			$instance = self::createEmpty();

			$queryResult = $xpath->query('./ShowHeaderTitle', $contextNode);
			$instance->showTitle = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->showTitle;

			$queryResult = $xpath->query('./ShowBackButton', $contextNode);
			$instance->showBackButton = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->showBackButton;

			$queryResult = $xpath->query('./ShowRightMenuButton', $contextNode);
			$instance->showRightButton = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->showRightButton;

			$queryResult = $xpath->query('./EnableTopLogo', $contextNode);
			$enableTopLogo = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			if($enableTopLogo)
			{
				$queryResult = $xpath->query('./TopLogo', $contextNode);
				$imageFileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if (!empty($imageFileName))
					$instance->topLogo = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $parentShortcut->relativeLink . '/images/' . $imageFileName);

				$queryResult = $xpath->query('./EnableTopLogoDivider', $contextNode);
				$instance->showTopLogoDivider = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

				$queryResult = $xpath->query('./TopLogoDividerColor', $contextNode);
				$instance->topDividerColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $instance->topDividerColor;
			}
			return $instance;
		}

		/**
		 * @return MobilePageHeaderSettings
		 */
		public static function createEmpty()
		{
			$instance = new self();

			$instance->showTitle = true;
			$instance->showBackButton = true;
			$instance->showRightButton = true;
			$instance->showTopLogoDivider = false;
			$instance->topLogo = '';
			$instance->topDividerColor = '000000';

			return $instance;
		}
	}