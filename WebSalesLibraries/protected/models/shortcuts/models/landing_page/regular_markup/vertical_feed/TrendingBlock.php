<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\vertical_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\TrendingFeedQuerySettings;
	use application\models\feeds\common\FeedItemSettings;
	use application\models\feeds\vertical\FeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class TrendingBlock
	 */
	class TrendingBlock extends ContentBlock
	{
		/** @var  TrendingFeedQuerySettings */
		public $querySettings;

		/** @var  FeedSettings */
		public $viewSettings;

		/** @var  FeedItemSettings[] */
		public $dataItemSettings;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'trending-vertical';
			$this->initDefaultDataItemSettings();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->querySettings = LinkFeedQuerySettings::fromXml(LinkFeedQuerySettings::FeedTypeTrending, $xpath, $contextNode);
			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeTrending, $xpath, $contextNode);

			$queryResult = $xpath->query('./DataSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->dataItemSettings, $tag))
				{
					/** @var FeedItemSettings $dataItem */
					$dataItem = $this->dataItemSettings->{$tag};
					$dataItem->configureFromXml($xpath, $node);
				}
			}
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}

		private function initDefaultDataItemSettings()
		{
			$this->dataItemSettings = new \stdClass();
			foreach (FeedItemSettings::$tags as $tag)
				$this->dataItemSettings->{$tag} = new FeedItemSettings($tag);
		}
	}