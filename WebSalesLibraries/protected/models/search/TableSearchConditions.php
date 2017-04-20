<?
	use application\models\data_query\common\DataColumnSettings;

	/**
	 * Class TableSearchConditions
	 */
	class TableSearchConditions extends BaseSearchConditions
	{
		public function __construct()
		{
			parent::__construct();
			$this->columnSettings = DataColumnSettings::createEmpty();
		}

		/**
		 * @param string $encodedContent
		 * @return TableSearchConditions
		 */
		public static function fromJson($encodedContent)
		{
			$instance = new self();
			\Utils::loadFromJson($instance, $encodedContent);
			return $instance;
		}

		/**
		 * @param $xpath DOMXPath
		 * @param $contextNode DOMNode
		 * @return TableSearchConditions
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();
			$instance->configureFromXml($xpath, $contextNode);
			return $instance;
		}
	}