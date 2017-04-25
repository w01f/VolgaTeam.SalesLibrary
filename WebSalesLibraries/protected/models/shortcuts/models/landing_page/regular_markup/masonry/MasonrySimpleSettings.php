<?
	namespace application\models\shortcuts\models\landing_page\regular_markup\masonry;

	class MasonrySimpleSettings extends MasonrySettings
	{
		/** @var  MasonryFilter[] */
		public $filters;

		/** @var  MasonryFilter */
		public $defaultFilter;

		public function __construct()
		{
			$this->feedType = self::MasonryTypeSimple;
			parent::__construct();
			$this->filters = array();
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
		}
	}