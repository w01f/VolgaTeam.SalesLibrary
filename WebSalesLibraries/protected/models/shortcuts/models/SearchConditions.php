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
		 * @param $configContext
		 */
		public function __construct($configContext)
		{
			$config = new DOMDocument();
			$config->loadXML($configContext);
			$xpath = new DomXPath($config);

			/** @var $queryResult DOMNodeList */
			$queryResult = $xpath->query('//SearchCondition/Text');
			$this->text = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//SearchCondition/StartDate');
			$this->startDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//SearchCondition/EndDate');
			$this->endDate = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('//SearchCondition/DateFileModified');
			$this->dateModified = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('//SearchCondition/FileType');
			foreach ($queryResult as $node)
				$this->fileTypes[] = trim($node->nodeValue);

			$queryResult = $xpath->query('//SearchCondition/Library');
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

			$queryResult = $xpath->query('//SearchCondition/SuperFilter');
			foreach ($queryResult as $node)
				$this->superFilters[] = trim($node->nodeValue);

			$queryResult = $xpath->query('//SearchCondition/Categories/Category');
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

			$queryResult = $xpath->query('//SearchCondition/HideIfNoTag');
			$this->onlyWithCategories = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('//SearchCondition/HideDuplicated');
			$this->hideDuplicated = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('//SearchCondition/SearchByName');
			$this->searchByName = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('//SearchCondition/SearchByContent');
			$this->searchByContent = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : null;

			$queryResult = $xpath->query('//SearchCondition/SortBy');
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

			$queryResult = $xpath->query('//SearchCondition/Sortorder');
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