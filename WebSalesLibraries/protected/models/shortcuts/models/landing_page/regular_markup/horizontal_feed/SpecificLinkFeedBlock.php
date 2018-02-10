<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed;

	use application\models\data_query\link_feed\LinkFeedItem;
	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\data_query\link_feed\LinkFeedQuerySettings;
	use application\models\data_query\link_feed\SpecificLinkFeedQuerySettings;
	use application\models\feeds\common\SimpleDetailsSettings;
	use application\models\feeds\horizontal\FeedSettings;
	use application\models\feeds\horizontal\SpecificLinkFeedSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	/**
	 * Class SpecificLinkFeedBlock
	 */
	class SpecificLinkFeedBlock extends BlockContainer
	{
		/** @var  SpecificLinkFeedQuerySettings */
		public $querySettings;

		/** @var  SpecificLinkFeedSettings */
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
			$this->type = 'specific-links-feed';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);
			$this->querySettings = LinkFeedQuerySettings::fromXml(LinkFeedQuerySettings::FeedTypeSpecificLinks, $xpath, $contextNode);
			$this->viewSettings = FeedSettings::fromXml(FeedSettings::FeedTypeSpecificLinks, $xpath, $contextNode);
			$this->detailsSettings = SimpleDetailsSettings::fromXml($xpath, $contextNode);
		}

		/** @return LinkFeedItem[] */
		public function getFeedItems()
		{
			return LinkFeedQueryHelper::queryFeedItems($this->querySettings);
		}
	}