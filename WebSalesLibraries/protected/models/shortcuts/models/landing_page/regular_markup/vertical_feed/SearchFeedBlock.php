<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\vertical_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SearchFeedQuerySettings;
	use application\models\feeds\common\SimpleDetailsSettings;
	use application\models\feeds\vertical\FeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SearchFeedBlock
	 */
	class SearchFeedBlock extends ContentBlock implements \IDataQueryableBlock
	{
		/** @var  SearchFeedQuerySettings */
		public $querySettings;

		/** @var  FeedSettings */
		public $viewSettings;

		/** @var  SimpleDetailsSettings */
		public $detailsSettings;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'search-feed-vertical';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->querySettings = LinkFeedQuerySettings::fromXml($this, LinkFeedQuerySettings::FeedTypeSearch, $xpath, $contextNode);
			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeSearch, $xpath, $contextNode);
			$this->detailsSettings = SimpleDetailsSettings::fromXml($xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings, $this->parentShortcut->allowPublicAccess);
		}

		/**
		 * @return LinkFeedQuerySettings
		 */
		public function getQuerySettings()
		{
			return $this->querySettings;
		}
	}