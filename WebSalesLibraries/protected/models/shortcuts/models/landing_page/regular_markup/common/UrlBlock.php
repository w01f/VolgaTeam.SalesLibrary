<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;
	/**
	 * Class UrlBlock
	 */
	class UrlBlock extends BlockContainer
	{
		public $url;
		public $isMailTo;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'url';
			$this->url = '#';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Url', $contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->url;
			$this->isMailTo = strpos($this->url, 'mailto');
		}
	}