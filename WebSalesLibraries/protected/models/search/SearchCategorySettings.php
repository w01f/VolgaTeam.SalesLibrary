<?

	/**
	 * Class SearchCategorySettings
	 */
	class SearchCategorySettings
	{
		public $fieldName;
		public $maxRows;

		public function __construct()
		{
			$this->fieldName = 'tag';
			$this->maxRows = 9999;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return SearchCategorySettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./Level', $contextNode);
			$level = $queryResult->length > 0 ? intval($queryResult->item(0)->nodeValue) : null;
			switch ($level)
			{
				case 1:
					$instance->fieldName = 'group';
					break;
				case 2:
					$instance->fieldName = 'category';
					break;
				default:
					$instance->fieldName = 'tag';
					break;
			}

			$queryResult = $xpath->query('./MaxRows', $contextNode);
			$instance->maxRows = $queryResult->length > 0 ? intval($queryResult->item(0)->nodeValue) : 9999;

			return $instance;
		}
	}