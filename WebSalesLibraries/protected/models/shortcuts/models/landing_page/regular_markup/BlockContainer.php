<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;

	/**
	 * Class BlockContainer
	 */
	abstract class BlockContainer extends ContentBlock
	{
		/** @var  ContentBlock[] */
		public $items;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		protected function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->items = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
				$this->items[] = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
		}
	}