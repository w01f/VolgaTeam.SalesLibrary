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
		public $samePage;
		public $showTagsSelector;

		/** @var  TableSearchConditions */
		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;

		public $buttonColor;

		/** @var  CategoryManager */
		public $categoryManager;


		public function __construct()
		{
			$this->categoryManager = new CategoryManager();
		}

		/**
		 * @param $configEncoded string
		 * @param null string $subSearchTemplatesImagePath
		 */
		protected function configureFromXml($configEncoded, $subSearchTemplatesImagePath = null)
		{
			$config = new DOMDocument();
			$config->loadXML($configEncoded);
			$xpath = new DomXPath($config);
			$queryResult = $xpath->query('//SearchBar');
			$this->configured = $queryResult->length > 0;
			$queryResult = $xpath->query('//SearchBar/Alignment');
			$this->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
			$queryResult = $xpath->query('//SearchBar/Title');
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/Defaultlabel');
			$this->defaultLabel = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('//SearchBar/OpenOnSamePage');
			$this->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$queryResult = $xpath->query('//SearchBar/ShowTagsSelector');
			$this->showTagsSelector = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('//SearchBar/SearchCondition');
			$this->conditions = TableSearchConditions::fromXml($xpath, $queryResult->item(0));

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

			$queryResult = $xpath->query('//SearchBar/ButtonColor');
			$this->buttonColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SearchBar/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, $subSearchTemplatesImagePath);
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$this->subConditions = $subSearchConditions;
		}

		/**
		 * @param $shortcut BaseShortcut
		 * @return SearchBar
		 */
		public static function fromShortcut($shortcut)
		{
			$searchBar = self::createEmpty();
			$searchBar->configureFromXml($shortcut->linkRecord->config, Yii::app()->getBaseUrl(true) . $shortcut->relativeLink);
			return $searchBar;
		}

		/**
		 * @param $internalLinkInfo InternalLibraryContentPreviewInfo
		 * @return SearchBar
		 */
		public static function fromInternalLink($internalLinkInfo)
		{
			$searchBar = self::createEmpty();
			$searchBar->configureFromXml($internalLinkInfo->styleSettingsEncoded);
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