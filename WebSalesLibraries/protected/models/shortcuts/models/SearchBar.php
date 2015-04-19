<?php

	/**
	 * Class SearchBar
	 */
	class SearchBar implements IShortcutSearchOptionsContainer
	{
		public $configured;
		public $alignment;
		public $title;
		public $defaultLabel;
		public $conditions;
		public $showResultsBar;
		public $samePage;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;

		public $categoryManager;

		/**
		 * @param $page ShortcutsPageRecord
		 */
		public function __construct($page)
		{
			$config = new DOMDocument();
			$config->loadXML($page->config);
			$xpath = new DomXPath($config);
			$queryResult = $xpath->query('//SearchBar');
			$this->configured = $queryResult->length > 0;
			$queryResult = $xpath->query('//SearchBar/Alignment');
			$this->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
			$queryResult = $xpath->query('//SearchBar/Title');
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/Defaultlabel');
			$this->defaultLabel = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/ShowResultsBar');
			$this->showResultsBar = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/OpenOnSamePage');
			$this->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$xpath->query('//Config/SearchCondition');
			$this->conditions = SearchConditions::fromXml($xpath, $xpath->query('//Config/SearchBar/SearchCondition')->item(0));

			$queryResult = $xpath->query('//SearchBar/EnableSubSearch');
			$this->enableSubSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('//SearchBar/AllButtonVisible');
			$this->showSubSearchAll = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/SearchButtonVisible');
			$this->showSubSearchSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/LinksButtonVisible');
			$this->showSubSearchTemplates = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/SubSearchDefault');
			$this->subSearchDefaultView = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'all';

			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SearchBar/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, Yii::app()->getBaseUrl(true) . $page->source_path);
			foreach ($subSearchConditions as $subSearchCondition)
				$subSearchCondition->image_path .= '?' . $page->id;
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$this->subConditions = $subSearchConditions;

			$this->categoryManager = new CategoryManager();
			$this->categoryManager->loadCategories();
		}

		public function getSearchOptions()
		{
			$options = new ShortcutsSearchOptions();
			$options->title = $this->title;
			$options->isPage = true;
			$options->openInSamePage = $this->samePage;

			$options->enableSubSearch = $this->enableSubSearch;
			$options->showSubSearchAll = $this->showSubSearchAll;
			$options->showSubSearchSearch = $this->showSubSearchSearch;
			$options->showSubSearchTemplates = $this->showSubSearchTemplates;
			$options->subSearchDefaultView = $this->subSearchDefaultView;

			$options->conditions = $this->conditions;

			return $options;
		}
	}