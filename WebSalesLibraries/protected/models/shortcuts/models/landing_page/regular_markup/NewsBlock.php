<?

	namespace application\models\shortcuts\models\landing_page\regular_markup;
	use application\models\news_block\NewsBlockSettings;

	/**
	 * Class NewsBlock
	 */
	class NewsBlock extends BlockContainer
	{
		/** @var  NewsBlockSettings */
		public $settings;

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

			$this->settings = NewsBlockSettings::fromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
			{
				$typeAttribute = $node->attributes->getNamedItem("Type");
				$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
				$contentBlock = null;
				switch ($type)
				{
					case "url":
						$contentBlock = new NewsUrlItem($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
					case "shortcut":
						$contentBlock = new NewsShortcutItem($this->parentShortcut, $this);
						$contentBlock->configureFromXml($xpath, $node);
						break;
					default:
						$contentBlock = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
						break;
				}
				if (isset($contentBlock))
					$this->items[] = $contentBlock;
			}
		}
	}