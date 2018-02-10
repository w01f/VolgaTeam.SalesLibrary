<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\TrendingDetailsSettings;
	use application\models\feeds\horizontal\FeedSettings;
	use application\models\feeds\horizontal\TrendingFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class TrendingBlock
	 */
	class TrendingBlock extends BlockContainer
	{
		/** @var  TrendingFeedQuerySettings */
		public $querySettings;

		/** @var  TrendingFeedSettings */
		public $viewSettings;

		/** @var  TrendingDetailsSettings */
		public $detailsSettings;

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
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);
			$this->querySettings = LinkFeedQuerySettings::fromXml(LinkFeedQuerySettings::FeedTypeTrending, $xpath, $contextNode);
			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeTrending, $xpath, $contextNode);
			$this->detailsSettings = TrendingDetailsSettings::fromXml($xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}
	}