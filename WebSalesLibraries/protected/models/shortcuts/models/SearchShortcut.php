<?php

	/**
	 * Class SearchShortcut
	 */
	class SearchShortcut
	{
		public $id;
		public $type;
		public $name;
		public $title;
		public $tooltip;
		public $imagePath;
		public $ribbonLogoPath;
		public $sourceLink;
		public $samePage;
		public $showResultsBar;

		public $conditions;

		public $enableSubSearch;
		public $showSubSearchAll;
		public $showSubSearchSearch;
		public $showSubSearchTemplates;
		public $subSearchDefaultView;
		public $subConditions;

		private $linkRecord;

		/**
		 * @param $linkRecord
		 */
		public function __construct($linkRecord)
		{
			$this->id = $linkRecord->id;
			$this->linkRecord = $linkRecord;
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$this->type = trim($linkConfig->getElementsByTagName("Type")->item(0)->nodeValue);
			$samePageTags = $linkConfig->getElementsByTagName("OpenOnSamePage");
			$this->samePage = $samePageTags->length > 0 ? filter_var(trim($samePageTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$titleTags = $linkConfig->getElementsByTagName("Title");
			$this->title = $titleTags->length > 0 ? trim($titleTags->item(0)->nodeValue) : '';
			$nameTags = $linkConfig->getElementsByTagName("line1");
			$this->name = $nameTags->length > 0 ? trim($nameTags->item(0)->nodeValue) : '';
			$tooltipTags = $linkConfig->getElementsByTagName("line2");
			$this->tooltip = $tooltipTags->length > 0 ? trim($tooltipTags->item(0)->nodeValue) : '';
			$baseUrl = Yii::app()->getBaseUrl(true);
			$this->imagePath = $baseUrl . $linkRecord->image_path . '?' . $linkRecord->id;
			$this->ribbonLogoPath = $baseUrl . $linkRecord->source_path . '/link_logo.png' . '?' . $linkRecord->id;
			$this->sourceLink = Yii::app()->createAbsoluteUrl('shortcuts/getSearchShortcut', array('linkId' => $linkRecord->id, 'samePage' => Yii::app()->browser->isMobile() ? true : $this->samePage));
			$showResultsBarTags = $linkConfig->getElementsByTagName("ShowResultsBar");
			$this->showResultsBar = $showResultsBarTags->length > 0 ? filter_var(trim($showResultsBarTags->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$xpath = new DomXPath($linkConfig);
			$this->conditions = new SearchConditions($xpath, $xpath->query('//Config/SearchCondition')->item(0));

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

			$this->subConditions = array();
			$subSearchConditions = $xpath->query('//Config/SubSearchCondition/Item');
			foreach ($subSearchConditions as $conditionNode)
				$this->subConditions[] = new SubSearchTemplate($xpath, $conditionNode, $baseUrl . $linkRecord->source_path);
			foreach ($this->subConditions as $subSearchCondition)
				$subSearchCondition->image_path .= '?' . $linkRecord->id;
		}
	}