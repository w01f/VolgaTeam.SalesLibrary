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
		 * @return \SearchConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('Text', $contextNode);
			$instance->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('StartDate', $contextNode);
			$instance->startDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('EndDate', $contextNode);
			$instance->endDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('FileType', $contextNode);
			foreach ($queryResult as $node)
			{
				$fileTypeDescription = trim($node->nodeValue);
				switch ($fileTypeDescription)
				{
					case 'video':
						$instance->fileTypes[] = 'mp4';
						$instance->fileTypes[] = 'mp3';
						$instance->fileTypes[] = 'wmv';
						break;
					case 'image':
						$instance->fileTypes[] = 'png';
						$instance->fileTypes[] = 'jpeg';
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