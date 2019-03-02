<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;

	class UrlItem extends BaseItem
	{
		public $url;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function __construct($xpath, $contextNode, $parentContainer)
		{
			$this->type = 'url';
			$this->contentView = 'urlItem';

			parent::__construct($xpath, $contextNode, $parentContainer);

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

		/** @return string */
		public function getItemData()
		{
			return null;
		}
	}