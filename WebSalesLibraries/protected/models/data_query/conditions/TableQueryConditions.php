<?
	namespace application\models\data_query\conditions;

	use application\models\data_query\data_table\DataTableColumnSettings;

	/**
	 * Class TableSearchConditions
	 */
	class TableQueryConditions extends BaseQueryConditions
	{
		public function __construct()
		{
			parent::__construct();
			$this->columnSettings = DataTableColumnSettings::createEmpty();
		}

		/**
		 * @param string $encodedContent
		 * @return TableQueryConditions
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();
			\Utils::loadFromJson($instance, $encodedContent);
			return $instance;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return TableQueryConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			$instance->configureFromXml($xpath, $contextNode);
			return $instance;
		}
	}