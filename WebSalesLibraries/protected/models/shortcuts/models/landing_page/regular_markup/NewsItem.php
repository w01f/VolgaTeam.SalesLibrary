<?
	namespace application\models\shortcuts\models\landing_page\regular_markup;
	/**
	 * Class NewsItem
	 */
	class NewsItem extends BlockContainer
	{
		public $id;
		/** @var  ImageBlock */
		public $imageSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'news-item';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./ContentBlock', $contextNode);
			foreach ($queryResult as $node)
			{
				$contentBlock = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
				if ($contentBlock->type == 'image')
					$this->imageSettings = $contentBlock;
				else
					$this->items[] = $contentBlock;
			}
		}
	}