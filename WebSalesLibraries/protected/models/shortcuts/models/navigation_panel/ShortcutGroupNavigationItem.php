<?

	/**
	 * Class ShortcutGroupNavigationItem
	 */
	class ShortcutGroupNavigationItem extends BaseNavigationItem
	{
		/** @var ShortcutGroup */
		public $shortcutGroup;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 * @param $isPhone boolean
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath, $isPhone)
		{
			$this->type = 'shortcut-group';
			$this->contentView = 'shortcutGroupItem';

			parent::__construct($parent, $xpath, $contextNode, $imagePath, $isPhone);

			$queryResult = $xpath->query('StaticID', $contextNode);
			$shortcutGroupId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutGroupId))
			{
				/** @var  $shortcutGroupRecord ShortcutGroupRecord */
				$shortcutGroupRecord = ShortcutGroupRecord::model()->findByPk($shortcutGroupId);
				if (isset($shortcutGroupRecord))
				{
					$selectedSuperGroupTag = ShortcutsManager::getSelectedSuperGroup();
					$this->shortcutGroup = new ShortcutGroup($shortcutGroupRecord, $selectedSuperGroupTag, $isPhone);
				}
			}
			$this->settings->enabled &= isset($this->shortcutGroup);
		}

		/** @return string */
		public function getUrl()
		{
			return $this->settings->enabled ? $this->shortcutGroup->getUrl() : '#';
		}

		/** @return string */
		public function getItemData()
		{
			return null;
		}

		/** @return string */
		public function getTarget()
		{
			return "_self";
		}
	}