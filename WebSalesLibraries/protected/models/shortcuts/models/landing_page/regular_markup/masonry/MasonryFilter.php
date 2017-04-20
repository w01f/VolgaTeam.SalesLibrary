<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;
	/**
	 * Class MasonryFilter
	 */
	class MasonryFilter
	{
		public $title;

		/** @var  array */
		public $tags;

		public function __construct()
		{
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

			$queryResult = $xpath->query('./Tag', $contextNode);
			foreach ($queryResult as $node)
				$this->tags[] = trim($node->nodeValue);
		}
	}