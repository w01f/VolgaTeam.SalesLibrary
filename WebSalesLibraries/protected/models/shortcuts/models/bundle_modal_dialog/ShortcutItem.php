<?
	namespace application\models\shortcuts\models\bundle_modal_dialog;


	class ShortcutItem extends BaseItem
	{
		public $shortcut;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function __construct($xpath, $contextNode, $parentContainer)
		{
			$this->type = 'shortcut';
			$this->contentView = 'shortcutItem';

			parent::__construct($xpath, $contextNode, $parentContainer);

			$queryResult = $xpath->query('./ShortcutID', $contextNode);
			$shortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($shortcutId))
			{
				/** @var  $shortcutRecord \ShortcutLinkRecord */
				$shortcutRecord = \ShortcutLinkRecord::model()->findByPk($shortcutId);
				if (isset($shortcutRecord))
					$this->shortcut = $shortcutRecord->getRegularModel(false, null);
			}
		}

		/** @return string */
		public function getUrl()
		{
			return $this->shortcut->getSourceLink();
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}

		/** @return string */
		public function getItemData()
		{
			return $this->shortcut->getMenuItemData();
		}
	}