<?

	namespace application\models\data_query\conditions;

	/**
	 * Class QueryCacheSettings
	 */
	class QueryCacheSettings
	{
		public $enableCache;
		public $cacheId;
		public $expireInHours;

		public function __construct()
		{
			$this->enableCache = false;
			$this->cacheId = uniqid();
			$this->expireInHours = 0;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return QueryCacheSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./Enabled', $contextNode);
			$instance->enableCache = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->enableCache;

			$queryResult = $xpath->query('./Frequency', $contextNode);
			$instance->expireInHours = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->expireInHours;

			return $instance;
		}
	}