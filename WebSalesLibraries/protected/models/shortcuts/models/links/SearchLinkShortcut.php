<?php
	use application\models\data_query\conditions\TableQueryConditions;

	/**
	 * Class SearchLinkShortcut
	 */
	class SearchLinkShortcut extends PageContentShortcut implements IShortcutSearchOptionsContainer
	{
		/** @var  TableQueryConditions */
		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;

		/** @var  SubSearchTemplate[] */
		public $subConditions;
		/** @var  SearchBar */
		public $subSearchBar;

		public $conditionNotMatchLogoPath;

		public $defaultPageLength;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$this->subConditions = array();
			$this->subSearchBar = SearchBar::createEmpty();
		}

		public function loadPageConfig()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);

			$enableSubSearchTags = $linkConfig->getElementsByTagName("EnableSubSearch");
			$this->enableSubSearch = $enableSubSearchTags->length > 0 ? filter_var(trim($enableSubSearchTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$showSubSearchAllTags = $linkConfig->getElementsByTagName("AllButtonVisible");
			$this->showSubSearchAll = $showSubSearchAllTags->length > 0 ? filter_var(trim($showSubSearchAllTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$showSubSearchSearchTags = $linkConfig->getElementsByTagName("SearchButtonVisible");
			$this->showSubSearchSearch = $showSubSearchSearchTags->length > 0 ? filter_var(trim($showSubSearchSearchTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$showSubSearchLinksTags = $linkConfig->getElementsByTagName("LinksButtonVisible");
			$this->showSubSearchTemplates = $showSubSearchLinksTags->length > 0 ? filter_var(trim($showSubSearchLinksTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			$subSearchDefaultViewTags = $linkConfig->getElementsByTagName("SubSearchDefault");
			$this->subSearchDefaultView = $subSearchDefaultViewTags->length > 0 ? strtolower(trim($subSearchDefaultViewTags->item(0)->nodeValue)) : 'all';

			parent::loadPageConfig();

			$baseUrl = Yii::app()->getBaseUrl(true);
			$noCatsCustomImagePath = $this->linkRecord->source_path . DIRECTORY_SEPARATOR . 'no_cats.png';
			if (isset($noCatsCustomImagePath) && @getimagesize($noCatsCustomImagePath))
				$this->conditionNotMatchLogoPath = $baseUrl . str_replace('\\', '/', str_replace(ShortcutsManager::getShortcutsRootPath(), '', $noCatsCustomImagePath)) . '?' . $this->linkRecord->id_group . $this->linkRecord->id;
			else
				$this->conditionNotMatchLogoPath = $baseUrl . '/images/shortcuts/no_cats.png' . '?' . $this->linkRecord->id_group . $this->linkRecord->id;

			$xpath = new DomXPath($linkConfig);
			$this->conditions = TableQueryConditions::fromXml($xpath, $xpath->query('//Config/SearchCondition')->item(0));

			/** @var SubSearchTemplate[] $subSearchConditions */
			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, $baseUrl . $this->relativeLink);
			foreach ($subSearchConditions as $subSearchCondition)
				$subSearchCondition->imagePath .= '?' . $this->linkRecord->id_group . $this->linkRecord->id;
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$this->subConditions = $subSearchConditions;

			$this->subSearchBar = SearchBar::fromShortcut($this);

			$queryResult = $xpath->query('//Config/DefaultPageLength');
			$this->defaultPageLength = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : null;
		}

		/**
		 * @return ShortcutsSearchOptions
		 */
		public function getSearchOptions()
		{
			$options = new ShortcutsSearchOptions();
			$options->title = $this->title;
			$options->isSearchBar = false;
			$options->openInSamePage = $this->samePage;

			$options->enableSubSearch = $this->enableSubSearch;
			$options->showSubSearchAll = $this->showSubSearchAll;
			$options->showSubSearchSearch = $this->showSubSearchSearch && $this->subSearchBar->configured;
			$options->showSubSearchTemplates = $this->showSubSearchTemplates && count($this->subConditions) > 0;
			$options->subSearchDefaultView = $this->subSearchDefaultView;
			$options->defaultPageLength = $this->defaultPageLength;
			$options->hideFixedPanel = false;

			$options->emptyResultLogo = $this->conditionNotMatchLogoPath;

			$options->conditions = $this->conditions;

			return $options;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Search';
		}

		/**
		 * @return string
		 */
		public function getTitleForActivityTracker()
		{
			if ($this->isPhone)
				return 'Mobile Search Shortcut ' . parent::getTitleForActivityTracker();
			return parent::getTitleForActivityTracker();
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('sub-search-all', $actionsByKey))
				$actionsByKey['sub-search-all']->enabled = $actionsByKey['sub-search-all']->enabled && $this->enableSubSearch && $this->showSubSearchAll;
			if (array_key_exists('sub-search-criteria', $actionsByKey))
				$actionsByKey['sub-search-criteria']->enabled = $actionsByKey['sub-search-criteria']->enabled && $this->enableSubSearch && $this->showSubSearchSearch;
			if (array_key_exists('sub-search-links', $actionsByKey))
				$actionsByKey['sub-search-links']->enabled = $actionsByKey['sub-search-links']->enabled && $this->enableSubSearch && $this->showSubSearchTemplates;
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['searchOptions'] = $this->getSearchOptions();
			return $data;
		}
	}