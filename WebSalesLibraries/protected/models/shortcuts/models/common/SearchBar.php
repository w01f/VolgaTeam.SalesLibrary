<?php
	use application\models\data_query\conditions\TableQueryConditions;

	/**
	 * Class SearchBar
	 */
	class SearchBar implements IShortcutSearchOptionsContainer, IShortcutActionContainer
	{
		public const HoverTipTagInput = "input";
		public const HoverTipTagActionButton = "action-button";
		public const HoverTipTagTagsButton = "tags-button";
		public const HoverTipTagOptionsButton = "options-button";

		public $id;

		public $configured;

		public $alignment;
		public $title;
		public $placeholder;
		public $label;
		public $samePage;
		public $showTagsSelector;
		public $defaultPageLength;
		public $hoverTips;
		public $hideFixedPanel;

		/** @var  TableQueryConditions */
		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;

		/** @var  SearchBarStyle */
		public $style;

		/** @var  CategoryManager */
		public $categoryManager;


		public function __construct()
		{
			$this->id = uniqid();
			$this->categoryManager = new CategoryManager();
			$this->style = new SearchBarStyle();
			$this->hoverTips = array();
		}

		/**
		 * @param $configEncoded string
		 * @param null string $subSearchTemplatesImagePath
		 */
		public function configureFromXml($configEncoded, $subSearchTemplatesImagePath = null)
		{
			$config = new DOMDocument();
			$config->loadXML($configEncoded);
			$xpath = new DomXPath($config);
			$queryResult = $xpath->query('//SearchBar');
			$this->configured = $queryResult->length > 0;
			if ($this->configured)
			{
				$rootNode = $queryResult->item(0);

				$queryResult = $xpath->query('./Alignment', $rootNode);
				$this->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
				$queryResult = $xpath->query('./Title', $rootNode);
				$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
				$queryResult = $xpath->query('./Placeholder', $rootNode);
				$this->placeholder = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'Type category or keyword here...';
				$queryResult = $xpath->query('./Label', $rootNode);
				$this->label = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
				$queryResult = $xpath->query('./OpenOnSamePage', $rootNode);
				$this->samePage = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				$queryResult = $xpath->query('./ShowTagsSelector', $rootNode);
				$this->showTagsSelector = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				$queryResult = $xpath->query('./DefaultPageLength');
				$this->defaultPageLength = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;
				$queryResult = $xpath->query('./HideFixedPanel', $rootNode);
				$this->hideFixedPanel = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;

				$queryResult = $xpath->query('./HoverTips/Placeholder', $rootNode);
				$this->hoverTips[self::HoverTipTagInput] = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : self::HoverTipTagInput;
				$queryResult = $xpath->query('./HoverTips/Tags', $rootNode);
				$this->hoverTips[self::HoverTipTagTagsButton] = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : self::HoverTipTagTagsButton;
				$queryResult = $xpath->query('./HoverTips/SearchOptions', $rootNode);
				$this->hoverTips[self::HoverTipTagOptionsButton] = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : self::HoverTipTagOptionsButton;
				$queryResult = $xpath->query('./HoverTips/SearchButton', $rootNode);
				$this->hoverTips[self::HoverTipTagActionButton] = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : self::HoverTipTagActionButton;

				$queryResult = $xpath->query('./SearchCondition', $rootNode);
				$this->conditions = TableQueryConditions::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('./EnableSubSearch', $rootNode);
				$this->enableSubSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
				$queryResult = $xpath->query('./AllButtonVisible', $rootNode);
				$this->showSubSearchAll = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				$queryResult = $xpath->query('./SearchButtonVisible', $rootNode);
				$this->showSubSearchSearch = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				$queryResult = $xpath->query('./LinksButtonVisible', $rootNode);
				$this->showSubSearchTemplates = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
				$queryResult = $xpath->query('./SubSearchDefault', $rootNode);
				$this->subSearchDefaultView = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'all';

				$queryResult = $xpath->query('./Style', $rootNode);
				if ($queryResult->length > 0)
					$this->style->configureFromXml($xpath, $queryResult->item(0));

				$subSearchConditions = array();
				$subSearchConditionNodes = $xpath->query('./SubSearchCondition/Item', $rootNode);
				foreach ($subSearchConditionNodes as $conditionNode)
					$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, $subSearchTemplatesImagePath);
				$sortHelper = new ObjectSortHelper('imageName', 'asc');
				usort($subSearchConditions, array($sortHelper, 'sort'));
				$this->subConditions = $subSearchConditions;
			}
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
			$options->defaultPageLength = $this->defaultPageLength;

			$options->enableSubSearch = $this->enableSubSearch;
			$options->showSubSearchAll = $this->showSubSearchAll;
			$options->showSubSearchSearch = $this->showSubSearchSearch;
			$options->showSubSearchTemplates = $this->showSubSearchTemplates;
			$options->subSearchDefaultView = $this->subSearchDefaultView;
			$options->hideFixedPanel = $this->hideFixedPanel;

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