<?
	class RegularNavigationItemSettings
	{
		public $enabled;
		public $title;
		public $tooltip;
		public $iconUrlExpanded;
		public $iconUrlCollapsed;
		public $textColor;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('./Enabled', $contextNode);
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('./IconPanel', $contextNode);
			$this->iconUrlExpanded = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');
			$queryResult = $xpath->query('./IconBar', $contextNode);
			$this->iconUrlCollapsed = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');

			$queryResult = $xpath->query('./Text', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Tooltip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./IconPanelColor', $contextNode);
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $parent->textColor;
		}
	}