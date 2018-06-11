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
		 * @param $isPhone boolean
		 */
		public function __construct($parent, $xpath, $contextNode, $imagePath, $isPhone)
		{
			$this->type = 'url';
			$this->contentView = 'urlItem';

			parent::__construct($parent, $xpath, $contextNode, $imagePath, $isPhone);

			$queryResult = $xpath->query('Url', $contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/** @return string */
		public function getUrl()
		{
			return $this->settings->enabled ? $this->url : '#';
		}

		/** @return string */
		public function getItemData()
		{
			return null;
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}
	}