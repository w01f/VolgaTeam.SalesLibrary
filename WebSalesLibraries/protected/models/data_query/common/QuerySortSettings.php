<?
	namespace application\models\data_query\common;

	/**
	 * Class TableSortSettings
	 */
	class QuerySortSettings
	{
		public $isConfigured;
		public $columnTag;
		public $order;

		public function __construct()
		{
			$this->isConfigured = false;
			$this->columnTag = QuerySettings::DataTagFileName;
			$this->order = 'asc';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Column', $contextNode);
			if($queryResult->length > 0)
			{
				$this->columnTag = trim($queryResult->item(0)->nodeValue);
				$this->isConfigured = true;
			}

			$queryResult = $xpath->query('./Order', $contextNode);
			if($queryResult->length > 0)
			{
				$this->order = trim($queryResult->item(0)->nodeValue);
				$this->isConfigured = true;
			}
		}
	}