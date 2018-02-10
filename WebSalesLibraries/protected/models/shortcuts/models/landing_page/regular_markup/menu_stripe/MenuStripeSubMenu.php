<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class MenuStripeSubMenu
	 */
	class MenuStripeSubMenu extends MenuStripeItem implements IParentMenu
	{
		public $showArrow;
		/** @var  ItemSpacing */
		public $itemSpacing;
		/** @var  MenuStripeItem[] */
		public $items;

		/**
		 * @param $parentMenu IParentMenu
		 */
		public function __construct($parentMenu)
		{
			parent::__construct($parentMenu);
			$this->type = 'menu';
			$this->itemSpacing = $this->parentMenu->getItemSpacing();
			$this->showArrow = $this->parentMenu->getShowArrow();

			$this->items = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			if ($this->isAccessGranted)
			{
				$queryResult = $xpath->query('./ShowArrow', $contextNode);
				$this->showArrow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->showArrow;

				$queryResult = $xpath->query('./ItemSpacing', $contextNode);
				if ($queryResult->length > 0)
					$this->itemSpacing = ItemSpacing::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('./Item', $contextNode);
				foreach ($queryResult as $node)
				{
					$typeAttribute = $node->attributes->getNamedItem("Type");
					$type = isset($typeAttribute) ? strtolower(trim($typeAttribute->nodeValue)) : null;
					$menuItem = null;
					switch ($type)
					{
						case "menu":
							$menuItem = new MenuStripeSubMenu($this);
							$menuItem->configureFromXml($xpath, $node);
							break;
						case "url":
							$menuItem = new MenuStripeUrl($this);
							$menuItem->configureFromXml($xpath, $node);
							break;
						case "shortcut":
							$menuItem = new MenuStripeShortcut($this);
							$menuItem->configureFromXml($xpath, $node);
							break;
						case "divider":
							$menuItem = new MenuStripeDivider($this);
							$menuItem->configureFromXml($xpath, $node);
							break;
					}
					if (isset($menuItem) && $menuItem->isAccessGranted)
						$this->items[] = $menuItem;
				}
			}
		}

		/** @return TextAppearance */
		public function getTextAppearance()
		{
			return $this->textAppearance;
		}

		/** @return ItemSpacing */
		public function getItemSpacing()
		{
			return $this->itemSpacing;
		}

		/** @return boolean */
		public function getShowArrow()
		{
			return $this->showArrow;
		}
	}