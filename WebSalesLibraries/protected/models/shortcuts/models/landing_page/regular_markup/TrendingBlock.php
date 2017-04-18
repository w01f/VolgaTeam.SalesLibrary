<?

	namespace application\models\shortcuts\models\landing_page\regular_markup;

	use application\models\link_feed\LinkFeedItem;
	use application\models\link_feed\LinkFeedManager;
	use application\models\link_feed\LinkFeedSettings;
	use application\models\link_feed\TrendingFeedSettings;

	/**
	 * Class TrendingBlock
	 */
	class TrendingBlock extends BlockContainer
	{
		/** @var  TrendingFeedSettings */
		public $settings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'trending';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);
			$this->settings = LinkFeedSettings::fromXml(LinkFeedSettings::FeedTypeTrending, $xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedManager::queryFeedItems($this->settings);
		}
	}