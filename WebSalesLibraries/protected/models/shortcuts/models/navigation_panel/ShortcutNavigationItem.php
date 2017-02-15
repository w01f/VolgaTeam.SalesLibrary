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
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath)
		{
			$this->type = 'shortcut';
			$this->contentView = 'shortcutItem';

			parent::__construct($parent, $xpath, $contextNode, $imagePath);

			$queryResult = $xpath->query('StaticID', $contextNode);
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord ShortcutLinkRecord */
				$shortcutRecord = ShortcutLinkRecord::model()->findByPk($shortcutId);
				if (isset($shortcutRecord))
					/** @var  $shortcut BaseShortcut */
					$this->shortcut = $shortcutRecord->getModel(false, null);
			}
		}
	}