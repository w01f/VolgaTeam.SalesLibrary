<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\calendar;

	use application\models\calendar\models\CalendarSettings;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	class CalendarBlock extends ContentBlock
	{
		/** @var CalendarSettings */
		public $settings;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'calendar';
			$this->settings = new CalendarSettings();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			if (!$this->parentShortcut->usePermissions || $this->isAccessGranted)
			{
				$this->settings = CalendarSettings::fromXml($xpath, $contextNode);
			}
		}
	}