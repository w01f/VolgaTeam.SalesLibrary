<?php
	class PageGrid
	{
		public $configured;
		public $alignment;
		public $columnsCount;
		public $paddingTop;

		public function __construct($pageRecord)
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
			$queryResult = $xpath->query('//Config/Grid/TopPad');
			$this->paddingTop = $queryResult->length > 0 ? (int)trim($queryResult->item(0)->nodeValue) : 0;
		}
	}