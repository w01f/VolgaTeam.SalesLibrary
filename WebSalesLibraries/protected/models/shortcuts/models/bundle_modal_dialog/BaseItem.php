<?

	namespace application\models\shortcuts\models\bundle_modal_dialog;


	abstract class BaseItem
	{
		public $id;
		public $type;

		public $contentView;

		public $title;
		public $tooltip;
		public $imageUrl;

		public $textColor;
		public $textSize;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function __construct($xpath, $contextNode, $parentContainer)
		{
			$queryResult = $xpath->query('./ItemID', $contextNode);
			$this->id = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : uniqid();

			$queryResult = $xpath->query('./Type', $contextNode);
			$this->type = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : uniqid();

			$queryResult = $xpath->query('./Title', $contextNode);
			$this->title = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./HoverTip', $contextNode);
			$this->tooltip = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;

			$queryResult = $xpath->query('./Image', $contextNode);
			$imageFile = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : null;
			$this->imageUrl = \Utils::formatUrl(\Yii::app()->getBaseUrl(true) . $parentContainer->parentShortcut->relativeLink . '/images/' . $imageFile);

			$queryResult = $xpath->query('./TextSize', $contextNode);
			$this->textSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $parentContainer->parentShortcut->textSize;

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $parentContainer->parentShortcut->textColor;
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 * @return BaseItem
		 */
		public static function fromXml($xpath, $contextNode, $parentContainer)
		{
			$queryResult = $xpath->query('Type', $contextNode);
			$itemType = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : '';
			switch ($itemType)
			{
				case 'shortcut':
					$item = new ShortcutItem($xpath, $contextNode, $parentContainer);
					break;
				case 'url':
					$item = new UrlItem($xpath, $contextNode, $parentContainer);
					break;
				case 'tab':
					$item = new TabToggleItem($xpath, $contextNode, $parentContainer);
					break;
				default:
					$item = null;
			}
			return isset($item) ? $item : null;
		}

		/** @return string */
		public abstract function getUrl();

		/** @return string */
		public abstract function getTarget();

		/** @return string */
		public abstract function getItemData();
	}