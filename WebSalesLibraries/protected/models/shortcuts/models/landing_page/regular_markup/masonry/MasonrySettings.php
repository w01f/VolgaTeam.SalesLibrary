<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;


	abstract class MasonrySettings
	{
		const MasonryTypeSimple = 'simple';
		const MasonryTypeTrending = 'trending';
		const MasonryTypeSearch = 'search';
		const MasonryTypeSpecificLinks = 'specific-links';

		public $feedType;

		/** @var  \Padding */
		public $itemsPadding;

		/** @var  \Padding */
		public $textPadding;

		public $enableCaptionZoom;
		public $captionZoomScale;

		public function __construct()
		{
			$this->itemsPadding = new \Padding(0);

			$this->textPadding = new \Padding(0);
			$this->textPadding->isConfigured = true;
			$this->textPadding->top = 5;

			$this->enableCaptionZoom = true;
			$this->captionZoomScale = 1.25;
		}

		/**
		 * @param string $feedType
		 * @param string $encodedContent
		 * @return MasonrySettings
		 * @throws \Exception
		 */
		public static function fromJson($feedType, $encodedContent)
		{
			switch ($feedType)
			{
				case self::MasonryTypeTrending:
					$instance = new TrendingFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::MasonryTypeSearch:
					$instance = new SearchFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::MasonryTypeSpecificLinks:
					$instance = new SpecificLinkFeedSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::MasonryTypeSimple:
					$instance = new MasonrySimpleSettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				default:
					throw  new \Exception('Unknown masonry type');
			}
		}

		/**
		 * @param $feedType string
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return MasonrySettings
		 * @throws \Exception
		 */
		public static function fromXml($feedType, $xpath, $contextNode)
		{
			switch ($feedType)
			{
				case self::MasonryTypeTrending:
					$instance = new TrendingFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::MasonryTypeSearch:
					$instance = new SearchFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::MasonryTypeSpecificLinks:
					$instance = new SpecificLinkFeedSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::MasonryTypeSimple:
					$instance = new MasonrySimpleSettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				default:
					throw  new \Exception('Unknown masonry type');
			}
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./ImagePadding', $contextNode);
			if ($queryResult->length > 0)
				$this->itemsPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./TextPadding', $contextNode);
			if ($queryResult->length > 0)
				$this->textPadding = \Padding::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./EnableCaptionZoom', $contextNode);
			$this->enableCaptionZoom = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enableCaptionZoom;

			$queryResult = $xpath->query('./CaptionZoomScale', $contextNode);
			$this->captionZoomScale = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->captionZoomScale;
		}
	}