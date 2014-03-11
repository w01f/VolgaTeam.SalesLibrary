<?php
	class PageGrid
	{
		public $configured;
		public $alignment;
		public $columnsCount;
		public $paddingTop;
		public $dividerWidth;

		public function __construct($pageRecord)
		{
			if (!Yii::app()->browser->isMobile())
			{
				$pageConfig = new DOMDocument();
				$pageConfig->loadXML($pageRecord->config);
				$xpath = new DomXPath($pageConfig);
				$queryResult = $xpath->query('//Config/Grid');
				$this->configured = $queryResult->length > 0;
				$queryResult = $xpath->query('//Config/Grid/Alignment');
				$this->alignment = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : 'left';
				$queryResult = $xpath->query('//Config/Grid/Columns');
				$this->columnsCount = $queryResult->length > 0 ? (int)trim($queryResult->item(0)->nodeValue) : 3;
				$queryResult = $xpath->query('//Config/Grid/TopMargin');
				$this->paddingTop = $queryResult->length > 0 ? (int)trim($queryResult->item(0)->nodeValue) : 0;
				$queryResult = $xpath->query('//Config/Grid/DividerLines');
				$this->dividerWidth = $queryResult->length > 0 ? (int)trim($queryResult->item(0)->nodeValue) : 0;
			}
			else
			{
				$this->configured = true;
				$this->alignment = 'center';
				$this->columnsCount = 4;
				$this->paddingTop = 0;
			}
		}
	}