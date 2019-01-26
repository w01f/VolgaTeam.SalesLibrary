<?

	use application\models\data_query\link_feed\LinkFeedQueryHelper;
	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\common\MarkupSettings;

	/**
	 * Class LandingPageShortcut
	 */
	class LandingPageShortcut extends ContainerShortcut
	{
		/** @var  MarkupSettings */
		public $markupSettings;

		public $enabledMobile;
		/** @var  \BaseShortcut */
		public $alternativeMobileShortcut;
		/** @var  \application\models\shortcuts\models\landing_page\mobile_items\MobileSettings */
		public $mobileSettings;

		public function initRegularModel()
		{
			parent::initRegularModel();

			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			$queryResult = $xpath->query('//Config/MobileSettings/Enabled');
			$this->enabledMobile = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : true;
			if (!$this->enabledMobile)
			{
				$queryResult = $xpath->query('//Config/MobileSettings/AltShortcutID');
				$altShortcutId = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
				if (!empty($altShortcutId))
				{
					/**@var $originalLinkRecord \ShortcutLinkRecord */
					$originalLinkRecord = \ShortcutLinkRecord::model()->findByPk($altShortcutId);
					if (isset($originalLinkRecord))
					{
						$this->alternativeMobileShortcut = $originalLinkRecord->getRegularModel($this->isPhone);

						$this->alternativeMobileShortcut->groupId = $this->groupId;
						$this->alternativeMobileShortcut->bundleId = $this->bundleId;
						$this->alternativeMobileShortcut->order = $this->order;

						$this->alternativeMobileShortcut->initRegularModel();

						$this->alternativeMobileShortcut->loadAppearanceData($this->linkRecord->config);
						$this->alternativeMobileShortcut->isAccessGranted &= $this->isAccessGranted;
					}
				}
			}
		}

		public function loadPageConfig()
		{
			parent::loadPageConfig();
			$this->loadMarkup();
		}

		private function loadMarkup()
		{
			$linkConfig = new DOMDocument();
			$linkConfig->loadXML($this->linkRecord->config);
			$xpath = new DomXPath($linkConfig);

			if ($this->isPhone)
			{
				$queryResult = $xpath->query('//Config/MobileSettings');
				if ($queryResult->length > 0)
					$this->mobileSettings = \application\models\shortcuts\models\landing_page\mobile_items\MobileSettings::fromXml($this, $xpath, $queryResult->item(0));
			}
			else
			{
				$queryResult = $xpath->query('//Config/MarkupSettings');
				if ($queryResult->length > 0)
					$this->markupSettings = MarkupSettings::fromXml($this, $xpath, $queryResult->item(0));
			}
		}

		/**
		 * @return array
		 */
		public function getPageData()
		{
			$data = parent::getPageData();
			$data['serviceData'] = $this->getMenuItemData();
			return $data;
		}

		/**
		 * @return string
		 */
		public function getTypeForActivityTracker()
		{
			return 'Landing page';
		}

		/**
		 * @param $actionsByKey array
		 * @param $xpath DOMXPath
		 * @param $actionConfigNodes DOMNodeList
		 */
		protected function customizeActions($actionsByKey, $xpath, $actionConfigNodes)
		{
			parent::customizeActions($actionsByKey, $xpath, $actionConfigNodes);
			if (array_key_exists('show-search', $actionsByKey))
				$actionsByKey['show-search']->enabled = $actionsByKey['show-search']->enabled && $this->searchBar->configured;
			if (array_key_exists('hide-search', $actionsByKey))
				$actionsByKey['hide-search']->enabled = $actionsByKey['hide-search']->enabled && $this->searchBar->configured;
		}

		/**
		 * @param $ignoreExpirationDate boolean
		 */
		public function prepareDataQueryCache($ignoreExpirationDate)
		{
			$this->usePermissions = false;
			$this->loadMarkup();
			foreach ($this->markupSettings->contentBlocks as $contentBlock)
				self::prepareDataQueryCacheInternal($contentBlock, $ignoreExpirationDate);
		}

		/**
		 * @param $contentBlock ContentBlock
		 * @param $ignoreExpirationDate boolean
		 */
		private static function prepareDataQueryCacheInternal($contentBlock, $ignoreExpirationDate)
		{
			if ($contentBlock instanceof BlockContainer)
			{
				/* @var BlockContainer $dataQueryableBlock */
				$blockContainer = $contentBlock;
				foreach ($blockContainer->items as $childBlock)
					self::prepareDataQueryCacheInternal($childBlock, $ignoreExpirationDate);
			}
			if ($contentBlock instanceof IDataQueryableBlock)
			{
				/* @var IDataQueryableBlock $dataQueryableBlock */
				$dataQueryableBlock = $contentBlock;
				$querySettings = $dataQueryableBlock->getQuerySettings();
				echo sprintf("Snapshot processing - %s", $contentBlock->id);
				echo PHP_EOL;
				LinkFeedQueryHelper::prepareDataQueryCache($querySettings, $ignoreExpirationDate);
			}
		}
	}