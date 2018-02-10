<?

	namespace application\models\shortcuts\models\landing_page\mobile_items;

	/** Class ShortcutItem */
	class ShortcutItem extends BaseMobileItem
	{
		/** @var  \BaseShortcut */
		public $shortcut;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 */
		public function __construct($parentShortcut)
		{
			parent::__construct($parentShortcut);
			$this->type = 'shortcut';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ShortcutID', $contextNode);
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
				if (isset($shortcutRecord))
					/** @var  $shortcut \BaseShortcut */
					$this->shortcut = $shortcutRecord->getModel(true, null);
			}
		}

		public function getSourceLink()
		{
			return isset($this->shortcut) ? $this->shortcut->getSourceLink() : '#';
		}

		/**
		 * @return string
		 */
		public function getMenuItemData()
		{
			return isset($this->shortcut) ? $this->shortcut->getMenuItemData() : '';
		}
	}