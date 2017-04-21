<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class MasonryBlock
	 */
	class MasonryBlock extends BlockContainer
	{
		/** @var  \Padding */
		public $itemsPadding;

		public $enableCaptionZoom;
		public $captionZoomScale;

		/** @var  MasonryFilter[] */
		public $filters;

		/** @var  MasonryFilter */
		public $defaultFilter;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'masonry';
			$this->itemsPadding = new \Padding(0);
			$this->enableCaptionZoom = true;
			$this->captionZoomScale = 1.25;
			$this->filters = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ImagePadding', $contextNode);
			if ($queryResult->length > 0)
				$this->itemsPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./EnableCaptionZoom', $contextNode);
			$this->enableCaptionZoom = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enableCaptionZoom;

			$queryResult = $xpath->query('./CaptionZoomScale', $contextNode);
			$this->captionZoomScale = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->captionZoomScale;

			$queryResult = $xpath->query('./Filter/Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$filter = new MasonryFilter();
				$filter->configureFromXml($xpath, $node);
				$this->filters[] = $filter;
				if ($filter->isDefault)
					$this->defaultFilter = $filter;
			}

			if ($this->isAccessGranted)
			{
				$queryResult = $xpath->query('./ContentBlock', $contextNode);
				foreach ($queryResult as $node)
				{
					$typeAttribute = $node->attributes->getNamedItem("Type");
					$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
					$contentBlock = null;
					switch ($type)
					{
						case "url":
							$contentBlock = new MasonryUrl($this->parentShortcut, $this);
							$contentBlock->configureFromXml($xpath, $node);
							break;
						case "shortcut":
							$contentBlock = new MasonryShortcut($this->parentShortcut, $this);
							$contentBlock->configureFromXml($xpath, $node);
							break;
					}
					if (isset($contentBlock) && $contentBlock->isAccessGranted)
						$this->items[] = $contentBlock;
				}
			}
		}
	}