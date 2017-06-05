<?
	namespace application\models\feeds\common;

	/**
	 * Class FeedControlSettings
	 */
	class FeedControlSettings
	{
		public $enabled;
		public $title;

		/** @var  \HideCondition */
		public $hideCondition;

		public function __construct()
		{
			$this->hideCondition = new \HideCondition();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			$queryResult = $xpath->query('./Enable', $contextNode);
			$this->enabled = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->enabled;

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->title;

			$queryResult = $xpath->query('./Hide', $contextNode);
			if ($queryResult->length > 0)
				$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));
		}
	}