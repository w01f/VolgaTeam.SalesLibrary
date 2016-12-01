<?php

	/**
	 * Class SearchBar
	 */
	class SearchBar implements IShortcutSearchOptionsContainer, IShortcutActionContainer
	{
		public $configured;

		public $alignment;
		public $title;
		public $defaultLabel;
		public $conditions;
		public $samePage;
		public $showTagsSelector;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;

		public $categoryManager;

		/**
		 * @param $shortcut BaseShortcut
		 * @return SearchBar
		 */
		public static function fromShortcut($shortcut)
		{
			$searchBar = self::createEmpty();

			$config = new DOMDocument();
			$config->loadXML($shortcut->linkRecord->config);
			$xpath = new DomXPath($config);
			$queryResult = $xpath->query('//SearchBar');
			$searchBar->configured = $queryResult->length > 0;
			$queryResult = $xpath->query('//SearchBar/Alignment');
			$searchBar->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
			$queryResult = $xpath->query('//SearchBar/Title');
			$searchBar->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/Defaultlabel');
			$searchBar->defaultLabel = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/OpenOnSamePage');
			$searchBar->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/ShowTagsSelector');
			$searchBar->showTagsSelector = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$xpath->query('//Config/SearchCondition');
			$searchBar->conditions = SearchConditions::fromXml($xpath, $xpath->query('//Config/SearchBar/SearchCondition')->item(0));

			$queryResult = $xpath->query('//SearchBar/EnableSubSearch');
			$searchBar->enableSubSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('//SearchBar/AllButtonVisible');
			$searchBar->showSubSearchAll = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/SearchButtonVisible');
			$searchBar->showSubSearchSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/LinksButtonVisible');
			$searchBar->showSubSearchTemplates = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/SubSearchDefault');
			$searchBar->subSearchDefaultView = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'all';

			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SearchBar/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, Yii::app()->getBaseUrl(true) . $shortcut->relativeLink);
			foreach ($subSearchConditions as $subSearchCondition)
				$subSearchCondition->image_path .= '?' . $shortcut->id;
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$searchBar->subConditions = $subSearchConditions;

			$searchBar->categoryManager = new CategoryManager();
			$searchBar->categoryManager->loadCategories();

			return $searchBar;
		}

		/**
		 * @return SearchBar
		 */
		public static function createEmpty()
		{
			$searchBar = new SearchBar();
			$searchBar->configured = false;
			return $searchBar;
		}

		public function getSearchOptions()
		{
			$options = new ShortcutsSearchOptions();
			$options->title = $this->title;
			$options->isSearchBar = true;
			$options->openInSamePage = $this->samePage;

			$options->enableSubSearch = $this->enableSubSearch;
			$options->showSubSearchAll = $this->showSubSearchAll;
			$options->showSubSearchSearch = $this->showSubSearchSearch;
			$options->showSubSearchTemplates = $this->showSubSearchTemplates;
			$options->subSearchDefaultView = $this->subSearchDefaultView;

			$options->conditions = $this->conditions;

			return $options;
		}

		/**
		 * @return ShortcutAction[]
		 */
		public function getActions()
		{
			$actions = array();
			$actionsByKey = ShortcutAction::getActionsByShortcutType('search');
			if (array_key_exists('sub-search-all', $actionsByKey))
				$actionsByKey['sub-search-all']->enabled = $this->enableSubSearch && $this->showSubSearchAll;
			if (array_key_exists('sub-search-criteria', $actionsByKey))
				$actionsByKey['sub-search-criteria']->enabled = $this->enableSubSearch && $this->showSubSearchSearch;
			if (array_key_exists('sub-search-links', $actionsByKey))
				$actionsByKey['sub-search-links']->enabled = $this->enableSubSearch && $this->showSubSearchTemplates;
			foreach ($actionsByKey as $action)
			{
				/** @var $action ShortcutAction */
				if ($action->enabled == true)
					$actions[] = $action;
			}
			$sortHelper = new ObjectSortHelper('order', 'asc');
			usort($actions, array($sortHelper, 'sort'));
			return $actions;
		}
	}