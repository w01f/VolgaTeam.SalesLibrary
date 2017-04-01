<?

	/**
	 * Class SearchConditions
	 */
	class SearchConditions
	{
		public $text;
		public $fileTypes;
		public $startDate;
		public $endDate;
		public $libraries;
		public $superFilters;
		public $categories;

		public $textExactMatch;
		public $onlyWithCategories;
		public $onlyByName;
		public $searchByContent;

		/** @var  array */
		public $columnSettings;

		/** @var  TableSortSettings */
		public $sortSettings;

		/** @var  SearchDateSettings */
		public $dateSettings;
		/** @var  SearchCategorySettings */
		public $categorySettings;
		/** @var  SearchViewCountSettings */
		public $viewCountSettings;
		/** @var  SearchThumbnailSettings */
		public $thumbnailSettings;

		public $baseDatasetKey;

		public function __construct()
		{
			$this->fileTypes = array();
			$this->libraries = array();
			$this->superFilters = array();
			$this->categories = array();

			$this->columnSettings = TableColumnSettings::createEmpty();

			$this->dateSettings = new SearchDateSettings();
			$this->categorySettings = new SearchCategorySettings();
			$this->viewCountSettings = new SearchViewCountSettings();
			$this->thumbnailSettings = new SearchThumbnailSettings();

			$this->sortSettings = new TableSortSettings();

			$this->onlyWithCategories = false;
			$this->onlyByName = false;
		}

		/**
		 * @param string $encodedContent
		 * @return SearchConditions
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();

			$data = CJSON::decode($encodedContent, true);
			foreach ($data as $key => $value)
			{
				if (is_array($value))
					$value = CJSON::decode(CJSON::encode($value), false);
				$instance->{$key} = $value;
			}

			return $instance;
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @return SearchConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('./Text', $contextNode);
			$instance->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$today = date(Yii::app()->params['outputDateFormat']);
			$queryResult = $xpath->query('./StartDate', $contextNode);
			$startDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($startDateText))
			{
				switch ($startDateText)
				{
					case "today":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' - 1 days'));
						break;
					case "current week":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime("last Monday"));
						break;
					case "last week":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime("Monday last week"));
						break;
					case "current month":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime(date('Y-m-1')));
						break;
					default:
						if (strstr($startDateText, ' days ago'))
						{
							$startDateText = str_replace(' days ago', '', $startDateText);
							$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' - ' . $startDateText . ' days'));
						}
						else
							$instance->startDate = $startDateText;
				}
			}

			$queryResult = $xpath->query('./EndDate', $contextNode);
			$endDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($endDateText))
			{
				switch ($endDateText)
				{
					case "today":
					case "current week":
					case "current month":
						$instance->endDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->endDate = $today;
						break;
					case "last week":
						$instance->startDate = date(Yii::app()->params['outputDateFormat'], strtotime("Sunday last week"));
						break;
					default:
						if (strstr($endDateText, ' days ago'))
						{
							$endDateText = str_replace(' days ago', '', $endDateText);
							$instance->endDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' - ' . $endDateText . ' days'));
						}
						else
							$instance->endDate = $endDateText;
				}
			}

			$queryResult = $xpath->query('./FileType', $contextNode);
			foreach ($queryResult as $node)
			{
				$fileTypeDescription = trim($node->nodeValue);
				switch ($fileTypeDescription)
				{
					case 'video':
						$instance->fileTypes[] = 'mp3';
						$instance->fileTypes[] = 'mp4';
						$instance->fileTypes[] = 'wmv';
						$instance->fileTypes[] = 'video';
						break;
					case 'image':
						$instance->fileTypes[] = 'png';
						$instance->fileTypes[] = 'jpeg';
						$instance->fileTypes[] = 'gif';
						break;
					default:
						$instance->fileTypes[] = $fileTypeDescription;
						break;
				}
			}

			$queryResult = $xpath->query('./Library', $contextNode);
			if ($queryResult->length > 0)
				foreach ($queryResult as $node)
				{
					/** @var $libraryRecord LibraryRecord */
					$libraryRecord = LibraryRecord::model()->find('name=?', array(trim($node->nodeValue)));
					if (isset($libraryRecord))
					{
						$library = new stdClass();
						$library->id = $libraryRecord->id;
						$library->name = $libraryRecord->name;
						$instance->libraries[] = $library;
					}
				}

			$queryResult = $xpath->query('./SuperFilter', $contextNode);
			foreach ($queryResult as $node)
				$instance->superFilters[] = trim($node->nodeValue);

			$queryResult = $xpath->query('./Categories/Category', $contextNode);
			/** @var $node DOMElement */
			foreach ($queryResult as $node)
			{
				$groupName = $node->getAttribute('Group');
				$category = new SearchCategory();
				$category->name = $groupName;
				$tagNodes = $node->getElementsByTagName('Tag');
				foreach ($tagNodes as $tagNode)
					$category->items[] = trim($tagNode->nodeValue);
				$instance->categories[] = $category;
			}

			$queryResult = $xpath->query('./TextExactMatch', $contextNode);
			$instance->textExactMatch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./HideIfNoTag', $contextNode);
			$instance->onlyWithCategories = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./SearchByName', $contextNode);
			$instance->onlyByName = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('./ColumnSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->columnSettings = TableColumnSettings::loadColumnsFromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./DateSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->dateSettings = SearchDateSettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./CategorySettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->categorySettings = SearchCategorySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./LinkViewsSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->viewCountSettings = SearchViewCountSettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ThumbnailSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->thumbnailSettings = SearchThumbnailSettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./SortSettings', $contextNode);
			if ($queryResult->length > 0)
				$instance->sortSettings->configureFromXml($xpath, $queryResult->item(0));

			return $instance;
		}
	}