<?php

	/**
	 * Class SearchConditions
	 */
	class SearchConditions
	{
		public $text;
		public $textExactMatch;
		public $fileTypes;
		public $startDate;
		public $endDate;
		public $libraries;
		public $superFilters;
		public $categories;
		public $onlyWithCategories;
		public $onlyByName;

		public $sortColumn;
		public $sortDirection;

		public $baseDatasetKey;

		public $searchByContent;

		public function __construct()
		{
			$this->fileTypes = array();
			$this->libraries = array();
			$this->superFilters = array();
			$this->categories = array();
			$this->onlyWithCategories = false;
			$this->onlyByName = false;
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
			$queryResult = $xpath->query('Text', $contextNode);
			$instance->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('TextExactMatch', $contextNode);
			$instance->textExactMatch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$today = date(Yii::app()->params['outputDateFormat']);
			$queryResult = $xpath->query('StartDate', $contextNode);
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

			$queryResult = $xpath->query('EndDate', $contextNode);
			$endDateText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($endDateText))
			{
				switch ($endDateText)
				{
					case "today":
						$instance->endDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' + 1 days'));
						break;
					case "yesterday":
						$instance->endDate = date(Yii::app()->params['outputDateFormat'], strtotime($today . ' - 1 days'));
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

			$queryResult = $xpath->query('FileType', $contextNode);
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

			$queryResult = $xpath->query('Library', $contextNode);
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

			$queryResult = $xpath->query('SuperFilter', $contextNode);
			foreach ($queryResult as $node)
				$instance->superFilters[] = trim($node->nodeValue);

			$queryResult = $xpath->query('Categories/Category', $contextNode);
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

			$queryResult = $xpath->query('HideIfNoTag', $contextNode);
			$instance->onlyWithCategories = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('SearchByName', $contextNode);
			$instance->onlyByName = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

			$queryResult = $xpath->query('SortBy', $contextNode);
			foreach ($queryResult as $node)
			{
				switch (strtolower(trim($node->nodeValue)))
				{
					case "tag":
						$instance->sortColumn = "tag";
						break;
					case "station":
						$instance->sortColumn = "library";
						break;
					case "type":
						$instance->sortColumn = "file_type";
						break;
					case "link":
						$instance->sortColumn = "name";
						break;
					case "date":
						$instance->sortColumn = "date_modify";
						break;
					case "rate":
						$instance->sortColumn = "rate";
						break;
					case "views":
						$instance->sortColumn = "views";
						break;
				}
				break;
			}

			$queryResult = $xpath->query('Sortorder', $contextNode);
			foreach ($queryResult as $node)
			{
				switch (strtolower(trim($node->nodeValue)))
				{
					case "ascending":
						$instance->sortDirection = "asc";
						break;
					case "descending":
						$instance->sortDirection = "desc";
						break;
				}
				break;
			}

			return $instance;
		}
	}