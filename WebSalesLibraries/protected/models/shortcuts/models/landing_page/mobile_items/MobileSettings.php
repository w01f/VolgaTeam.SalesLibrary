<?

	namespace application\models\shortcuts\models\landing_page\mobile_items;

	/** Class MobileSettings */
	class MobileSettings
	{
		/** @var  BaseMobileItem[] */
		public $items;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return MobileSettings
		 */
		public static function fromXml($parentShortcut, $xpath, $contextNode)
		{
			$mobileSettings = new self();

			$queryResult = $xpath->query('./Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$shortcutItem = BaseMobileItem::fromXml($parentShortcut, $xpath, $node);
				if (isset($shortcutItem))
					$mobileSettings->items[] = $shortcutItem;
			}

			return $mobileSettings;
		}
	}