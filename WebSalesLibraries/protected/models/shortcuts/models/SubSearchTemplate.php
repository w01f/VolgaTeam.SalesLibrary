<?php

	/**
	 * Class SubSearchTemplate
	 */
	class SubSearchTemplate
	{
		public $tooltip;
		public $imagePath;
		public $conditions;

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @param $imagePath string
		 */
		public function __construct($xpath, $contextNode, $imagePath)
		{
			$queryResult = $xpath->query('Image', $contextNode);
			$this->imagePath = $queryResult->length > 0 ? ($imagePath . '/template_images/' . strtolower(trim($queryResult->item(0)->nodeValue))) : '';
			$queryResult = $xpath->query('ToolTip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('SearchCondition', $contextNode);
			if ($queryResult->length > 0)
				$this->conditions = new SearchConditions($xpath, $queryResult->item(0));
		}
	}