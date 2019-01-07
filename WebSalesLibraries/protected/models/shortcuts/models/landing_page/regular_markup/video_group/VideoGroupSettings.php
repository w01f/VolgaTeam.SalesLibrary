<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;


	class VideoGroupSettings
	{
		/** @var \Size */
		public $columnsCount;

		/** @var \Size */
		public $itemHeight;

		public function __construct()
		{
			$this->columnsCount = new \Size(3);
			$this->itemHeight = new \Size(150);
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return VideoGroupSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./ColumnsCount', $contextNode);
			if ($queryResult->length > 0)
				$instance->columnsCount = \Size::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ItemHeight', $contextNode);
			if ($queryResult->length > 0)
				$instance->itemHeight = \Size::fromXml($xpath, $queryResult->item(0));

			return $instance;
		}
	}