<?php

	/**
	 * Class SearchBar
	 */
	class SearchBar
	{
		public $configured;
		public $alignment;
		public $title;
		public $defaultLabel;
		public $conditions;
		public $showResultsBar;
		public $samePage;

		public $categoryManager;

		/**
		 * @param $configContext
		 */
		public function __construct($configContext)
		{
			$config = new DOMDocument();
			$config->loadXML($configContext);
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
			$this->conditions = new SearchConditions($configContext);

			$this->categoryManager = new CategoryManager();
			$this->categoryManager->loadCategories();
		}
	}