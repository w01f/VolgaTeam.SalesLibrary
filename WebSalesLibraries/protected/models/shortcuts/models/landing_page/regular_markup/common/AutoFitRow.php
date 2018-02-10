<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	/**
	 * Class AutoFitRow
	 */
	class AutoFitRow extends BlockContainer
	{
		/** @var  \Size */
		public $itemsPerRow;

		/** @var  \HideCondition */
		public $hideCondition;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'auto-fit-row';
			$this->itemsPerRow = new \Size(12);
			$this->hideCondition = new \HideCondition();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ItemsPerRow', $contextNode);
			if ($queryResult->length > 0)
				$this->itemsPerRow = \Size::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));
		}
	}