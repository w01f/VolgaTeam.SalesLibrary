<?

	class AliasShortcut extends BaseShortcut
	{
		/** @var  BaseShortcut */
		public $originalShortcut;

		public function initRegularModel()
		{
			parent::initRegularModel();

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/ShortcutID');
			$originalShortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			if (isset($originalShortcutId))
			{
				/**@var $originalLinkRecord ShortcutLinkRecord */
				$originalLinkRecord = ShortcutLinkRecord::model()->findByPk($originalShortcutId);
				if (isset($originalLinkRecord))
				{
					$this->originalShortcut = $originalLinkRecord->getRegularModel($this->isPhone);

					$this->originalShortcut->groupId = $this->groupId;
					$this->originalShortcut->bundleId = $this->bundleId;
					$this->originalShortcut->order = $this->order;

					$this->originalShortcut->initRegularModel();

					$this->originalShortcut->loadAppearanceData($this->linkRecord->config);
					$this->originalShortcut->isAccessGranted &= $this->isAccessGranted;
				}
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