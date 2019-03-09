<?

	namespace application\models\data_query\link_feed;

	use application\models\data_query\conditions\QueryCacheSettings;
	use application\models\data_query\conditions\ThumbnailQuerySettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

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
		const LinkFormatExcel = 'xls';
		const LinkFormatVideo = 'video';
		const LinkFormatHyperlink = 'links';
		const LinkFormatUrl = 'url';
		const LinkFormatQuicksite = 'quicksite';
		const LinkFormatHtml5 = 'html5';
		const LinkFormatYouTube = 'youtube';
		const LinkFormatVimeo = 'vimeo';
		const LinkFormatInternalLibrary = 'internal library';
		const LinkFormatInternalPage = 'internal page';
		const LinkFormatInternalWindow = 'internal window';
		const LinkFormatInternalShortcut = 'internal shortcut';
		const LinkFormatInternalLink = 'internal link';

		public $feedType;

		public $linkFormatsInclude;
		public $linkFormatsExclude;
		public $maxLinks;
		public $hideLinksWithinBundle;

		/** @var  ThumbnailQuerySettings */
		public $thumbnailSettings;

		/** @var  QueryCacheSettings */
		public $cacheSettings;

		public function __construct()
		{
			$this->maxLinks = 30;
			$this->hideLinksWithinBundle = true;
			$this->linkFormatsInclude = array(self::LinkFormatPowerPoint, self::LinkFormatDocument, self::LinkFormatVideo, self::LinkFormatHyperlink);
			$this->linkFormatsExclude = array();
			$this->thumbnailSettings = new ThumbnailQuerySettings();
			$this->cacheSettings = new QueryCacheSettings();
		}

		/**
		 * @param string $feedType
		 * @param string $encodedContent
		 * @return LinkFeedQuerySettings
		 * @throws \Exception
		 */
		public static function fromJson($feedType, $encodedContent)
		{
			$instance = null;
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedQuerySettings();
					\Utils::loadFromJson($instance, $encodedContent);
					break;
				case self::FeedTypeSearch:
					$instance = new SearchFeedQuerySettings();
					\Utils::loadFromJson($instance, $encodedContent);
					break;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedQuerySettings();
					\Utils::loadFromJson($instance, $encodedContent);
					break;
				default:
					throw  new \Exception('Unknown feed type');
			}
			return $instance;
		}

		/**
		 * @param $parentContentBlock ContentBlock
		 * @param $feedType string
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return LinkFeedQuerySettings
		 * @throws \Exception
		 */
		public static function fromXml($parentContentBlock, $feedType, $xpath, $contextNode)
		{
			$instance = null;
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$instance = new TrendingFeedQuerySettings();
					$instance->configureFromXml($xpath, $contextNode);
					break;
				case self::FeedTypeSearch:
					$instance = new SearchFeedQuerySettings();
					$instance->configureFromXml($xpath, $contextNode);
					break;
				case self::FeedTypeSpecificLinks:
					$instance = new SpecificLinkFeedQuerySettings();
					$instance->configureFromXml($xpath, $contextNode);
					break;
				default:
					throw  new \Exception('Unknown feed type');
			}
			$instance->cacheSettings->cacheId = $parentContentBlock->id;
			return $instance;
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
				$this->linkFormatsInclude = array();
				foreach ($queryResult as $node)
				{
					$format = strtolower(trim($node->nodeValue));
					switch ($format)
					{
						case self::LinkFormatPowerPoint:
						case self::LinkFormatVideo:
						case self::LinkFormatDocument:
						case self::LinkFormatHyperlink:
							$this->linkFormatsInclude[] = $format;
							break;
					}
				}
			}

			$queryResult = $xpath->query('./Formats/Exclude/Item', $contextNode);
			if ($queryResult->length > 0)
			{
				$this->linkFormatsExclude = array();
				foreach ($queryResult as $node)
					$this->linkFormatsExclude[] = strtolower(trim($node->nodeValue));
			}

			$queryResult = $xpath->query('./ThumbnailSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->thumbnailSettings = ThumbnailQuerySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./SnapshotData', $contextNode);
			if ($queryResult->length > 0)
				$this->cacheSettings = QueryCacheSettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./HideLinkBundleDuplicates', $contextNode);
			$this->hideLinksWithinBundle = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}
	}