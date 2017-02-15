<?

	/**
	 * Class BaseNavigationItem
	 */
	abstract class BaseNavigationItem
	{
		public $type;
		public $title;
		public $tooltip;
		public $iconUrlExpanded;
		public $iconUrlCollapsed;
		public $textColor;

		public $contentView;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('IconPanel', $contextNode);
			$this->iconUrlExpanded = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');
			$queryResult = $xpath->query('IconBar', $contextNode);
			$this->iconUrlCollapsed = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');

			$queryResult = $xpath->query('Text', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('Tooltip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('IconPanelColor', $contextNode);
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $parent->textColor;
		}

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @return BaseNavigationItem
		 */
		public static function fromXml($parent, $xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('Type', $contextNode);
			$itemType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			switch ($itemType)
			{
				case 'shortcut':
					$item = new ShortcutNavigationItem($parent, $xpath, $contextNode, $imagePath);
					break;
				case 'url':
					$item = new UrlNavigationItem($parent, $xpath, $contextNode, $imagePath);
					break;
				default:
					return null;
			}
			return $item;
		}
	}