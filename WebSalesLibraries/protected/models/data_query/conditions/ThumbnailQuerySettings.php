<?
	namespace application\models\data_query\conditions;
	/**
	 * Class SearchThumbnailSettings
	 */
	class ThumbnailQuerySettings
	{
		const ThumbnailModeTop = 0;
		const ThumbnailModeRandom = 1;

		public $mode;

		public function __construct()
		{
			$this->mode = self::ThumbnailModeTop;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ThumbnailQuerySettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./Mode', $contextNode);
			$mode = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			switch ($mode)
			{
				case 'top':
					$instance->mode = self::ThumbnailModeTop;
					break;
				case 'random':
					$instance->mode = self::ThumbnailModeRandom;
					break;
			}

			return $instance;
		}
	}