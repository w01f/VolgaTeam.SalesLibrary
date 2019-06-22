<?

	namespace application\models\marketing_contest\models;


	class ArchiveSettings
	{
		public $archiveAfterDays;
		public $archiveAfterHours;

		public function __construct()
		{
			$this->archiveAfterDays = 0;
			$this->archiveAfterHours = 0;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @return ArchiveSettings
		 */
		public static function fromXml($xpath, $contextNode)
		{
			$archiveSettings = new self();

			$queryResult = $xpath->query('./Days', $contextNode);
			$archiveSettings->archiveAfterDays = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $archiveSettings->archiveAfterDays;

			$queryResult = $xpath->query('./Hours', $contextNode);
			$archiveSettings->archiveAfterHours = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $archiveSettings->archiveAfterHours;

			return $archiveSettings;
		}

		public function isConfigured()
		{
			return $this->archiveAfterDays > 0 || $this->archiveAfterHours > 0;
		}
	}