<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\vertical_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\TrendingDetailsSettings;
	use application\models\feeds\vertical\FeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class TrendingBlock
	 */
	class TrendingBlock extends ContentBlock implements \IDataQueryableBlock
	{
		/** @var  TrendingFeedQuerySettings */
		public $querySettings;

		/** @var  FeedSettings */
		public $viewSettings;

		/** @var  TrendingDetailsSettings */
		public $detailsSettings;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'trending-vertical';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			try
			{
				$this->querySettings = LinkFeedQuerySettings::fromXml($this, LinkFeedQuerySettings::FeedTypeTrending, $xpath, $contextNode);
				$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeTrending, $xpath, $contextNode);
				$this->detailsSettings = TrendingDetailsSettings::fromXml($xpath, $contextNode);
			}
			catch (\Exception $ex)
			{
			}
		}

		/** @return LinkFeedItem[]
		 */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}

		/**
		 * @return LinkFeedQuerySettings
		 */
		public function getQuerySettings()
		{
			return $this->querySettings;
		}
	}