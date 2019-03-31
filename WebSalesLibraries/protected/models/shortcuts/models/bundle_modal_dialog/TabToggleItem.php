<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class TabToggleItem extends BaseItem
	{
		public $tabId;

		public function __construct()
		{
			$this->type = 'tab';
			$this->contentView = 'tabToggleItem';
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
	}