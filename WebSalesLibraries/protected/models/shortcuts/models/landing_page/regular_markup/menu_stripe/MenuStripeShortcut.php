<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	/**
	 * Class MenuStripeShortcut
	 */
	class MenuStripeShortcut extends MenuStripeItem
	{
		/** @var  \BaseShortcut */
		public $shortcut;

		/**
		 * @param $parentMenu IParentMenu
		 */
		public function __construct($parentMenu)
		{
			parent::__construct($parentMenu);
			$this->type = 'shortcut';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			if ($this->isAccessGranted)
			{
				$queryResult = $xpath->query('./ShortcutID', $contextNode);
				$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if (isset($shortcutId))
				{
					/** @var  $shortcutRecord \ShortcutLinkRecord */
					$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
					if (isset($shortcutRecord))
						/** @var  $shortcut \BaseShortcut */
						$this->shortcut = $shortcutRecord->getModel(false, null);
				}
			}

			$this->enable &= isset($this->shortcut);
		}
	}