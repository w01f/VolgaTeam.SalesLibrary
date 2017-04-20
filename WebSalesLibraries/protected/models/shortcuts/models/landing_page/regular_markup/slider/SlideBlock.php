<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\slider;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ImageBlock;

	/**
	 * Class SlideBlock
	 */
	class SlideBlock extends BlockContainer
	{
		/** @var  ImageBlock */
		public $imageSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'slide';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			if ($this->isAccessGranted)
			{
				$queryResult = $xpath->query('./ContentBlock', $contextNode);
				foreach ($queryResult as $node)
				{
					$contentBlock = ContentBlock::fromXml($this->parentShortcut, $this, $xpath, $node);
					if ($contentBlock->type == 'image')
						$this->imageSettings = $contentBlock;
					else if ($contentBlock->isAccessGranted)
						$this->items[] = $contentBlock;
				}
			}
		}
	}