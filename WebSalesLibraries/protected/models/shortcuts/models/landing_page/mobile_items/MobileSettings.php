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
			$mobileSettings = new MobileSettings();

			$queryResult = $xpath->query('./Item', $contextNode);
			foreach ($queryResult as $node)
				$mobileSettings->items[] = BaseMobileItem::fromXml($parentShortcut, $xpath, $node);

			return $mobileSettings;
		}
	}