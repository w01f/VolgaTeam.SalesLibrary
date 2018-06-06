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
		public $usePermissions;

		/** @var  MarkupSettings */
		public $markupSettings;

		/** @var  \application\models\shortcuts\models\landing_page\mobile_items\MobileSettings */
		public $mobileSettings;

		public function loadPageConfig()
		{
			parent::loadPageConfig();
			$this->usePermissions = true;
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