<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;

	abstract class VideoGroupItem
	{
		/** @var VideoGroupBlock */
		public $parentGroup;
		public $index;
		public $isConfigured;

		/** @var VideoPlaceholder */
		public $placeholder;

		public abstract function getVideoUrl();

		/**
		 * @param $parentGroup VideoGroupBlock
		 */
		public function __construct($parentGroup)
		{
			$this->parentGroup = $parentGroup;
			$this->isConfigured = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
		}
	}