<?

	/**
	 * Class NavigationPanel
	 */
	class NavigationPanel
	{
		public $itemsGap;
		public $textSize;
		public $textColor;
		public $hoverColor;
		public $backColor;
		public $imagePadding;
		public $dividerWidth;
		public $dividerColor;

		public $showScroll;

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

			$queryResult = $xpath->query('//Config/Appearance/Span');
			$navigationPanel->itemsGap = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/TextSize');
			$navigationPanel->textSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 12;
			$queryResult = $xpath->query('//Config/Appearance/TextColor');
			$navigationPanel->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '000000';
			$queryResult = $xpath->query('//Config/Appearance/BackColor');
			$navigationPanel->backColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'ffffff';
			$queryResult = $xpath->query('//Config/Appearance/HoverColor');
			$navigationPanel->hoverColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : 'c6e2ff';
			$queryResult = $xpath->query('//Config/Appearance/ImagePadding');
			$navigationPanel->imagePadding = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/DividerWidth');
			$navigationPanel->dividerWidth = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;
			$queryResult = $xpath->query('//Config/Appearance/DividerColor');
			$navigationPanel->dividerColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '000000';
			$queryResult = $xpath->query('//Config/Appearance/FitInScroll');
			$navigationPanel->showScroll = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;

			$navigationPanel->items = array();
			$queryResult = $xpath->query('//Config/Items/Item');
			foreach ($queryResult as $itemConfigNode)
			{
				$navigationItem = BaseNavigationItem::fromXml($xpath, $itemConfigNode, $imagePath);
				if (isset($navigationItem))
					$navigationPanel->items[] = $navigationItem;
			}

			return $navigationPanel;
		}
	}