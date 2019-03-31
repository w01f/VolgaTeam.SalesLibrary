<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class UrlItem extends BaseItem
	{
		public $url;

		public function __construct()
		{
			$this->type = 'url';
			$this->contentView = 'urlItem';
			parent::__construct();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function loadFromXml($xpath, $contextNode, $parentContainer)
		{
			parent::loadFromXml($xpath, $contextNode, $parentContainer);

			$queryResult = $xpath->query('Url', $contextNode);
			$this->url = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
		}

		/** @return string */
		public function getUrl()
		{
			return $this->url;
		}

		/** @return string */
		public function getTarget()
		{
			return "_blank";
		}
	}