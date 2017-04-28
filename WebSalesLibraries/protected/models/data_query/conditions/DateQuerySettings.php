<?
	namespace application\models\data_query\conditions;

	/**
	 * Class DateQuerySettings
	 */
	class DateQuerySettings
	{
		const DateModeLinksFileDate = 'file date';
		const DateModeLinksAdded = 'link added';
		const DateModeLinksChanged = 'link changed';

		public $dateMode;

		public function __construct()
		{
			$this->dateMode = self::DateModeLinksFileDate;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return DateQuerySettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$instance = new self();

			$queryResult = $xpath->query('./Mode', $contextNode);
			$dateMode = $queryResult->length > 0 ? strtolower(trim($queryResult->item(0)->nodeValue)) : $instance->dateMode;
			if (in_array($dateMode, array(self::DateModeLinksFileDate, self::DateModeLinksAdded, self::DateModeLinksChanged)))
				$instance->dateMode = $dateMode;

			return $instance;
		}

		/**
		 * @param $dateMode string
		 * @return string
		 */
		public static function getDateColumnName($dateMode)
		{
			switch ($dateMode)
			{
				case self::DateModeLinksFileDate:
					return 'file_date';
				case self::DateModeLinksAdded:
					return 'date_add';
				case self::DateModeLinksChanged:
					return 'date_modify';
				default:
					return 'file_date';
			}
		}
	}