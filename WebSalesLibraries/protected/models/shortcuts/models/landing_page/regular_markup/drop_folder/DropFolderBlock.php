<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\drop_folder;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	class DropFolderBlock extends ContentBlock
	{
		public $folderName;
		public $minHeight;
		public $maxFileSize;
		public $maxFileSizeExcessMessage;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'drop-folder';
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			ContentBlock::configureFromXml($xpath, $contextNode);

			if (!$this->parentShortcut->usePermissions || $this->isAccessGranted)
			{
				$queryResult = $xpath->query('./Storage', $contextNode);
				if ($queryResult->length > 0)
					$this->folderName = trim($queryResult->item(0)->nodeValue);

				$queryResult = $xpath->query('./MinHeight', $contextNode);
				$this->minHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 140;

				$queryResult = $xpath->query('./MaxSize', $contextNode);
				$this->maxFileSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 256;

				$queryResult = $xpath->query('./MaxSizeMessage', $contextNode);
				$this->maxFileSizeExcessMessage = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			}
		}
	}