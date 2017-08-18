<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	class MasonrySimpleSettings extends MasonrySettings
	{
		/** @var  MasonryFilter[] */
		public $filters;

		/** @var  MasonryFilter */
		public $defaultFilter;

		/** @var  MasonryFilterButtonStyle */
		public $buttonStyle;

		public function __construct()
		{
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

			$queryResult = $xpath->query('./Filter/Item', $contextNode);
			foreach ($queryResult as $node)
			{
				$filter = new MasonryFilter();
				$filter->configureFromXml($xpath, $node);
				if ($filter->isAccessGranted)
				{
					$this->filters[] = $filter;
					if ($filter->isDefault)
						$this->defaultFilter = $filter;
				}
			}

			$queryResult = $xpath->query('./FilterButtonStyle', $contextNode);
			if ($queryResult->length > 0)
				$this->buttonStyle = MasonryFilterButtonStyle::fromXml($xpath, $queryResult->item(0));
		}
	}