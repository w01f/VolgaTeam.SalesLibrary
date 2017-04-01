<?

	namespace application\models\trending;

	/**
	 * Class TrendingSettings
	 */
	class TrendingSettings
	{
		const LinkFormatPowerPoint = 'ppt';
		const LinkFormatDocument = 'document';
		const LinkFormatWord = 'doc';
		const LinkFormatPdf = 'pdf';
		const LinkFormatVideo = 'video';

		const DataRangeTypeToday = 'today';
		const DataRangeTypeWeek = 'week';
		const DataRangeTypeMonth = 'month';

		const ThumbnailModeTop = 'top';
		const ThumbnailModeRandom = 'random';

		const LinksPerSlide3 = 3;
		const LinksPerSlide4 = 4;
		const LinksPerSlide6 = 6;

		const LinksScrollModeLink = 'link';
		const LinksScrollModeSlide = 'slide';

		public $linkFormats;
		public $libraries;
		public $dateRangeType;
		public $thumbnailMode;
		public $linksPerSlide;
		public $maxLinks;
		public $linksScrollMode;
		public $slideShow;
		public $slideShowInterval;

		/** @var  TrendingControlSettings[] */
		public $controlSettings;

		public $controlActiveColor;

		public function __construct()
		{
			$this->linkFormats = array(self::LinkFormatPowerPoint, self::LinkFormatDocument, self::LinkFormatVideo);
			$this->libraries = array();
			$this->dateRangeType = self::DataRangeTypeWeek;
			$this->linksPerSlide = self::LinksPerSlide4;
			$this->maxLinks = 30;
			$this->linksScrollMode = self::LinksScrollModeLink;
			$this->thumbnailMode = self::ThumbnailModeTop;
			$this->slideShow = false;
			$this->slideShowInterval = 5000;

			$this->initDefaultControlSettings();
		}

		/**
		 * @param string $encodedContent
		 * @return TrendingSettings
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();

			$data = \CJSON::decode($encodedContent, true);
			foreach ($data as $key => $value)
			{
				if (is_array($value))
					$value = \CJSON::decode(\CJSON::encode($value), false);
				$instance->{$key} = $value;
			}

			return $instance;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TrendingSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			/** @var $queryResult \DOMNodeList */
			$queryResult = $xpath->query('./LinksPerSlide', $contextNode);
			$linksPerSlide = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->linksPerSlide;
			if (in_array($linksPerSlide, array(self::LinksPerSlide3, self::LinksPerSlide4, self::LinksPerSlide6)))
				$instance->linksPerSlide = $linksPerSlide;

			$queryResult = $xpath->query('./MaxLinks', $contextNode);
			$instance->maxLinks = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->maxLinks;

			$queryResult = $xpath->query('./LinksScrollMode', $contextNode);
			$linksScrollMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $instance->linksScrollMode;
			if (in_array($linksScrollMode, array(self::LinksScrollModeLink, self::LinksScrollModeSlide)))
				$instance->linksScrollMode = $linksScrollMode;

			$queryResult = $xpath->query('./Formats/Item', $contextNode);
			if ($queryResult->length > 0)
			{
				$instance->linkFormats = array();
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
							$instance->linkFormats[] = $format;
							break;
					}
				}
			}

			$queryResult = $xpath->query('./Libraries/Item', $contextNode);
			if ($queryResult->length > 0)
			{
				$instance->libraries = array();
				foreach ($queryResult as $node)
				{
					$libraryName = trim($node->nodeValue);
					if (!empty($libraryName))
						$instance->libraries[] = $libraryName;
				}
			}

			$queryResult = $xpath->query('./DateRange', $contextNode);
			$dateRangeType = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $instance->dateRangeType;
			if (in_array($dateRangeType, array(self::DataRangeTypeToday, self::DataRangeTypeWeek, self::DataRangeTypeMonth)))
				$instance->dateRangeType = $dateRangeType;

			$queryResult = $xpath->query('./ThumbnailMode', $contextNode);
			$thumbnailMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $instance->thumbnailMode;
			if (in_array($thumbnailMode, array(self::ThumbnailModeTop, self::ThumbnailModeRandom)))
				$instance->thumbnailMode = $thumbnailMode;

			$queryResult = $xpath->query('./SlideShow', $contextNode);
			$instance->slideShow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $instance->slideShow;

			$queryResult = $xpath->query('./SlideShowInterval', $contextNode);
			$instance->slideShowInterval = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $instance->slideShowInterval;

			$queryResult = $xpath->query('./ControlSettings/Item', $contextNode);
			/** @var $node \DOMElement */
			foreach ($queryResult as $node)
			{
				$tag = $node->getAttribute('tag');
				if (!empty($tag) && array_key_exists($tag, $instance->controlSettings))
				{
					/** @var TrendingControlSettings $controlSettings */
					$controlSettings = $instance->controlSettings[$tag];
					$controlSettings->configureFromXml($xpath, $node);
				}
			}

			$queryResult = $xpath->query('./ControlActiveColor', $contextNode);
			$instance->controlActiveColor = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : null;

			return $instance;
		}

		private function initDefaultControlSettings()
		{
			$this->controlSettings = array();
			foreach (TrendingControlSettings::$tags as $tag)
				$this->controlSettings[$tag] = TrendingControlSettings::createDefault($tag);
		}
	}