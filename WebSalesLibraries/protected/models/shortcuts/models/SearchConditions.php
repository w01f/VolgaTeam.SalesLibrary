<?php

	/**
	 * Class SearchConditions
	 */
	class SearchConditions
	{
		public $text;
		public $startDate;
		public $endDate;
		public $dateModified;
		public $fileTypes;
		public $libraries;
		public $superFilters;
		public $categories;
		public $onlyWithCategories;
		public $hideDuplicated;
		public $searchByName;
		public $searchByContent;
		public $sortColumn;
		public $sortDirection;

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 */
		public function __construct($xpath, $contextNode)
		{
			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('Text', $contextNode);
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('StartDate', $contextNode);
			$this->startDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('EndDate', $contextNode);
			$this->endDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('DateFileModified', $contextNode);
			$this->dateModified = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('FileType', $contextNode);
			foreach ($queryResult as $node)
				$this->fileTypes[] = trim($node->nodeValue);

			$queryResult = $xpath->query('Library', $contextNode);
			if ($queryResult->length > 0)
				foreach ($queryResult as $node)
				{
					/** @var $libraryRecord LibraryRecord */
					$libraryRecord = LibraryRecord::model()->find('name=?', array(trim($node->nodeValue)));
					if (isset($libraryRecord))
						$this->libraries[] = $libraryRecord->id;
				}
			else
			{
				$libraryRecords = LibraryRecord::model()->findAll();
				foreach ($libraryRecords as $libraryRecord)
					$this->libraries[] = $libraryRecord->id;
			}

			$queryResult = $xpath->query('SuperFilter', $contextNode);
			foreach ($queryResult as $node)
				$this->superFilters[] = trim($node->nodeValue);

			$queryResult = $xpath->query('Categories/Category', $contextNode);
			/** @var $node DOMElement */
			foreach ($queryResult as $node)
			{
				$groupName = $node->getAttribute('Group');
				$tagNodes = $node->getElementsByTagName('Tag');
				foreach ($tagNodes as $tagNode)
				{
					$category = new Category();
					$category->category = $groupName;
					$category->tag = trim($tagNode->nodeValue);
					$this->categories[] = $category;
				}
			}

			$queryResult = $xpath->query('HideIfNoTag', $contextNode);
			$this->onlyWithCategories = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('HideDuplicated', $contextNode);
			$this->hideDuplicated = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('SearchByName', $contextNode);
			$this->searchByName = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('SearchByContent', $contextNode);
			$this->searchByContent = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('SortBy', $contextNode);
			foreach ($queryResult as $node)
			{
				switch (strtolower(trim($node->nodeValue)))
				{
					case "tag":
						$this->sortColumn = "tag";
						break;
					case "station":
						$this->sortColumn = "library";
						break;
					case "type":
						$this->sortColumn = "file_type";
						break;
					case "link":
						$this->sortColumn = "name";
						break;
					case "date":
						$this->sortColumn = "date_modify";
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
						$this->sortDirection = "asc";
						break;
					case "descending":
						$this->sortDirection = "desc";
						break;
				}
				break;
			}
		}
	}