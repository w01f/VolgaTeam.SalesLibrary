<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\list_block;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class ListBlock
	 */
	class ListBlock extends BlockContainer
	{
		public $borderColor;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'list';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./BorderColor', $contextNode);
			$this->borderColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
		}
	}