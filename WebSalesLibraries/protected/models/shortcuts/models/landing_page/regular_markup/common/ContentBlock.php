<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\common;

	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SearchFeedBlock as HorizontalSearchFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SpecificLinkFeedBlock as HorizontalSpecificLinkFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\TrendingBlock as HorizontalTrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\list_block\ListItem;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\MasonryBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\scroll_stripe\ScrollStripeBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SlideBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\slider\SliderBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\style\BackgroundStyle;
	use application\models\shortcuts\models\landing_page\regular_markup\style\BorderStyle;
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;
	use application\models\shortcuts\models\landing_page\regular_markup\horizontal_feed\SimpleFeedBlock as SimpleHorizontalFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\toggle_panel\TogglePanelBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\toggle_panel\TogglePanelItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedBlock as SimpleVerticalFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SimpleFeedItem;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\TrendingBlock as VerticalTrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SearchFeedBlock as VerticalSearchFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\vertical_feed\SpecificLinkFeedBlock as VerticalSpecificLinkFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\TrendingBlock as MasonryTrendingBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\SearchFeedBlock as MasonrySearchFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\masonry\SpecificLinkFeedBlock as MasonrySpecificLinkFeedBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryPageBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryPageBundleBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\wallbin\LibraryWindowBlock;

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

		public $centerBlock;

		/** @var  \Padding */
		public $padding;
		/** @var  \Padding */
		public $margin;
		/** @var  TextAppearance */
		public $textAppearance;
		/** @var  BorderStyle */
		public $border;
		/** @var  BackgroundStyle */
		public $background;

		public $isAccessGranted;

		public $imagePath;

		/**
		 * @param $parentShortcut \LandingPageShortcut
		 * @param $parentBlock BlockContainer
		 */
		protected function __construct($parentShortcut, $parentBlock)
		{
			$this->id = uniqid();

			$this->centerBlock = false;

			$this->parentShortcut = $parentShortcut;
			$this->parentBlock = $parentBlock;

			$this->padding = new \Padding(0);
			$this->margin = new \Padding(0);

			$this->imagePath = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $this->parentShortcut->relativeLink . '/images/');
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		protected function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./HoverTip', $contextNode);
			$this->hoverText = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./CenterBlock', $contextNode);
			$this->centerBlock = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->centerBlock;

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

			$queryResult = $xpath->query('./Background', $contextNode);
			if ($queryResult->length > 0)
				$this->background = BackgroundStyle::fromXml($xpath, $queryResult->item(0));

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
				case 'auto-fit-row':
					return 'common/autoFitRow';
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
				case 'shortcut-slider':
					return 'horizontal_feed/simpleFeed';
				case 'trending':
					return 'horizontal_feed/trending';
				case 'search-feed':
					return 'horizontal_feed/searchFeed';
				case 'specific-links-feed':
					return 'horizontal_feed/specificLinkFeed';
				case 'news':
					return 'vertical_feed/simpleFeed';
				case 'trending-vertical':
					return 'vertical_feed/trending';
				case 'search-feed-vertical':
					return 'vertical_feed/searchFeed';
				case 'specific-links-feed-vertical':
					return 'vertical_feed/specificLinkFeed';
				case 'scroll-stripe':
					return 'common/scrollStripe';
				case 'masonry':
					return 'masonry/masonry';
				case 'trending-masonry':
					return 'masonry/trending';
				case 'search-feed-masonry':
					return 'masonry/searchFeed';
				case 'specific-links-feed-masonry':
					return 'masonry/specificLinkFeed';
				case 'toggle-panel':
					return 'toggle_panel/panel';
				case 'toggle-item':
					return 'toggle_panel/item';
				case 'library':
					return 'wallbin/library';
				case 'library-page-bundle':
					return 'wallbin/libraryPageBundle';
				case 'library-page':
					return 'wallbin/libraryPage';
				case 'library-window':
					return 'wallbin/libraryWindow';
				case 'search-results':
					return 'common/searchResults';
				default:
					return 'common/undefinedBlock';
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
				case "auto-fit-row":
					$row = new AutoFitRow($parentShortcut, $parentBlock);
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
				case "trending-masonry":
					$trendingBlock = new MasonryTrendingBlock($parentShortcut, $parentBlock);
					$trendingBlock->configureFromXml($xpath, $contextNode);
					return $trendingBlock;
				case "search-feed-masonry":
					$searchFeedBlock = new MasonrySearchFeedBlock($parentShortcut, $parentBlock);
					$searchFeedBlock->configureFromXml($xpath, $contextNode);
					return $searchFeedBlock;
				case "specific-links-feed-masonry":
					$specificLinksFeedBlock = new MasonrySpecificLinkFeedBlock($parentShortcut, $parentBlock);
					$specificLinksFeedBlock->configureFromXml($xpath, $contextNode);
					return $specificLinksFeedBlock;
				case "shortcut-slider":
					$simpleFeedBlock = new SimpleHorizontalFeedBlock($parentShortcut, $parentBlock);
					$simpleFeedBlock->configureFromXml($xpath, $contextNode);
					return $simpleFeedBlock;
				case "news":
					$simpleFeedBlock = new SimpleVerticalFeedBlock($parentShortcut, $parentBlock);
					$simpleFeedBlock->configureFromXml($xpath, $contextNode);
					return $simpleFeedBlock;
				case "news block":
					$newsItem = new SimpleFeedItem($parentShortcut, $parentBlock);
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
				case "toggle-panel":
					$togglePanelBlock = new TogglePanelBlock($parentShortcut, $parentBlock);
					$togglePanelBlock->configureFromXml($xpath, $contextNode);
					return $togglePanelBlock;
				case "toggle-item":
					$togglePanelItem = new TogglePanelItem($parentShortcut, $parentBlock);
					$togglePanelItem->configureFromXml($xpath, $contextNode);
					return $togglePanelItem;
				case "library":
					$libraryItem = new LibraryBlock($parentShortcut, $parentBlock);
					$libraryItem->configureFromXml($xpath, $contextNode);
					return $libraryItem;
				case "library-page-bundle":
					$libraryPageBundleItem = new LibraryPageBundleBlock($parentShortcut, $parentBlock);
					$libraryPageBundleItem->configureFromXml($xpath, $contextNode);
					return $libraryPageBundleItem;
				case "library-page":
					$libraryPageItem = new LibraryPageBlock($parentShortcut, $parentBlock);
					$libraryPageItem->configureFromXml($xpath, $contextNode);
					return $libraryPageItem;
				case "library-window":
					$libraryWindow = new LibraryWindowBlock($parentShortcut, $parentBlock);
					$libraryWindow->configureFromXml($xpath, $contextNode);
					return $libraryWindow;
				case "search-table":
					$searchResults = new SearchResultsBlock($parentShortcut, $parentBlock);
					$searchResults->configureFromXml($xpath, $contextNode);
					return $searchResults;
				default:
					return new UndefinedBlock($parentShortcut, $parentBlock);
			}
		}
	}