<?

	namespace application\models\feeds\horizontal;

	use application\models\feeds\common\ControlsStyle;
	use application\models\feeds\common\FeedControlSettings;
	use application\models\feeds\common\FeedItemSettings;

	/**
	 * Class LinkFeedSettings
	 */
	abstract class FeedSettings
	{
		const FeedTypeSimpleSlider = 'shortcut-slider';
		const FeedTypeTrending = 'trending';
		const FeedTypeSearch = 'search';
		const FeedTypeSpecificLinks = 'specific-links';

		const LinksPerSlide1 = 1;
		const LinksPerSlide2 = 2;
		const LinksPerSlide3 = 3;
		const LinksPerSlide4 = 4;
		const LinksPerSlide6 = 6;

		const LinksScrollModeLink = 'link';
		const LinksScrollModeSlide = 'slide';
		const LinksScrollModeFade = 'fade';

		public $feedType;

		public $linksPerSlide;
		public $linksScrollMode;
		public $slideShow;
		public $slideShowInterval;
		public $maxImageHeight;
		public $animationSpeed;

		/** @var  FeedControlSettings[] */
		public $controlSettings;

		public function __construct()
		{
			$this->linksPerSlide = self::LinksPerSlide4;
			$this->linksScrollMode = self::LinksScrollModeLink;
			$this->slideShow = false;
			$this->slideShowInterval = 5000;
			$this->maxImageHeight = 0;
			$this->animationSpeed = ".6";
			$this->initDefaultControlSettings();
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
			/** @var $queryResult \DOMNodeList */
			$queryResult = $xpath->query('./LinksPerSlide', $contextNode);
			$linksPerSlide = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->linksPerSlide;
			if (in_array($linksPerSlide, array(self::LinksPerSlide1, self::LinksPerSlide2, self::LinksPerSlide3, self::LinksPerSlide4, self::LinksPerSlide6)))
				$this->linksPerSlide = $linksPerSlide;


			$queryResult = $xpath->query('./LinksScrollMode', $contextNode);
			$linksScrollMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->linksScrollMode;
			if (in_array($linksScrollMode, array(self::LinksScrollModeLink, self::LinksScrollModeSlide, self::LinksScrollModeFade)))
				$this->linksScrollMode = $linksScrollMode;

			$queryResult = $xpath->query('./SlideShow', $contextNode);
			$this->slideShow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->slideShow;

			$queryResult = $xpath->query('./SlideShowInterval', $contextNode);
			$this->slideShowInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->slideShowInterval;

			$queryResult = $xpath->query('./FadeSpeed', $contextNode);
			$this->animationSpeed = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->animationSpeed;

			$queryResult = $xpath->query('./ControlSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && property_exists($this->controlSettings, $tag))
				{
					/** @var FeedControlSettings $controlSettings */
					$controlSettings = $this->controlSettings->{$tag};
					$controlSettings->configureFromXml($xpath, $node);
				}
			}
		}

		protected abstract function initDefaultControlSettings();
	}