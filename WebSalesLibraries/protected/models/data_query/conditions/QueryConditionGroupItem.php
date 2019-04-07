<?

	namespace application\models\data_query\conditions;


	class QueryConditionGroupItem
	{
		public $text;
		public $fileTypesInclude;
		public $fileTypesExclude;
		public $startDate;
		public $endDate;
		public $libraries;
		public $superFilters;
		public $categories;

		public $textExactMatch;
		public $onlyByName;
		public $onlyWithCategories;

		/** @var QueryConditionGroup[] */
		public $conditionGroups;

		public function __construct()
		{
			$this->fileTypesInclude = array();
			$this->fileTypesExclude = array();
			$this->libraries = array();
			$this->superFilters = array();
			$this->categories = array();
			$this->conditionGroups = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			/** @var $queryResult \DOMNodeList */
			$queryResult = $xpath->query('./Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$today = date(\Yii::app()->params['outputDateFormat']);
			$queryResult = $xpath->query('./StartDate', $contextNode);
			$startDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (!empty($startDateText))
			{
				switch ($startDateText)
				{
					case "today":
						$this->startDate = $today;
						break;
					case "yesterday":
						$this->startDate = date(\Yii::app()->params['outputDateFormat'], strtotime($today . ' - 1 days'));
						break;
					case "current week":
						$this->startDate = date(\Yii::app()->params['outputDateFormat'], strtotime('last monday', strtotime('tomorrow')));
						break;
					case "last week":
						$this->startDate = date(\Yii::app()->params['outputDateFormat'], strtotime("Monday last week", strtotime('tomorrow')));
						break;
					case "current month":
						$this->startDate = date(\Yii::app()->params['outputDateFormat'], strtotime(date('Y-m-1')));
						break;
					case "last month":
						$this->startDate = date(\Yii::app()->params['outputDateFormat'], mktime(0, 0, 0, date("m") - 1, 1));
						break;
					default:
						if (strstr($startDateText, ' days ago'))
						{
							$startDateText = str_replace(' days ago', '', $startDateText);
							$this->startDate = date(\Yii::app()->params['outputDateFormat'], strtotime($today . ' - ' . $startDateText . ' days'));
						}
						else
							$this->startDate = $startDateText;
				}
			}

			$queryResult = $xpath->query('./EndDate', $contextNode);
			$endDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (!empty($endDateText))
			{
				switch ($endDateText)
				{
					case "today":
					case "current week":
					case "current month":
						$this->endDate = date(\Yii::app()->params['outputDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$this->endDate = $today;
						break;
					case "last week":
						$this->endDate = date(\Yii::app()->params['mysqlDateFormat'], strtotime('last monday', strtotime('tomorrow')));
						break;
					case "last month":
						$this->endDate = date(\Yii::app()->params['outputDateFormat'], mktime(0, 0, 0, date("m"), 0));
						break;
					default:
						if (strstr($endDateText, ' days ago'))
						{
							$endDateText = str_replace(' days ago', '', $endDateText);
							$this->endDate = date(\Yii::app()->params['outputDateFormat'], strtotime($today . ' - ' . $endDateText . ' days'));
						}
						else
							$this->endDate = $endDateText;
				}
			}

			$queryResult = $xpath->query('./FileType', $contextNode);
			foreach ($queryResult as $node)
			{
				$fileTypeDescription = trim($node->nodeValue);
				switch ($fileTypeDescription)
				{
					case 'video':
						$this->fileTypesInclude[] = 'mp3';
						$this->fileTypesInclude[] = 'mp4';
						$this->fileTypesInclude[] = 'wmv';
						$this->fileTypesInclude[] = 'video';
						break;
					case 'image':
						$this->fileTypesInclude[] = 'png';
						$this->fileTypesInclude[] = 'jpeg';
						$this->fileTypesInclude[] = 'gif';
						break;
					default:
						$this->fileTypesInclude[] = $fileTypeDescription;
						break;
				}
			}

			$queryResult = $xpath->query('./FileTypeExclude', $contextNode);
			foreach ($queryResult as $node)
				$this->fileTypesExclude[] = trim($node->nodeValue);

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

			$queryResult = $xpath->query('./SuperFilter', $contextNode);
			foreach ($queryResult as $node)
				$this->superFilters[] = trim($node->nodeValue);

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

			$queryResult = $xpath->query('./TextExactMatch', $contextNode);
			$this->textExactMatch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./HideIfNoTag', $contextNode);
			$this->onlyWithCategories = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./SearchByName', $contextNode);
			$this->onlyByName = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./Group', $contextNode);
			foreach ($queryResult as $node)
			{
				$conditionGroup = new QueryConditionGroup();
				$conditionGroup->configureFromXml($xpath, $node);
				$this->conditionGroups[] = $conditionGroup;
			}
		}
	}