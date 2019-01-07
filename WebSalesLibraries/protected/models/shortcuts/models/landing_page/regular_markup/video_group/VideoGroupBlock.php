<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\video_group;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;

	class VideoGroupBlock extends ContentBlock
	{
		/** @var  VideoGroupSettings */
		public $settings;

		/** @var  VideoGroupItem[] */
		public $items;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'video-group';
			$this->settings = new VideoGroupSettings();
			$this->items = array();
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
				$this->settings = VideoGroupSettings::fromXml($xpath, $contextNode);

				$queryResult = $xpath->query('./VideoItem', $contextNode);
				$itemIndex = 0;
				foreach ($queryResult as $node)
				{
					$typeAttribute = $node->attributes->getNamedItem("Type");
					$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
					$videoItem = null;
					switch ($type)
					{
						case "link":
							$videoItem = new VideoLinkItem($this);
							break;
						case "file":
							$videoItem = new VideoFileItem($this);
							break;
						default:
							break;
					}
					if (isset($videoItem))
					{
						$videoItem->configureFromXml($xpath, $node);
						//if ($videoItem->isConfigured)
						{
							$videoItem->index = $itemIndex;
							$this->items[] = $videoItem;
							$itemIndex++;
						}
					}
				}
			}
		}
	}