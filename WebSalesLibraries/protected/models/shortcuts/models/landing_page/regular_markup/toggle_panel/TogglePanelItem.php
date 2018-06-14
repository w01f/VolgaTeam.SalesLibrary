<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\toggle_panel;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class TogglePanelItem
	 */
	class TogglePanelItem extends BlockContainer
	{
		public $tag;
		public $isDefault;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'toggle-item';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Tag', $contextNode);
			$this->tag = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}
	}