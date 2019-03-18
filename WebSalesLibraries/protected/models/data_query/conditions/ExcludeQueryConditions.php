<?

	namespace application\models\data_query\conditions;

	/**
	 * Class ExcludeQueryConditions
	 */
	class ExcludeQueryConditions
	{
		public $text;
		public $textExactMatch;

		public $libraries;
		public $categories;

		/** @var  SpecificLinkCondition[] */
		public $linkConditions;

		public function __construct()
		{
			$this->libraries = array();
			$this->categories = array();
			$this->linkConditions = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configurefromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./TextExactMatch', $contextNode);
			$this->textExactMatch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./Library', $contextNode);
			if ($queryResult->length > 0)
				foreach ($queryResult as $node)
				{
					/** @var $libraryRecord \LibraryRecord */
					$libraryRecord = \LibraryRecord::model()->find('name=?', array(trim($node->nodeValue)));
					if (isset($libraryRecord))
					{
						$library = new \stdClass();
						$library->id = $libraryRecord->id;
						$library->name = $libraryRecord->name;
						$this->libraries[] = $library;
					}
				}

			$queryResult = $xpath->query('./Categories/Category', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$groupName = $node->getAttribute('Group');
				$category = new CategoryConditionItem();
				$category->name = $groupName;
				$tagNodes = $node->getElementsByTagName('Tag');
				foreach ($tagNodes as $tagNode)
					$category->items[] = trim($tagNode->nodeValue);
				$this->categories[] = $category;
			}

			$queryResult = $xpath->query('./SpecificLinks/Item', $contextNode);
			foreach ($queryResult as $node)
				$this->linkConditions[] = SpecificLinkCondition::fromXml($xpath, $node);
		}
	}