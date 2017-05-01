<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\vertical_feed;

	use application\models\feeds\vertical\FeedSettings;
	use application\models\feeds\vertical\SimpleFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SimpleFeedBlock
	 */
	class SimpleFeedBlock extends BlockContainer
	{
		/** @var  SimpleFeedSettings */
		public $viewSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'news';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeSimpleSlider, $xpath, $contextNode);

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
							$contentBlock = new SimpleFeedUrlItem($this->parentShortcut, $this);
							$contentBlock->configureFromXml($xpath, $node);
							break;
						case "shortcut":
							$contentBlock = new SimpleFeedShortcutItem($this->parentShortcut, $this);
							$contentBlock->configureFromXml($xpath, $node);
							break;
						default:
							$contentBlock = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
							break;
					}
					if (isset($contentBlock) && $contentBlock->isAccessGranted)
						$this->items[] = $contentBlock;
				}
			}
		}
	}