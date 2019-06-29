<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;

	use application\models\wallbin\models\web\LibraryFolder;
	use application\models\wallbin\models\web\LibraryLink;
	use application\models\wallbin\models\web\LibraryManager;
	use application\models\wallbin\models\web\LibraryPage;

	class VideoLinkItem extends VideoGroupItem
	{
		public $mp4Url;
		public $thumbnailUrl;

		public function getVideoUrl()
		{
			return $this->mp4Url;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			$queryResult = $xpath->query('./Library', $contextNode);
			$libraryName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Page', $contextNode);
			$pageName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Window', $contextNode);
			$windowName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Link', $contextNode);
			$fileName = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			if (empty($libraryName) || empty($pageName) || empty($windowName) || empty($fileName))
				return;

			$libraryName = str_replace("'", "''", $libraryName);
			$pageName = str_replace("'", "''", $pageName);
			$windowName = str_replace("'", "''", $windowName);
			$fileName = str_replace("'", "''", $fileName);

			$defaultPlaceholderUrl = null;

				/** @var \LinkRecord $linkRecord */
			$linkRecord = \LinkRecord::getLinkByName($libraryName, $pageName, $windowName, $fileName);
			if (isset($linkRecord))
			{
				$libraryManager = new LibraryManager();
				$library = $libraryManager->getLibraryById($linkRecord->id_library, false);


				$link = new LibraryLink(new LibraryFolder(new LibraryPage($library)));
				$link->load($linkRecord);

				if (isset($link->universalPreview->mp4))
					$this->mp4Url = $link->universalPreview->mp4->link;
				else
					$this->mp4Url = str_replace('SalesLibraries/SalesLibraries', 'SalesLibraries', $link->fileLink);

				$defaultPlaceholderUrl = $link->universalPreview->mp4Thumb->link;

				$this->isConfigured = true;
			}

			$queryResult = $xpath->query('./Image', $contextNode);
			if ($queryResult->length > 0)
				$this->placeholder = VideoPlaceholder::fromXml($xpath, $queryResult->item(0), $this->parentGroup->parentShortcut->relativeLink, $defaultPlaceholderUrl);
			else
				$this->placeholder = VideoPlaceholder::createDefault($defaultPlaceholderUrl);
		}
	}