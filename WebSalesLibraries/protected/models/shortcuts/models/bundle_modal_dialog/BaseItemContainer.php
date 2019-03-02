<?
	namespace application\models\shortcuts\models\bundle_modal_dialog;

	abstract class BaseItemContainer
	{
		public $id;

		/** @var \BundleModalDialogShortcut */
		public $parentShortcut;

		public $itemWidth;
		public $itemHeight;

		public $textColor;
		public $textSize;

		/** @var BaseItem[] */
		public $items;

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 * @param $shortcut \BundleModalDialogShortcut
		 */
		public function __construct($xpath, $contextNode, $shortcut)
		{
			$this->parentShortcut = $shortcut;

			$queryResult = $xpath->query('./TabID', $contextNode);
			$this->id = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : uniqid();

			$queryResult = $xpath->query('./ItemWidth', $contextNode);
			$this->itemWidth = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('./ItemHeight', $contextNode);
			$this->itemHeight = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : 0;

			$queryResult = $xpath->query('./TextSize', $contextNode);
			$this->textSize = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->parentShortcut->textSize;

			$queryResult = $xpath->query('./TextColor', $contextNode);
			$this->textColor = $queryResult->length > 0 ? trim($queryResult->item(0)->nodeValue) : $this->parentShortcut->textColor;

			$this->items = array();
			$queryResult = $xpath->query('./Items/Item', $contextNode);
			foreach ($queryResult as $itemConfigNode)
			{
				$baseItem = BaseItem::fromXml($xpath, $itemConfigNode, $this);
				if (isset($baseItem))
					$this->items[] = $baseItem;
			}
		}
	}