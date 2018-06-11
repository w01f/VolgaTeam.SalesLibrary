<?

	/**
	 * Class ShortcutNavigationItem
	 */
	class ShortcutNavigationItem extends BaseNavigationItem
	{
		public $shortcut;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @param $isPhone boolean
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath, $isPhone)
		{
			$this->type = 'shortcut';
			$this->contentView = 'shortcutItem';

			parent::__construct($parent, $xpath, $contextNode, $imagePath, $isPhone);

			$queryResult = $xpath->query('StaticID', $contextNode);
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord ShortcutLinkRecord */
				$shortcutRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
				if (isset($shortcutRecord))
					$this->shortcut = $shortcutRecord->getRegularModel(false, null);
			}
			$this->settings->enabled &= isset($this->shortcut);
		}

		/** @return string */
		public function getUrl()
		{
			return $this->settings->enabled ? $this->shortcut->getSourceLink() : '#';
		}

		/** @return string */
		public function getItemData()
		{
			return $this->shortcut->getMenuItemData();
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}
	}