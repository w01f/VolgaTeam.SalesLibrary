<?

	namespace application\models\feeds\vertical;

	/**
	 * Class FeedSettings
	 */
	abstract class FeedSettings
	{
		const FeedTypeSimpleSlider = 'news';
		const FeedTypeTrending = 'trending';
		const FeedTypeSearch = 'search';
		const FeedTypeSpecificLinks = 'specific-links';

		const ScrollDirectionUp = 'up';
		const ScrollDirectionDown = 'down';

		public $feedType;

		public $title;
		public $icon;
		public $itemsCount;
		public $autoPlay;
		public $direction;
		public $tickerInterval;
		public $hideHeader;
		public $hideFooter;

		/** @var  FeedStyle */
		public $style;

		public function __construct()
		{
			$this->title = 'News';
			$this->icon = null;
			$this->itemsCount = 3;
			$this->autoPlay = true;
			$this->direction = self::ScrollDirectionUp;
			$this->tickerInterval = 4000;
			$this->hideHeader = false;
			$this->hideFooter = false;
			$this->style = new FeedStyle();
		}

		/**
		 * @param string $feedType
		 * @param string $encodedContent
		 * @return FeedSettings
		 * @throws \Exception
		 */
		public static function fromJson($feedType, $encodedContent)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::FeedTypeSearch:
					$instance = new SearchFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::FeedTypeSimpleSlider:
					$instance = new SimpleFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				default:
					throw  new \Exception('Unknown feed type');
			}
		}

		/**
		 * @param $feedType string
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return FeedSettings
		 * @throws \Exception
		 */
		public static function fromXml($feedType, $xpath, $contextNode)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::FeedTypeSearch:
					$instance = new SearchFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::FeedTypeSimpleSlider:
					$instance = new SimpleFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				default:
					throw  new \Exception('Unknown feed type');
			}
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->title;

			$queryResult = $xpath->query('./Icon', $contextNode);
			$this->icon = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->icon;

			$queryResult = $xpath->query('./VisibleItems', $contextNode);
			$this->itemsCount = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->itemsCount;

			$queryResult = $xpath->query('./AutoPlay', $contextNode);
			$this->autoPlay = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->autoPlay;

			$queryResult = $xpath->query('./Direction', $contextNode);
			$direction = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->direction;
			if (in_array($direction, array(self::ScrollDirectionUp, self::ScrollDirectionDown)))
				$this->direction = $direction;

			$queryResult = $xpath->query('./TickerInterval', $contextNode);
			$this->tickerInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->itemsCount;

			$queryResult = $xpath->query('./HideHeader', $contextNode);
			$this->hideHeader = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->hideHeader;

			$queryResult = $xpath->query('./HideFooter', $contextNode);
			$this->hideFooter = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->hideFooter;

			$queryResult = $xpath->query('./Style', $contextNode);
			if ($queryResult->length > 0)
				$this->style->configureFromXml($xpath, $queryResult->item(0));
		}
	}