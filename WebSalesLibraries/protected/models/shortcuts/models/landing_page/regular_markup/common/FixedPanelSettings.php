<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class FixedPanelSettings
	 */
	class FixedPanelSettings
	{
		/** @var  MarkupSettings */
		public $topPanel;

		/** @var  MarkupSettings */
		public $bottomPanel;

		public $topHeight;
		public $bottomHeight;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return FixedPanelSettings
		 */
		public static function fromXml($parentShortcut, $xpath, $contextNode)
		{
			$fixedPanelSettings = new FixedPanelSettings();

			$queryResult = $xpath->query('./Top/Markup', $contextNode);
			if ($queryResult->length > 0)
				$fixedPanelSettings->topPanel = MarkupSettings::fromXml($parentShortcut, $xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Bottom/Markup', $contextNode);
			if ($queryResult->length > 0)
				$fixedPanelSettings->topPanel = MarkupSettings::fromXml($parentShortcut, $xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Top/Height', $contextNode);
			$fixedPanelSettings->topHeight = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 0;

			$queryResult = $xpath->query('./Bottom/Height', $contextNode);
			$fixedPanelSettings->bottomHeight = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 0;

			return $fixedPanelSettings;
		}
	}