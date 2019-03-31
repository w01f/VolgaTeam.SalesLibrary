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

		public function __construct()
		{
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $parentContainer BaseItemContainer
		 */
		public function loadFromXml($xpath, $contextNode, $parentContainer)
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
					$item = new ShortcutItem();
					break;
				case 'url':
					$item = new UrlItem();
					break;
				case 'tab':
					$item = new TabToggleItem();
					break;
				default:
					$item = null;
			}
			if (isset($item))
			{
				$item->loadFromXml($xpath, $contextNode, $parentContainer);
				return $item;
			}
			return null;
		}

		/**
		 * @param $itemType string
		 * @param $encodedContent string
		 * @return BaseItem
		 */
		public static function fromJson($itemType, $encodedContent)
		{
			$item = null;
			switch ($itemType)
			{
				case 'shortcut':
					$item = new ShortcutItem();
					break;
				case 'url':
					$item = new UrlItem();
					break;
				case 'tab':
					$item = new TabToggleItem();
					break;
				default:
					$item = null;
			}
			if (isset($item))
			{
				\Utils::loadFromJson($item, $encodedContent);
				return $item;
			}
			return null;
		}

		/** @return string */
		public abstract function getUrl();

		/** @return string */
		public abstract function getTarget();
	}