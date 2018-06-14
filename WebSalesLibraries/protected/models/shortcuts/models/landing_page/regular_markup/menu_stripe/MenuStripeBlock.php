<?

	namespace application\models\shortcuts\models\landing_page\regular_markup\menu_stripe;

	use application\models\shortcuts\models\landing_page\regular_markup\common\BlockContainer;
	use application\models\shortcuts\models\landing_page\regular_markup\common\ContentBlock;
	use application\models\shortcuts\models\landing_page\regular_markup\style\TextAppearance;

	/**
	 * Class MenuStripeBlock
	 */
	class MenuStripeBlock extends ContentBlock implements IParentMenu
	{
		public $expandOnHover;
		public $showArrow;
		public $animationSpeed;
		public $floatRight;

		/** @var  ItemSpacing */
		public $itemSpacing;

		/** @var  \HideCondition */
		public $hideCondition;

		/** @var  MenuStripeItem[] */
		public $items;

		/**
		 * @param $parentShortcut \PageContentShortcut
		 * @param $parentBlock BlockContainer
		 */
		public function __construct($parentShortcut, $parentBlock)
		{
			parent::__construct($parentShortcut, $parentBlock);
			$this->type = 'menu-stripe';

			$this->expandOnHover = true;
			$this->showArrow = true;
			$this->animationSpeed = 0;
			$this->floatRight = false;
			$this->itemSpacing = new ItemSpacing(0);
			$this->hideCondition = new \HideCondition();

			$this->items = array();
		}

		/**
		 * @param $xpath \DOMXPath
		 * @param $contextNode \DOMNode
		 */
		public function configureFromXml($xpath, $contextNode)
		{
			parent::configureFromXml($xpath, $contextNode);

			if (!$this->parentShortcut->usePermissions || $this->isAccessGranted)
			{
				$queryResult = $xpath->query('./ExpandOnHover', $contextNode);
				$this->expandOnHover = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->expandOnHover;

				$queryResult = $xpath->query('./ShowArrow', $contextNode);
				$this->showArrow = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->showArrow;

				$queryResult = $xpath->query('./AnimationSpeed', $contextNode);
				$this->animationSpeed = $queryResult->length > 0 ? intval(trim($queryResult->item(0)->nodeValue)) : $this->animationSpeed;

				$queryResult = $xpath->query('./FloatRight', $contextNode);
				$this->floatRight = $queryResult->length > 0 ? filter_var(trim($queryResult->item(0)->nodeValue), FILTER_VALIDATE_BOOLEAN) : $this->floatRight;

				$queryResult = $xpath->query('./ItemSpacing', $contextNode);
				if ($queryResult->length > 0)
					$this->itemSpacing = ItemSpacing::fromXml($xpath, $queryResult->item(0));

				$queryResult = $xpath->query('./Hide', $contextNode);
				if ($queryResult->length > 0)
					$this->hideCondition = \HideCondition::fromXml($xpath, $queryResult->item(0));

				$items = array();
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
					}
					if (isset($menuItem) && $menuItem->isAccessGranted)
						$items[] = $menuItem;
				}

				if ($this->floatRight)
					for ($i = count($items) - 1; $i >= 0; $i--)
						$this->items[] = $items[$i];
				else
					$this->items = $items;
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