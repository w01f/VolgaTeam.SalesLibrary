<?

	/**
	 * Class BaseNavigationItem
	 */
	abstract class BaseNavigationItem
	{
		public $type;
		public $title;
		public $tooltip;
		public $iconUrl;

		public $contentView;

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 */
		public function __construct($xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('Icon', $contextNode);
			$this->iconUrl = $imagePath . '/' . ($queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '');

			$queryResult = $xpath->query('Text', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('Tooltip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @return BaseNavigationItem
		 */
		public static function fromXml($xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('Type', $contextNode);
			$itemType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			switch ($itemType)
			{
				case 'shortcut':
					$item = new ShortcutNavigationItem($xpath, $contextNode, $imagePath);
					break;
				case 'url':
					$item = new UrlNavigationItem($xpath, $contextNode, $imagePath);
					break;
				default:
					return null;
			}
			return $item;
		}
	}