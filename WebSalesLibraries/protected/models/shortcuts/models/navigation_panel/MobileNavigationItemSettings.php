<?

	class MobileNavigationItemSettings
	{
		public $enabled;
		public $title;
		public $icon;
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

			$queryResult = $xpath->query('./Icon', $contextNode);
			if ($queryResult->length == 0)
				$queryResult = $xpath->query('./IconPanel', $contextNode);
			$this->icon = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');

			$queryResult = $xpath->query('Text', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $parent->textColor;
		}
	}