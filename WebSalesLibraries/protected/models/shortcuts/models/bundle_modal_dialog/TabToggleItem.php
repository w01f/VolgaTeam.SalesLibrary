<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class TabToggleItem extends BaseItem
	{
		public $tabId;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function __construct($xpath, $contextNode, $parentContainer)
		{
			$this->type = 'tab';
			$this->contentView = 'tabToggleItem';

			parent::__construct($xpath, $contextNode, $parentContainer);

			$queryResult = $xpath->query('TabID', $contextNode);
			$this->tabId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/** @return string */
		public function getUrl()
		{
			return '#';
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}

		/** @return string */
		public function getItemData()
		{
			return null;
		}
	}