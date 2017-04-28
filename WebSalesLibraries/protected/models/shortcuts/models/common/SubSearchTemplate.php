<?php
	use application\models\data_query\conditions\TableQueryConditions;

	/**
	 * Class SubSearchTemplate
	 */
	class SubSearchTemplate implements IShortcutSearchOptionsContainer
	{
		public $tooltip;
		public $disabled;
		public $imageName;
		public $imagePath;

		/** @var TableQueryConditions */
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
			$queryResult = $xpath->query('Disabled', $contextNode);
			$this->disabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
			$queryResult = $xpath->query('SearchCondition', $contextNode);
			if ($queryResult->length > 0)
				$this->conditions = TableQueryConditions::fromXml($xpath, $queryResult->item(0));
		}

		public function getSearchOptions()
		{
			$options = new ShortcutsSearchOptions();
			$options->isSearchBar = false;
			$options->openInSamePage = true;
			$options->conditions = $this->conditions;
			return $options;
		}
	}