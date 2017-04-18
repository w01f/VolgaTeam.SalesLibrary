<?
	namespace application\models\link_feed;

	/**
	 * Class FeedControlSettings
	 */
	abstract class FeedControlSettings
	{
		const ControlTagScrollButton = 'scroll';

		const ControlTagLinkFormatPowerPoint = 'ppt';
		const ControlTagLinkFormatVideo = 'video';
		const ControlTagLinkFormatDocuments = 'document';

		public $enabled;
		public $title;

		/** @var  \HideCondition */
		public $hideCondition;

		/**
		 * @param $tag string
		 */
		protected function initDefaults($tag){
			switch ($tag)
			{
				case self::ControlTagLinkFormatPowerPoint:
					$this->enabled = true;
					$this->title = 'presentations';
					break;
				case self::ControlTagLinkFormatDocuments:
					$this->enabled = true;
					$this->title = 'documents';
					break;
				case self::ControlTagLinkFormatVideo:
					$this->enabled = true;
					$this->title = 'video';
					break;
			}
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