<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;

	/**
	 * Class MasonryUrl
	 */
	class MasonryUrl extends MasonryItem
	{
		public $url;
		public $isMailTo;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'url';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Url', $contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$this->isMailTo = strpos($this->url, 'mailto');
		}
	}