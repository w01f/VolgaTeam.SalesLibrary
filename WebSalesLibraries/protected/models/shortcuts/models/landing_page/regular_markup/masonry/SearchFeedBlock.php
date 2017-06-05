<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SearchFeedQuerySettings;
	use application\models\feeds\common\SimpleDetailsSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SearchFeedBlock
	 */
	class SearchFeedBlock extends ContentBlock
	{
		/** @var  SearchFeedQuerySettings */
		public $querySettings;

		/** @var  SearchFeedSettings */
		public $viewSettings;

		/** @var  SimpleDetailsSettings */
		public $detailsSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'search-feed-masonry';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->querySettings = LinkFeedQuerySettings::fromXml(LinkFeedQuerySettings::FeedTypeSearch, $xpath, $contextNode);
			$this->viewSettings = MasonrySettings::fromXml(MasonrySettings::MasonryTypeSearch, $xpath, $contextNode);
			$this->detailsSettings = SimpleDetailsSettings::fromXml($xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}
	}