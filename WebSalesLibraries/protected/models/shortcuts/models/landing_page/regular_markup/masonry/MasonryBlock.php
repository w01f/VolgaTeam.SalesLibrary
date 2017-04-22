<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class MasonryBlock
	 */
	class MasonryBlock extends BlockContainer
	{
		/** @var  MasonrySimpleSettings */
		public $viewSettings;

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
			$this->filters = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->viewSettings = MasonrySettings::fromXml(MasonrySettings::MasonryTypeSimple, $xpath, $contextNode);

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