<?

	/**
	 * Class NavigationPanel
	 */
	class NavigationPanel
	{
		public $id;

		public $textColor;
		public $textSize;

		public $imagePadding;

		public $itemsGapExpanded;
		public $backColorExpanded;
		public $hoverColorExpanded;
		public $dividerWidthExpanded;
		public $dividerColorExpanded;

		public $itemsGapCollapsed;
		public $backColorCollapsed;
		public $hoverColorCollapsed;
		public $dividerWidthCollapsed;
		public $dividerColorCollapsed;

		public $showScroll;

		/** @var  HideCondition */
		public $hideCondition;

		/** @var  BaseNavigationItem[] */
		public $items;

		/**
		 * @param $xpath DOMXPath
		 * @param $imagePath string
		 * @return NavigationPanel
		 */
		public static function fromXml($xpath, $imagePath)
		{
			$navigationPanel = new NavigationPanel();

			$queryResult = $xpath->query('//Config/Appearance/TextSize');
			$navigationPanel->textSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 12;
			$queryResult = $xpath->query('//Config/Appearance/TextColor');
			$navigationPanel->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'eee';

			$queryResult = $xpath->query('//Config/Appearance/ImagePadding');
			$navigationPanel->imagePadding = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('//Config/Appearance/FitInScroll');
			$navigationPanel->showScroll = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$queryResult = $xpath->query('//Config/Appearance/Panel/Span');
			$navigationPanel->itemsGapExpanded = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/Panel/BackColor');
			$navigationPanel->backColorExpanded = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'ffffff';
			$queryResult = $xpath->query('//Config/Appearance/Panel/HoverColor');
			$navigationPanel->hoverColorExpanded = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'c6e2ff';
			$queryResult = $xpath->query('//Config/Appearance/Panel/DividerWidth');
			$navigationPanel->dividerWidthExpanded = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/Panel/DividerColor');
			$navigationPanel->dividerColorExpanded = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '000000';

			$queryResult = $xpath->query('//Config/Appearance/Bar/Span');
			$navigationPanel->itemsGapCollapsed = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/Bar/BackColor');
			$navigationPanel->backColorCollapsed = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'ffffff';
			$queryResult = $xpath->query('//Config/Appearance/Bar/HoverColor');
			$navigationPanel->hoverColorCollapsed = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'c6e2ff';
			$queryResult = $xpath->query('//Config/Appearance/Bar/DividerWidth');
			$navigationPanel->dividerWidthCollapsed = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/Bar/DividerColor');
			$navigationPanel->dividerColorCollapsed = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '000000';

			$queryResult = $xpath->query('//Config/Appearance/Hide');
			if ($queryResult->length > 0)
				$navigationPanel->hideCondition = HideCondition::fromXml($xpath, $queryResult->item(0));
			else
				$navigationPanel->hideCondition = new HideCondition();

			$navigationPanel->items = array();
			$queryResult = $xpath->query('//Config/Items/Item');
			foreach ($queryResult as $itemConfigNode)
			{
				$navigationItem = BaseNavigationItem::fromXml($navigationPanel, $xpath, $itemConfigNode, $imagePath);
				if (isset($navigationItem))
					$navigationPanel->items[] = $navigationItem;
			}

			return $navigationPanel;
		}
	}