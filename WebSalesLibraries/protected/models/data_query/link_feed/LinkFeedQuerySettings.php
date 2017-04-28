<?
	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\ThumbnailQuerySettings;

	/**
	 * Class LinkFeedQuerySettings
	 */
	class LinkFeedQuerySettings
	{
		const FeedTypeTrending = 'trending';
		const FeedTypeSearch = 'search';
		const FeedTypeSpecificLinks = 'specific-links';

		const LinkFormatPowerPoint = 'ppt';
		const LinkFormatDocument = 'document';
		const LinkFormatWord = 'doc';
		const LinkFormatPdf = 'pdf';
		const LinkFormatVideo = 'video';

		public $feedType;

		public $linkFormats;
		public $maxLinks;
		/** @var  ThumbnailQuerySettings */
		public $thumbnailSettings;

		public function __construct()
		{
			$this->linkFormats = array(self::LinkFormatPowerPoint, self::LinkFormatDocument, self::LinkFormatVideo);
			$this->thumbnailSettings = new ThumbnailQuerySettings();
			$this->maxLinks = 30;
		}

		/**
		 * @param string $feedType
		 * @param string $encodedContent
		 * @return LinkFeedQuerySettings
		 * @throws \Exception
		 */
		public static function fromJson($feedType, $encodedContent)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedQuerySettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::FeedTypeSearch:
					$instance = new SearchFeedQuerySettings();
					\Utils::loadFromJson($instance, $encodedContent);
					return $instance;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedQuerySettings();
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
		 * @return LinkFeedQuerySettings
		 * @throws \Exception
		 */
		public static function fromXml($feedType, $xpath, $contextNode)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedQuerySettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::FeedTypeSearch:
					$instance = new SearchFeedQuerySettings();
					$instance->configureFromXml($xpath, $contextNode);
					return $instance;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedQuerySettings();
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
			$queryResult = $xpath->query('./MaxLinks', $contextNode);
			$this->maxLinks = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->maxLinks;

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

			$queryResult = $xpath->query('./ThumbnailSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->thumbnailSettings = ThumbnailQuerySettings::fromXml($xpath, $queryResult->item(0));
		}
	}