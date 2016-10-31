<?php

	/**
	 * Class SearchLinkShortcut
	 */
	class SearchLinkShortcut extends PageContentShortcut implements IShortcutSearchOptionsContainer
	{
		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;
		public $subSearchBar;
		public $conditionNotMatchLogoPath;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

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

			parent::__construct($linkRecord, $isPhone);

			$baseUrl = Yii::app()->getBaseUrl(true);
			$noCatsCustomImagePath = $linkRecord->source_path . DIRECTORY_SEPARATOR . 'no_cats.png';
			if (isset($noCatsCustomImagePath) && @getimagesize($noCatsCustomImagePath))
				$this->conditionNotMatchLogoPath = $baseUrl . str_replace('\\', '/', str_replace(ShortcutsManager::getShortcutsRoot(), '', $noCatsCustomImagePath)) . '?' . $linkRecord->id_group . $linkRecord->id;
			else
				$this->conditionNotMatchLogoPath = $baseUrl . '/images/shortcuts/no_cats.png' . '?' . $linkRecord->id_group . $linkRecord->id;

			$xpath = new DomXPath($linkConfig);
			$this->conditions = SearchConditions::fromXml($xpath, $xpath->query('//Config/SearchCondition')->item(0));

			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, $baseUrl . $this->relativeLink);
			foreach ($subSearchConditions as $subSearchCondition)
				$subSearchCondition->image_path .= '?' . $linkRecord->id_group . $linkRecord->id;
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$this->subConditions = $subSearchConditions;

			$this->subSearchBar = new SearchBar($this);
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