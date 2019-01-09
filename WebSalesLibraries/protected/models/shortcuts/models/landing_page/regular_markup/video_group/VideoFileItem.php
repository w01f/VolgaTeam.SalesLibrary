<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;


	class VideoFileItem extends VideoGroupItem
	{
		public $fileUrl;


		public function getVideoUrl()
		{
			return $this->fileUrl;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./File', $contextNode);
			$videoFileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			if (empty($videoFileName))
				return;

			$baseUrl = \Yii::app()->getBaseUrl(true);
			$this->fileUrl = \Utils::formatUrl($baseUrl . $this->parentGroup->parentShortcut->relativeLink . '/video/' . $videoFileName);

			$queryResult = $xpath->query('./Image', $contextNode);
			if ($queryResult->length > 0)
				$this->placeholder = VideoPlaceholder::fromXml($xpath, $queryResult->item(0), $this->parentGroup->parentShortcut->relativeLink, null);

			$this->isConfigured = true;
		}
	}