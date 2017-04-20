<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class Column
	 */
	class Column extends BlockContainer
	{
		/** @var  Size */
		public $size;

		/** @var  \HideCondition */
		public $hideCondition;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'column';
			$this->size = new Size(12);
			$this->hideCondition = new \HideCondition();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Size', $contextNode);
			if ($queryResult->length > 0)
				$this->size = Size::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));
		}
	}