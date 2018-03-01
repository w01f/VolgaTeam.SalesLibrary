<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	class MasonrySimpleSettings extends MasonrySettings
	{
		private $parentShortcutId;
		private $parentBlockId;

		/** @var  MasonryFilter[] */
		public $filters;

		/** @var  MasonryFilter */
		public $defaultFilter;

		/** @var  MasonryFilterButtonStyle */
		public $buttonStyle;

		/*
		* @param string $parentShortcutId
		 * @param string $parentBlockId
		*/
		public function __construct($parentShortcutId, $parentBlockId)
		{
			$this->parentShortcutId = $parentShortcutId;
			$this->parentBlockId = $parentBlockId;
			$this->feedType = self::MasonryTypeSimple;
			parent::__construct();
			$this->filters = array();
			$this->buttonStyle = MasonryFilterButtonStyle::createDefault();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$defaultFilterTags = null;
			$defaultFilterCookieTag = sprintf('DefaultFilterTags-%s-%s', $this->parentShortcutId, $this->parentBlockId);
			if (isset(\Yii::app()->request->cookies[$defaultFilterCookieTag]))
				$defaultFilterTags = \Yii::app()->request->cookies[$defaultFilterCookieTag]->value;

			$queryResult = $xpath->query('./Filter/Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$filter = new MasonryFilter();
				$filter->configureFromXml($xpath, $node);
				if ($filter->isAccessGranted)
				{
					$this->filters[] = $filter;
					if (!empty($defaultFilterTags))
					{
						if (('.' . implode(', .', $filter->tags)) === $defaultFilterTags)
							$this->defaultFilter = $filter;
					}
					else if ($filter->isDefault)
						$this->defaultFilter = $filter;
				}
			}

			$queryResult = $xpath->query('./FilterButtonStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->buttonStyle = MasonryFilterButtonStyle::fromXml($xpath, $queryResult->item(0));
		}
	}