<?

	namespace application\models\data_query\conditions;

	use application\models\data_query\data_table\DataTableColumnSettings;
	use application\models\data_query\common\QuerySortSettings;

	/**
	 * Class BaseQueryConditions
	 */
	abstract class BaseQueryConditions extends QueryConditionGroupItem
	{
		public $baseDatasetKey;
		public $searchByContent;

		/** @var  DateQuerySettings */
		public $dateSettings;
		/** @var  CategoryQuerySettings */
		public $categorySettings;
		/** @var  ViewCountQuerySettings */
		public $viewCountSettings;
		/** @var  ThumbnailQuerySettings */
		public $thumbnailSettings;

		/** @var  ExcludeQueryConditions */
		public $excludeQueryConditions;

		/** @var  DataTableColumnSettings[] */
		public $columnSettings;
		/** @var  QuerySortSettings */
		public $sortSettings;

		public $limit;
		public $hideLinksWithinBundle;

		public function __construct()
		{
			parent::__construct();

			$this->dateSettings = new DateQuerySettings();
			$this->categorySettings = new CategoryQuerySettings();
			$this->viewCountSettings = new ViewCountQuerySettings();
			$this->thumbnailSettings = new ThumbnailQuerySettings();
			$this->excludeQueryConditions = new ExcludeQueryConditions();
			$this->sortSettings = new QuerySortSettings();
			$this->limit = 0;
			$this->hideLinksWithinBundle = false;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./DateSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->dateSettings = DateQuerySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./CategorySettings', $contextNode);
			if ($queryResult->length > 0)
				$this->categorySettings = CategoryQuerySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./LinkViewsSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->viewCountSettings = ViewCountQuerySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ThumbnailSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->thumbnailSettings = ThumbnailQuerySettings::fromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ExcludeConditions', $contextNode);
			if ($queryResult->length > 0)
				$this->excludeQueryConditions->configurefromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./ColumnSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->columnSettings = DataTableColumnSettings::loadColumnsFromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./SortSettings', $contextNode);
			if ($queryResult->length > 0)
				$this->sortSettings->configureFromXml($xpath, $queryResult->item(0));

			$queryResult = $xpath->query('./HideLinkBundleDuplicates', $contextNode);
			$this->hideLinksWithinBundle = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : false;
		}
	}