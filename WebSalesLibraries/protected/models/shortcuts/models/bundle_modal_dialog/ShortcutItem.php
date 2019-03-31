<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;


	class ShortcutItem extends BaseItem
	{
		public $shortcutId;

		public function __construct()
		{
			$this->type = 'shortcut';
			$this->contentView = 'shortcutItem';
			parent::__construct();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function loadFromXml($xpath, $contextNode, $parentContainer)
		{
			parent::loadFromXml($xpath, $contextNode, $parentContainer);

			$queryResult = $xpath->query('./ShortcutID', $contextNode);
			$this->shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/** @return \BaseShortcut */
		public function getShortcut()
		{
			if (isset($this->shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($this->shortcutId);
				if (isset($shortcutRecord))
					return $shortcutRecord->getRegularModel(false, null);
			}
			return null;
		}

		/** @return string */
		public function getUrl()
		{
			$shortcut = $this->getShortcut();
			if (isset($shortcut))
				return $shortcut->getSourceLink();
			return null;
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}
	}