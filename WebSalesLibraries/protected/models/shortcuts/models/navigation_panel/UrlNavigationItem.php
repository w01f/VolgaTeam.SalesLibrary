<?

	/**
	 * Class UrlNavigationItem
	 */
	class UrlNavigationItem extends BaseNavigationItem
	{
		public $url;

		/**
		 * @param $parent NavigationPanel
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath)
		{
			$this->type = 'url';
			$this->contentView = 'urlItem';

			parent::__construct($parent, $xpath, $contextNode, $imagePath);

			$queryResult = $xpath->query('Url',$contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}
	}