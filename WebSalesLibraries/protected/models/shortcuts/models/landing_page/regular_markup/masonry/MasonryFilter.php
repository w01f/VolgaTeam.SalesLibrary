<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\shortcuts\models\landing_page\regular_markup\common\TextAppearance;

	/**
	 * Class MasonryFilter
	 */
	class MasonryFilter
	{
		public $id;
		public $isDefault;
		public $title;

		/** @var  array */
		public $tags;

		/** @var  TextAppearance */
		public $textAppearance;

		public function __construct()
		{
			$this->id = uniqid();
			$this->isDefault = false;
			$this->tags = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./IsDefault', $contextNode);
			$this->isDefault = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->isDefault;

			$queryResult = $xpath->query('./Tag', $contextNode);
			foreach ($queryResult as $node)
				$this->tags[] = trim($node->nodeValue);

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));
		}
	}