<?

	namespace application\models\shortcuts\models\landing_page\mobile_items;

	/** Class BaseMobileItem */
	abstract class BaseMobileItem
	{
		/** @var \LandingPageShortcut */
		protected $parentShortcut;

		public $type;

		public $useIcon;
		public $iconClass;
		public $iconColor;
		public $imageUrl;

		public $title;
		public $titleColor;
		/**
		 * @param $parentShortcut \LandingPageShortcut
		 */
		protected function __construct($parentShortcut)
		{
			$this->parentShortcut = $parentShortcut;
		}

		/**
		 * @return string
		 */
		public abstract function getSourceLink();

		/**
		 * @return string
		 */
		public abstract function getMenuItemData();

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$this->useIcon = true;
			$this->iconClass = 'default-icon';
			$queryResult = $xpath->query('./Icon', $contextNode);
			if ($queryResult->length > 0)
			{
				$iconValue = trim($queryResult->item(0)->nodeValue);
				$this->useIcon = strpos($iconValue, '.png') === false && strpos($iconValue, '.svg') === false ;
				if ($this->useIcon)
				{
					$this->iconClass = $iconValue;

					$queryResult = $xpath->query('./IconColor', $contextNode);
					if ($queryResult->length > 0)
						$this->iconColor = trim($queryResult->item(0)->nodeValue);
				}
				else
					$this->imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentShortcut->relativeLink . '/images/' . $iconValue);
			}

			$queryResult = $xpath->query('./Title', $contextNode);
			if ($queryResult->length > 0)
				$this->title = trim($queryResult->item(0)->nodeValue);

			$queryResult = $xpath->query('./TextColor', $contextNode);
			if ($queryResult->length > 0)
				$this->titleColor = trim($queryResult->item(0)->nodeValue);
		}

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return BaseMobileItem
		 */
		public static function fromXml($parentShortcut, $xpath, $contextNode)
		{
			$typeAttribute = $contextNode->attributes->getNamedItem("Type");
			$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
			switch ($type)
			{
				case "url":
					$urlItem = new UrlItem($parentShortcut);
					$urlItem->configureFromXml($xpath, $contextNode);
					return $urlItem;
				case "shortcut":
					$shortcutItem = new ShortcutItem($parentShortcut);
					$shortcutItem->configureFromXml($xpath, $contextNode);
					return isset($shortcutItem) ? $shortcutItem : null;
				default:
					return null;
			}
		}
	}
