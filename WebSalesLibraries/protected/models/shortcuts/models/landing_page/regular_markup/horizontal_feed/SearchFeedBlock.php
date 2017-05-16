<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SearchFeedQuerySettings;
	use application\models\feeds\horizontal\FeedSettings;
	use application\models\feeds\horizontal\SearchFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SearchFeedBlock
	 */
	class SearchFeedBlock extends BlockContainer
	{
		/** @var  SearchFeedQuerySettings */
		public $querySettings;

		/** @var  SearchFeedSettings */
		public $viewSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'search-feed';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);
			$this->querySettings = LinkFeedQuerySettings::fromXml(LinkFeedQuerySettings::FeedTypeSearch, $xpath, $contextNode);
			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeSearch, $xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}
	}