<?php

	/**
	 * Class SubSearchTemplate
	 */
	class SubSearchTemplate
	{
		public $tooltip;
		public $imageName;
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
			$this->imageName = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
			$this->imagePath = isset($this->imageName) ? ($imagePath . '/template_images/' . $this->imageName) : '';
			$queryResult = $xpath->query('ToolTip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			$queryResult = $xpath->query('SearchCondition', $contextNode);
			if ($queryResult->length > 0)
				$this->conditions = new SearchConditions($xpath, $queryResult->item(0));
		}
	}