<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SearchFeedBlock  as HorizontalSearchFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SpecificLinkFeedBlock  as HorizontalSpecificLinkFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\TrendingBlock as HorizontalTrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListItem;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SlideBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SliderBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\NewsItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\TrendingBlock as VerticalTrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SearchFeedBlock as VerticalSearchFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SpecificLinkFeedBlock as VerticalSpecificLinkFeedBlock;

	/**
	 * Class ContentBlock
	 */
	abstract class ContentBlock
	{
		public $id;

		/** @var \LandingPageShortcut */
		protected $parentShortcut;

		/** @var BlockContainer */
		protected $parentBlock;

		public $type;

		public $hoverText;

		/** @var  \Padding */
		public $padding;
		/** @var  \Padding */
		public $margin;
		/** @var  TextAppearance */
		public $textAppearance;
		/** @var  BorderStyle */
		public $border;

		public $isAccessGranted;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		protected function __construct($parentShortcut, $parentBlock)
		{
			$this->id = uniqid();

			$this->parentShortcut = $parentShortcut;
			$this->parentBlock = $parentBlock;

			$this->padding = new \Padding(0);
			$this->margin = new \Padding(0);
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./HoverTip', $contextNode);
			$this->hoverText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Padding', $contextNode);
			if ($queryResult->length > 0)
				$this->padding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Margin', $contextNode);
			if ($queryResult->length > 0)
				$this->margin = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./TextStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->textAppearance = TextAppearance::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./Border', $contextNode);
			if ($queryResult->length > 0)
				$this->border = BorderStyle::fromXml($xpath, $queryResult->item(0));

			$user = \Yii::app()->user;
			$userGroups = \UserIdentity::getCurrentUserGroups();

			$approvedUsers = array();
			$queryResult = $xpath->query('./ApprovedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedUsers[] = trim($groupNode->nodeValue);

			$excludedUsers = array();
			$queryResult = $xpath->query('./ExcludedUsers/User', $contextNode);
			foreach ($queryResult as $groupNode)
				$excludedUsers[] = trim($groupNode->nodeValue);

			$approvedGroups = array();
			$queryResult = $xpath->query('./ApprovedGroups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$approvedGroups[] = trim($groupNode->nodeValue);

			$excludedGroups = array();
			$queryResult = $xpath->query('./ExcludedGroups/Group', $contextNode);
			foreach ($queryResult as $groupNode)
				$excludedGroups[] = trim($groupNode->nodeValue);

			$this->isAccessGranted = true;

			if (isset($user) && count($excludedUsers) > 0)
				$this->isAccessGranted &= !in_array($user->login, $excludedUsers);
			if (isset($user) && count($excludedGroups) > 0)
				$this->isAccessGranted &= !array_intersect($userGroups, $excludedGroups);

			if ($this->isAccessGranted && (count($approvedUsers) > 0 || count($approvedGroups) > 0))
			{
				$this->isAccessGranted = false;
				if (isset($user) && isset($user->login))
				{
					$this->isAccessGranted |= in_array($user->login, $approvedUsers);
					if (count($userGroups) > 0)
						$this->isAccessGranted |= array_intersect($userGroups, $approvedGroups);
				}
			}
		}

		/** @return TextAppearance */
		public function getTextAppearance()
		{
			if (isset($this->textAppearance))
				return $this->textAppearance;
			if (isset($this->parentBlock))
				return $this->parentBlock->textAppearance;
			return null;
		}

		/** @return string */
		public function getViewName()
		{
			switch ($this->type)
			{
				case 'row':
					return 'common/row';
				case 'column':
					return 'common/column';
				case 'image':
					return 'common/image';
				case 'text':
					return 'common/text';
				case 'list':
					return 'list/list';
				case 'list-item':
					return 'list/listItem';
				case 'url':
					return 'common/url';
				case 'shortcut':
					return 'common/shortcut';
				case 'search-bar':
					return 'common/searchBar';
				case 'slider':
					return 'common/slider';
				case 'trending':
					return 'horizontal_feed/trending';
				case 'search-feed':
					return 'horizontal_feed/searchFeed';
				case 'specific-links-feed':
					return 'horizontal_feed/specificLinkFeed';
				case 'news':
					return 'vertical_feed/news';
				case 'trending-vertical':
					return 'vertical_feed/trending';
				case 'search-feed-vertical':
					return 'vertical_feed/searchFeed';
				case 'specific-links-vertical':
					return 'vertical_feed/specificLinkFeed';
				case 'scroll-stripe':
					return 'common/scrollStripe';
				case 'masonry':
					return 'masonry/masonry';
				default:
					return 'undefinedBlock';
			}
		}

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ContentBlock
		 */
		public static function fromXml($parentShortcut, $parentBlock, $xpath, $contextNode)
		{
			$typeAttribute = $contextNode->attributes->getNamedItem("Type");
			$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
			switch ($type)
			{
				case "row":
					$row = new Row($parentShortcut, $parentBlock);
					$row->configureFromXml($xpath, $contextNode);
					return $row;
				case "column":
					$column = new Column($parentShortcut, $parentBlock);
					$column->configureFromXml($xpath, $contextNode);
					return $column;
				case "image":
					$imageBlock = new ImageBlock($parentShortcut, $parentBlock);
					$imageBlock->configureFromXml($xpath, $contextNode);
					return $imageBlock;
				case "text":
					$textBlock = new TextBlock($parentShortcut, $parentBlock);
					$textBlock->configureFromXml($xpath, $contextNode);
					return $textBlock;
				case "list":
					$listBlock = new ListBlock($parentShortcut, $parentBlock);
					$listBlock->configureFromXml($xpath, $contextNode);
					return $listBlock;
				case "list-item":
					$listItem = new ListItem($parentShortcut, $parentBlock);
					$listItem->configureFromXml($xpath, $contextNode);
					return $listItem;
				case "url":
					$urlBlock = new UrlBlock($parentShortcut, $parentBlock);
					$urlBlock->configureFromXml($xpath, $contextNode);
					return $urlBlock;
				case "shortcut":
					$shortcutBlock = new ShortcutBlock($parentShortcut, $parentBlock);
					$shortcutBlock->configureFromXml($xpath, $contextNode);
					return $shortcutBlock;
				case "search-bar":
					$searchBarBlock = new SearchBarBlock($parentShortcut, $parentBlock);
					$searchBarBlock->configureFromXml($xpath, $contextNode);
					return $searchBarBlock;
				case "slider":
					$sliderBlock = new SliderBlock($parentShortcut, $parentBlock);
					$sliderBlock->configureFromXml($xpath, $contextNode);
					return $sliderBlock;
				case "slide":
					$slideBlock = new SlideBlock($parentShortcut, $parentBlock);
					$slideBlock->configureFromXml($xpath, $contextNode);
					return $slideBlock;
				case "trending":
					$trendingBlock = new HorizontalTrendingBlock($parentShortcut, $parentBlock);
					$trendingBlock->configureFromXml($xpath, $contextNode);
					return $trendingBlock;
				case "search-feed":
					$searchFeedBlock = new HorizontalSearchFeedBlock($parentShortcut, $parentBlock);
					$searchFeedBlock->configureFromXml($xpath, $contextNode);
					return $searchFeedBlock;
				case "specific-links-feed":
					$specificLinksFeedBlock = new HorizontalSpecificLinkFeedBlock($parentShortcut, $parentBlock);
					$specificLinksFeedBlock->configureFromXml($xpath, $contextNode);
					return $specificLinksFeedBlock;
				case "trending-vertical":
					$trendingBlock = new VerticalTrendingBlock($parentShortcut, $parentBlock);
					$trendingBlock->configureFromXml($xpath, $contextNode);
					return $trendingBlock;
				case "search-feed-vertical":
					$searchFeedBlock = new VerticalSearchFeedBlock($parentShortcut, $parentBlock);
					$searchFeedBlock->configureFromXml($xpath, $contextNode);
					return $searchFeedBlock;
				case "specific-links-feed-vertical":
					$specificLinksFeedBlock = new VerticalSpecificLinkFeedBlock($parentShortcut, $parentBlock);
					$specificLinksFeedBlock->configureFromXml($xpath, $contextNode);
					return $specificLinksFeedBlock;
				case "news":
					$newsBlock = new NewsBlock($parentShortcut, $parentBlock);
					$newsBlock->configureFromXml($xpath, $contextNode);
					return $newsBlock;
				case "news block":
					$newsItem = new NewsItem($parentShortcut, $parentBlock);
					$newsItem->configureFromXml($xpath, $contextNode);
					return $newsItem;
				case "scroll-tab":
					$scrollStripeBlock = new ScrollStripeBlock($parentShortcut, $parentBlock);
					$scrollStripeBlock->configureFromXml($xpath, $contextNode);
					return $scrollStripeBlock;
				case "masonry":
					$masonryBlock = new MasonryBlock($parentShortcut, $parentBlock);
					$masonryBlock->configureFromXml($xpath, $contextNode);
					return $masonryBlock;
				default:
					return new UndefinedBlock($parentShortcut, $parentBlock);
			}
		}
	}