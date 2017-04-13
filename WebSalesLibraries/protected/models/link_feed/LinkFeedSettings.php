<?

	namespace application\models\link_feed;

	/**
	 * Class LinkFeedSettings
	 */
	abstract class LinkFeedSettings
	{
		const LinksPerSlide3 = 3;
		const LinksPerSlide4 = 4;
		const LinksPerSlide6 = 6;

		const FeedTypeTrending = 'trending';
		const FeedTypeSearch = 'search';
		const FeedTypeSpecificLinks = 'specific-links';

		const ThumbnailModeTop = 'top';
		const ThumbnailModeRandom = 'random';

		const LinksScrollModeLink = 'link';
		const LinksScrollModeSlide = 'slide';

		const LinkFormatPowerPoint = 'ppt';
		const LinkFormatDocument = 'document';
		const LinkFormatWord = 'doc';
		const LinkFormatPdf = 'pdf';
		const LinkFormatVideo = 'video';

		public $feedType;

		public $linkFormats;
		public $thumbnailMode;
		public $linksPerSlide;
		public $maxLinks;
		public $linksScrollMode;
		public $slideShow;
		public $slideShowInterval;
		public $controlActiveColor;

		/** @var  FeedItemSettings[] */
		public $dataItemSettings;

		public function __construct()
		{
			$this->linkFormats = array(self::LinkFormatPowerPoint, self::LinkFormatDocument, self::LinkFormatVideo);
			$this->linksPerSlide = self::LinksPerSlide4;
			$this->maxLinks = 30;
			$this->linksScrollMode = self::LinksScrollModeLink;
			$this->thumbnailMode = self::ThumbnailModeTop;
			$this->slideShow = false;
			$this->slideShowInterval = 5000;

			$this->initDefaultDataItemSettings();

			$this->initDefaultControlSettings();
		}

		/**
		 * @param string $feedType
		 * @param string $encodedContent
		 * @return LinkFeedSettings
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
				default:
					throw  new \Exception('Unknown feed type');
			}
		}

		/**
		 * @param $feedType string
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return LinkFeedSettings
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
			if (in_array($linksPerSlide, array(self::LinksPerSlide3, self::LinksPerSlide4, self::LinksPerSlide6)))
				$this->linksPerSlide = $linksPerSlide;

			$queryResult = $xpath->query('./MaxLinks', $contextNode);
			$this->maxLinks = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->maxLinks;

			$queryResult = $xpath->query('./LinksScrollMode', $contextNode);
			$linksScrollMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->linksScrollMode;
			if (in_array($linksScrollMode, array(self::LinksScrollModeLink, self::LinksScrollModeSlide)))
				$this->linksScrollMode = $linksScrollMode;

			$queryResult = $xpath->query('./Formats/Item', $contextNode);
			if ($queryResult->length > 0)
			{
				$this->linkFormats = array();
				foreach ($queryResult as $node)
				{
					$format = strtolower(trim($node->nodeValue));
					switch ($format)
					{
						case self::LinkFormatPowerPoint:
						case self::LinkFormatVideo:
						case self::LinkFormatPdf:
						case self::LinkFormatWord:
						case self::LinkFormatDocument:
							$this->linkFormats[] = $format;
							break;
					}
				}
			}

			$queryResult = $xpath->query('./ThumbnailMode', $contextNode);
			$thumbnailMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $this->thumbnailMode;
			if (in_array($thumbnailMode, array(self::ThumbnailModeTop, self::ThumbnailModeRandom)))
				$this->thumbnailMode = $thumbnailMode;

			$queryResult = $xpath->query('./SlideShow', $contextNode);
			$this->slideShow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->slideShow;

			$queryResult = $xpath->query('./SlideShowInterval', $contextNode);
			$this->slideShowInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->slideShowInterval;

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

			$queryResult = $xpath->query('./ControlActiveColor', $contextNode);
			$this->controlActiveColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;
		}

		protected abstract function initDefaultControlSettings();

		protected function initDefaultDataItemSettings()
		{
			$this->dataItemSettings = new \stdClass();
			foreach (FeedItemSettings::$tags as $tag)
				$this->dataItemSettings->{$tag} = new FeedItemSettings($tag);
		}
	}