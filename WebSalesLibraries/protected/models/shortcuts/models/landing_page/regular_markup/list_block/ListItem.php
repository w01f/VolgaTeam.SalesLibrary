<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\list_block;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class ListItem
	 */
	class ListItem extends BlockContainer
	{
		public $isActive;
		public $backColor;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'list-item';
			$this->isActive = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./IsActive', $contextNode);
			$this->isActive = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./BackColor', $contextNode);
			$this->backColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
		}
	}