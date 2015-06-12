<?php

	/**
	 * Class SearchShortcut
	 */
	class SearchShortcut extends BaseShortcut implements IShortcutSearchOptionsContainer
	{
		public $ribbonLogoPath;
		public $showResultsBar;

		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;
		public $conditionNotMatchLogoPath;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			parent::__construct($linkRecord);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);

			$this->viewPath = 'directLink';

			if (!Yii::app()->browser->isMobile())
			{
				$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
				$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			}
			else
			{
				$this->samePage = true;
			}

			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/rbnlogo.png' . '?' . $linkRecord->id_page . $linkRecord->id;

			$noCatsCustomImagePath = $baseUrl . $linkRecord->source_path . '/no_cats.png' . '?' . $linkRecord->id_page . $linkRecord->id;
			if (isset($noCatsCustomImagePath) && @getimagesize($noCatsCustomImagePath))
				$this->conditionNotMatchLogoPath = $noCatsCustomImagePath;
			else
				$this->conditionNotMatchLogoPath = $baseUrl . '/images/shortcuts/no_cats.png' . '?' . $linkRecord->id_page . $linkRecord->id;

			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getSearchShortcut', array('linkId' => $linkRecord->id, 'samePage' => $this->samePage));
			$showResultsBarTags = $linkConfig->getElementsByTagName("ShowResultsBar");
			$this->showResultsBar = $showResultsBarTags->length > 0 ? filter_var(trim($showResultsBarTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$xpath = new DomXPath($linkConfig);
			$this->conditions = SearchConditions::fromXml($xpath, $xpath->query('//Config/SearchCondition')->item(0));

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

			$subSearchConditions = array();
			$subSearchConditionNodes = $xpath->query('//Config/SubSearchCondition/Item');
			foreach ($subSearchConditionNodes as $conditionNode)
				$subSearchConditions[] = new SubSearchTemplate($xpath, $conditionNode, $baseUrl . $linkRecord->source_path);
			foreach ($subSearchConditions as $subSearchCondition)
				$subSearchCondition->image_path .= '?' . $linkRecord->id_page . $linkRecord->id;
			$sortHelper = new ObjectSortHelper('imageName', 'asc');
			usort($subSearchConditions, array($sortHelper, 'sort'));
			$this->subConditions = $subSearchConditions;
		}

		/**
		 * @return string
		 */
		public function getServiceData()
		{
			$result = '';
			if (isset($this->ribbonLogoPath) && @getimagesize($this->ribbonLogoPath))
				$result .= '<div class="ribbon-logo-path">' . $this->ribbonLogoPath . '</div>';
			$result .= '<div class="link-id">' . $this->id . '</div>';
			$result .= '<div class="link-type">' . $this->type . '</div>';
			$result .= '<div class="link-name">' . $this->name . ' - ' . $this->tooltip . '</div>';
			$result .= '<div class="url">' . $this->sourceLink . '</div>';
			$result .= '<div class="activity-data">' . CJSON::encode(array(
					'action' => 'Shortcut Search Link',
					'title' => sprintf('%s - %s', $this->name, $this->tooltip)
				)) . '</div>';
			//For Mobile Version only
			$result .= '<div class="link-header">' . $this->mobileHeader . '</div>';
			//-----------------------
			return $result;
		}

		public function getSearchOptions()
		{
			$options = new ShortcutsSearchOptions();
			$options->title = $this->title;
			$options->isPage = false;
			$options->openInSamePage = $this->samePage;

			$options->enableSubSearch = $this->enableSubSearch;
			$options->showSubSearchAll = $this->showSubSearchAll;
			$options->showSubSearchSearch = $this->showSubSearchSearch;
			$options->showSubSearchTemplates = $this->showSubSearchTemplates;
			$options->subSearchDefaultView = $this->subSearchDefaultView;

			$options->emptyResultLogo = $this->conditionNotMatchLogoPath;

			$options->conditions = $this->conditions;

			return $options;
		}
	}