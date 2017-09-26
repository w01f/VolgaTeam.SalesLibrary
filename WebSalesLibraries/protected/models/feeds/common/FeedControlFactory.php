<?

	namespace application\models\feeds\common;


	abstract class FeedControlFactory
	{
		const FeedTypeTrending = 'trending';
		const FeedTypeSearch = 'search';
		const FeedTypeSpecificLinks = 'specific-links';
		const FeedTypeSimple = 'simple';

		/**
		 * @param $controlTag string
		 * @return FeedControlSettings
		 * @throws \Exception
		 */
		public function createControl($controlTag)
		{
			switch ($controlTag)
			{
				case FeedControlTag::ControlTagLinkFormatPowerPoint:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'presentations';
					return $control;
				case FeedControlTag::ControlTagLinkFormatDocuments:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'documents';
					return $control;
				case FeedControlTag::ControlTagLinkFormatVideo:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'video';
					return $control;
				case FeedControlTag::ControlTagLinkFormatHyperlinks:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'links';
					return $control;
				case FeedControlTag::ControlTagDetailsButton:
					$control = new FeedDetailsControlSettings();
					$control->enabled = false;
					$control->title = 'details';
					return $control;
				case FeedControlTag::ControlTagScrollButton:
					$control = new FeedControlSettings();
					$control->enabled = true;
					$control->title = 'scroll';
					return $control;
				default:
					throw  new \Exception('Unknown control type');
			}
		}

		/**
		 * @param $feedType string
		 * @param $controlTag string
		 * @return FeedControlSettings
		 * @throws \Exception
		 */
		public static function getControl($feedType, $controlTag)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					$factory = new TrendingFeedControlFactory();
					break;
				case self::FeedTypeSearch:
					$factory = new SearchFeedControlFactory();
					break;
				case self::FeedTypeSpecificLinks:
					$factory = new SpecificLinkFeedControlFactory();
					break;
				case self::FeedTypeSimple:
					$factory = new SimpleFeedControlFactory();
					break;
				default:
					throw  new \Exception('Unknown feed type');
			}
			return $factory->createControl($controlTag);
		}

		/**
		 * @param $feedType string
		 * @return array
		 * @throws \Exception
		 */
		public static function getAvailableControlTags($feedType)
		{
			switch ($feedType)
			{
				case self::FeedTypeTrending:
					return TrendingFeedControlFactory::$tags;
					break;
				case self::FeedTypeSearch:
					return SearchFeedControlFactory::$tags;
					break;
				case self::FeedTypeSpecificLinks:
					return SpecificLinkFeedControlFactory::$tags;
					break;
				case self::FeedTypeSimple:
					return SimpleFeedControlFactory::$tags;
					break;
				default:
					throw  new \Exception('Unknown feed type');
			}
		}
	}