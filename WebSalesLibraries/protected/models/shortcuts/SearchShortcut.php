<?php
	class SearchShortcut
	{
		public $type;
		public $title;
		public $imagePath;
		public $sourceLink;
		public $samePage;

		private $linkRecord;

		public $text;
		public $startDate;
		public $endDate;
		public $dateModified;
		public $fileTypes;
		public $libraries;
		public $superFilters;
		public $categories;
		public $hideDuplicated;
		public $searchByName;
		public $searchByContent;

		public $showResultsBar;

		public function __construct($linkRecord)
		{
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getSearchShortcut', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
		}

		public function loadSearchConditions()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/SearchCondition/Text');
			foreach ($queryResult as $node)
			{
				$this->text = trim($node->nodeValue);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/StartDate');
			foreach ($queryResult as $node)
			{
				$this->startDate = trim($node->nodeValue);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/EndDate');
			foreach ($queryResult as $node)
			{
				$this->endDate = trim($node->nodeValue);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/DateFileModified');
			foreach ($queryResult as $node)
			{
				$this->dateModified = filter_var(trim($node->nodeValue), FILTER_VALIDATE_BOOLEAN);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/FileType');
			foreach ($queryResult as $node)
				$this->fileTypes[] = trim($node->nodeValue);

			$queryResult = $xpath->query('//Config/SearchCondition/Library');
			if ($queryResult->length > 0)
				foreach ($queryResult as $node)
				{
					$libraryRecord = LibraryStorage::model()->find('name=?', array(trim($node->nodeValue)));
					if (isset($libraryRecord))
						$this->libraries[] = $libraryRecord->id;
				}
			else
			{
				$libraryRecords = LibraryStorage::model()->findAll();
				foreach ($libraryRecords as $libraryRecord)
					$this->libraries[] = $libraryRecord->id;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/SuperFilter');
			foreach ($queryResult as $node)
				$this->superFilters[] = trim($node->nodeValue);

			$queryResult = $xpath->query('//Config/SearchCondition/Categories/Category');
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

			$queryResult = $xpath->query('//Config/SearchCondition/HideDuplicated');
			foreach ($queryResult as $node)
			{
				$this->hideDuplicated = filter_var(trim($node->nodeValue), FILTER_VALIDATE_BOOLEAN);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/SearchByName');
			foreach ($queryResult as $node)
			{
				$this->searchByName = filter_var(trim($node->nodeValue), FILTER_VALIDATE_BOOLEAN);
				break;
			}

			$queryResult = $xpath->query('//Config/SearchCondition/SearchByContent');
			foreach ($queryResult as $node)
			{
				$this->searchByContent = filter_var(trim($node->nodeValue), FILTER_VALIDATE_BOOLEAN);
				break;
			}
			$showResultsBarTags = $linkConfig->getElementsByTagName("ShowResultsBar");
			$this->showResultsBar = $showResultsBarTags->length > 0 ? filter_var(trim($showResultsBarTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
		}
	}