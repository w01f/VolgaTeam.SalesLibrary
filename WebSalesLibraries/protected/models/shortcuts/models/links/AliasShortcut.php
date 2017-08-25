<?

	class AliasShortcut extends BaseShortcut
	{
		/** @var  BaseShortcut */
		public $originalShortcut;

		/**
		 * @param $linkRecord
		 * @param $isPhone boolean
		 */
		public function __construct($linkRecord, $isPhone)
		{
			parent::__construct($linkRecord, $isPhone);
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/ShortcutID');
			$originalShortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($originalShortcutId))
			{
				/**@var $originalLinkRecord ShortcutLinkRecord */
				$originalLinkRecord = ShortcutLinkRecord::model()->findByPk($originalShortcutId);
				$this->originalShortcut = $originalLinkRecord->getModel($isPhone);

				$this->originalShortcut->groupId = $this->groupId;
				$this->originalShortcut->bundleId = $this->bundleId;
				$this->originalShortcut->order = $this->order;
				$this->originalShortcut->carouselGroup = $this->carouselGroup;

				$this->originalShortcut->loadAppearanceData($xpath);
				$this->originalShortcut->isAccessGranted &= $this->isAccessGranted;
			}
		}

		/**
		 * @return string
		 */
		public function getSourceLink()
		{
			return $this->originalShortcut->getSourceLink();
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return $this->originalShortcut->getTypeForActivityTracker();
		}
	}