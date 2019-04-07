<?

	namespace application\models\data_query\conditions;


	class QueryConditionGroup
	{
		public const QueryConditionGroupOperatorOr = 'or';
		public const QueryConditionGroupOperatorAnd = 'and';

		public $operator;

		/** @var QueryConditionGroupItem[] */
		public $conditionItems;

		public function __construct()
		{
			$this->operator = self::QueryConditionGroupOperatorAnd;
			$this->conditionItems = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Operator', $contextNode);
			$operator = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
			if (in_array($operator, array(self::QueryConditionGroupOperatorAnd, self::QueryConditionGroupOperatorOr)))
				$this->operator = $operator;

			$queryResult = $xpath->query('./Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$conditionItem = new QueryConditionGroupItem();
				$conditionItem->configureFromXml($xpath, $node);
				$this->conditionItems[] = $conditionItem;
			}
		}
	}