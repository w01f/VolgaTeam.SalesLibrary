<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SpecificLinkFeedQuerySettings;
	use application\models\feeds\common\SimpleDetailsSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SpecificLinkFeedBlock
	 */
	class SpecificLinkFeedBlock extends ContentBlock implements \IDataQueryableBlock
	{
		/** @var  SpecificLinkFeedQuerySettings */
		public $querySettings;

		/** @var  MasonryFeedSettings */
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
			$this->type = 'specific-links-feed-masonry';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			$this->querySettings = LinkFeedQuerySettings::fromXml($this, LinkFeedQuerySettings::FeedTypeSpecificLinks, $xpath, $contextNode);
			$this->viewSettings = MasonrySettings::fromXml(MasonrySettings::MasonryTypeSpecificLinks, $xpath, $contextNode, $this->parentShortcut->id, $this->id);
			$this->detailsSettings = SimpleDetailsSettings::fromXml($xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
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